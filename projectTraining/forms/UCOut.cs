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
    public partial class UCOut : UserControl
    {
        MySqlConnection connection;
        MySqlCommand cmd;
        MySqlDataReader dr;
        classDB db = new classDB();
        public UCOut()
        {
            InitializeComponent();


            connection = new MySqlConnection();
            connection.ConnectionString = db.GetConnection();
        }

        private void UCOut_Load(object sender, EventArgs e)
        {

            LoadData();
        }

        public void LoadData()
        {
            gridView.Rows.Clear();

            connection.Open();
            cmd = new MySqlCommand("select * from tbl_inventory_out", connection);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {


                gridView.Rows.Add(dr["transaction_no"].ToString(), dr["product_code"].ToString(), dr["product_name"].ToString(), dr["unit_of_measure"].ToString(), dr["product_out"].ToString(), dr["price"].ToString(), dr["date_out"].ToString());

            }
            dr.Close();
            connection.Close();
        }
    }
}
