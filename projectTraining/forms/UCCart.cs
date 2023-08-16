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
    public partial class UCCart : UserControl
    {
        public UCCart()
        {
            InitializeComponent();
        }

        public string CName
        {
            get => lblName.Text;
            set => lblName.Text = value;        
        }

        public string CPrice
        {
            get => lblprice.Text;
            set => lblprice.Text = value;
        }
        public string CUom
        {
            get => lblOum.Text;
            set => lblOum.Text = value;
        }
        public string _ctotal;

        

        private void UCCart_Load(object sender, EventArgs e)
        {

        }

        private void txtqty_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(lblprice.Text, out float price) &&
          int.TryParse(txtqty.Text, out int qty))
            {
                float total = price * qty;
                lbltotal.Text = total.ToString();
            }
            else
            {
                lbltotal.Text = "Invalid Input";
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (float.TryParse(txtqty.Text, out float qtyValue))
            {
                qtyValue += 1; // Increment the quantity value

                if (float.TryParse(lblprice.Text, out float priceValue))
                {
                    float total = priceValue * qtyValue;
                    lbltotal.Text = total.ToString();
                }
                else
                {
                    lbltotal.Text = "Invalid Price";
                }

                txtqty.Text = qtyValue.ToString(); // Update the quantity textbox
            }
            else
            {
                lbltotal.Text = "Invalid Quantity";
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (float.TryParse(txtqty.Text, out float qtyValue))
            {
                qtyValue += -1; // Increment the quantity value

                if (float.TryParse(lblprice.Text, out float priceValue))
                {
                    float total = priceValue * qtyValue;
                    lbltotal.Text = total.ToString();
                }
                else
                {
                    lbltotal.Text = "Invalid Price";
                }

                txtqty.Text = qtyValue.ToString(); // Update the quantity textbox
            }
            else
            {
                lbltotal.Text = "Invalid Quantity";
            }
        }
    }
}
