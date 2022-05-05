using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace Homework_test
{
    public partial class TransactionsForm : Form
    {
        string cs = "Data Source=MetropolBankDatabase.db;Version=3";
        Regex regexNumbers = new Regex("^[0-9-]*$");
        public TransactionsForm()
        {
            InitializeComponent();
        }

        private void TransactionsForm_Load(object sender, EventArgs e)
        {
            
        }

        // Created a separate function because it's called twice later in the program to perform transactions (reason: improving readability)
        public void MakeATransaction ()
        {
            Transaction createdTransaction = new Transaction();


            using (var connection = new SQLiteConnection(cs))
            {

                // Storing inputs into a Transaction object.

                createdTransaction.TransactionSender = SenderNameField.Text;
                createdTransaction.TransactionReceiver = ReceiverNameField.Text;
                createdTransaction.TransactionCurrency = SenderCurrency.Text;
                createdTransaction.TransactionSenderAcc = SenderAcc.Text;
                createdTransaction.TransactionReceiverAcc = ReceiverAcc.Text;
                createdTransaction.TransactionAmount = Convert.ToDouble(TransactAmount.Text);
                createdTransaction.TransactionDate = DateTime.Now.ToString();
                createdTransaction.TransactionType = "Money Transfer";

                double withAFee = (createdTransaction.TransactionAmount - (createdTransaction.TransactionAmount / 100.00));


                connection.Open();

                // Regular query that deals with transactions below 10,000 in value.

                string transactionQuery = "BEGIN TRANSACTION; " +
                                          "UPDATE Accounts " +
                                          "SET DepositAmount=DepositAmount-" + createdTransaction.TransactionAmount + " " +
                                          "WHERE AccountNumber='" + createdTransaction.TransactionSenderAcc + "'; " +
                                          "" +
                                          "UPDATE Accounts " +
                                          "SET DepositAmount=DepositAmount+" + createdTransaction.TransactionAmount + " " +
                                          "WHERE AccountNumber='" + createdTransaction.TransactionReceiverAcc + "'; " +
                                          "" +
                                          "" +
                                          "INSERT INTO Transactions(SenderName,ReceiverName,SenderAccount,ReceiverAccount,Amount,CurrencyCode,TransactionDate,TransactionType) " +
                                          "VALUES ('@sname','@rname','@saccount','@raccount', @amount,'@currencycode','@transactdate','@transacttype'); " +
                                          "" +
                                          "" +
                                          "COMMIT; ";


                // A special query that deals with transactions over 10,000 in value.

                string transactionQueryWithFee = "BEGIN TRANSACTION; " +
                                                 "UPDATE Accounts " +
                                                 "SET DepositAmount=DepositAmount-" + withAFee + " " +
                                                 "WHERE AccountNumber='" + createdTransaction.TransactionSenderAcc + "'; " +
                                                 "" +
                                                 "UPDATE Accounts " +
                                                 "SET DepositAmount=DepositAmount+" + withAFee + " " +
                                                 "WHERE AccountNumber='" + createdTransaction.TransactionReceiverAcc + "'; " +
                                                 "" +
                                                 "" +
                                                 "INSERT INTO Transactions(SenderName,ReceiverName,SenderAccount,ReceiverAccount,Amount,CurrencyCode,TransactionDate,TransactionType) " +
                                                 "VALUES ('@sname','@rname','@saccount','@raccount', @amount, '@currencycode', '@transactdate', '@transacttype'); " +
                                                 "" +
                                                 "" +
                                                 "" +
                                                 "COMMIT; ";

                SQLiteCommand command;
                if (createdTransaction.TransactionAmount > 10000.0) command = new SQLiteCommand(transactionQueryWithFee, connection);
                else command = new SQLiteCommand(transactionQuery, connection);

                command.Parameters.AddWithValue("@sname", createdTransaction.TransactionSender);
                command.Parameters.AddWithValue("@rname", createdTransaction.TransactionReceiver);
                command.Parameters.AddWithValue("@saccount", createdTransaction.TransactionSenderAcc);
                command.Parameters.AddWithValue("@raccount", createdTransaction.TransactionReceiverAcc);

                if (createdTransaction.TransactionAmount > 10000.0) command.Parameters.AddWithValue("@amount", withAFee);
                else command.Parameters.AddWithValue("@amount", createdTransaction.TransactionAmount);

                command.Parameters.AddWithValue("@currencycode", createdTransaction.TransactionCurrency);
                command.Parameters.AddWithValue("@transactdate", createdTransaction.TransactionDate);
                command.Parameters.AddWithValue("@transacttype", createdTransaction.TransactionType);

                int a = (int)command.ExecuteNonQuery();

                if (a > 0)
                {
                    ValidationStr.Visible = true;
                    ValidationStr.Text = "Successful transaction!";
                }
                else
                {
                    ValidationStr.Visible = true;
                    ValidationStr.Text = "Transaction failed.";
                }

                connection.Close();
            }
        }

        // Checks currency and retreives other data.
        private void button1_Click(object sender, EventArgs e)
        {
            if (SenderAcc.Text != "" && ReceiverAcc.Text != "" && TransactAmount.Text != "" && TransactAmount.Text != "" && regexNumbers.IsMatch(SenderAcc.Text) && regexNumbers.IsMatch(ReceiverAcc.Text))
            {
                ValidationStr.Visible = false;
                using (var connection = new SQLiteConnection(cs))
                {
                    connection.Open();
                   
                    string getAccountDetails = "SELECT DepositAmount, AccountLimit " +
                                               "FROM Accounts " +
                                               "WHERE AccountNumber='" + SenderAcc.Text + "'; ";
                    var commandAccDetails = new SQLiteCommand(getAccountDetails, connection);
                    var dataReader = commandAccDetails.ExecuteReader();

                    if (dataReader.Read())
                    {
                        SenderDepositField.Text = Convert.ToString(dataReader["DepositAmount"]);
                        SenderLimitField.Text = Convert.ToString(dataReader["AccountLimit"]);
                    }

                    connection.Close();
                }

                using(var connection = new SQLiteConnection(cs))
                {
                    connection.Open();
                    string getQuery = "SELECT CurrencyCode " +
                                       "FROM Accounts " +
                                       "WHERE AccountNumber='" + SenderAcc.Text + "' OR AccountNumber='" + ReceiverAcc.Text + "'; ";
                    var command = new SQLiteCommand(getQuery, connection);
                    var dataReader = command.ExecuteReader();

                    List<string> values = new List<string>();

                    while (dataReader.Read())
                    {
                        values.Add(dataReader.GetString(0));
                    }
                    connection.Close();

                    SenderCurrency.Text = values[1];
                    ReceiverCurrency.Text = values[0];

                }


                using (var connection = new SQLiteConnection(cs))
                {
                    connection.Open();
                    string getSender = "SELECT Name " +
                                         "FROM Users u " +
                                         "INNER JOIN Accounts a " +
                                         "ON u.UserID=a.UserID " +
                                         "WHERE a.AccountNumber='" + SenderAcc.Text + "'; ";
                    var commandSender = new SQLiteCommand(getSender, connection);
                    var dataReader = commandSender.ExecuteReader();

                    if (dataReader.Read())
                    {
                        SenderNameField.Text = dataReader["Name"].ToString();
                    }

                    string getReceiver = "SELECT Name " +
                                        "FROM Users u " +
                                        "INNER JOIN Accounts a " +
                                        "ON u.UserID=a.UserID " +
                                        "WHERE a.AccountNumber='" + ReceiverAcc.Text + "'; ";
                    var commandReceiver = new SQLiteCommand(getReceiver, connection);
                    var dataReaderReceiver = commandReceiver.ExecuteReader();

                    if (dataReaderReceiver.Read())
                    {
                        ReceiverNameField.Text = dataReaderReceiver["Name"].ToString();
                    }

                    connection.Close();

                }

            }
                 else
            {
                MessageBox.Show("All fields are required and use numeric values and dashes (-) only.");
            }
                
         }


        private void SendBtn_Click(object sender, EventArgs e)
        {
            // Checks if any of the field is empty, if the currency is the same and if there's enough money on the account.
            if (SenderAcc.Text != "" && ReceiverAcc.Text != "" && TransactAmount.Text != "" && regexNumbers.IsMatch(SenderAcc.Text) && regexNumbers.IsMatch(ReceiverAcc.Text))
            {
                if (SenderCurrency.Text == ReceiverCurrency.Text)
                {
                    if (Convert.ToDouble(SenderDepositField.Text) - Convert.ToDouble(TransactAmount.Text) < Convert.ToDouble(SenderDepositField.Text) + Convert.ToDouble(SenderLimitField.Text))
                    {
                        MakeATransaction();
                    }
                    
                    
                    // ERROR FOUND: ACCOUNT LIMIT FEATURE DOESN'T SEEM TO WORK PROPERLY, WILL FIX IN THE FUTURE ONCE I FIGURE OUT WHERE EXACTLY THE PROBLEM IS
                    // BELOW IS JUST A TEMPORARY PIECE OF CODE TO SHOW MY WAY OF THINKING
                    
                    /*
                    else if (Convert.ToDouble(SenderDepositField.Text) - Convert.ToDouble(TransactAmount.Text) < 0 && Convert.ToDouble(SenderDepositField.Text) - Convert.ToDouble(TransactAmount.Text) < Convert.ToDouble(SenderDepositField.Text) + Convert.ToDouble(SenderLimitField.Text))
                    {
                        MakeATransaction();
                    }
                    */

                    else
                    {
                        MessageBox.Show("Not Enough Money on the Account.");
                    }
                }

                else
                {
                    MessageBox.Show("Chosen accounts don't use the same currency.");
                }
            }
            else
            {
                MessageBox.Show("All fields required. Make sure to check the validity of your input and load remaining data first.");
            }
        }      
    }

    
}
