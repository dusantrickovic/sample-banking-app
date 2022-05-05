
namespace Homework_test
{
    partial class TransactionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SendBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SenderAcc = new System.Windows.Forms.TextBox();
            this.ReceiverAcc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TransactAmount = new System.Windows.Forms.TextBox();
            this.SenderCurrency = new System.Windows.Forms.TextBox();
            this.ReceiverCurrency = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ValidationStr = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SenderDepositField = new System.Windows.Forms.TextBox();
            this.SenderLimitField = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ReceiverNameField = new System.Windows.Forms.TextBox();
            this.SenderNameField = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SendBtn
            // 
            this.SendBtn.Location = new System.Drawing.Point(265, 321);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(237, 56);
            this.SendBtn.TabIndex = 0;
            this.SendBtn.Text = "SEND MONEY";
            this.SendBtn.UseVisualStyleBackColor = true;
            this.SendBtn.Click += new System.EventHandler(this.SendBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sender\'s Account:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(140, 195);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Receiver\'s Account:";
            // 
            // SenderAcc
            // 
            this.SenderAcc.Location = new System.Drawing.Point(284, 142);
            this.SenderAcc.Name = "SenderAcc";
            this.SenderAcc.Size = new System.Drawing.Size(204, 27);
            this.SenderAcc.TabIndex = 4;
            // 
            // ReceiverAcc
            // 
            this.ReceiverAcc.Location = new System.Drawing.Point(284, 192);
            this.ReceiverAcc.Name = "ReceiverAcc";
            this.ReceiverAcc.Size = new System.Drawing.Size(204, 27);
            this.ReceiverAcc.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(140, 251);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Amount to Send:";
            // 
            // TransactAmount
            // 
            this.TransactAmount.Location = new System.Drawing.Point(284, 251);
            this.TransactAmount.Name = "TransactAmount";
            this.TransactAmount.Size = new System.Drawing.Size(204, 27);
            this.TransactAmount.TabIndex = 7;
            // 
            // SenderCurrency
            // 
            this.SenderCurrency.Location = new System.Drawing.Point(505, 142);
            this.SenderCurrency.Name = "SenderCurrency";
            this.SenderCurrency.ReadOnly = true;
            this.SenderCurrency.Size = new System.Drawing.Size(75, 27);
            this.SenderCurrency.TabIndex = 8;
            // 
            // ReceiverCurrency
            // 
            this.ReceiverCurrency.Location = new System.Drawing.Point(505, 192);
            this.ReceiverCurrency.Name = "ReceiverCurrency";
            this.ReceiverCurrency.ReadOnly = true;
            this.ReceiverCurrency.Size = new System.Drawing.Size(75, 27);
            this.ReceiverCurrency.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(265, 404);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(237, 50);
            this.button1.TabIndex = 10;
            this.button1.Text = "CHECK POSSIBILITY";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ValidationStr
            // 
            this.ValidationStr.AutoSize = true;
            this.ValidationStr.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ValidationStr.Location = new System.Drawing.Point(343, 467);
            this.ValidationStr.Name = "ValidationStr";
            this.ValidationStr.Size = new System.Drawing.Size(13, 20);
            this.ValidationStr.TabIndex = 11;
            this.ValidationStr.Text = ".";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(140, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "Sender\'s Deposit:";
            // 
            // SenderDepositField
            // 
            this.SenderDepositField.Location = new System.Drawing.Point(284, 45);
            this.SenderDepositField.Name = "SenderDepositField";
            this.SenderDepositField.ReadOnly = true;
            this.SenderDepositField.Size = new System.Drawing.Size(204, 27);
            this.SenderDepositField.TabIndex = 13;
            // 
            // SenderLimitField
            // 
            this.SenderLimitField.Location = new System.Drawing.Point(284, 92);
            this.SenderLimitField.Name = "SenderLimitField";
            this.SenderLimitField.ReadOnly = true;
            this.SenderLimitField.Size = new System.Drawing.Size(204, 27);
            this.SenderLimitField.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(140, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "Sender\'s Limit:";
            // 
            // ReceiverNameField
            // 
            this.ReceiverNameField.Location = new System.Drawing.Point(677, 95);
            this.ReceiverNameField.Name = "ReceiverNameField";
            this.ReceiverNameField.ReadOnly = true;
            this.ReceiverNameField.Size = new System.Drawing.Size(204, 27);
            this.ReceiverNameField.TabIndex = 19;
            // 
            // SenderNameField
            // 
            this.SenderNameField.Location = new System.Drawing.Point(677, 45);
            this.SenderNameField.Name = "SenderNameField";
            this.SenderNameField.ReadOnly = true;
            this.SenderNameField.Size = new System.Drawing.Size(204, 27);
            this.SenderNameField.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(533, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "Receiver\'s Name:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(533, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 20);
            this.label7.TabIndex = 16;
            this.label7.Text = "Sender\'s Name:";
            // 
            // TransactionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 496);
            this.Controls.Add(this.ReceiverNameField);
            this.Controls.Add(this.SenderNameField);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.SenderLimitField);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.SenderDepositField);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ValidationStr);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ReceiverCurrency);
            this.Controls.Add(this.SenderCurrency);
            this.Controls.Add(this.TransactAmount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ReceiverAcc);
            this.Controls.Add(this.SenderAcc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SendBtn);
            this.Name = "TransactionsForm";
            this.Text = "TransactionsForm";
            this.Load += new System.EventHandler(this.TransactionsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SendBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SenderAcc;
        private System.Windows.Forms.TextBox ReceiverAcc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TransactAmount;
        private System.Windows.Forms.TextBox SenderCurrency;
        private System.Windows.Forms.TextBox ReceiverCurrency;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label ValidationStr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox SenderDepositField;
        private System.Windows.Forms.TextBox SenderLimitField;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ReceiverNameField;
        private System.Windows.Forms.TextBox SenderNameField;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}