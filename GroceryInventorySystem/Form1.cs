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
    public partial class Form1 : Form
    {
        public static string Username;
        public static string Password;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Username = textBox1.Text;
            Password = textBox2.Text;

            if (Username == "admin" && Password == "12345")
            {

                this.Hide();

                Form2 f2 = new Form2();
                f2.ShowDialog();
            }
            else 
            {
                MessageBox.Show("error, please try again");
            
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string err; if (GroceryInventorySystem.Database.TestConnection(out err));
        }

       
    }

}
