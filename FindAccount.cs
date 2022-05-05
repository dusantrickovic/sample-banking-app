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
    public partial class FindAccount : Form
    {

        // Database path
        string cs = "Data Source=MetropolBankDatabase.db;Version=3";

        /*SQLiteConnection connection;
        SQLiteCommand command;
        SQLiteDataReader datareader; */

        public FindAccount()
        {
            InitializeComponent();
        }

        private void CancelBttn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            // Clears the grid (UserList) before displaying the results of a query.
            UsersList.Rows.Clear();


            String FindAccount = FindAccNumBox.Text;


            // Ensures that the full list of users will be displayed if the search field is empty
            if (FindAccount == "" && UsersList.Rows.Count >= 0) Display_Data("SELECT u.UserID, Name, LastName, StreetName, CustomerType, a.AccountType, AccountNumber FROM Users u INNER JOIN Accounts a ON u.UserID = a.UserID;");

            // If it's not empty, search the number that's LIKE any of the digits. The more numbers the user inputs, the more accurate results they can get.
            if (FindAccount != "")
            {
                var s = "SELECT u.UserID, Name, LastName, StreetName, CustomerType, a.AccountType, AccountNumber FROM Users u INNER JOIN Accounts a ON u.UserID = a.UserID WHERE a.AccountNumber LIKE'%" + FindAccount + "%';";
                Display_Data(s);

                User ResultUser = new User();


                ResultUser.Name = UsersList.Rows[0].Cells[1].Value.ToString();
            }

        }


        private void FindAccount_Load(object sender, EventArgs e)
        {
            // Loads the Search Form and hides the next stages until they are called.
            UserInfoCard.Hide();
            UpdateInfoCard.Hide();
            WithdrawalPanel.Hide();
            MakeADepositPanel.Hide();

            // Fills up the GridView Box by calling the function defined below.
            string OnLoadResults = "SELECT u.UserID, Name, LastName, StreetName, CustomerType, a.AccountType, AccountNumber FROM Users u INNER JOIN Accounts a ON u.UserID = a.UserID;";
            Display_Data(OnLoadResults);

        }

        private void UsersList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // On click of the appropriate field, a panel populated with basic info about the user and their account shows up. 
            UserInfoCard.Visible = true;

            // Meanwhile, the Update card is still hidden until called.
            UpdateInfoCard.Hide();

            // Only works once you search for the account. Will try and fix that later.
            // UPDATE (circa 5 minutes later): You have to click on the text value itself, not on the empty space in the field.

            int current = UsersList.CurrentRow.Index;
            NameField.Text = UsersList.Rows[current].Cells[1].Value.ToString();
            LastNameField.Text = UsersList.Rows[current].Cells[2].Value.ToString();
            StreetNameField.Text = UsersList.Rows[current].Cells[3].Value.ToString();
            CustomerTypeField.Text = UsersList.Rows[current].Cells[4].Value.ToString();
            AccountTypeField.Text = UsersList.Rows[current].Cells[5].Value.ToString();
            AccountNumberField.Text = UsersList.Rows[current].Cells[6].Value.ToString();


            // Create new instances of the User and ResultAccount classes.
            User ResultUser = new User();
            Account ResultAccount = new Account();

            // Populating the previously created instance with data.
            ResultUser.Name = NameField.Text;
            ResultUser.LastName = LastNameField.Text;
            ResultUser.StreetName = StreetNameField.Text;
            ResultUser.CustomerType = CustomerTypeField.Text;
            ResultAccount.AccountNumber = AccountNumberField.Text;

            // Opening up a new SQLite connection in order to retreive remaining data.
            var connection = new SQLiteConnection(cs);
            connection.Open();

            string LoadData = "SELECT DateOfBirth, HouseNumber, PostalCode, CurrencyCode, DepositAmount, AccountLimit, a.UniqueUserNumber FROM Users u INNER JOIN Accounts a ON u.UserID=a.UserID WHERE AccountNumber LIKE '%" + ResultAccount.AccountNumber + "%';";
            var command = new SQLiteCommand(LoadData, connection);


            var datareader = command.ExecuteReader();

            // Unpacking the data from the reader and assigning the remaining values.
            if (datareader.Read())
            {
                ResultUser.DateOfBirth = Convert.ToString(datareader["DateOfBirth"]);
                ResultUser.HouseNumber = Convert.ToString(datareader["HouseNumber"]);
                ResultUser.PostalCode = Convert.ToString(datareader["PostalCode"]);
                ResultAccount.CurrencyCode = Convert.ToString(datareader["CurrencyCode"]);
                ResultAccount.DepositAmount = Convert.ToDouble(datareader["DepositAmount"]);
                ResultAccount.CardLimit = Convert.ToDouble(datareader["AccountLimit"]);
                ResultAccount.UniqueUserNumber = Convert.ToString(datareader["UniqueUserNumber"]);
            }

            var command2 = new SQLiteCommand("SELECT AccountNumber, CurrencyCode FROM Accounts a WHERE a.UniqueUserNumber='" + ResultAccount.UniqueUserNumber + "' ORDER BY AccountNumber;", connection);

            var ReadAccounts = command2.ExecuteReader();

            while (ReadAccounts.Read())
            {
                AccountListField.Text += Convert.ToString(ReadAccounts["AccountNumber"] + " - " + ReadAccounts["CurrencyCode"] + "\n");
            }

            // Closing the connection.
            connection.Close();

            // Filling out the rest of the ReadOnly TextBoxes of the form.
            BirthDateBox.Text = ResultUser.DateOfBirth;
            HouseNumField.Text = ResultUser.HouseNumber;
            PostCodeField.Text = ResultUser.PostalCode;
            DepositField.Text = ResultAccount.DepositAmount.ToString();
            CardLimitField.Text = ResultAccount.CardLimit.ToString();
            UniqueCustomerIdField.Text = ResultAccount.UniqueUserNumber.ToString();
            CurrencyInfoCard.Text = ResultAccount.CurrencyCode.ToString();


            // Populating the textboxes in the Update panel.
            UpdateStreetNameField.Text = ResultUser.StreetName;
            UpdateHouseNumField.Text = ResultUser.HouseNumber;
            UpdatePostalField.Text = ResultUser.PostalCode;
            UpdateTypeField.Text = ResultUser.CustomerType;



        }

        // The function that runs the general Read request to the database and displays values.
        public void Display_Data(string x)
        {
            var connection = new SQLiteConnection(cs);
            connection.Open();

            string LoadData = x;
            var command = new SQLiteCommand(LoadData, connection);

            var datareader = command.ExecuteReader();

            while (datareader.Read())
            {
                UsersList.Rows.Add(datareader.GetInt32(0), datareader.GetString(1), datareader.GetString(2), datareader.GetString(3), datareader.GetString(4), datareader.GetString(5), datareader.GetString(6));

            }

            connection.Close();
        }

        // The cancel button that closes the form.
        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // The Cancel button on the Update Form panel.
        private void button5_Click(object sender, EventArgs e)
        {
            UpdateInfoCard.Hide();
        }

        // The Update button on the UserInfoCard panel.
        private void button1_Click(object sender, EventArgs e)
        {
            UpdateInfoCard.Show();
            MakeADepositPanel.Hide();
            WithdrawalPanel.Hide();

        }

        // Ensures the same behavior on selection change.
        private void UsersList_SelectionChanged(object sender, EventArgs e)
        {
            int current = UsersList.CurrentRow.Index;
            NameField.Text = UsersList.Rows[current].Cells[1].Value.ToString();
            LastNameField.Text = UsersList.Rows[current].Cells[2].Value.ToString();
            StreetNameField.Text = UsersList.Rows[current].Cells[3].Value.ToString();
            CustomerTypeField.Text = UsersList.Rows[current].Cells[4].Value.ToString();
            AccountTypeField.Text = UsersList.Rows[current].Cells[5].Value.ToString();
            AccountNumberField.Text = UsersList.Rows[current].Cells[6].Value.ToString();
        }

        private void UserInfoCard_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            UserInfoCard.Visible = false;
        }

        // Collecting the updated information and sending an Update query to the database.
        private void UpdateInfoButton_Click(object sender, EventArgs e)
        {

            // Creating new objects to store the updated info
            User EditedInfoUser = new User();
            Account EditedAccount = new Account();

            // Creating a few booleans to keep track of each state later on.
            bool StreetNameChanged = false;
            bool HouseNumChanged = false;
            bool PostalCodeChanged = false;
            bool CustomerTypeChanged = false;

            List<bool> booleans = new List<bool> {StreetNameChanged,
                                                  HouseNumChanged,
                                                  PostalCodeChanged,
                                                  CustomerTypeChanged};


            //Focusing on the current, chosen row of the grid and extracting necessary data.
            int current = UsersList.CurrentRow.Index;
            //EditedAccount.AccountNumber = UsersList.Rows[current].Cells[6].Value.ToString();
            EditedInfoUser.CustomerType = UsersList.Rows[current].Cells[4].Value.ToString();
            EditedAccount.UniqueUserNumber = UniqueCustomerIdField.Text;

            // Checking if there are any changes with the street name. If so, change state. If not, return it to false.
            if (UpdateStreetNameField.Text != "")
            {
                if (UpdateStreetNameField.Text != StreetNameField.Text)
                {
                    EditedInfoUser.StreetName = UpdateStreetNameField.Text;
                    StreetNameChanged = true;
                }
                else
                {
                    EditedInfoUser.StreetName = StreetNameField.Text;
                    StreetNameChanged = false;
                }
            }

            // Checking if there are any changes with the postal code field. If so, change state. If not, return it to false.
            if (UpdatePostalField.Text != "")
            {
                if (UpdatePostalField.Text != PostCodeField.Text)
                {
                    EditedInfoUser.PostalCode = UpdatePostalField.Text;
                    PostalCodeChanged = true;
                }
                else
                {
                    EditedInfoUser.PostalCode = PostCodeField.Text;
                    PostalCodeChanged = false;
                }
            }

            // Checking if there are any changes with the house number. If so, change state. If not, return it to false.
            if (UpdateHouseNumField.Text != "")
            {
                if (UpdateHouseNumField.Text != HouseNumField.Text)
                {
                    EditedInfoUser.HouseNumber = UpdateHouseNumField.Text;
                    HouseNumChanged = true;
                }
                else
                {
                    EditedInfoUser.HouseNumber = HouseNumField.Text;
                    HouseNumChanged = false;
                }
            }

            // Checking if there are any changes with the customer type. If so, change state. If not, return it to false.
            if (UpdateTypeField.Text != EditedInfoUser.CustomerType)
            {
                EditedInfoUser.CustomerType = UpdateTypeField.Text;
                CustomerTypeChanged = true;
            }

            // Opening up the connection with the SQLite database.
            using (var connection = new SQLiteConnection(cs))
            {
                connection.Open();

                // If street name changed, execute a query to update it for the user with the exact unique user number.
                if (StreetNameChanged == true)
                {
                    string str = "BEGIN TRANSACTION; " +
                                 "UPDATE Users " +
                                 "SET StreetName='" + EditedInfoUser.StreetName + "' " +
                                 "WHERE EXISTS (SELECT Accounts.UniqueUserNumber " +
                                               "FROM Accounts " +
                                               "WHERE Users.UserID=Accounts.UserID AND Accounts.UniqueUserNumber='" + EditedAccount.UniqueUserNumber + "'); " +
                                 "COMMIT;";

                    var command = new SQLiteCommand(str, connection);

                    command.Prepare();

                    command.ExecuteNonQuery();

                    int a = (int)command.ExecuteNonQuery();

                    if (a > 0) UpdateStatusLabel.Text = "Success! " + a;
                    else UpdateStatusLabel.Text = "Not Success";
                }

                // If postal code changed, execute a query to update it for the user with the exact unique user number.
                if (PostalCodeChanged == true)
                {
                    string str = "BEGIN TRANSACTION; " +
                                 "UPDATE Users " +
                                 "SET PostalCode = " + EditedInfoUser.PostalCode + " " +
                                 "WHERE EXISTS (SELECT Accounts.UniqueUserNumber " +
                                               "FROM Accounts " +
                                               "WHERE Users.UserID= Accounts.UserID AND Accounts.UniqueUserNumber = '" + EditedAccount.UniqueUserNumber + "'); " +
                                 "COMMIT;";
                    var command = new SQLiteCommand(str, connection);
                    command.Prepare();
                    command.ExecuteNonQuery();
                    int a = (int)command.ExecuteNonQuery();

                    if (a > 0) UpdateStatusLabel.Text = "Success! " + a;
                    else UpdateStatusLabel.Text = "Not Success";
                }

                // If house number changed, execute a query to update it for the user with the exact unique user number.
                if (HouseNumChanged == true)
                {
                    string str = "BEGIN TRANSACTION; " +
                                 "UPDATE Users " +
                                 "SET HouseNumber = '" + EditedInfoUser.HouseNumber + "' " +
                                 "WHERE EXISTS (SELECT Accounts.UniqueUserNumber " +
                                               "FROM Accounts " +
                                               "WHERE Users.UserID= Accounts.UserID AND Accounts.UniqueUserNumber = '" + EditedAccount.UniqueUserNumber + "'); " +
                                 "COMMIT;";
                    var command = new SQLiteCommand(str, connection);
                    command.Prepare();
                    command.ExecuteNonQuery();
                    int a = (int)command.ExecuteNonQuery();

                    if (a > 0) UpdateStatusLabel.Text = "Success!";
                    else UpdateStatusLabel.Text = "Failure";
                }

                // If customer type changed, execute a query to update it for the user with the exact unique user number.
                if (CustomerTypeChanged == true)
                {
                    string str = "BEGIN TRANSACTION; " +
                                 "UPDATE Users " +
                                 "SET CustomerType = '" + EditedInfoUser.CustomerType + "' " +
                                 "WHERE EXISTS (SELECT Accounts.UniqueUserNumber " +
                                               "FROM Accounts " +
                                               "WHERE Users.UserID= Accounts.UserID AND Accounts.UniqueUserNumber = '" + EditedAccount.UniqueUserNumber + "'); " +
                                 "COMMIT;";
                    var command = new SQLiteCommand(str, connection);
                    command.Prepare();
                    command.ExecuteNonQuery();
                    int a = (int)command.ExecuteNonQuery();

                    if (a > 0) UpdateStatusLabel.Text = "Success! " + a;
                    else UpdateStatusLabel.Text = "Not Success";
                }
            }

            // Updating everything else that comes with the Customer Type change.
            using (var connection = new SQLiteConnection(cs))
            {
                connection.Open();
                if (CustomerTypeField.Text == "Regular")
                {

                    if (EditedAccount.AccountType == "Deposit")
                    {
                        String str = "BEGIN TRANSACTION; " +
                                     "UPDATE Accounts " +
                                     "SET WithdrawalFee = 1, " +
                                     "AccountLimit = 0.0 " +
                                     "WHERE Accounts.UniqueUserNumber='" + EditedAccount.UniqueUserNumber + "' " +
                                     "AND EXISTS (SELECT UserID, CustomerType " +
                                                 "FROM Users " +
                                                 "WHERE Accounts.UserID = Users.UserID AND CustomerType='Regular'); " +
                                     "COMMIT TRANSACTION;";
                        var command = new SQLiteCommand(str, connection);
                        command.Prepare();
                        command.ExecuteNonQuery();
                    }


                    else
                    {
                        String str = "BEGIN TRANSACTION; " +
                                     "UPDATE Accounts " +
                                     "SET WithdrawalFee = 1, " +
                                     "AccountLimit = 5000.0 " +
                                     "WHERE Accounts.UniqueUserNumber='" + EditedAccount.UniqueUserNumber + "' AND EXISTS (SELECT UserID, CustomerType " +
                                                                                                                 "FROM Users " +
                                                                                                                 "WHERE Accounts.UserID = Users.UserID AND CustomerType='Regular'); " +
                                     "COMMIT;";
                        var command = new SQLiteCommand(str, connection);
                        command.Prepare();
                        command.ExecuteNonQuery();

                    }
                }

                else
                {
                    String str = "BEGIN TRANSACTION; " +
                                 "UPDATE Accounts " +
                                 "SET WithdrawalFee = 0, " +
                                 "AccountLimit = 20000.0 " +
                                 "WHERE Accounts.UniqueUserNumber='" + EditedAccount.UniqueUserNumber + "' AND EXISTS (SELECT UserID, CustomerType " +
                                                                                                             "FROM Users " +
                                                                                                             "WHERE Accounts.UserID = Users.UserID AND CustomerType='Premium'); " +
                                 "COMMIT;";
                    var command = new SQLiteCommand(str, connection);
                    command.Prepare();
                    command.ExecuteNonQuery();

                }

            }

            UpdateStatusLabel.Text = "Successfully updated!";

        }

        private void WithdrawBtn_Click(object sender, EventArgs e)
        {
            WithdrawalPanel.Show();
            MakeADepositPanel.Hide();
            UpdateInfoCard.Hide();

            WIthdrawAccNumField.Text = AccountNumberField.Text;
            CurrencyLabel.Text = CurrencyInfoCard.Text;
            CurrencyLabel1.Text = CurrencyLabel.Text;
            RealAmountField.Text = "0";

            WithdrawAmountField.Text = "0";

            if (CustomerTypeField.Text == "Regular")
            {
                WithdrawalFeeField.Text = Convert.ToString(1);
            }
            else
            {
                WithdrawalFeeField.Text = Convert.ToString(0);
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            Regex regexNumbers = new Regex("^[0-9]+$");

            if (WithdrawAmountField.Text != "0,00" && regexNumbers.IsMatch(WithdrawAmountField.Text))
            {
                if (Convert.ToDouble(RealAmountField.Text) < Convert.ToDouble(DepositField.Text) &&
                    (Convert.ToDouble(DepositField.Text) - Convert.ToDouble(RealAmountField.Text)) > Convert.ToDouble(CardLimitField.Text))
                {
                    using (var connection = new SQLiteConnection(cs))
                    {
                        connection.Open();

                        string query = "BEGIN TRANSACTION; " +
                                       "UPDATE Accounts " +
                                       "SET DepositAmount = DepositAmount-" + Double.Parse(RealAmountField.Text) + " " +
                                       "WHERE AccountNumber='" + WIthdrawAccNumField.Text + "'; " +
                                       "" +
                                       "" +
                                       "INSERT INTO Transactions (SenderName, ReceiverName, SenderAccount, ReceiverAccount, Amount, CurrencyCode, TransactionDate, TransactionType) " +
                                       "VALUES( '/', '" + NameField.Text + "', '/', '" + AccountNumberField.Text + "', " + Convert.ToDouble(RealAmountField.Text) + ", '"+CurrencyInfoCard.Text+"','" + DateTime.Now.ToString() + "','Withdrawal'); " +
                                       "COMMIT;";

                        var command = new SQLiteCommand(query, connection);

                        int a = (int)command.ExecuteNonQuery();

                        if (a > 0) WithdrawStatusLabel.Text = "Success!";
                        else WithdrawStatusLabel.Text = "Unsuccessful.";
                        
                    }

                } else
                {
                    MessageBox.Show("You don't have enough money on your account.");
                }
            }
            else
            {
                WithdrawStatusLabel.Text = "Please, input a valid amount.";
            }

        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            Regex regexNumbers = new Regex("^[0-9]+$");
            if (regexNumbers.IsMatch(WithdrawAmountField.Text) || WithdrawAmountField.Text != "")
            {
                if (WithdrawalFeeField.Text == Convert.ToString(1.00))
                {
                    double realAmount = Convert.ToDouble(WithdrawAmountField.Text) - (Convert.ToDouble(WithdrawAmountField.Text) * Convert.ToDouble(WithdrawalFeeField.Text) / Convert.ToDouble(100));
                    RealAmountField.Text = Convert.ToString(realAmount);
                }
                else
                {
                    RealAmountField.Text = WithdrawAmountField.Text;
                }
            } else
            {
                MessageBox.Show("Please make a valid input.");
            }
        }

        private void DepositBtn_Click(object sender, EventArgs e)
        {
            MakeADepositPanel.Show();
            WithdrawalPanel.Hide();
            UpdateInfoCard.Hide();

            DepositToAccount.Text = AccountNumberField.Text;

            NewDepositAmount.Text = Convert.ToString(0.0);

            DepositCurrency.Text = CurrencyLabel1.Text;
        }

        private void DepositPanelBtn_Click(object sender, EventArgs e)
        {
            Regex regexNumbers = new Regex("^[0-9]+$");

            if (NewDepositAmount.Text != "0" && regexNumbers.IsMatch(NewDepositAmount.Text))
            {
                using (var connection = new SQLiteConnection(cs))
                {
                    connection.Open();

                    string query = "BEGIN TRANSACTION; " +
                                       "UPDATE Accounts " +
                                       "SET DepositAmount = DepositAmount+" + Double.Parse(NewDepositAmount.Text) + " " +
                                       "WHERE AccountNumber='" + DepositToAccount.Text + "'; " +
                                       "" +
                                       "" +
                                       "INSERT INTO Transactions (SenderName, ReceiverName, SenderAccount, ReceiverAccount, Amount, CurrencyCode, TransactionDate, TransactionType) " +
                                       "VALUES( '/', '" + NameField.Text + "', '/', '" + AccountNumberField.Text + "', " + Convert.ToDouble(NewDepositAmount.Text) + ", '"+CurrencyInfoCard.Text+"', '" + DateTime.Now.ToString() + "','Deposit'); " +
                                       "COMMIT;";
                    var command = new SQLiteCommand(query, connection);

                    int a = (int)command.ExecuteNonQuery();

                    if (a > 0) DepositStatusLabel.Text = "Success!";
                    else DepositStatusLabel.Text = "Unsuccessful.";
                }

                
            }
            else
            {
                MessageBox.Show("Please make a valid input.");
            }
        }
    }
    }






