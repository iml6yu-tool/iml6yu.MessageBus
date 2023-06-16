using iml6yu.MessageBus.Constand;
using System;

namespace iml6yu.MessageBus
{
    /// <summary>
    /// 消息通道
    /// </summary>
    public sealed class MessageChannel
    {
        private string channelName;
        public string Name => channelName;
        public MessageChannel(string channelName)
        {
            this.channelName = channelName;
        } 

        public override string ToString()
        {
            return channelName;
        }

        public override int GetHashCode()
        {
            return channelName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is MessageChannel c)
            {
                if (string.IsNullOrEmpty(c.channelName)) return false;
                return this.channelName == c.channelName;
            }
            return false;
        }
    }
}
