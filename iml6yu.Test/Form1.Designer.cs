namespace iml6yu.Test
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            btnOpenNewWin = new Button();
            btnSendMessage = new Button();
            btnSendObj = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(55, 72);
            button1.Name = "button1";
            button1.Size = new Size(185, 23);
            button1.TabIndex = 0;
            button1.Text = "ResetEvent Set";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(274, 72);
            button2.Name = "button2";
            button2.Size = new Size(175, 23);
            button2.TabIndex = 1;
            button2.Text = "Get Thread State";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(487, 72);
            button3.Name = "button3";
            button3.Size = new Size(181, 23);
            button3.TabIndex = 2;
            button3.Text = "Thread Start";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // btnOpenNewWin
            // 
            btnOpenNewWin.Location = new Point(26, 263);
            btnOpenNewWin.Name = "btnOpenNewWin";
            btnOpenNewWin.Size = new Size(154, 23);
            btnOpenNewWin.TabIndex = 3;
            btnOpenNewWin.Text = "开启一个新窗口";
            btnOpenNewWin.UseVisualStyleBackColor = true;
            btnOpenNewWin.Click += btnOpenNewWin_Click;
            // 
            // btnSendMessage
            // 
            btnSendMessage.Location = new Point(222, 263);
            btnSendMessage.Name = "btnSendMessage";
            btnSendMessage.Size = new Size(154, 23);
            btnSendMessage.TabIndex = 4;
            btnSendMessage.Text = "发送当前时间";
            btnSendMessage.UseVisualStyleBackColor = true;
            btnSendMessage.Click += btnSendMessage_Click;
            // 
            // btnSendObj
            // 
            btnSendObj.Location = new Point(423, 263);
            btnSendObj.Name = "btnSendObj";
            btnSendObj.Size = new Size(154, 23);
            btnSendObj.TabIndex = 5;
            btnSendObj.Text = "发送一个对象";
            btnSendObj.UseVisualStyleBackColor = true;
            btnSendObj.Click += btnSendObj_Click;
            // 
            // button4
            // 
            button4.Location = new Point(55, 125);
            button4.Name = "button4";
            button4.Size = new Size(185, 23);
            button4.TabIndex = 6;
            button4.Text = "ResetEvent ReSet";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button4);
            Controls.Add(btnSendObj);
            Controls.Add(btnSendMessage);
            Controls.Add(btnOpenNewWin);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button btnOpenNewWin;
        private Button btnSendMessage;
        private Button btnSendObj;
        private Button button4;
    }
}