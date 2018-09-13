using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Ado.Net_Async_Await
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection = null;
        DataTable dataTable = null;
        SqlCommand sqlCommand = null;
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
        string select = "waitfor delay '00:00:10';";
        public Form1()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(connectionString);
        }

         private async void button1_Click(object sender, EventArgs e)
        {
            
            button1.Enabled = false;
            try
            {
                await sqlConnection.OpenAsync();
                if (textBox1.Text != String.Empty && textBox1.Text == "select * from Books")
                {
                    await FillAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConnection?.Close();
            }
        }
        private async Task FillAsync()
        {
            select += textBox1.Text;
            sqlCommand = new SqlCommand(select, sqlConnection);
            dataTable = new DataTable();
            SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
            int line = 0;
            do
            {
                while (await reader.ReadAsync())
                {
                    if (line == 0)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dataTable.Columns.Add(reader.GetName(i));
                        }
                        line++;
                        DataRow row = dataTable.NewRow();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[i] = reader[i];
                        }
                        dataTable.Rows.Add(row);
                    }

                }
            } while (await reader.NextResultAsync());
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dataTable;
        }
    }
}
