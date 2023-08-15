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
    public partial class UCIn : UserControl
    {
        MySqlConnection connection;
        MySqlCommand cmd;
        MySqlDataReader dr;
        classDB db = new classDB();
        public UCIn()
        {
            InitializeComponent();
            connection = new MySqlConnection();
            connection.ConnectionString = db.GetConnection();
        }
        public void LoadData()
        {
            gridView.Rows.Clear();

            connection.Open();
            cmd = new MySqlCommand("select * from tbl_inventory_in", connection);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {


                gridView.Rows.Add(dr["product_code"].ToString(), dr["product_name"].ToString(), dr["product_in"].ToString(), dr["unit_of_measure"].ToString(), dr["price"].ToString(), dr["date_in"].ToString());

            }
            dr.Close();
            connection.Close();
        }

        private void UCIn_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
