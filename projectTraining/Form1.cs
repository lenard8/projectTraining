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
using MySql.Data;
namespace projectTraining
{
    public partial class Form1 : Form
    {
        MySqlConnection connection;
        MySqlCommand cmd;
        MySqlDataReader dr;
        classDB db = new classDB();
        public Form1()
        {
            InitializeComponent();
            connection = new MySqlConnection();
            connection.ConnectionString = db.GetConnection();
        }
        void Login()
        {
           
            string _username, _password, _firstname, _lastname = "";


            connection.Open();
            cmd = new MySqlCommand("select * from tbl_users where user_name = @user_name AND password = @password", connection);
            cmd.Parameters.AddWithValue("@user_name", txtusername.Text);
            cmd.Parameters.AddWithValue("@password", txtpassword.Text);
            dr = cmd.ExecuteReader();
            dr.Read();


            if (dr.HasRows)
            {
                

                _username = dr["user_name"].ToString();
                _password = dr["password"].ToString();
                _firstname = dr["first_name"].ToString();
                _lastname = dr["last_name"].ToString();

               
               // lblname.Text = _firstname + " " + _lastname;

                MessageBox.Show("Welcome " + _firstname + " " + _lastname); ;
              
                forms.frmPOS posForm = new forms.frmPOS();
                posForm.SetUserLabel(dr["first_name"].ToString());  // Set the user label in frmPOS
               // posForm.ShowDialog();

                forms.frmDashboard form = new forms.frmDashboard();
                form.ShowDialog();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password");
                txtpassword.Clear();
                txtusername.Clear();
                txtusername.Focus();
            }
            connection.Close();


        }
  


        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Close();

        }
    }
}
