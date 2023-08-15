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
    public partial class UCinventory : UserControl
    {
        MySqlConnection connection;
        MySqlCommand cmd;
        MySqlDataReader dr;
        classDB db = new classDB();

        public UCinventory()
        {
            InitializeComponent();

            connection = new MySqlConnection();
            connection.ConnectionString = db.GetConnection();
        }
        public void LoadData()
        {
            gridView.Rows.Clear();

            connection.Open();
            cmd = new MySqlCommand("select * from tbl_products", connection);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {


                gridView.Rows.Add(dr["id"].ToString(), dr["description"].ToString(), dr["brand"].ToString(), dr["category"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["date_time"].ToString());

            }
            dr.Close();
                      connection.Close();
        }

        public void AddData()
        {
            if (txtbrand.Text == "" || txtcategory.Text == "" || txtdes.Text == "" || txtprice.Text == "" || txtqty.Text == "")
            {
                MessageBox.Show("Please Complete the Details.");
            }
            else
            {
                connection.Open();
                cmd = new MySqlCommand("INSERT INTO tbl_products (id, description, brand, category, price, qty, date_time, image) VALUES (@Id, @Description, @Brand, @Category, @Price, @Qty, @DateTime, @Image)", connection);
                cmd.Parameters.AddWithValue("@Id", txtpcode.Text);
                cmd.Parameters.AddWithValue("@Description", txtdes.Text);
                cmd.Parameters.AddWithValue("@Brand", txtbrand.Text);
                cmd.Parameters.AddWithValue("@Category", txtcategory.Text);
                cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(txtprice.Text)); // Convert to appropriate data type
                cmd.Parameters.AddWithValue("@Qty", Convert.ToInt32(txtqty.Text)); // Convert to appropriate data type
                cmd.Parameters.AddWithValue("@DateTime", DateTime.Now);
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                cmd.Parameters.AddWithValue("@Image", ms.ToArray());

                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Product Added!");
                LoadData();
             
            }

        }

        public void UpdateData()
        {
            connection.Open();
            cmd = new MySqlCommand("UPDATE tbl_products set description = '" + txtdes.Text + "', brand = '" + txtbrand.Text + "', category = '" + txtcategory.Text + "', price = '" + txtprice.Text + "', qty = '" + txtqty.Text + "' WHERE id like '" + txtpcode.Text + "'", connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Product Updated!");
            LoadData();

        }

        public void DeleteData()
        {
            connection.Open();
            cmd = new MySqlCommand("delete from tbl_products WHERE id like '" + txtpcode.Text + "'", connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Product Deleted!");
            LoadData();

        }
        public void Clear()
        {

            txtbrand.Clear();
            txtcategory.Clear();
            txtdes.Clear();
            txtpcode.Text = "102382";
            txtprice.Clear();
            txtqty.Clear();
            txtbrand.Focus();

        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void UCinventory_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void gridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex; // Use e.RowIndex instead of gridView.CurrentRow.Index

            // Retrieve values from cells
            txtpcode.Text = gridView[0, rowIndex].Value.ToString();
            txtdes.Text = gridView[1, rowIndex].Value.ToString();
            txtbrand.Text = gridView[2, rowIndex].Value.ToString();
            txtcategory.Text = gridView[3, rowIndex].Value.ToString();
            txtprice.Text = gridView[4, rowIndex].Value.ToString();
            txtqty.Text = gridView[5, rowIndex].Value.ToString();

            // Retrieve image data from cell
            byte[] imageData = (byte[])gridView[6, rowIndex].Value;

            // Convert byte array to Image
            if (imageData != null && imageData.Length > 0)
            {
                using (MemoryStream memoryStream = new MemoryStream(imageData))
                {
                    Image image = Image.FromStream(memoryStream);
                    pictureBox1.Image = image; // Assuming "pictureBox" is the name of your PictureBox control
                }
            }
            else
            {
                // If no image data, you might want to clear the PictureBox
                pictureBox1.Image = null;
            }

        }
 

        private void btnLogin_Click(object sender, EventArgs e)
        {
            AddData();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Clear();

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            UpdateData();
            Clear();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DeleteData();
            Clear();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog opendlg = new OpenFileDialog();
            if (opendlg.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(opendlg.FileName);
                pictureBox1.Image = image;
            }
        }
    }
}
