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
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
        }
        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }
        private void btnDashBoard_Click(object sender, EventArgs e)
        {
           forms.UCdashboard uc = new forms.UCdashboard();
            addUserControl(uc);
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            forms.UCinventory uc = new forms.UCinventory();
            addUserControl(uc);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            forms.UCimport uc = new forms.UCimport();
            addUserControl(uc);
        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            forms.frmPOS frm = new forms.frmPOS();
            frm.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            forms.UCPortioning uc = new forms.UCPortioning();
            addUserControl(uc);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            forms.UCIn uc = new forms.UCIn();
            addUserControl(uc);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            forms.UCOut uc = new forms.UCOut();
            addUserControl(uc);

        }
    }
}
