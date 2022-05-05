
namespace Homework_test
{
    partial class Homepage
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
            this.CreateNewButton = new System.Windows.Forms.Button();
            this.FindUserBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CreateNewButton
            // 
            this.CreateNewButton.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.CreateNewButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CreateNewButton.Location = new System.Drawing.Point(115, 46);
            this.CreateNewButton.Name = "CreateNewButton";
            this.CreateNewButton.Size = new System.Drawing.Size(178, 57);
            this.CreateNewButton.TabIndex = 1;
            this.CreateNewButton.Text = "CREATE NEW ACCOUNT";
            this.CreateNewButton.UseVisualStyleBackColor = false;
            this.CreateNewButton.Click += new System.EventHandler(this.CreateNewButton_Click);
            // 
            // FindUserBtn
            // 
            this.FindUserBtn.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.FindUserBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FindUserBtn.Location = new System.Drawing.Point(115, 139);
            this.FindUserBtn.Name = "FindUserBtn";
            this.FindUserBtn.Size = new System.Drawing.Size(178, 57);
            this.FindUserBtn.TabIndex = 2;
            this.FindUserBtn.Text = "FIND ACCOUNT";
            this.FindUserBtn.UseVisualStyleBackColor = false;
            this.FindUserBtn.Click += new System.EventHandler(this.FindUserBtn_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Location = new System.Drawing.Point(115, 236);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(178, 57);
            this.button1.TabIndex = 3;
            this.button1.Text = "MAKE A TRANSACTION";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.TransactionButton_Click);
            // 
            // Homepage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 354);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.FindUserBtn);
            this.Controls.Add(this.CreateNewButton);
            this.Name = "Homepage";
            this.Text = "Metropol Bank - Home";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button CreateNewButton;
        private System.Windows.Forms.Button FindUserBtn;
        private System.Windows.Forms.Button button1;
    }
}

