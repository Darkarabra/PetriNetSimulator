using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetriNetApp
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private DataTable Table;

        public Form2(DataTable table) : this()
        {
            Table = table;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text)){
                XLWorkbook wb = new XLWorkbook();
                wb.Worksheets.Add(Table, "wyniki");
                wb.SaveAs(textBox1.Text + ".xlsx");
                this.Close();
            }            
        }
    }
}
