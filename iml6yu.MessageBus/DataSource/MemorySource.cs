using iml6yu.MessageBus.Constand;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iml6yu.MessageBus.DataSource
{
    /// <summary>
    /// 内存数据源
    /// </summary>

#if DEBUG
    public class MemorySource : IMessageSource
#else
    internal class MemorySource : IMessageSource
#endif
    {
        private readonly ConcurrentDictionary<MessageChannel, ConcurrentQueue<dynamic>> messages = new ConcurrentDictionary<MessageChannel, ConcurrentQueue<dynamic>>();
        public ConcurrentDictionary<MessageChannel, ConcurrentQueue<dynamic>> Messages => messages;

        public void AddMessage<T>(MessageChannel channel, T message)
        {

            if (!messages.ContainsKey(channel))
                messages.TryAdd(channel, new ConcurrentQueue<dynamic>());
            MessageBusArges<T> msg = new MessageBusArges<T>(message);
            messages[channel].Enqueue(msg);
        }

        public ConcurrentQueue<dynamic> GetMessages(MessageChannel channel)
        {
            if (!messages.ContainsKey(channel)) return null;
            return messages[channel];
        }

        public dynamic GetMessage(MessageChannel channel)
        {
            if (!messages.ContainsKey(channel)) return null;
            if (messages[channel].TryDequeue(out dynamic e))
                return e;
            return null;
        }

        public void ClearChannel(MessageChannel channel)
        {
            if (messages.ContainsKey(channel))
                messages[channel] = new ConcurrentQueue<dynamic>();
        }
        public void ClearChannel<T>(MessageChannel channel)
        {
            if (!messages.ContainsKey(channel)) return;

            ConcurrentQueue<dynamic> allmessages = GetMessages(channel);
            var tmpMsgs = allmessages.Where(t => !t is T).ToArray();
            var newQueue = new ConcurrentQueue<dynamic>();
            for (var i = tmpMsgs.Length - 1; i >= 0; i--)
            {
                newQueue.Enqueue(tmpMsgs[i]);
            }
            messages[channel] = newQueue;
        }

        public void RemoveChannel(MessageChannel channel)
        {
            if (messages.ContainsKey(channel))
                messages.TryRemove(channel, out ConcurrentQueue<dynamic> _);
        }



        public int GetMessageCount(MessageChannel channel)
        {
            if (!messages.ContainsKey(channel)) return -1;
            return messages[channel].Count;
        }

        public int GetChannelCount()
        {
            return messages.Count;
        }


        public ICollection<MessageChannel> GetChannels()
        {
            return Messages.Keys;
        }

        public bool HasMessage()
        {
            return Messages.All(t => t.Value != null && t.Value.Count > 0);
        }
    }
}
