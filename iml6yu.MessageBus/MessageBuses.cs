using iml6yu.MessageBus.Constand;
using iml6yu.MessageBus.DataSource;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace iml6yu.MessageBus
{
    public sealed class MessageBuses
    {
        /// <summary>
        /// 订阅者
        /// Key:[string] ChannelName
        /// Value:[ISubscriber] 订阅者
        /// </summary>
        private static readonly ConcurrentDictionary<(MessageChannel, Type), ConcurrentBag<dynamic>> subscribers = new ConcurrentDictionary<(MessageChannel, Type), ConcurrentBag<dynamic>>();

        /// <summary>
        /// 数据源
        /// </summary>
        private static IMessageSource dataSource = new MemorySource();

        private static Thread threadBusDistapch = new Thread(MessageDistapch);
        private static readonly CancellationTokenSource tokenSource = new CancellationTokenSource();
        private static AutoResetEvent resetEvent = new AutoResetEvent(false);

        /// <summary>
        /// 设置数据源
        /// </summary>
        /// <param name="ds"></param>
        public static void SetDataSource(IMessageSource ds)
        {
            dataSource = ds;
        }

        /// <summary>
        /// 创建一个通道
        /// </summary>
        /// <param name="channelName"></param>
        /// <returns></returns> 
        public static MessageChannel CreateChannel(string channelName)
        {
            return new MessageChannel(channelName);
        }

        /// <summary>
        /// 订阅一个频道
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="callback">回调函数</param>
        /// <returns></returns>
        public static ISubscriber<T> Subscrib<T>(MessageChannel channel, Action<MessageChannel, MessageBusArges<T>> callback = null)
        {
            var sub = new DefaultSubscriber<T>();
            var key = (channel, messageType: typeof(T));
            if (!subscribers.ContainsKey(key))
                if (!subscribers.TryAdd(key, new ConcurrentBag<dynamic>()))
                    throw new Exception($"监听通道({key.channel}:{key.messageType})失败！");
            subscribers[key].Add(sub);
            if (callback != null)
                sub.OnNoticed += callback;
            return sub;
        }

        /// <summary>
        /// 订阅一个通道
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callback">回调函数</param>
        /// <returns></returns>
        public static ISubscriber<T> Subscrib<T>(Action<MessageChannel, MessageBusArges<T>> callback = null)
        {
            return Subscrib<T>(CreateChannel(typeof(T).FullName), callback);
        }
        /// <summary>
        /// 订阅一个频道
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="callback">回调函数</param>
        /// <returns></returns>
        public static ISubscriber<T> Subscrib<T>(string channel, Action<MessageChannel, MessageBusArges<T>> callback = null)
        {
            return Subscrib<T>(CreateChannel(channel), callback);
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="channel"></param>
        public static void UnSubscrib<T>(MessageChannel channel)
        {
            var subscriberKey = GetOrDefaultSubscriberKey<T>(channel);
            if (subscriberKey.channel == null) return;

            if (!subscribers.TryRemove(subscriberKey, out ConcurrentBag<dynamic> _))
                throw new Exception($"移除监听通道({subscriberKey.channel}:{subscriberKey.messageType})失败！");

            //如果T类型的订阅者已经是0个，则清理掉数据中未发送的T类型数据
            if (subscribers[subscriberKey].Count > 0) return;
            dataSource.ClearChannel<T>(channel);

            //如果通道channel内的消息是0条，则移除channel
            if (dataSource.GetMessageCount(channel) > 0) return;
            dataSource.RemoveChannel(channel);
        }
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="channel"></param>
        public static void UnSubscrib<T>(string channel)
        {
            UnSubscrib<T>(CreateChannel(channel));
        }
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <typeparam name="T"></typeparam> 
        public static void UnSubscrib<T>()
        {
            UnSubscrib<T>(CreateChannel(typeof(T).FullName));
        }


        /// <summary>
        /// 发布消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        public static void Publish<T>(string channel, T message)
        {
            Publish<T>(CreateChannel(channel), message);
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        public static void Publish<T>(MessageChannel channel, T message)
        {
            var subscriberKey = GetOrDefaultSubscriberKey<T>(channel);
            if (subscriberKey.channel == null) return;

            dataSource.AddMessage(subscriberKey.channel, message);
            Task.Run(() =>
            {
                PushMessage(subscriberKey.channel);
            });
            StartMessageBusDispatch();
            resetEvent.Set();
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        public static void Publish<T>(T message)
        {
            Publish<T>(CreateChannel(typeof(T).FullName), message);
        }

        /// <summary>
        /// 开启当前时间调度（默认发送消息后依然会开启当前调度）
        /// </summary>
        public static void StartMessageBusDispatch()
        {
            if (threadBusDistapch.ThreadState == ThreadState.Unstarted)
                threadBusDistapch.Start();
        }



        /// <summary>
        /// 终止当前BUS
        /// </summary>
        public static void Abort()
        {
            tokenSource?.Cancel();
        }

        /// <summary>
        /// 获取订阅者字典的Key
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="messageType"></param>
        /// <returns></returns>
        internal static (MessageChannel channel, Type messageType) GetOrDefaultSubscriberKey(MessageChannel channel, Type messageType)
        {
            var key = (channel, messageType);
            if (!subscribers.ContainsKey(key)) return (null, null);
            return key;
        }

        /// <summary>
        /// 获取订阅者字典的Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <returns></returns>
        internal static (MessageChannel channel, Type messageType) GetOrDefaultSubscriberKey<T>(MessageChannel channel)
        {
            return GetOrDefaultSubscriberKey(channel, typeof(T));
        }

        /// <summary>
        /// 获取订阅者字典的Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <returns></returns>
        internal static (MessageChannel channel, Type messageType) GetOrDefaultSubscriberKey<T>(string channel)
        {
            return GetOrDefaultSubscriberKey(CreateChannel(channel), typeof(T));
        }
        /// <summary>
        /// 推送通道内的一条消息
        /// </summary>
        /// <param name="channel"></param>
        internal static void PushMessage(MessageChannel channel)
        {
            var msg = dataSource.GetMessage(channel);
            if (msg == null) return;

            /*
            * 为啥要这样写Try catch呢，
            * 因为当泛型中的参数类型定义访问级别是internal时， msg.Data会出现错误
            */
            Type msgType;
            bool dataTypeIsInternal = false;
            try
            {
                msgType = msg.Data.GetType();
                dataTypeIsInternal = true;
            }
            catch
            {
                msgType = msg.GetType().GetProperty("Data").PropertyType;
            }
            var subcriberKey = GetOrDefaultSubscriberKey(channel, msgType);
            if (subcriberKey.channel == null) return;

            var subs = subscribers[subcriberKey];
            if (subs == null || subs.Count == 0) return;

            Parallel.ForEach(subs, (sub, state) =>
            {
                /*
                * 为啥要这样写Try catch呢，
                * 因为当泛型中的参数类型定义访问级别是internal时， sub.Notice会出现错误
                */
                if (dataTypeIsInternal)
                    sub.Notice(subcriberKey.channel, msg);
                else
                    sub.GetType().GetMethod("Notice").Invoke(sub, new object[] { subcriberKey.channel, msg });
            });
        }

        /// <summary>
        /// 调度消息
        /// </summary>
        /// <param name="obj"></param> 
        private static void MessageDistapch(object obj)
        {
            while (!tokenSource.IsCancellationRequested)
            {
                var channels = dataSource.GetChannels();
                if (channels == null || channels.Count == 0 || !dataSource.HasMessage())
                    resetEvent.WaitOne();
                else
                {
                    Parallel.ForEach(channels, (channel, state) =>
                    {
                        if (!tokenSource.IsCancellationRequested)
                            PushMessage(channel);
                    });
                    Thread.Sleep(1);
                }

            }
        }
    }
}
