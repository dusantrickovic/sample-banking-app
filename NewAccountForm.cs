using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SQLite;

namespace Homework_test
{
    
    public partial class NewAccountForm : Form
    {
        string cs = "Data Source=MetropolBankDatabase.db;Version=3";
        public NewAccountForm()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void NewAccountForm_Load(object sender, EventArgs e)
        {
            // Hides the next panel of input on load.
            //AccountInfoNextPanel.Hide();

            DateOfBirthCalendar.Hide();
            AccountInformationPanel.Hide();
            UserStatusLabel.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Displays the next panel of input on NEXT.
            //AccountInfoNextPanel.Show();

            AccountInformationPanel.Show();

            Random rnd = new Random();

            User createdUser = new User();
            Account createdAccount = new Account();

            // Creating a new User object containing basic data about the user
            createdAccount.CurrencyName = CurrencyName.Text;
            createdAccount.CurrencyCode = CurrencyCode.Text;

            if (TypeRegular.Checked) createdUser.CustomerType = "Regular";
            else createdUser.CustomerType = "Premium";

            if (CreditAcc.Checked) createdAccount.AccountType = "Credit";
            else createdAccount.AccountType = "Debit";

            // Updating the label by the appropriate textboxes to the currency code
            CurrencyLabel.Text = createdAccount.CurrencyCode.ToString();
            LimitCurrencyLabel.Text = CurrencyLabel.Text;


            // Dealing with card limits for different types of accounts
            if (createdAccount.AccountType == "Debit") CardLimitBox.Text = "0";
            else
            {
                if (createdUser.CustomerType == "Regular") CardLimitBox.Text = "5000";
                else CardLimitBox.Text = "20000";
            };

            // Dealing with withdrawal fees for different types of customers
            if (createdUser.CustomerType == "Regular") WithdrawalFeeBox.Text = "1";
            else WithdrawalFeeBox.Text = "0";

            /* Generating an account number using randomized numbers
             * and displaying it in the ReadOnly textbox in the form
             */

            int CardNumFirstSet = rnd.Next(1000);
            CardNum_1.Text = CardNumFirstSet.ToString("000");

            int CardNumSecondSet = rnd.Next(1000);
            CardNum_2.Text = CardNumSecondSet.ToString("000");

            int CardNumThirdSet = rnd.Next(100);
            CardNum_3.Text = CardNumThirdSet.ToString("00");
        } 

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void DateOfBirth_Focus(object sender, EventArgs e)
        {
            DateOfBirthCalendar.Show();
        }

        private void DateOfBirth_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateOfBirth.Text = DateOfBirthCalendar.SelectionStart.Date.ToShortDateString();
            DateOfBirthCalendar.Hide();
            
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            AccountInformationPanel.Hide();

            
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            
            // Defining RegEx for input validation.
            Regex regex = new Regex("^[a-zA-Z]+$");
            Regex regexNumbers = new Regex("^[0-9]+$");
            int falseCount = 2;

            // Creating new User and Account objects for storing data.
            Account UserAccount = new Account();
            User CreateUser = new User();

            // Storing values from each TextBox into an array (a List).
            List<string> textBoxes = new List<string> { UserName.Text, 
                                            UserLastName.Text, 
                                            DateOfBirth.Text, 
                                            UniqueUserNumberField.Text,
                                            StreetName.Text,
                                            HouseNum.Text,
                                            PostCode.Text,
                                            CountryCode.Text,
                                            CountryName.Text,
                                            CurrencyName.Text,
                                            CurrencyCode.Text,
                                            DepositBox.Text };

            // Doing the same with RadioButton states.
            List<bool> radioButtons = new List<bool> { TypeRegular.Checked,
                                                       TypePremium.Checked,
                                                       DebitAcc.Checked,
                                                       CreditAcc.Checked };
            
