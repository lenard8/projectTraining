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
    public partial class UCPortioning : UserControl
    {
        MySqlConnection connection;
        MySqlCommand cmd;
        MySqlDataReader dr;
        classDB db = new classDB();
        public UCPortioning()
        {
            InitializeComponent();
            connection = new MySqlConnection();
            connection.ConnectionString = db.GetConnection();
        }

        public void LoadData()
        {
            gridView.Rows.Clear();

            connection.Open();
            cmd = new MySqlCommand("select * from tbl_inventory", connection);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {


                gridView.Rows.Add(dr["product_code"].ToString(), dr["product_name"].ToString(), dr["unit_of_measure"].ToString(), dr["qty"].ToString(), dr["price"].ToString());

            }
            dr.Close();
            connection.Close();
        }

        private void UCPortioning_Load(object sender, EventArgs e)
        {
            LoadData();
            selectId();

        }

        private void gridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            frmPortioning pors = new frmPortioning();

            int rowIndex = e.RowIndex;
            pors.txtcode.Text = gridView[0, rowIndex].Value.ToString();
            pors.txtproduct.Text = gridView[1, rowIndex].Value.ToString();
            pors.txtuom.Text = gridView[2, rowIndex].Value.ToString();
            pors.txtboxprice.Text = gridView[4, rowIndex].Value.ToString();
            pors.lbltransno.Text = lbltransno.Text;


            if (gridView.Columns[e.ColumnIndex].Name == "portion")

            {
                pors.ShowDialog();

            }
        }
        void selectId()
        {
            int _transno = 0; // Declare _transno as an integer
            connection.Open();

            using (MySqlCommand cmd = new MySqlCommand("SELECT transaction_no FROM tbl_inventory_out ORDER BY transaction_no DESC LIMIT 1", connection))
            {
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())  // Check if there are rows before reading
                    {
                        int.TryParse(dr["transaction_no"].ToString(), out _transno); // Convert to integer

                        _transno++; // Increment the value

                        lbltransno.Text = _transno.ToString(); // Convert back to string and assign to lbltransno.Text

                        //this.Hide();
                    }
                }
            }
            connection.Close();
        }
        private void populateItems()
        {
            UCCart[] cart = new UCCart[20];
            for (int i = 0; i < cart.Length; i++)
            {
                cart[i] = new UCCart();
                // cart[i].Title = ""
            }
        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            selectId();
        }

        private void gridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string _pcode, _product, _uom, _price, _qty, _ttotal = "";
            if (e.RowIndex >= 0) // Check if a valid row is clicked
            {
                DataGridViewRow selectedRow = gridView.Rows[e.RowIndex];

                // Assuming you want to display the data from the first column of the selected row
                string selectedCellValue = selectedRow.Cells[0].Value.ToString();
                string selectedCellValue2 = selectedRow.Cells[1].Value.ToString();
                string selectedCellValue3 = selectedRow.Cells[2].Value.ToString();
                string selectedCellValue4 = selectedRow.Cells[4].Value.ToString();

                _pcode = selectedCellValue;
                _product = selectedCellValue2;
                _uom = selectedCellValue3;
                _price = selectedCellValue4;
                _qty = "1";

                // Convert _qty and _price to numeric types
                int quantity = int.Parse(_qty);
                decimal price = decimal.Parse(_price);

                bool productFound = false;

                // Check if the product already exists in the grid
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value.ToString() == _pcode)
                    {
                        // Update the quantity
                        int existingQuantity = int.Parse(row.Cells[3].Value.ToString());
                        row.Cells[3].Value = (existingQuantity + quantity).ToString();

                        // Update the total
                        decimal existingTotal = decimal.Parse(row.Cells[5].Value.ToString());
                        decimal newTotal = existingTotal + (quantity * price);
                        row.Cells[5].Value = newTotal.ToString();

                        productFound = true;
                        break;
                    }
                }

                if (!productFound)
                {
                    // Calculate the total
                    decimal total = quantity * price;
                    _ttotal = total.ToString(); // Convert the total back to a string

                    dataGridView1.Rows.Add(_pcode, _product, _uom, _qty, _price, _ttotal);
                }
            }

        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            payment();
        }

        void paymentQ()

        {



        }
        void payment()
        {
            connection.Open();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string colValue = row.Cells["ccode"].Value.ToString();
                string col1Value = row.Cells["cname"].Value.ToString();
                string col2Value = row.Cells["cuom"].Value.ToString();
                string col3Value = row.Cells["cqty"].Value.ToString();
                string col4Value = row.Cells["cprice"].Value.ToString();
                DateTime col9Value = DateTime.Now;
                string formattedDate = col9Value.ToString("yyyy-MM-dd HH:mm:ss");

                string insertQuery = "INSERT INTO tbl_inventory_out (transaction_no, product_code, product_name, unit_of_measure, product_out, price, date_out) VALUES (@col, @col1, @col2, @col3, @col4, @col5, @col6);";
                using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                {
                    insertCmd.Parameters.AddWithValue("@col1", colValue);
                    insertCmd.Parameters.AddWithValue("@col2", col1Value);
                    insertCmd.Parameters.AddWithValue("@col3", col2Value);
                    insertCmd.Parameters.AddWithValue("@col4", col3Value);
                    insertCmd.Parameters.AddWithValue("@col5", col4Value);
                    insertCmd.Parameters.AddWithValue("@col6", formattedDate);
                    insertCmd.Parameters.AddWithValue("@col", lbltransno.Text);

                    insertCmd.ExecuteNonQuery();
                }

                // Decrease the quantity in tbl_inventory
                string updateQuery = "UPDATE tbl_inventory SET qty = qty - @quantity WHERE product_code = @productCode AND unit_of_measure = @Uom;";
                using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                {
                    updateCmd.Parameters.AddWithValue("@quantity", col3Value); // The quantity from the DataGridView
                    updateCmd.Parameters.AddWithValue("@productCode", colValue);
                    updateCmd.Parameters.AddWithValue("@Uom", col2Value);
                    updateCmd.ExecuteNonQuery();
                }
            }

            connection.Close();
            MessageBox.Show("Thank you!.");
            LoadData();
            selectId();

            // Optionally, clear the DataGridView if needed.
            dataGridView1.Rows.Clear();
        }


        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
            dataGridView1.Rows.Clear();
        }
    }
    }

