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
    public partial class UCpos : UserControl
    {
        public UCpos()
        {
            InitializeComponent();
        }

        #region Properties

        private string _title;
        private string _price;
        private Image _icon;

        [Category("Custom Props")]
            public string Title
        {
            get { return _title; }
            set { _title = value; lbldes.Text = value; }
        }

        [Category("Custom Props")]
        public string Price
        {
            get { return _price; }
            set { _price = value; lblprice.Text = value; }
        }


        [Category("Custom Props")]
        public Image Icon
        {
            get { return _icon; }
            set { _icon = value; picturebox1.Image = value; }
        }
        #endregion

        private void UCpos_MouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(217, 229, 242); //change user control back color when mouse enters.
        }

        private void UCpos_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(255, 255, 255); //reset user control back color when mouse leaves.
        }

        private void UCpos_Load(object sender, EventArgs e)
        {

        }
    }
}