            // Checking if any element from the TextBox.Text list is an empty string
            bool atleastOneTextboxEmpty = textBoxes.Exists(t => t == "");

            // Checking if there are more than two unchecked RadioButtons.
            foreach (bool rb in radioButtons)
            {
                if (rb == false) falseCount++;
            }

            // Checking if there are any empty fields or unchecked radio buttons.
            if (atleastOneTextboxEmpty == true || falseCount > 2)
            {
                UserStatusLabel.Visible = true;
                UserStatusLabel.Text = "All fields required.";
            }

            else
            {
                // If there aren't, proceeding to check whether all text fields contain only text.
                if (!regex.IsMatch(UserName.Text) ||
                    !regex.IsMatch(UserLastName.Text) ||
                    !regex.IsMatch(CountryName.Text))
                {
                    UserStatusLabel.Visible = true;
                    UserStatusLabel.Text = "Name, Last Name, and Country Name, must all contain letters only.";
                }
                else
                {
                    // If they do, proceeding to check if all numeric fields contain only numbers.
                    if (!regexNumbers.IsMatch(UniqueUserNumberField.Text) || 
                        !regexNumbers.IsMatch(CountryCode.Text) || !regexNumbers.IsMatch(DepositBox.Text))
                    {
                        UserStatusLabel.Visible = true;
                        UserStatusLabel.Text = "Unique User ID, Country Code, and Deposit must all contain numbers only.";
                    }
                    else
                    {
                        // If they do, checking if the Unique User ID (basically an equivalent to the Serbian JMBG) contains under or over 13 digits.
                        if (UniqueUserNumberField.Text.Length < 13 || UniqueUserNumberField.Text.Length > 13)
                        {
                            UserStatusLabel.Visible = true;
                            UserStatusLabel.Text = "Unique User ID must contain exactly 13 digits.";
                        }
                        else
                        {
                            // If everything is valid, proceeding to populate the earlier created objects.
                            CreateUser.Name = UserName.Text;
                            CreateUser.LastName = UserLastName.Text;
                            CreateUser.DateOfBirth = DateOfBirth.Text;
                            CreateUser.StreetName = StreetName.Text;
                            CreateUser.HouseNumber = HouseNum.Text;
                            CreateUser.PostalCode = PostCode.Text;
                            CreateUser.CountryName = CountryName.Text;
                            CreateUser.CountryCode = Convert.ToInt32(CountryCode.Text);
                            
                            if (TypeRegular.Checked) CreateUser.CustomerType = "Regular";
                            else CreateUser.CustomerType = "Premium";

                            UserAccount.UniqueUserNumber = Convert.ToString(UniqueUserNumberField.Text);
                            UserAccount.CurrencyName = CurrencyName.Text;
                            UserAccount.CurrencyCode = CurrencyCode.Text;
                            
                            // Storing the account number into the Account object instance
                            UserAccount.AccountNumber = Convert.ToString(CardNum_1.Text + '-' + CardNum_2.Text + '-' + CardNum_3.Text);

                            // Storing other information into the Account object instance
                            UserAccount.DepositAmount = Convert.ToDouble(DepositBox.Text);
                            UserAccount.WithdrawalFee = Convert.ToInt32(WithdrawalFeeBox.Text);
                            UserAccount.CardLimit = Convert.ToDouble(CardLimitBox.Text);

                            // Dealing with Credit Payment Types
                            if (MonthlyCredit.Checked) UserAccount.CreditPaymentType = "Monthly";
                            else if (QuarterlyCredit.Checked) UserAccount.CreditPaymentType = "Quarterly";
                            else UserAccount.CreditPaymentType = "Yearly";


                            using(var connection = new SQLiteConnection(cs))
                            {
                                connection.Open();

                                // Query to insert the newly created user into both the Users and the Accounts table.

                                var str = "BEGIN TRANSACTION; " +
                                             "INSERT INTO Users(UniqueUserNumber,Name,LastName,DateOfBirth,CustomerType,StreetName,HouseNumber,PostalCode,CountryName,CountryCode) " +
                                             "VALUES ( '@un', '@name', '@lastname','@dateofbirth','@customertype','@streetname','@housenumber', @postalcode, '@countryname', '@countrycode); " +
                                             "" +
                                             "" +
                                             "INSERT INTO Accounts(UniqueUserNumber,AccountNumber,AccountType,CurrencyName,CurrencyCode,DepositAmount,AccountLimit,WithdrawalFee,CreditPaymentType) " +
                                             "VALUES ( '@uniqueusernumber', '@accountnumber', '@accounttype', '@currencyname', '@currencycode', @depositamount, @accountlimit, @withdrawalfee, '@creditpaymenttype); " +
                                             "COMMIT;";
                                var command = new SQLiteCommand(str, connection);
                                command.Parameters.AddWithValue("@un", UserAccount.UniqueUserNumber);
                                command.Parameters.AddWithValue("@name", CreateUser.Name);
                                command.Parameters.AddWithValue("@lastname", CreateUser.LastName);
                                command.Parameters.AddWithValue("@dateofbirth", CreateUser.DateOfBirth);
                                command.Parameters.AddWithValue("@streetname", CreateUser.StreetName);
                                command.Parameters.AddWithValue("@customertype", CreateUser.CustomerType);
                                command.Parameters.AddWithValue("@housenumber", CreateUser.HouseNumber);
                                command.Parameters.AddWithValue("@postalcode", CreateUser.PostalCode);
                                command.Parameters.AddWithValue("@countryname", CreateUser.CountryName);
                                command.Parameters.AddWithValue("@countrycode", CreateUser.CountryCode);

                                command.Parameters.AddWithValue("@accountnumber", UserAccount.AccountNumber);
                                command.Parameters.AddWithValue("@currencyname", UserAccount.CurrencyName);
                                command.Parameters.AddWithValue("@currencycode", UserAccount.CurrencyCode);
                                command.Parameters.AddWithValue("@depositamount", UserAccount.DepositAmount);
                                command.Parameters.AddWithValue("@accountlimit", UserAccount.CardLimit);
                                command.Parameters.AddWithValue("@withdrawalfee", UserAccount.WithdrawalFee);
                                command.Parameters.AddWithValue("@creditpaymenttype", UserAccount.CreditPaymentType);

                                int a = (int)command.ExecuteNonQuery();

                                if (a > 0) UserStatusLabel.Text = "Success";
                                else UserStatusLabel.Text = "Fail";

                            }
                            

                        }
                    }
                }

                

            }

        }

        private void CurrencyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int current = CurrencyName.SelectedIndex;

            switch(current)
            {
                default: 
                case 0:
                    {
                        CurrencyCode.Text = "EUR";
                        break;
                    }
                case 1:
                    {
                        CurrencyCode.Text = "USD";
                        break;
                    }
                case 2:
                    {
                        CurrencyCode.Text = "GBP";
                        break;
                    }
                case 3:
                    {
                        CurrencyCode.Text = "RSD";
                        break;
                    }
            }
        }
    }
    class User
    {
        public string Name,
            LastName,
            DateOfBirth,
            StreetName,
            HouseNumber,
            PostalCode,
            CountryName,
            CustomerType;
        public int CountryCode;
    }

    class Account
    {
        public string AccountType,
                      AccountNumber, 
                      CreditPaymentType,
                      CurrencyName,
                      CurrencyCode,
                      UniqueUserNumber;

        public double DepositAmount,
                      CardLimit;

        public int WithdrawalFee;
    }

    class Transaction
    {
        public string TransactionSender,
                      TransactionReceiver,
                      TransactionSenderAcc,
                      TransactionReceiverAcc,
                      TransactionDate,
                      TransactionType,
                      TransactionCurrency;

        public double TransactionAmount;
                      
    }
}
