using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace projectTraining.forms
{
    public partial class UCdashboard : UserControl
    {
        MySqlConnection connection;
        MySqlCommand cmd;
        MySqlDataReader dr;
        classDB db = new classDB();
        public UCdashboard()
        {
            InitializeComponent();
            connection = new MySqlConnection();
            connection.ConnectionString = db.GetConnection();
        }
        public void SearchData()
        {
            gridView.Rows.Clear();

            connection.Open();
            cmd = new MySqlCommand("select * from tbl_products where description like '%" + txtSearch.Text + "%'", connection);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {


                gridView.Rows.Add(dr["id"].ToString(), dr["description"].ToString(), dr["brand"].ToString(), dr["category"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["date_time"].ToString());

            }
            dr.Close();
            connection.Close();
        }

        void FilterData()
        {
            gridView.Rows.Clear();

            connection.Open();
            cmd = new MySqlCommand("select * from tbl_products where category like '" + comboBox1.Text + "' and description like '%" + txtSearch.Text + "%'", connection);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {


                gridView.Rows.Add(dr["id"].ToString(), dr["description"].ToString(), dr["brand"].ToString(), dr["category"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["date_time"].ToString());

            }
            dr.Close();
            connection.Close();
        }
        void LoadData()
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

        private void UCdashboard_Load(object sender, EventArgs e)
        {
            Category();
              LoadData();
        }

        void Category()
        {

            connection.Open();
            cmd = new MySqlCommand("select category from new_table", connection);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                string category = dr.GetString("category");
                comboBox1.Items.Add(category);
               
            }
            dr.Close();
            connection.Close();


        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string filename;
                using (SaveFileDialog svf = new SaveFileDialog())
                {
                    svf.Filter = "AT Load File (.aload) | (*.aload)";
                    svf.AddExtension = true;
                    svf.DefaultExt = ".aload";
                    svf.ShowDialog();

                    filename = svf.FileName;
                }

                    if (gridView.DataSource != null)

                    {

                        int colCount = gridView.Columns.Count;
                        int rowCount = gridView.Rows.Count;

                        TextWriter tw = new StreamWriter(filename);

                        for (int i = 0; i < rowCount - 1; i ++)
                        {
                            for (int j = 0; j < colCount; j     ++)
                            {
                                tw.Write(" " + gridView.Rows[i].Cells[j].Value.ToString() + " " + "|");
                            }
                            tw.WriteLine("");
                            tw.WriteLine("-----------------------------------------------");
                            
                        }
                        tw.Close();
                    MessageBox.Show("Done!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }   


        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                SearchData();
            }
            else
            {
                FilterData();
            }

     
       
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("description", typeof(string));
            dt.Columns.Add("brand", typeof(string));
            dt.Columns.Add("category", typeof(string));
            dt.Columns.Add("price", typeof(string));
            dt.Columns.Add("qty", typeof(string));

            foreach (DataGridViewRow dgv in gridView.Rows)
            {
                dt.Rows.Add(dgv.Cells[0].Value, dgv.Cells[1].Value, dgv.Cells[2].Value, dgv.Cells[3].Value, dgv.Cells[4].Value, dgv.Cells[5].Value);

            }
            ds.Tables.Add(dt);
            ds.WriteXmlSchema("users.xml");

            forms.frmReports form2 = new forms.frmReports();
            forms.CrystalReport1 cr = new forms.CrystalReport1();
            cr.SetDataSource(ds);
            form2.crystalReportViewer1.ReportSource = cr;
            form2.crystalReportViewer1.Refresh();
            form2.Show();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApp.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkbook.Sheets[1];

                for (int i = 1; i <= gridView.Columns.Count; i++)
                {
                    excelWorksheet.Cells[1, i] = gridView.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < gridView.Rows.Count; i++)
                {
                    for (int j = 0; j < gridView.Columns.Count; j++)
                    {
                        if (gridView.Rows[i].Cells[j].Value != null)
                        {
                            if (j == 6 && gridView.Rows[i].Cells[j].Value is DateTime dateTimeValue)
                            {
                                excelWorksheet.Cells[i + 2, j + 1] = dateTimeValue.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            else
                            {
                                excelWorksheet.Cells[i + 2, j + 1] = gridView.Rows[i].Cells[j].Value.ToString();
                            }
                        }
                    }
                }

                excelWorkbook.SaveAs(filePath);
                excelWorkbook.Close();
                excelApp.Quit();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (gridView.Rows.Count > 0)
            {
                try
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
                    saveFileDialog.Title = "Export to Text File";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                        {
                            foreach (DataGridViewRow row in gridView.Rows)
                            {
                                for (int i = 0; i < gridView.Columns.Count; i++)
                                {
                                    writer.Write(row.Cells[i].Value.ToString());
                                    if (i < gridView.Columns.Count - 1)
                                    {
                                        writer.Write("\t"); // Tab-separated values
                                    }
                                }
                                writer.WriteLine(); // Move to the next line
                            }
                        }
                        MessageBox.Show("Data exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No data to export!", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
      
        }
    }
    }


