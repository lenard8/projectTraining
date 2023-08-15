using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace projectTraining.forms
{
    public partial class frmPOS : Form
    {

        MySqlConnection connection;
        MySqlCommand cmd;
        MySqlDataReader dr;
        classDB db = new classDB();
        public frmPOS()
        {
            InitializeComponent();
            connection = new MySqlConnection();
            connection.ConnectionString = db.GetConnection();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            forms.frmDashboard frm = new forms.frmDashboard();
            frm.Show();
            Hide();
        }
        public DataTable ReadItemsTable()
        {

            connection.Open();

            //query to select all records from table -> (Table_Items)
            string query = "SELECT * FROM tbl_products";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            try
            {
                using (MySqlDataAdapter sda = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); //save all records coming from database in data table. 
                    return dt; // return data table.
                }

            }
            catch
            {
                throw;
            }

        }

        public DataTable GetItems()
        {
            try
            {
                // ClassDAL objdal = new ClassDAL(); //data access layer class object to access functions
                return ReadItemsTable();
            }
            catch (Exception e)
            {
                DialogResult result = MessageBox.Show(e.Message.ToString());
                return null;
            }
        }
        private void GenerateDynamicUserControl()
        {
            flowLayoutPanel1.Controls.Clear();

            //  ClassBLL objbll = new ClassBLL(); //business login layer(BLL) class object to access function

            DataTable dt = GetItems(); //keeping items coming from database in data table object.

            if (dt != null) //check if data table is not null
            {
                if (dt.Rows.Count > 0) // also has some records in it
                {

                    /*
                     * dt.Rows.Count = no of rows in a data table
                     * array size will be dynamic as per the no of rows being stored in data table.
                     */
                    UCpos[] listItems = new UCpos[dt.Rows.Count];

                    for (int i = 0; i < 1; i++) //this loop runs once only
                    {
                        //this loop runs untill the records ends in data table.
                        foreach (DataRow row in dt.Rows) //read rows one by one from data table.
                        {
                            listItems[i] = new UCpos();

                            //reconverting binary formatted image coming from database to normal image. 
                            MemoryStream ms = new MemoryStream((byte[])row["Image"]);
                            listItems[i].Icon = new Bitmap(ms);

                            //get title & subtitle from current row in a data table and set to user control.
                            listItems[i].Title = row["description"].ToString();
                            listItems[i].Price = row["price"].ToString();


                            flowLayoutPanel1.Controls.Add(listItems[i]);

                            listItems[i].Click += new System.EventHandler(this.UserControl_Click);
                        }
                    }
                }
            }
        }

        private void frmPOS_Load(object sender, EventArgs e)
        {
            GenerateDynamicUserControl();
           

        }

        private void lblLogout_Click(object sender, EventArgs e)
        {
            forms.frmDashboard frm = new forms.frmDashboard();
            frm.Show();
            Hide();
        }

        public void UserControl_Click(object sender, EventArgs e)
        {
            forms.frmDashboard frm = new forms.frmDashboard();

            UCpos obj = (UCpos)sender; // user control object to access controls used in it like( icon, title and sub title)

            double total = 0;


            bool found = false;
            foreach (DataGridViewRow row in gridView.Rows)
            {
                if (row.Cells["ORDER"].Value.ToString() == obj.Title)
                {
                    int quantity = Convert.ToInt32(row.Cells["QTY"].Value) + 1;
                    row.Cells["QTY"].Value = quantity;

                    double price = Convert.ToDouble(row.Cells["PRICE"].Value);
                    total = quantity * price;
                    row.Cells["TOTAL"].Value = total;

                    found = true;
                    break;
                }
            }

            if (!found)
            {
                gridView.Rows.Add(obj.Title, obj.Price, 1, obj.Price);
            }
            lbltotal.Text = "0";
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                lbltotal.Text = Convert.ToString(double.Parse(lbltotal.Text) + double.Parse(gridView.Rows[i].Cells[2].Value.ToString()));
            }

            lbltotalprice.Text = "0";
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                lbltotalprice.Text = Convert.ToString(double.Parse(lbltotalprice.Text) + double.Parse(gridView.Rows[i].Cells[3].Value.ToString()));
            }

        }

        private void gridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        double total1;

        private void gridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex >= 0)
            {
                DataGridViewCell quantityCell = gridView.Rows[e.RowIndex].Cells["qty"];
                DataGridViewCell priceCell = gridView.Rows[e.RowIndex].Cells["price"];
                DataGridViewCell totalPriceCell = gridView.Rows[e.RowIndex].Cells["total"];

                if (quantityCell.Value != null && priceCell.Value != null)
                {
                    int currentQuantity = Convert.ToInt32(quantityCell.Value);
                    decimal price = Convert.ToDecimal(priceCell.Value);

                    if (gridView.Columns[e.ColumnIndex].Name == "plus")
                    {
                        // Increment the quantity by 1
                        int newQuantity = currentQuantity + 1;
                        quantityCell.Value = newQuantity;
                    }
                    else if (gridView.Columns[e.ColumnIndex].Name == "minus")
                    {
                        // Ensure quantity doesn't go below 0
                        int newQuantity = Math.Max(currentQuantity - 1, 0);
                        quantityCell.Value = newQuantity;
                    }

                    // Update the total price by multiplying the new quantity with the price
                    decimal totalPrice = Convert.ToInt32(quantityCell.Value) * price;
                    totalPriceCell.Value = totalPrice;

                    lbltotal.Text = "0";
                    for (int i = 0; i < gridView.Rows.Count; i++)
                    {
                        lbltotal.Text = Convert.ToString(double.Parse(lbltotal.Text) + double.Parse(gridView.Rows[i].Cells[2].Value.ToString()));
                    }

                    lbltotalprice.Text = "0";
                    for (int i = 0; i < gridView.Rows.Count; i++)
                    {
                        lbltotalprice.Text = Convert.ToString(double.Parse(lbltotalprice.Text) + double.Parse(gridView.Rows[i].Cells[3].Value.ToString()));
                    }
                }
            }

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            frmPayment pay = new frmPayment();
            pay.txtTotal.Text = lbltotalprice.Text;
            pay.ShowDialog();


        }
        public void SetUserLabel(string userName)
        {
            lblUser.Text = userName;
        }
    }   
}
        


    
  

