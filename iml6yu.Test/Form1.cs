using iml6yu.MessageBus;
using iml6yu.MessageBus.Constand;
using System.Diagnostics;

namespace iml6yu.Test
{
    public partial class Form1 : Form
    {
        ISubscriber<bool> subscriber;
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            MessageBuses.StartMessageBusDispatch();
            subscriber = MessageBuses.Subscrib<bool>("newWin");
            subscriber.OnNoticed += Sub_OnNoticed;

            MessageBuses.Subscrib<MyClass1>().OnNoticed += Form1_OnNoticed; ;
        }

        private void Form1_OnNoticed(MessageChannel arg1, MessageBusArges<MyClass1> arg2)
        {
            Debug.WriteLine(arg2.Data.ToString());
        }

        private void Sub_OnNoticed(MessageChannel arg1, MessageBusArges<bool> arg2)
        {
            if (arg2.Data)
            {
                Debug.WriteLine("0000000000");
                this.Invoke(new Action(() =>
                {
                    Debug.WriteLine("11111111");
                    new Form2().Show();
                }));
                Debug.WriteLine("222222222222");
            }


        }

        Thread t;
        Task task;
        AutoResetEvent resetEvent = new AutoResetEvent(false);
        private void Form1_Load(object? sender, EventArgs e)
        {
            t = new Thread(new ParameterizedThreadStart(o =>
            {
                Debug.WriteLine("������");
                resetEvent.WaitOne();
                Debug.WriteLine("������");
            }));

            //task = Task.Run(() =>
            //{
            //    Debug.WriteLine("������");
            //    resetEvent.WaitOne();
            //    Debug.WriteLine("������");
            //});
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(t.ThreadState.ToString());
            resetEvent.Set();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(t.ThreadState.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            t.Start();
        }

        private void btnOpenNewWin_Click(object sender, EventArgs e)
        {
            MessageBuses.Publish("newWin", true);
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            MessageBuses.Publish(DateTime.Now);
        }

        private void btnSendObj_Click(object sender, EventArgs e)
        {
            MessageBuses.Publish(new MyClass1());
        }
    }
}