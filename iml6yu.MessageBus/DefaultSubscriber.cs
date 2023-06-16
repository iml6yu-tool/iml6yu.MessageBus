using iml6yu.MessageBus.Constand;
using System;
using System.Collections.Generic;
using System.Text;

namespace iml6yu.MessageBus
{
    public class DefaultSubscriber<TMsg> : ISubscriber<TMsg>  
    {
        /// <summary>
        /// 当收到通知后触发的事情
        /// </summary>
        public event Action<MessageChannel,  MessageBusArges<TMsg>> OnNoticed;
        public virtual void Notice(MessageChannel channel, MessageBusArges<TMsg> busArges)
        {
            OnNoticed?.Invoke(channel, busArges);
        }
    }
}
