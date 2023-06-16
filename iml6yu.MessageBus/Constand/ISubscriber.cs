using System;
using System.Collections.Generic;
using System.Text;

namespace iml6yu.MessageBus.Constand
{
    public interface ISubscriber<TMsg>
    {
        /// <summary>
        /// 当收到通知后触发的事情
        /// </summary>
        event Action<MessageChannel, MessageBusArges<TMsg>> OnNoticed;
        void Notice(MessageChannel channel, MessageBusArges<TMsg> busArges);
    }
}
