using Pijaloci.PijalociClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pijaloci
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        PijalociClass c = new PijalociClass();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //GET the value from input fields
            c.Price = textBox2.Text;
            c.DrinkName = textBox3.Text;
            c.TableNo = textBox4.Text;

            //inserting data into Database
            bool success = c.Insert(c);
            if(success==true)
            {
                //successfully inserted
                MessageBox.Show("Noviot pijalok uspesno vnesen");
                //Povikaj Clear Methoda
                Clear();

            }
            else
            {
                //Failed to add Pijalok
                MessageBox.Show("Neuspesno vnesuvanje na pijalok");
            }

            //Load Data on Data GridView
            DataTable dt = c.Select();
            dataGridView1.DataSource = dt;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Load Data on Data GridView
            DataTable dt = c.Select();
            dataGridView1.DataSource = dt;
        }

        //methoda za briseknje na podatocite posle vnesuvanjeto od textbox
        public void Clear()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //get the data from textboxes
            c.DrinkID = int.Parse(textBox1.Text);
            c.Price = textBox2.Text;
            c.DrinkName = textBox3.Text;
            c.TableNo = textBox4.Text;

            //Update data in database
            bool success = c.Update(c);
            if(success==true)
            {
                //Update uspesno
                MessageBox.Show("Na Pijalokot e upesno napraveno update");
                //Load Data on Data GridView
                DataTable dt = c.Select();
                dataGridView1.DataSource = dt;

                //Call Clear Method
                Clear();


            }
            else
            {
                //Update neuspeno
                MessageBox.Show("Update na pijalokot e neuspesno");
         
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get the data from data grid view and load it to the textboxes respectively
            //identify the row on whitch mouse is clicked
            int rowIndex = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[rowIndex].Cells[3].Value.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Call clear method here
            Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Get the DrinkID from the application
            c.DrinkID = Convert.ToInt32(textBox1.Text);
            bool success = c.Delete(c);
            if(success==true)
            {
                //Uspeno izbrisano
                MessageBox.Show("Pijalokot e uspesno izbrisan");
                //Refresh Data GridWiew
                //Load Data on Data GridView
                DataTable dt = c.Select();
                dataGridView1.DataSource = dt;

                //Call the Clear Method
                Clear();


            }
            else
            {
                //Neuspeno izbrisano
                MessageBox.Show("Pijalokot ne e izbrisan");
            }

        }
        static string mykonekcija = ConfigurationManager.ConnectionStrings["Konekcija"].ConnectionString;

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //get the value from textbox
            string keyword = textBox5.Text;

            SqlConnection conn = new SqlConnection(mykonekcija);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT *FROM tbl_pijaloci WHERE Price LIKE '%"+keyword+ "%' OR DrinkName LIKE '%" + keyword + "%' OR TableNo LIKE '%" + keyword + "%' ", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
