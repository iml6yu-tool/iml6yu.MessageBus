using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iml6yu.MessageBus.Constand
{
    public interface IMessageSource
    {
        ConcurrentDictionary<MessageChannel, ConcurrentQueue<dynamic>> Messages { get; }

        /// <summary>
        /// 添加一个消息
        /// </summary>
        /// <param name="channel">通道</param>
        /// <param name="message">消息</param>
        void AddMessage<T>(MessageChannel channel, T message);

        /// <summary>
        /// 获取通道内的所有消息
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        ConcurrentQueue<dynamic> GetMessages(MessageChannel channel);

        /// <summary>
        /// 获取通道内的一个消息
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        dynamic GetMessage(MessageChannel channel);

        /// <summary>
        /// 获取通道内的消息长度
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        int GetMessageCount(MessageChannel channel);

        /// <summary>
        /// 获取通道数
        /// </summary>
        /// <returns></returns>
        int GetChannelCount();

        /// <summary>
        /// 清除通道内的所有消息
        /// </summary>
        /// <param name="channel"></param>
        void ClearChannel(MessageChannel channel);

        /// <summary>
        /// 清除通道内所有T类型的消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        void ClearChannel<T>(MessageChannel channel);

        /// <summary>
        /// 移除通道
        /// </summary>
        /// <param name="channel"></param>
        void RemoveChannel(MessageChannel channel);

        /// <summary>
        /// 获取所有通道
        /// </summary>
        /// <returns></returns>
        ICollection<MessageChannel> GetChannels();

        /// <summary>
        /// 是否有待发送的消息
        /// </summary>
        /// <returns></returns>
        bool HasMessage();
       
    }
}
