using iml6yu.MessageBus;
using System;
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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.Load += Form2_Load;
        }

        private void Form2_Load(object? sender, EventArgs e)
        {
            MessageBuses.Subscrib<MyClass1>().OnNoticed += Form2_OnNoticed;
            MessageBuses.Subscrib<DateTime>().OnNoticed += Form2_OnNoticed1;
        }

        private void Form2_OnNoticed1(MessageChannel arg1, MessageBusArges<DateTime> arg2)
        {
            this.Invoke(new Action(() => { MessageBox.Show(arg2.Data.ToString()); }));
        }

        private void Form2_OnNoticed(MessageChannel arg1, MessageBusArges<MyClass1> arg2)
        {
            this.Invoke(new Action(() => { MessageBox.Show(arg2.Data.ToString()); }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBuses.Publish<MyClass1>(new MyClass1());
        }
    }
}
