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
        SqlDataAdapter sqlAdapter = null;
        DataSet dataSet = null;
        SqlCommandBuilder sqlBuilder = null;
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
                sqlConnection.Open();
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
            dataGridView1.DataSource = null;
            select += textBox1.Text;
            dataSet = new DataSet();
            sqlAdapter = new SqlDataAdapter(select, sqlConnection);
            sqlBuilder = new SqlCommandBuilder(sqlAdapter);
            sqlAdapter.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];
        }
    }
}
