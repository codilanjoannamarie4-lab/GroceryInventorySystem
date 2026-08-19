using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GroceryInventorySystem
{
    public partial class Form4 : Form
    {

        string connString = "server = localhost; user=root; database = grocery_inventory; port = 3306; password=;";
        MySqlConnection conn;
        private string name;
        private object productID;

        public void LoadCard()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT Category_id, Category FROM category_table";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading Grid: " + ex.Message);
            }
        }

        public Form4()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form6 f6 = new Form6();
            f6.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form7 f7 = new Form7();
            f7.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form5 f5 = new Form5();
            f5.ShowDialog();
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

            MessageBox.Show("Adding New Category...");

            using (MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;password=;database=grocery_inventory"))
            {
                try
                {
                    conn.Open();

                    string query = @"INSERT INTO category_table (Category) VALUES (@category)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@category", category.Text);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("New Category Added Successfully!");

                        using (MySqlConnection myConnection = new MySqlConnection("server=localhost;userid=root;password=;database=grocery_inventory"))
                        {
                            try
                            {
                                myConnection.Open();
                                string myQuery = "SELECT Category_id, Category FROM category_table";
                                MySqlDataAdapter myAdapter = new MySqlDataAdapter(myQuery, myConnection);
                                DataTable myDataTable = new DataTable();
                                myAdapter.Fill(myDataTable);

                                dataGridView1.DataSource = myDataTable;

                                dataGridView1.Columns["Category_id"].HeaderText = "Category ID";
                                dataGridView1.Columns["Category"].HeaderText = "Category";

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




        private void LoadData()
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form4_Load_1(object sender, EventArgs e)
        {
            LoadCard();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Ensure a row is selected in the DataGridView
            if (dataGridView1 == null || dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Please select a category to delete.", "No selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Try to get the Category_id from the selected row
            object cellValue = dataGridView1.CurrentRow.Cells["Category_id"]?.Value;
            if (cellValue == null || !int.TryParse(cellValue.ToString(), out int categoryId))
            {
                MessageBox.Show("Selected row does not contain a valid Category ID.", "Invalid selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Confirm deletion with the user
            var result = MessageBox.Show($"Are you sure you want to delete category ID {categoryId}?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
                return;

            // Perform parameterized DELETE to avoid SQL injection and syntax errors
            using (MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;password=;database=grocery_inventory"))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM category_table WHERE Category_id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", categoryId);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            MessageBox.Show("No matching category was found to delete.", "Delete result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Deleted!", "Delete result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting category: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Refresh the DataGridView contents
            try
            {
                using (MySqlConnection myConnection = new MySqlConnection("server=localhost;userid=root;password=;database=grocery_inventory"))
                {
                    myConnection.Open();
                    string myQuery = "SELECT Category_id, Category FROM category_table";
                    MySqlDataAdapter myAdapter = new MySqlDataAdapter(myQuery, myConnection);
                    DataTable myDataTable = new DataTable();
                    myAdapter.Fill(myDataTable);

                    dataGridView1.DataSource = myDataTable;
                    dataGridView1.Columns["Category_id"].HeaderText = "Category ID";
                    dataGridView1.Columns["Category"].HeaderText = "Category";
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Clear()
        {
            throw new NotImplementedException();
        }

        private void Categ_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            string connString = "server=localhost;userid=root;password=;database=grocery_inventory";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    string query = @"UPDATE category_table 
                 SET Category = @category 
                 WHERE Category_id = @id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@id", category_id.Text);
                    cmd.Parameters.AddWithValue("@category", category.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Category Updated Successfully!");

                        using (MySqlConnection myConnection = new MySqlConnection("server=localhost;userid=root;password=;database=grocery_inventory"))
                        {
                            try
                            {
                                myConnection.Open();
                                string myQuery = "SELECT Category_id, Category FROM category_table";
                                MySqlDataAdapter myAdapter = new MySqlDataAdapter(myQuery, myConnection);
                                DataTable myDataTable = new DataTable();
                                myAdapter.Fill(myDataTable);

                                dataGridView1.DataSource = myDataTable;

                                dataGridView1.Columns["Category_id"].HeaderText = "Category ID";
                                dataGridView1.Columns["Category"].HeaderText = "Category";

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
                        MessageBox.Show("No changes made. Check if the Category ID exists.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            

        }

        

   
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }
    }

}
