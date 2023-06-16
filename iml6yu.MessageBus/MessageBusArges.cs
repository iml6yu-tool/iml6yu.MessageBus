using iml6yu.MessageBus.Constand;
using System;
using System.Collections.Generic;
using System.Text;

namespace iml6yu.MessageBus
{
    /// <summary>
    /// 消息参数
    /// </summary>
    public class MessageBusArges<T> 
    {
        public MessageBusArges(T data)
        {
            this.Data = data;
        }

        public T Data { get; set; }

        public Type GetDataType()
        {
            return typeof(T);
        }
    }
}
