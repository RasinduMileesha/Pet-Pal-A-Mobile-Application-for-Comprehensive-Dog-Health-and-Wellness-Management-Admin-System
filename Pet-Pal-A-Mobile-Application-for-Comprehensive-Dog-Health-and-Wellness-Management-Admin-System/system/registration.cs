using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace system
{
    public partial class registration : Form
    {
        public registration()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-T0CRIR8;Initial Catalog=PetDocDB;User ID=sa;Password=991831;TrustServerCertificate=True";

            string nicValue = nic.Text.Trim();
            string nameValue = name.Text.Trim();
            string mobileValue = mobile.Text.Trim();
            string emailValue = gmail.Text.Trim();
            string passwordValue = password.Text.Trim();

            // Validate fields
            if (string.IsNullOrEmpty(nicValue) || string.IsNullOrEmpty(nameValue) ||
                string.IsNullOrEmpty(mobileValue) || string.IsNullOrEmpty(emailValue) ||
                string.IsNullOrEmpty(passwordValue))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Validate email format
            if (!Regex.IsMatch(emailValue, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Invalid email format.");
                return;
            }

            // Ensure mobile number contains only digits
            if (!Regex.IsMatch(mobileValue, @"^\d+$"))
            {
                MessageBox.Show("Mobile number must contain only digits.");
                return;
            }

            // Insert data into the admin table
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO admin (NIC, Name, Mobile, Email, Password) VALUES (@NIC, @Name, @Mobile, @Email, @Password)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@NIC", nicValue);
                        cmd.Parameters.AddWithValue("@Name", nameValue);
                        cmd.Parameters.AddWithValue("@Mobile", mobileValue);
                        cmd.Parameters.AddWithValue("@Email", emailValue);
                        cmd.Parameters.AddWithValue("@Password", passwordValue);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Registration successful!");
                        }
                        else
                        {
                            MessageBox.Show("Registration failed.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Navigate to the login form
            var loginForm = new login();
            loginForm.Show();
            this.Hide();
        }


        private void registration_Load(object sender, EventArgs e)
        {
            // Form load logic (if needed)
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Optional: Validate NIC field
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Optional: Add functionality for clicking the picture box
        }
    }
}
