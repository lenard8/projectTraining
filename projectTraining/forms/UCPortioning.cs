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
            if (e.RowIndex >= 0 && e.RowIndex < gridView.Rows.Count)
            {
                DataGridViewRow selectedRow = gridView.Rows[e.RowIndex];

                // Null checks for cell values
                string selectedCellValue1 = selectedRow.Cells[1].Value?.ToString();
                string selectedCellValue2 = selectedRow.Cells[2].Value?.ToString();
                string selectedCellValue3 = selectedRow.Cells[4].Value?.ToString();

                if (!string.IsNullOrEmpty(selectedCellValue1) &&
                    !string.IsNullOrEmpty(selectedCellValue2) &&
                    !string.IsNullOrEmpty(selectedCellValue3))
                {
                    // Check if the data is already added to the flowLayoutPanel1
                    bool isDataAlreadyAdded = IsDataAlreadyAdded(selectedCellValue1, selectedCellValue2, selectedCellValue3);

                    if (!isDataAlreadyAdded)
                    {
                        UCCart cart = new UCCart();
                        cart.lblName.Text = selectedCellValue1;
                        cart.lblOum.Text = selectedCellValue2;
                        cart.lblprice.Text = selectedCellValue3;

                        // Perform data conversion and multiplication
                        if (float.TryParse(selectedCellValue3, out float price) &&
                            int.TryParse(cart.txtqty.Text, out int qty))
                        {
                            float total = price * qty;
                            cart.lbltotal.Text = total.ToString();
                        }

                        cart.Dock = DockStyle.Top;
                        flowLayoutPanel1.Controls.Add(cart);
                    }
                }
            }
        }

        private bool IsDataAlreadyAdded(string name, string oum, string price)
        {
            foreach (Control control in flowLayoutPanel1.Controls)
            {
                if (control is UCCart cartControl)
                {
                    if (cartControl.lblName.Text == name &&
                        cartControl.lblOum.Text == oum &&
                        cartControl.lblprice.Text == price)
                    {
                        MessageBox.Show("Data is already added");
                        return true; // Data is already added
                    }
                }
            }
            return false; // Data is not already added
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

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            addNew();
        }
        public projectTraining.forms.UCCart addNew()
        {
            projectTraining.forms.UCCart cart = new projectTraining.forms.UCCart();
            cart.Dock = DockStyle.Top;
            flowLayoutPanel1.Controls.Add(cart);

            return cart;
        }
        private void LoadCart()
        {
            UCCart cart = new UCCart();

            cart.Name = lblcname.Text;
            addNew();
        }
    }
    }

