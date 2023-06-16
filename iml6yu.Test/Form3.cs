using iml6yu.MessageBus;
using iml6yu.MessageBus.DataSource;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iml6yu.Test
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            this.Load += Form3_Load;
        }

        private void Form3_Load(object? sender, EventArgs e)
        {
            MessageBuses.Subscrib<MyClass1>();
            MessageBuses.Publish<MyClass1>(new MyClass1());
            //var x = MessageBuses.PushMessage(new MessageChannel(typeof(MyClass1).FullName));
            //var x  = MessageBuses.GetMessage(new MessageChannel(typeof(MyClass1).FullName));
            //ms.AddMessage<MyClass1>(new MessageChannel("11"), new MyClass1());

            //var x = ms.GetMessage(new MessageChannel("11"));

            //MessageBox.Show(x.Data.GetType().ToString());
        }
    }



    public class AAAA
    {
        public static void MyTestMethod()
        {
            ConcurrentQueue<dynamic> list = new ConcurrentQueue<dynamic>();
            AddMessage(list, new AAAA());

            var a = GetMessage(list);
            if (a != null)
            {
                var tp = a.Data.GetType();
                MessageBox.Show((tp == typeof(AAAA)).ToString());
            }
            else
                MessageBox.Show("Fail");
        }

        public static void AddMessage<T>(ConcurrentQueue<dynamic> list, T message)
        {
            MessageBusArges<T> msg = new MessageBusArges<T>(message);
            list.Enqueue(msg);
        }

        public static dynamic GetMessage(ConcurrentQueue<dynamic> list)
        {
            if (list.TryDequeue(out dynamic e))
                return e;
            return null;
        }
    }
}
