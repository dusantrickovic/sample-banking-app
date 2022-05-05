using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework_test
{
    public partial class Homepage : Form
    {
        // The Main Menu Form.
        public Homepage()
        {
            InitializeComponent();
        }

        private void CreateNewButton_Click(object sender, EventArgs e)
        {
            // Shows the new form for creating accounts.

            MessageBox.Show("1/2 This is the unfinished part unfortunately, since I was a bit sick this week. However, I have added a few records into the database myself, so it should still work normally with those.");
            MessageBox.Show("2/2 Every other mandatory requirement should work and the beginning ideas and codes for this part of the program are still in the 'NewAccountForm.cs'");
            
            // THE MAIN IDEA FOR THE SOLUTION FOR THIS IS LOCATED IN THE NewAccountForm FORM.
            
            //NewAccountForm f2 = new NewAccountForm();
            //f2.ShowDialog();
        }

        private void FindUserBtn_Click(object sender, EventArgs e)
        {
            FindAccount findForm = new FindAccount();
            findForm.Show();
        }

        // Opens the Transaction form.
        private void TransactionButton_Click(object sender, EventArgs e)
        {
            TransactionsForm MakeTransaction = new TransactionsForm();
            MakeTransaction.Show();
        }

    }
}
