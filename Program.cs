using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Text;

namespace Homework_test
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Homepage());

            SQLiteConnection SQLiteConn;
            SQLiteConn = CreateConnection();
        }

        static SQLiteConnection CreateConnection()
        {
            SQLiteConnection SqlConn;

            SqlConn = new SQLiteConnection("Data Source=MetropolBankDatabase.db;Version=3");

            try
            {
                SqlConn.Open();
            }
            catch (Exception e)
            {

            }
            return SqlConn;
        }

        static void FindAccount(SQLiteConnection conn)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM SampleTable";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                Console.WriteLine(myreader);
            }
            conn.Close();
        }
    }
}
