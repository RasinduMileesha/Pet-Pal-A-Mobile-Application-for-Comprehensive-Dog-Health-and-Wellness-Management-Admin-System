using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace system
{
    public partial class adminForm : Form
    {
        public adminForm()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-T0CRIR8;Initial Catalog=PetDocDB;User ID=sa;Password=991831;TrustServerCertificate=True");

        private void adminForm_Load(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0; // Default selection
            }
            comboBox1_SelectedIndexChanged(this, EventArgs.Empty);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                if (comboBox1.SelectedItem.ToString() == "Pet Owners")
                {
                    LoadPetOwnerData();
                }
                else if (comboBox1.SelectedItem.ToString() == "Vet")
                {
                    LoadVetData();
                }
            }
        }

        // Insert data
        private void button2_Click(object sender, EventArgs e)
        {
            string storedProcedure = comboBox1.SelectedItem.ToString() == "Pet Owners" ? "InsertPetOwner" : "InsertVet";
            string nicOrVetId = textBox1.Text;
            string name = textBox2.Text;
            string gmail = textBox3.Text;
            string mobileNo = textBox4.Text;
            string address = textBox7.Text;
            string city = textBox6.Text;
            string password = textBox5.Text;
            DateTime registrationDate = dateTimePicker1.Value;

            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(storedProcedure, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NIC", nicOrVetId);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Gmail", gmail);
                    cmd.Parameters.AddWithValue("@Mobile", mobileNo);
                    cmd.Parameters.AddWithValue("@CityProvince", city);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@RegistrationDate", registrationDate);

                    if (storedProcedure == "InsertPetOwner")
                    {
                        cmd.Parameters.AddWithValue("@Address", address);
                    }

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Insertion Successful!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            ClearTextBoxes();
        }

        // Update data
        private void button3_Click(object sender, EventArgs e)
        {
            string storedProcedure = comboBox1.SelectedItem.ToString() == "Pet Owners" ? "UpdatePetOwner" : "UpdateVet";
            string nicOrVetId = textBox1.Text;
            string name = textBox2.Text;
            string gmail = textBox3.Text;
            string mobileNo = textBox4.Text;
            string address = textBox7.Text;
            string city = textBox6.Text;
            string password = textBox5.Text;
            DateTime registrationDate = dateTimePicker1.Value;

            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(storedProcedure, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add appropriate ID parameter based on the type (Pet Owner or Vet)
                    if (storedProcedure == "UpdatePetOwner")
                    {
                        cmd.Parameters.AddWithValue("@NIC", nicOrVetId);
                        cmd.Parameters.AddWithValue("@Address", address);
                    }
                    else // for Vets
                    {
                        cmd.Parameters.AddWithValue("@VetId", nicOrVetId);
                    }

                    // Add remaining parameters
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Gmail", gmail);
                    cmd.Parameters.AddWithValue("@Mobile", mobileNo);
                    cmd.Parameters.AddWithValue("@CityProvince", city);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@RegistrationDate", registrationDate);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Update Successful!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            ClearTextBoxes();
        }


        // Delete data
        private void button4_Click(object sender, EventArgs e)
        {
            string storedProcedure = comboBox1.SelectedItem.ToString() == "Pet Owners" ? "DeletePetOwner" : "DeleteVet";
            string nicOrVetId = textBox1.Text;

            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(storedProcedure, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@NIC", nicOrVetId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Deletion Successful!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            ClearTextBoxes();
        }

        // Search data
        private void button1_Click(object sender, EventArgs e)
        {
            string storedProcedure;
            string parameterName;

            if (comboBox1.SelectedItem.ToString() == "Pet Owners")
            {
                storedProcedure = "SearchPetOwner_data";
                parameterName = "@NIC"; // Ensure this matches the stored procedure parameter
            }
            else
            {
                storedProcedure = "SearchVet";
                parameterName = "@VetID"; // Ensure this matches the stored procedure parameter
            }

            string nicOrVetId = textBox1.Text;

            try
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(storedProcedure, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(parameterName, nicOrVetId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBox2.Text = reader["Name"]?.ToString() ?? string.Empty;
                            textBox3.Text = reader["Gmail"]?.ToString() ?? string.Empty;
                            textBox4.Text = reader["Mobile"]?.ToString() ?? string.Empty;
                            textBox6.Text = reader["CityProvince"]?.ToString() ?? string.Empty;
                            textBox5.Text = reader["Password"]?.ToString() ?? string.Empty;

                            if (storedProcedure == "SearchPetOwner_data")
                            {
                                textBox7.Text = reader["Address"]?.ToString() ?? string.Empty;
                            }

                            if (reader["RegistrationDate"] != DBNull.Value)
                            {
                                dateTimePicker1.Value = Convert.ToDateTime(reader["RegistrationDate"]);
                            }
                            else
                            {
                                dateTimePicker1.Value = DateTime.Now;
                            }
                        }
                        else
                        {
                            MessageBox.Show("No records found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }








        // Load data based on selection
        private void LoadData()
        {
            if (comboBox1.SelectedItem.ToString() == "Pet Owners")
            {
                LoadPetOwnerData();
            }
            else if (comboBox1.SelectedItem.ToString() == "Vet")
            {
                LoadVetData();
            }
        }

        private void LoadPetOwnerData()
        {
            dataGridView1.DataSource = GetData("ListPetOwner_data");
        }

        private void LoadVetData()
        {
            dataGridView1.DataSource = GetData("ListVets_data");
        }

        private DataTable GetData(string storedProcedure)
        {
            DataTable dataTable = new DataTable();

            try
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(storedProcedure, con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            return dataTable;
        }

        private void ClearTextBoxes()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            dateTimePicker1.Value = DateTime.Now;
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            string name = textBox2.Text.Trim();  // Trim any leading or trailing spaces

            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                using (SqlCommand cmd = new SqlCommand("SearchPetOwner_Name", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", name);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            // Safely retrieve values to avoid errors if fields are null

                            textBox1.Text = reader["NICPassport"]?.ToString() ?? string.Empty;
                            textBox3.Text = reader["Gmail"]?.ToString() ?? string.Empty;
                            textBox4.Text = reader["Mobile"]?.ToString() ?? string.Empty;
                            textBox7.Text = reader["Address"]?.ToString() ?? string.Empty;
                            textBox6.Text = reader["CityProvince"]?.ToString() ?? string.Empty;
                            textBox5.Text = reader["Password"]?.ToString() ?? string.Empty;

                            // Check for a non-null date
                            if (reader["RegistrationDate"] != DBNull.Value)
                            {
                                dateTimePicker1.Value = Convert.ToDateTime(reader["RegistrationDate"]);
                            }
                            else
                            {
                                dateTimePicker1.Value = DateTime.Now; // Default or prompt for input
                            }
                        }
                        else
                        {
                            MessageBox.Show("No records found.");
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("SQL Error: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }
    }

}