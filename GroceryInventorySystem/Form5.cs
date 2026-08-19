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
    public partial class Form5 : Form
    {  // Use Database.GetConnection() instead of per-form connection fields

        public Form5()
        {
            InitializeComponent();
        }

        MySqlConnection conn = new
           MySqlConnection("server=localhost;userid=root;password=;database=grocery_inventory;");
        public void Loadgrocery_inventory()
        {
            try
            {
                using (var conn = Database.GetConnection())
                {
                    conn.Open();

                    string query = @"
                    SELECT p.product, c.category, u.supplier, s.quantity, s.date
                    FROM stock_in (restock) s
                    JOIN product p
                    ON s.product_id = p.product_id
                    JOIN supplier u
                    ON s.supplier_id = u.supplier_id
                    JOIN category c 
                    ON p.category_id = c.category_id";

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

        void LoadProducts()
        {
            
        }
      
        
        
        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form7 f7 = new Form7();
            f7.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form7 f7 = new Form7();
            f7.ShowDialog();
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
            using (MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;password=;database=grocery_inventory;"))
            {
                conn.Open();
                string query = "INSERT INTO restock_table (product_name, quantity, supplier, date) VALUES (@product, @quantity, @supplier, @date)";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@product", product_comboBox.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@quantity", quantity.Text);
                cmd.Parameters.AddWithValue("@supplier", supplier.Text);
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));

                cmd.ExecuteNonQuery();
                MessageBox.Show("Inserted!");
                quantity.Clear();

                LoadRestockData();
            }
        }

        private void LoadRestockData()
        {
            using (MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;password=;database=grocery_inventory;"))
            {
                try
                {
                    conn.Open();

                    // Convert date to string in the query to avoid conversion issues
                    string query = "SELECT product_name, quantity, supplier, DATE_FORMAT(date, '%Y-%m-%d') as date FROM restock_table";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;

                    // Set column headers
                    dataGridView1.Columns["product_name"].HeaderText = "Product Name";
                    dataGridView1.Columns["quantity"].HeaderText = "Quantity";
                    dataGridView1.Columns["supplier"].HeaderText = "Supplier";
                    dataGridView1.Columns["date"].HeaderText = "Date";

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

        private void Form5_Load_1(object sender, EventArgs e)
        {
            LoadRestockData();
            using (MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;password=;database=grocery_inventory;"))
            {
                conn.Open();
                string query = "SELECT product_name FROM product_table";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                product_comboBox.Items.Clear();

                while (reader.Read())
                {
                    product_comboBox.Items.Add(reader["product_name"].ToString());
                }

                reader.Close();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (product_comboBox.SelectedItem != null)
            {
                using (MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;password=;database=grocery_inventory;"))
                {
                    conn.Open();
                    string query = "SELECT Stock FROM product_table WHERE product_name = @product";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@product", product_comboBox.SelectedItem.ToString());

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
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (product_comboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a product to update.");
                return;
            }

            string selectedProduct = product_comboBox.SelectedItem.ToString();

            using (MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;password=;database=grocery_inventory;"))
            {
                conn.Open();
                string query = "UPDATE restock_table SET quantity = @quantity, supplier = @supplier, date = @date WHERE product_name = @product";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@quantity", quantity.Text);
                cmd.Parameters.AddWithValue("@supplier", supplier.Text);
                cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@product", selectedProduct);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Updated!");
                    LoadRestockData();
                    quantity.Clear();
                
                }
                else
                {
                    MessageBox.Show("No record found for this product.");
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (product_comboBox.SelectedItem != null)
            {
                DialogResult confirm = MessageBox.Show("Delete this record?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    using (MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;password=;database=grocery_inventory;"))
                    {
                        conn.Open();
                        string query = "DELETE FROM restock_table WHERE product_name = '" + product_comboBox.SelectedItem.ToString() + "'";
                        new MySqlCommand(query, conn).ExecuteNonQuery();
                        MessageBox.Show("Deleted!");
                        LoadRestockData();
                        quantity.Clear();

                    }
                }
            }
        }
    }
}       
    

