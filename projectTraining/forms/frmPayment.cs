using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projectTraining.forms
{
    public partial class frmPayment : Form
    {
        public frmPayment()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtCash.Text, out decimal cash) && decimal.TryParse(txtTotal.Text, out decimal total))
            {
                if (total > cash)
                {
                    txtChange.Text = "";
                }
                else
                {
                    decimal change = cash - total;
                    txtChange.Text = change.ToString();
                }
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (txtChange.Text == "")
            {
                MessageBox.Show("Invalid Payment");
            }
            else
            {

                MessageBox.Show("Thankyou!");

            }
        }
    }
}
