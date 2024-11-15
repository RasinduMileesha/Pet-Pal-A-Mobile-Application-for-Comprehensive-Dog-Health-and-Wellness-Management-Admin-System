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

namespace system
{
    public partial class registration : Form
    {
        public registration()
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

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-T0CRIR8;Initial Catalog=data;User ID=sa;Password=991831");

        private void button2_Click(object sender, EventArgs e)
        {
            //Enter Button code
            String nic = textBox1.Text, fname = textBox2.Text, gmail = textBox4.Text, uPass = textBox5.Text;
            int mobileNo = int.Parse(textBox3.Text);

            con.Open();
            SqlCommand c = new SqlCommand("exec Insertuser_data '" + nic + "', '" + fname + "','" + mobileNo + "','" + gmail + "','" + uPass + "'", con);
            c.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Registration Succesfull!");

            this.Hide();

            // Show the login form
            login loginForm = new login();
            loginForm.Show();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            login Login = new login();
            Login.Show();  
        }
    }
}
