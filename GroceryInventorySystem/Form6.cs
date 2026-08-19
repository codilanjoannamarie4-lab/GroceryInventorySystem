using MySql.Data.MySqlClient;
using System;
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
    public partial class Form6 : Form
    {
        // Use Database.GetConnection() instead of per-form connection fields

        public void LoadCard()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT p.product_ID, p.product_name, 
                                c.category, p.price, p.stock, p.exp_date
                         FROM product p
                         JOIN category c ON p.category_ID = c.category_ID";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            LoadCard();

           DialogResult dialogResult = MessageBox.Show("Form6 Loaded");
        }
        public Form6()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

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

        private void LoadStockOutData()
        {
            using (MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;password=;database=grocery_inventory"))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM stock_out_table";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;

                    // Format columns
                    dataGridView1.Columns["product_id"].HeaderText = "ID";
                    dataGridView1.Columns["product_name"].HeaderText = "Product Name";
                    dataGridView1.Columns["quantity"].HeaderText = "Quantity";
                    dataGridView1.Columns["date"].HeaderText = "Date";
                    dataGridView1.Columns["reason"].HeaderText = "Reason";

                    // Format date column
                    dataGridView1.Columns["date"].DefaultCellStyle.Format = "yyyy-MM-dd";

                    // Center align quantity
                    dataGridView1.Columns["quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    // Auto resize columns
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                  
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }


        private void Form6_Load_1(object sender, EventArgs e)
        {

            LoadStockOutData();
            using (MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;password=;database=grocery_inventory"))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT Product_Name FROM product_table";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    product_comboBox.Items.Clear();

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            product_comboBox.Items.Add(reader["Product_Name"].ToString());

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading products: " + ex.Message);
                }
            }
        }

        private void product_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (product_comboBox.SelectedItem != null)
            {
                string selectedProduct = product_comboBox.SelectedItem.ToString();

                using (MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;password=;database=grocery_inventory"))
                {
                    try
                    {
                        conn.Open();

                        string query = "SELECT Stock FROM product_table WHERE Product_Name = @productName";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@productName", selectedProduct);

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            quantity.Text = result.ToString();
                        }
                        else
                        {
                            quantity.Text = "0";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error retrieving stock: " + ex.Message);
                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (product_comboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a product to delete.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this product?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                string selectedProduct = product_comboBox.SelectedItem.ToString();

                using (MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;password=;database=grocery_inventory"))
                {
                    try
                    {
                        conn.Open();

                        string query = "DELETE FROM stock_out_table WHERE Product_Name = @productName";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@productName", selectedProduct);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Product deleted successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Remove from comboBox
                            product_comboBox.Items.Remove(selectedProduct);

                            // Clear the quantity textbox
                            quantity.Clear();

                            LoadStockOutData();



                        }
                        else
                        {
                            MessageBox.Show("Product not found!", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;password=;database=grocery_inventory"))
            {
                conn.Open();

                // Check if product already exists
                string checkQuery = "SELECT COUNT(*) FROM stock_out_table WHERE product_name = @product";
                MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@product", product_comboBox.SelectedItem.ToString());
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    // Update existing record
                    string updateQuery = "UPDATE stock_out_table SET quantity = @quantity, date = @date, reason = @reason WHERE product_name = @product";
                    MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@quantity", quantity.Text);
                    updateCmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                    updateCmd.Parameters.AddWithValue("@reason", reason.Text);
                    updateCmd.Parameters.AddWithValue("@product", product_comboBox.SelectedItem.ToString());
                    updateCmd.ExecuteNonQuery();
                    MessageBox.Show("Updated!");
                }
                else
                {
                    // Insert new record
                    string insertQuery = "INSERT INTO stock_out_table (product_name, quantity, date, reason) VALUES (@product, @quantity, @date, @reason)";
                    MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@product", product_comboBox.SelectedItem.ToString());
                    insertCmd.Parameters.AddWithValue("@quantity", quantity.Text);
                    insertCmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                    insertCmd.Parameters.AddWithValue("@reason", reason.Text);
                    insertCmd.ExecuteNonQuery();
                    MessageBox.Show("Inserted!");
                }

                LoadStockOutData();
            }
        }

        
    }
}
