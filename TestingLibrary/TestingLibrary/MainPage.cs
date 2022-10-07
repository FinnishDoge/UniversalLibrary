using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TestingLibrary
{
    public partial class MainPage : Form
    {
        SqlConnection cnn;
        string connetionString;
        public MainPage()
        {
            InitializeComponent();
            ListBox mylist = new ListBox();

            connetionString = @"Data Source=DESKTOP-CD7GTAS\SQLEXPRESS01;Initial Catalog=Demodb;User ID=Helper;Password=Helper";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            MessageBox.Show("Connection Open!");
            string sql = null;

            sql = "SELECT * FROM GameLibrary";

            using (SqlConnection cnn = new SqlConnection(connetionString))
                try
                {
                    // Open the connection to the database. 
                    // This is the first critical step in the process.
                    // If we cannot reach the db then we have connectivity problems
                    cnn.Open();

                    // Prepare the command to be executed on the db
                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {
                        // Let's ask the db to execute the query
                        int rowsAdded = cmd.ExecuteNonQuery();

                        if (rowsAdded > 0)
                        {
                            MessageBox.Show("Row inserted!!");
                        }
                        else
                        {
                            // Well this should never really happen
                            MessageBox.Show("No row inserted");
                        }

                    }
                }
                catch (Exception ex)
                {
                    // We should log the error somewhere, 
                    // for this example let's just show a message
                    MessageBox.Show("ERROR:" + ex.Message);
                }

            

            cnn.Close();
        }

        private void NewGameBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form1 = new AddGame();
            form1.Show();
        }

        

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
