using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GroceryInventorySystem
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        
        

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        // Renamed to PascalCase to satisfy IDE1006 naming rule
        private void Button5_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form6 f6 = new Form6();
            f6.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form5 f5 = new Form5();
            f5.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form4 f4 = new Form4();
            f4.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form1 f1 = new Form1();
            f1.ShowDialog();
        }


        private void button8_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Adding New Product...");
            // 2. Database Connection
            using (MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;password=;database=grocery_inventory"))
            {
                try
                {
                    conn.Open();

                    // 3. Changed to INSERT query
                    // We list the columns first, then the @parameters
                    string query = @"INSERT INTO supplier_table 
                         (supplier, contact, address) 
                         VALUES (@supplier, @contact, @address)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // 4. Bind variables to parameters
                    cmd.Parameters.AddWithValue("@supplier", name.Text);
                    cmd.Parameters.AddWithValue("@contact", contact.Text);
                    cmd.Parameters.AddWithValue("@address", address.Text);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("New Product Added Successfully!");

                        string myConnectionString = "server=localhost;userid=root;password=;database=grocery_inventory";

                        using (MySqlConnection myConnection = new MySqlConnection(myConnectionString))
                        {
                            try
                            {
                                myConnection.Open();
                                string myQuery = "SELECT * FROM supplier_table";
                                MySqlDataAdapter myAdapter = new MySqlDataAdapter(myQuery, myConnection);
                                DataTable myDataTable = new DataTable();
                                myAdapter.Fill(myDataTable);


                                dataGridView1.DataSource = myDataTable;


                                dataGridView1.Columns["supplier_id"].HeaderText = "Supplier ID";
                                dataGridView1.Columns["supplier"].HeaderText = "Supplier";
                                dataGridView1.Columns["contact"].HeaderText = "Contact";
                                dataGridView1.Columns["address"].HeaderText = "Address";


                                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error loading data: " + ex.Message);
                            }
                        }




                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

     

        private void splitter2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            string myConnectionString = "server=localhost;userid=root;password=;database=grocery_inventory";

            using (MySqlConnection myConnection = new MySqlConnection(myConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string myQuery = "SELECT * FROM supplier_table";
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter(myQuery, myConnection);
                    DataTable myDataTable = new DataTable();
                    myAdapter.Fill(myDataTable);


                    dataGridView1.DataSource = myDataTable;


                    dataGridView1.Columns["supplier_id"].HeaderText = "Supplier ID";
                    dataGridView1.Columns["supplier"].HeaderText = "Supplier";
                    dataGridView1.Columns["contact"].HeaderText = "Contact";
                    dataGridView1.Columns["address"].HeaderText = "Address";


                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }

        

        private void button9_Click(object sender, EventArgs e)
        {



            string connString = "server=localhost;userid=root;password=;database=grocery_inventory";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    // 3. The SQL Query
                    // Matches the column names from your phpMyAdmin setup
                    string query = @"UPDATE supplier_table 
                             SET supplier = @supplier, 
                                 contact = @contact, 
                                 address = @address 
                             WHERE supplier_id = @id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // 4. Using your variables to fill the parameters
                    cmd.Parameters.AddWithValue("@id", supplier_id.Text);
                    cmd.Parameters.AddWithValue("@supplier", name.Text);
                    cmd.Parameters.AddWithValue("@contact", contact.Text);
                    cmd.Parameters.AddWithValue("@address", address.Text);


                    // 5. Execute
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Item Updated Successfully!");
                        string myConnectionString = "server=localhost;userid=root;password=;database=grocery_inventory";

                        using (MySqlConnection myConnection = new MySqlConnection(myConnectionString))
                        {
                            try
                            {
                                myConnection.Open();
                                string myQuery = "SELECT * FROM supplier_table";
                                MySqlDataAdapter myAdapter = new MySqlDataAdapter(myQuery, myConnection);
                                DataTable myDataTable = new DataTable();
                                myAdapter.Fill(myDataTable);


                                dataGridView1.DataSource = myDataTable;


                                dataGridView1.Columns["supplier_id"].HeaderText = "Supplier ID";
                                dataGridView1.Columns["supplier"].HeaderText = "Supplier";
                                dataGridView1.Columns["contact"].HeaderText = "Contact";
                                dataGridView1.Columns["address"].HeaderText = "Address";


                                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error loading data: " + ex.Message);
                            }
                        }


                    }
                    else
                    {
                        MessageBox.Show("No changes made. Check if the Product ID exists.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;password=;database=grocery_inventory"))
            {
                conn.Open();

                // Validate supplier_id before attempting DELETE
                if (string.IsNullOrWhiteSpace(supplier_id.Text) || !int.TryParse(supplier_id.Text.Trim(), out int id))
                {
                    MessageBox.Show("Please select a valid supplier ID before deleting.");
                    return;
                }

                string query = "DELETE FROM supplier_table WHERE supplier_id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                string myConnectionString = "server=localhost;userid=root;password=;database=grocery_inventory";

                using (MySqlConnection myConnection = new MySqlConnection(myConnectionString))
                {
                    try
                    {
                        myConnection.Open();
                        string myQuery = "SELECT * FROM supplier_table";
                        MySqlDataAdapter myAdapter = new MySqlDataAdapter(myQuery, myConnection);
                        DataTable myDataTable = new DataTable();
                        myAdapter.Fill(myDataTable);


                        dataGridView1.DataSource = myDataTable;


                        dataGridView1.Columns["supplier_id"].HeaderText = "Supplier ID";
                        dataGridView1.Columns["supplier"].HeaderText = "Supplier";
                        dataGridView1.Columns["contact"].HeaderText = "Contact";
                        dataGridView1.Columns["address"].HeaderText = "Address";


                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading data: " + ex.Message);
                    }
                }

                MessageBox.Show("Deleted!");
            }
        }

        private void supplier_id_TextChanged(object sender, EventArgs e)
        {
            // Check if the supplier_id textbox is not empty
            if (!string.IsNullOrWhiteSpace(supplier_id.Text))
            {
                // Try to parse the ID to integer
                if (int.TryParse(supplier_id.Text, out int supplierId))
                {
                    // Fetch supplier details from database
                    using (MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;password=;database=grocery_inventory"))
                    {
                        try
                        {
                            conn.Open();

                            // Query to get supplier details by ID
                            string query = "SELECT supplier, contact, address FROM supplier_table WHERE supplier_id = @id";
                            MySqlCommand cmd = new MySqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@id", supplierId);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Populate the textboxes with retrieved data
                                    name.Text = reader["supplier"].ToString();
                                    contact.Text = reader["contact"].ToString();
                                    address.Text = reader["address"].ToString();
                                }
                                else
                                {
                                    // Clear the fields if ID not found
                                    name.Text = "";
                                    contact.Text = "";
                                    address.Text = "";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error retrieving supplier: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    // Clear fields if ID is not a valid number
                    name.Text = "";
                    contact.Text = "";
                    address.Text = "";
                }
            }
            else
            {
                // Clear fields if ID is empty
                name.Text = "";
                contact.Text = "";
                address.Text = "";
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            string myConnectionString = "server=localhost;userid=root;password=;database=grocery_inventory";

            using (MySqlConnection myConnection = new MySqlConnection(myConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string myQuery = "SELECT * FROM supplier_table";
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter(myQuery, myConnection);
                    DataTable myDataTable = new DataTable();
                    myAdapter.Fill(myDataTable);


                    dataGridView1.DataSource = myDataTable;


                    dataGridView1.Columns["supplier_id"].HeaderText = "Supplier ID";
                    dataGridView1.Columns["supplier"].HeaderText = "Supplier";
                    dataGridView1.Columns["contact"].HeaderText = "Contact";
                    dataGridView1.Columns["address"].HeaderText = "Address";


                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }
    }
}
