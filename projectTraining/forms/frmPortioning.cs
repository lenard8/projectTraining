using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace projectTraining.forms
{
    public partial class frmPortioning : Form
    {
        MySqlConnection connection;
        MySqlCommand cmd;
        MySqlDataReader dr;
        classDB db = new classDB();
        public frmPortioning()
        {
            InitializeComponent();
            connection = new MySqlConnection();
            connection.ConnectionString = db.GetConnection();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmPortioning_Load(object sender, EventArgs e)
        {
            txtqty.Focus();
        }
       

        void portion()
        {

            connection.Open();
            cmd = new MySqlCommand("UPDATE tbl_inventory_in set product_in = product_in + '" + txtprice.Text + "' WHERE product_code like '" + txtcode.Text + "' AND unit_of_measure like 'PC'", connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Product Updated!");
            portion1();
        }

        void portion1()
        {

            connection.Open();
            cmd = new MySqlCommand("UPDATE tbl_inventory_in SET product_in = -1 WHERE product_code LIKE '" + txtcode.Text + "' AND unit_of_measure LIKE 'Box'", connection);
            cmd.ExecuteNonQuery();
            connection.Close();
           // MessageBox.Show("Product Updated!");
        }

        void portionIn()
        {

            string _unit = "";

            if (txtuom.Text == "Box")
            {
                _unit = "PC";
            }
            else if (txtuom.Text == "Liter")

            {
                _unit = "ML";
            }
                
            connection.Close();
            connection.Open();
            cmd = new MySqlCommand("INSERT INTO tbl_inventory_in (product_code, product_name, unit_of_measure, product_in, price, date_in) VALUES (@Id, @Product, @ProductIn, @Unit, @Price, @DateIn)", connection);
            cmd.Parameters.AddWithValue("@Id", txtcode.Text);
            cmd.Parameters.AddWithValue("@Product", txtproduct.Text);
            cmd.Parameters.AddWithValue("@ProductIn", txtqty.Text);
            cmd.Parameters.AddWithValue("@unit", _unit);
            cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtprice.Text));
            cmd.Parameters.AddWithValue("@DateIn", DateTime.Now); // Adding the current date

            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("portioning success!");

        }
        void portionOut()
        {
            

            connection.Open();
            cmd = new MySqlCommand("INSERT INTO tbl_inventory_out (transaction_no, product_code, product_name, unit_of_measure, product_out, price, date_out) VALUES (@Transno, @Id, @Product, @UnitOfMeasure, '1', @Price, @DateOut)", connection);
            cmd.Parameters.AddWithValue("@Transno", lbltransno.Text);
            cmd.Parameters.AddWithValue("@Id", txtcode.Text);
            cmd.Parameters.AddWithValue("@Product", txtproduct.Text);
            cmd.Parameters.AddWithValue("@UnitOfMeasure", txtuom.Text);
            cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtboxprice.Text));
            cmd.Parameters.AddWithValue("@DateOut", DateTime.Now); 

            cmd.ExecuteNonQuery();
            connection.Close();
         //   portionUpdate();
            


        }
        void portionNew()
        {

            string _unit = "";

            if (txtuom.Text == "Box")
            {
                _unit = "PC";
            }
            else if (txtuom.Text == "Liter")

            {
                _unit = "ML";
            }
            connection.Close();
            connection.Open();
            cmd = new MySqlCommand("INSERT INTO tbl_inventory (product_code, product_name, unit_of_measure, qty, price) VALUES (@Id, @Product, @Unit , @Qty, @Price)", connection);
            cmd.Parameters.AddWithValue("@Id", txtcode.Text);
            cmd.Parameters.AddWithValue("@Product", txtproduct.Text);
            cmd.Parameters.AddWithValue("@Unit", _unit);
            cmd.Parameters.AddWithValue("@Qty", Convert.ToDecimal(txtqty.Text));
            cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtprice.Text));

           
            cmd.ExecuteNonQuery();
            connection.Close();
            portionIn();
            portionOut();
            portionUpdate();
        }
        void portionUpdateQ()
        {
            string _unit = "";

            if (txtuom.Text == "Box")
            {
                _unit = "PC";
            }
            else if(txtuom.Text == "Liter")

            {
                _unit = "ML";
            }

            using (connection)
            {
                connection.Open();

                using (cmd)
                {
                    cmd = new MySqlCommand("SELECT * FROM tbl_inventory WHERE product_code = @code AND unit_of_measure = '"+ _unit +"'", connection);
                    cmd.Parameters.AddWithValue("@code", txtcode.Text);
                    // Add the necessary parameters for the WHERE clause, only Pcode is needed for the SELECT query.

                    using (dr)
                    {
                        dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Close(); // Close the reader before executing another command.

                            cmd = new MySqlCommand("UPDATE tbl_inventory SET qty = qty + @Qty WHERE product_code = @Pcode AND unit_of_measure = 'PC'", connection);
                            cmd.Parameters.AddWithValue("@Qty", Convert.ToInt32(txtqty.Text));
                            cmd.Parameters.AddWithValue("@Pcode", txtcode.Text);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("portioning success!");
                            portionIn();
                            portionOut();
                            portionUpdate();
                            txtqty.Clear();
                            txtprice.Clear();
                            this.Hide();

                        }
                        else
                        {
                            dr.Close();
                            portionNew();
                        }
                    } }
      

                connection.Close(); // Close the connection after using it.
            }
        }


        public void portionUpdate()
        {

            connection.Open();
            cmd = new MySqlCommand("UPDATE tbl_inventory SET qty = qty -1 WHERE product_code LIKE '" + txtcode.Text + "' AND unit_of_measure LIKE 'Box'", connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            txtqty.Clear();
            txtprice.Clear();
            this.Hide();

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            UCPortioning uc = new UCPortioning();
            uc.LoadData();
            portionUpdateQ();
            
          //  portionIn();
        }
    }
}
