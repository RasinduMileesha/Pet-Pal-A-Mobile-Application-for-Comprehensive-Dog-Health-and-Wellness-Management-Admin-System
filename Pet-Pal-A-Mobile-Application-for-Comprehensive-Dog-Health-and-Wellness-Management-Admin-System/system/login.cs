using System;
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

        // Database connection string
        private string connectionString = "Data Source=DESKTOP-T0CRIR8;Initial Catalog=PetDocDB;User ID=sa;Password=991831";

        private void button2_Click(object sender, EventArgs e)
        {
            // Get user inputs
            string gmail = textBox4.Text.Trim();
            string uPass = textBox5.Text.Trim();

            // Input validation
            if (string.IsNullOrEmpty(gmail) || string.IsNullOrEmpty(uPass))
            {
                MessageBox.Show("Please enter both email and password.");
                return;
            }

            // Check for admin credentials
            if (gmail == "admin" && uPass == "admin")
            {
                adminForm AdminForm = new adminForm();
                this.Hide();
                AdminForm.Show();
                return; // Return to avoid executing the rest of the code
            }

            // Database check for user in the admin table
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // SQL query to check credentials in the admin table
                    string query = "SELECT COUNT(*) FROM admin WHERE Email = @gmail AND Password = @uPass";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Use parameters to avoid SQL injection
                        cmd.Parameters.AddWithValue("@gmail", gmail);
                        cmd.Parameters.AddWithValue("@uPass", uPass);

                        // Execute the query
                        int userExists = (int)cmd.ExecuteScalar();

                        // Check if the user exists and the credentials are valid
                        if (userExists > 0)
                        {
                            MessageBox.Show("Login successful!");
                            this.Hide();
                            adminForm admin = new adminForm();
                            admin.Show();
                        }
                        else
                        {
                            MessageBox.Show("Invalid email or password. Please try again.");
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
