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
    public partial class UCimport : UserControl
    {
        MySqlConnection connection;
        MySqlCommand cmd;
        MySqlDataReader dr;
        classDB db = new classDB();
        public UCimport()
        {
            InitializeComponent();

            connection = new MySqlConnection();
            connection.ConnectionString = db.GetConnection();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt";
            openFileDialog.Title = "Import Text File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                    {
                        gridView.Rows.Clear(); // Clear existing rows

                        string line;
                        int lineNumber = 0; // Track line number for error message
                        while ((line = reader.ReadLine()) != null)
                        {
                            lineNumber++;
                            string[] values = line.Split('\t');
                            if (values.Length == 6) // Ensure there are exactly 6 values in a line
                            {
                                gridView.Rows.Add(values);
                            }
                            else
                            {
                                MessageBox.Show($"Line {lineNumber} does not have 6 columns.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return; // Exit the import process
                            }
                        }
                    }

                    MessageBox.Show("Data imported successfully!", "Import Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(filePath);
                Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = excelWorkbook.Sheets[1]; // Assuming data is in the first sheet

                int numRows = excelWorksheet.UsedRange.Rows.Count;
                int numCols = 7; // Assuming you want 6 columns

                if (excelWorksheet.UsedRange.Columns.Count != numCols)
                {
                    MessageBox.Show("Imported data does not have exactly 7 columns.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    excelWorkbook.Close();
                    excelApp.Quit();
                    return;
                }

                gridView.Rows.Clear(); // Clear existing rows

                for (int i = 2; i <= numRows; i++) // Start from row 2 to skip headers
                {
                    var row = new DataGridViewRow();

                    for (int j = 1; j <= numCols; j++)
                    {
                        row.Cells.Add(new DataGridViewTextBoxCell
                        {
                            Value = excelWorksheet.Cells[i, j].Value
                        });
                    }

                    gridView.Rows.Add(row);
                }

                excelWorkbook.Close();
                excelApp.Quit();

                MessageBox.Show("Data imported successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private bool CheckIfDataExists(string col1Value, string col2Value)
        {
            string query = "SELECT COUNT(*) FROM tbl_products WHERE description = @col1 AND brand = @col2";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@col1", col1Value);
                cmd.Parameters.AddWithValue("@col2", col2Value);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();

                foreach (DataGridViewRow row in gridView.Rows)
                {
                    string colValue = row.Cells["pcode"].Value.ToString();
                    string col1Value = row.Cells["description"].Value.ToString();
                    string col2Value = row.Cells["brand"].Value.ToString();
                    string col3Value = row.Cells["category"].Value.ToString();
                    string col4Value = row.Cells["price"].Value.ToString();
                    string col5Value = row.Cells["qty"].Value.ToString();
                    DateTime col6Value = Convert.ToDateTime(row.Cells["datetime"].Value);

                    if (!CheckIfDataExists(col1Value, col2Value))
                    {
                        string insertQuery = "INSERT INTO tbl_products (id, description, brand, category, price, qty, date_time) VALUES (@col, @col1, @col2, @col3, @col4, @col5, @col6);";
                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                        {
                            insertCmd.Parameters.AddWithValue("@col", colValue);
                            insertCmd.Parameters.AddWithValue("@col1", col1Value);
                            insertCmd.Parameters.AddWithValue("@col2", col2Value);
                            insertCmd.Parameters.AddWithValue("@col3", col3Value);
                            insertCmd.Parameters.AddWithValue("@col4", col4Value);
                            insertCmd.Parameters.AddWithValue("@col5", col5Value);
                            insertCmd.Parameters.AddWithValue("@col6", col6Value);

                            insertCmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Data inserted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("The data is already in the database: ");
                        break; // Exit the loop if duplicate data is found
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("MySQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
    }

