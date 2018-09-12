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
        SqlCommand sqlCommand = null;
        string connectionString = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
        string select = "waitfor delay '00:00:10';";
        public Form1()
        {
            InitializeComponent();
            sqlConnection = new SqlConnection(connectionString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            if (textBox1.Text != String.Empty && textBox1.Text == "select * from Books")
            {
                select += textBox1.Text;
                sqlCommand = new SqlCommand(select, sqlConnection);

            }
        }
    }
}
