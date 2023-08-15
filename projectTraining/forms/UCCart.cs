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

        private string _pname;
        private string _um;
        private string _qty;
        private string _price;

        [Category("Custom Props")]
        public string Title
        {
            get { return _pname; }
            set { _pname = value; lblName.Text = value; }
        }

        [Category("Custom Props")]
        public string Price
        {
            get { return _um; }
            set { _um = value; lblOum.Text = value; }
        }

        [Category("Custom Props")]
        public string Qty
        {
            get { return _qty; }
            set { _qty = value; txtqty.Text = value; }
        }

        [Category("Custom Props")]
        public string Pri
        {
            get { return _price; }
            set { _price = value; lblprice.Text = value; }
        }
    }
}
