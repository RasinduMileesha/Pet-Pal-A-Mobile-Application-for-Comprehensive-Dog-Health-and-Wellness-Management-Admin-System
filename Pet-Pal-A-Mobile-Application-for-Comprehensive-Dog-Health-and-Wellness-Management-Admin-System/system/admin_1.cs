using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace system
{
    public partial class adminForm : Form
    {
        // Initialize the form
        public adminForm()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-T0CRIR8;Initial Catalog=data;User ID=sa;Password=991831");

        // Define the SQL connection string
       

        // Event handler for form load
        private void adminForm_Load(object sender, EventArgs e)
        {
            GetListUser_data();
        }

        // Event handler for the button click to insert user data
        private void button2_Click(object sender, EventArgs e)
        {
            //Enter Button code
            String nic = textBox1.Text, fname = textBox2.Text, gmail = textBox4.Text, uPass = textBox5.Text;
            int mobileNo = int.Parse(textBox3.Text);

            con.Open();
            SqlCommand c = new SqlCommand("exec Insertuser_data '" + nic + "', '" + fname + "','" + mobileNo + "','" + gmail + "','" + uPass + "'", con);
            c.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Insertion Succesfull!");
            GetListUser_data();

            //textbox clean code
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        void GetListUser_data()
        {
            //Gridview code
            SqlCommand c = new SqlCommand("exec Listuser_data", con);
            SqlDataAdapter sd = new SqlDataAdapter(c);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Update Button code
            String nic = textBox1.Text, fname = textBox2.Text, gmail = textBox4.Text, uPass = textBox5.Text;
            int mobileNo = int.Parse(textBox3.Text);

            con.Open();
            SqlCommand c = new SqlCommand("exec Updateuser_data '" + nic + "', '" + fname + "','" + mobileNo + "','" + gmail + "','" + uPass + "'", con);
            c.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Updation Succesfull!");
            GetListUser_data();

            //textbox clean code
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Delete Button code
            if (MessageBox.Show("This will delete Permanantly!!!", "Delete Document", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                String nic = textBox1.Text;
                con.Open();
                SqlCommand c = new SqlCommand("exec Deleteuser_data '" + nic + "'", con);
                c.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Deletion Succesfull!");
                GetListUser_data();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Search button code
            String nic = textBox1.Text;
            SqlCommand c = new SqlCommand("exec Searchuser_data '" + nic + "'", con);
            SqlDataAdapter sd = new SqlDataAdapter(c);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            dataGridView1.DataSource = dt;
            
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            home Home = new home();
            Home.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            login Login = new login();
            Login.Show();
        }
    }
}