using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestingLibrary
{
    public partial class AddUser : Form
    {
        public string connectionString;
        private SqlConnection cnn;
        public AddUser()
        {
            InitializeComponent();
            connectionString = @"Data Source=DESKTOP-CD7GTAS\SQLEXPRESS01;Initial Catalog=Demodb;User ID=Helper;Password=Helper";
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            MessageBox.Show("Connection Open!");
            cnn.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = null;

            // Prepare a proper parameterized query 
            sql = "Insert Into UserLibrary (gameName, gamePath) values(@name,@path)";

            // Create the connection (and be sure to dispose it at the end)
            using (cnn = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection to the database. 
                    // This is the first critical step in the process.
                    // If we cannot reach the db then we have connectivity problems
                    cnn.Open();

                    // Prepare the command to be executed on the db
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        // Create and set the parameters values 
                        cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = UsernameBox.Text;
                        cmd.Parameters.Add("@path", SqlDbType.NVarChar).Value = PasswrdBox.Text;

                        // Let's ask the db to execute the query
                        int rowsAdded = cmd.ExecuteNonQuery();

                        if (rowsAdded > 0)
                        {
                            MessageBox.Show("Row inserted!!");
                            this.Hide();
                            var mainPage = new MainPage();
                            mainPage.Show();

                        }
                        else
                            // Well this should never really happen
                            MessageBox.Show("No row inserted");

                    }
                }
                catch (Exception ex)
                {
                    // We should log the error somewhere, 
                    // for this example let's just show a message
                    MessageBox.Show("ERROR:" + ex.Message);
                }

            }
        }
    }
}
