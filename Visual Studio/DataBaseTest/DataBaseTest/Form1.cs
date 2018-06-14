using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Configuration;

namespace DataBaseTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            loadgrid();
        }

        private void loadgrid()
        {
            OleDbConnection con = new OleDbConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["Connection"].ToString();
            con.Open();
            MessageBox.Show("Connection Success");
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandText = "Select * from [Data]";
            cmd.Connection = con;
            OleDbDataAdapter da = new OleDbDataAdapter();
            DataSet ds = new DataSet();
     
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'salesDataDataSet.Data' table. You can move, or remove it, as needed.
            this.dataTableAdapter.Fill(this.salesDataDataSet.Data);

        }

        private void button1_Click(object sender, EventArgs e)
        {
             
        }

       
    }
}
