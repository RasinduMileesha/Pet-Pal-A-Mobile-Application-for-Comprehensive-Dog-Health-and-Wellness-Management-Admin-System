using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace system
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        // Private fields to store the values of gmail and uPass
        private string _gmail;
        private string _uPass;

        // Public properties to get and set the values
        public string Gmail
        {
            get { return _gmail; }
            set { _gmail = value; }
        }

        public string UPass
        {
            get { return _uPass; }
            set { _uPass = value; }
        }

        // Database connection string
        private string connectionString = "Data Source=DESKTOP-T0CRIR8;Initial Catalog=data;User ID=sa;Password=991831";

        private void button2_Click(object sender, EventArgs e)
        {
            // Assign values to properties
            Gmail = textBox4.Text;
            UPass = textBox5.Text;

            // Check for admin credentials
            if (Gmail == "admin" && UPass == "admin")
            {
                adminForm AdminForm = new adminForm();
                this.Hide();
                AdminForm.Show();
                return; // Return to avoid executing the rest of the code
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Use stored procedure for login
                    using (SqlCommand cmd = new SqlCommand("loginUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@gmail", Gmail);
                        cmd.Parameters.AddWithValue("@uPass", UPass);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string result = reader["Result"].ToString();
                                if (result == "Login Successful")
                                {
                                    this.Hide();
                                    home Home = new home();
                                    Home.Show();
                                }
                                else
                                {
                                    MessageBox.Show("Invalid credentials, please try again.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("No data returned from the server.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}
