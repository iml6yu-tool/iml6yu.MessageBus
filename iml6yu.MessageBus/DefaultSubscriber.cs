using iml6yu.MessageBus.Constand;
using System;
using System.Collections.Generic;
using System.Text;

namespace iml6yu.MessageBus
{
    /// <summary>
    /// 默认订阅类型
    /// </summary>
    /// <typeparam name="TMsg"></typeparam>
    public class DefaultSubscriber<TMsg> : ISubscriber<TMsg>  
    {
        /// <summary>
        ///  当收到通知后触发的事情
        ///  <code>
        /// (channel,msg) 
        /// </code>
        /// </summary>
        public event Action<MessageChannel,  MessageBusArges<TMsg>> OnNoticed;
        /// <summary>
        /// 触发行为
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="busArges"></param>
        public virtual void Notice(MessageChannel channel, MessageBusArges<TMsg> busArges)
        {
            OnNoticed?.Invoke(channel, busArges);
        }
    }
}
