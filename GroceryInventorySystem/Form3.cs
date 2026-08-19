using Google.Protobuf.Reflection;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;


namespace GroceryInventorySystem
{
    public partial class Form3 : Form
    {
        List<Product> grocery_inventory = new List<Product>();

        MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;password=;database=grocery_inventory");
        private string expiration_date = null;
        private object editExpiration_date;

        public void LoadCard()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT p.product_id, p.product_name, 
                                c.category, p.price, p.stock, p.exp_date
                         FROM product p
                         JOIN category c ON p.category_id = c.category_id";

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
        public Form3()
        {
            InitializeComponent();
            LoadCategories();

        }


        void LoadCategories()
        {
            using (var conn = Database.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM category_table";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
          

            string editName = productID.Text.Trim();
            string editCategory = categoryk.Text.Trim();
            string editPrice = price.Text.Trim();
            string editStock = stock.Text.Trim();
            string editProduct_Name = product_name.Text.Trim();

            string connString = "server=localhost;userid=root;password=;database=grocery_inventory";
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    // 3. The SQL Query
                    // Matches the column names from your phpMyAdmin setup
                    string query = @"UPDATE product_table 
                             SET Category = @category, 
                                 Price = @price, 
                                 Stock = @stock, 
                                 Expiration_Date = @exp, 
                                 Product_Name = @name 
                             WHERE Product_id = @id";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // 4. Using your variables to fill the parameters
                    cmd.Parameters.AddWithValue("@id", productID.Text);
                    cmd.Parameters.AddWithValue("@category", editCategory);
                    cmd.Parameters.AddWithValue("@price", editPrice);
                    cmd.Parameters.AddWithValue("@stock", editStock);
                    cmd.Parameters.AddWithValue("@exp", editExpiration_date);
                    cmd.Parameters.AddWithValue("@name", editProduct_Name);

                    // 5. Execute
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Item Updated Successfully!");
                        LoadInventoryData(); // Refresh your DataGridView to see the changes
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
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form4 f4 = new Form4();
            f4.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form5 f5 = new Form5();
            f5.ShowDialog();
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form1 f1 = new Form1();
            f1.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.Hide();

            Form4 f4 = new Form4();
            {
                try
                {
                    using (var conn = Database.GetConnection())
                    {
                        conn.Open();
                        string query = "SELECT * FROM members";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dt = new DataTable();

                        da.Fill(dt);
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = dt;

                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Loading Members:" + ex.Message);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // 1. Get values from your textboxes
            string editCategory = categoryk.Text.Trim();
            string editPrice = price.Text.Trim();
            string editStock = stock.Text.Trim();
            string editExpiration_date = dateTimePicker2.Text.Trim();
            string editProduct_Name = product_name.Text.Trim();

            // 2. Database Connection
            using (MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;password=;database=grocery_inventory"))
            {
                try
                {
                    conn.Open();

                    // 3. Changed to INSERT query
                    // We list the columns first, then the @parameters
                    string query = @"INSERT INTO product_table 
                         (Category, Price, Stock, Expiration_Date, Product_Name) 
                         VALUES (@category, @price, @stock, @exp, @name)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // 4. Bind variables to parameters
                    cmd.Parameters.AddWithValue("@category", editCategory);
                    cmd.Parameters.AddWithValue("@price", editPrice);
                    cmd.Parameters.AddWithValue("@stock", editStock);
                    cmd.Parameters.AddWithValue("@exp", editExpiration_date);
                    cmd.Parameters.AddWithValue("@name", editProduct_Name);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("New Product Added Successfully!");
                        LoadInventoryData(); // Refresh the grid to see the new item


                        price.Clear();
                        stock.Clear();
                        expiration_date = DateTime.Now.ToString();
                        product_name.Clear();
                        productID.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
        

        private void Form3_Load_1(object sender, EventArgs e)
        {
            LoadInventoryData();
            
        }

        public void LoadInventoryData()
        {
            string connString = "server=localhost;userid=root;password=;database=grocery_inventory";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM product_table"; 
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                   
                    dataGridView1.DataSource = dt;

                    
                    dataGridView1.Columns["Product_id"].HeaderText = "ID";
                    dataGridView1.Columns["Product_Name"].HeaderText = "Product Name";
                    dataGridView1.Columns["Category"].HeaderText = "Category";
                    dataGridView1.Columns["Price"].HeaderText = "Price (PHP)";
                    dataGridView1.Columns["Stock"].HeaderText = "In Stock";
                    dataGridView1.Columns["Expiration_Date"].HeaderText = "Expiry Date";

                  
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    
                    dataGridView1.Columns["Product_id"].Width = 50;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Delete the selected product by Product_id
            string connString = "server=localhost;userid=root;password=;database=grocery_inventory";
            using (MySqlConnection con = new MySqlConnection(connString))
            {
                try
                {
                    con.Open();
                    string query = "DELETE FROM product_table WHERE Product_id = @product_id";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@product_id", productID.Text.Trim());
                    int rows = cmd.ExecuteNonQuery();
                    MessageBox.Show(rows > 0 ? "Product Deleted!" : "No product found with that ID.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting product: " + ex.Message);
                }
            }

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            if(productID.Text != "")
            {
                string connString = "server=localhost;userid=root;password=;database=grocery_inventory";
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    try
                    {
                        conn.Open();
                        // Use Parameters to prevent SQL Injection
                        string query = "SELECT Category, Price, Stock, Expiration_Date, Product_Name FROM product_table WHERE Product_id = @id";

                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", productID.Text);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Display the data in your TextBoxes
                                categoryk.Text = reader["Category"].ToString();
                                price.Text = reader["Price"].ToString();
                                stock.Text = reader["Stock"].ToString();
                                expiration_date = reader["Expiration_Date"].ToString();
                                product_name.Text = reader["Product_Name"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("No product found with that ID.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }

            }
            else
            {
     
                price.Clear();
                stock.Clear();
                expiration_date = null;
                product_name.Clear();
            }
        }

       
    }
}
