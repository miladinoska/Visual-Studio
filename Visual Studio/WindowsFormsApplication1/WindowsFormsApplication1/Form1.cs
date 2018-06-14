using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

                                   
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'salesDataDataSet.Data' table. 
            // You can move, or remove it, as needed.
            this.dataTableAdapter.Fill(this.salesDataDataSet.Data);
            //form size
            this.Size = new Size(600, 410);
            //setting up datetime pickers from long to short date string
            dateTimePicker1.Value.ToShortDateString();
            dateTimePicker2.Value.ToShortDateString();
            //adding titles on charts
            chart1.Titles.Add("Quantity by Model");
            chart2.Titles.Add("Ravenue by Seller");

            //setting combo box values, cell 4 for the first -> Product
            comboBox1.Items.Clear();        
            for (int intCount = 0; intCount < dataGridView1.RowCount; intCount++)
            {

                string val = dataGridView1.Rows[intCount].Cells[4].FormattedValue.ToString();

             
                //check if it already exists
                if (!comboBox1.Items.Contains(val))
                {
                    comboBox1.Items.Add(val);
                }

            }
            //adding a new value in the combo box -> all
            comboBox1.Items.Add("All");

            //setting combo box values, cell 5 for the first -> Country
            comboBox2.Items.Clear();
            for (int intCount = 0; intCount < dataGridView1.RowCount; intCount++)
            {

                string val = dataGridView1.Rows[intCount].Cells[5].FormattedValue.ToString();

                //check if it already exists
                if (!comboBox2.Items.Contains(val))
                {
                    comboBox2.Items.Add(val);
                }
            }
            //adding a new value in the combo box -> all
            comboBox2.Items.Add("All");

            //setting combo box values, cell 6 for the first -> Seller
            comboBox3.Items.Clear();
            for (int intCount = 0; intCount < dataGridView1.RowCount; intCount++)
            {

                string val = dataGridView1.Rows[intCount].Cells[6].FormattedValue.ToString();

                //check if it already exists
                if (!comboBox3.Items.Contains(val))
                {
                    comboBox3.Items.Add(val);
                }
            }

            
        }
        //this code for selectedIndexChanged doesn't do anything
        
        public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
        
        public void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        public void comboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {

    
        }

       
        //Button2_Click has the Filter value
        private void button2_Click(object sender, EventArgs e)
        {
            //date filtering
            DateTime newfilter = dateTimePicker2.Value;
            string filterstring2 = newfilter.ToString();

            DateTime newfilter0 = dateTimePicker1.Value;
            string filterstring1 = newfilter0.ToString();

            string filter0 = "Date <= #" + filterstring2 + "# AND Date > #" + filterstring1 + "#";
                       
            //connect with other filters from combo boxes
            string filter1 = string.Format(comboBox1.Text);
            string filter2 = string.Format(comboBox2.Text);
            string filter3 = string.Format(comboBox3.Text);
            string filter = "";
             

            if ((filter1 != "All") && (filter1!="" ))
            {
                filter += string.Format("Product LIKE '{0}'", comboBox1.Text);
                
            }
             

            if ((filter2 != "All") && (filter2 != ""))
            {
                if (filter != "")
                {
                    filter += " AND ";
                }
                filter += string.Format("Country LIKE '{0}'", comboBox2.Text);
               
            }

            
            if (filter3 != "" )
            {
                if (filter2 != "All" && filter2 !="") 
                {
                    filter += " AND ";
                }
                filter += string.Format("Seller LIKE '{0}'", comboBox3.Text);
            }

             
            if (filter != "")
            {
                filter += " AND ";
            }
            filter += filter0;

            //dataBindingSource filter (which data to get)             
            dataBindingSource.Filter = filter;
            dataGridView1.Refresh();
        }

        //SHOW Charts button
        private void button3_Click(object sender, EventArgs e)
        {
            
            //new form size
            this.Size = new Size(600, 800);

            //chart one -> bubble chart
            chart1.Series["Series1"].Points.Clear();
            //chart two -> line chart
            chart2.Series["Series1"].Points.Clear();
            //calling function
            fillChart();  
        }

        //function that gets needed data from table
        //it's working like an array
        private void fillChart()
        {
            int quantity_sum = 0; //sum of quantity
            int ravenue_sum = 0; //sum of ravenue

            string[] country = new string[dataGridView1.RowCount];
            string[] product = new string[dataGridView1.RowCount];
            string[] seller = new string[dataGridView1.RowCount];
            string[] ravenue = new string[dataGridView1.RowCount];
            string[] quantity = new string[dataGridView1.RowCount];

            //iterating into table
            for (int intCount = 0; intCount < dataGridView1.RowCount; intCount++)
            {
                          
                quantity_sum += Convert.ToInt32(dataGridView1.Rows[intCount].Cells[9].Value);
                ravenue_sum +=  Convert.ToInt32(dataGridView1.Rows[intCount].Cells[7].Value);
                string val_product = dataGridView1.Rows[intCount].Cells[4].FormattedValue.ToString();
                string val_country = dataGridView1.Rows[intCount].Cells[5].FormattedValue.ToString();
                string val_seller = dataGridView1.Rows[intCount].Cells[6].FormattedValue.ToString();
                string val_ravenue = dataGridView1.Rows[intCount].Cells[7].FormattedValue.ToString();
                string val_quantity = dataGridView1.Rows[intCount].Cells[9].FormattedValue.ToString();

                //getting values from iteration
                country[intCount] = val_country;
                product[intCount] = val_product;
                seller[intCount] = val_seller;
                ravenue[intCount] = val_ravenue;
                quantity[intCount] = val_quantity;

               //chart 1 -> x and y axes
               chart1.Series["Series1"].Points.AddXY(product[intCount], quantity[intCount]);
               //chart 2 -> x and y axes
               chart2.Series["Series1"].Points.AddXY(seller[intCount], ravenue[intCount]);
             
            }
            //conversion of sum(s) to strings in another variables
            string quantity_sum1 = Convert.ToString(quantity_sum);
            string ravenue_sum1 = Convert.ToString(ravenue_sum);
            //adding new value on the charts -> Total (sum)
            chart1.Series["Series1"].Points.AddXY("total", quantity_sum1);
            chart2.Series["Series1"].Points.AddXY("total", ravenue_sum1);
            //to be shown value into labels like text
            label6.Text = "Total : " + quantity_sum1;
            label7.Text = "Total : " + ravenue_sum1;
        }

        //HIDE Charts button
        private void button1_Click(object sender, EventArgs e)
        {
            //new size for the form
            this.Size = new Size(600, 410);
        }  
 
    }
}
