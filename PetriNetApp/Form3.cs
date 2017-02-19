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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public Form3(Form1 Form) : this()
        {
            form = Form;
        }

        public int MIN;
        public int MAX;
        public string fileName;
        Form1 form;
        private void button1_Click(object sender, EventArgs e)
        {
            MIN = (int)min_change.Value;
            MAX = (int)max_change.Value;
            fileName = textBox_testfile.Text;
            if (MAX < MIN || string.IsNullOrEmpty(fileName))
                MessageBox.Show("Wrong data.");
            else
            {

                form.onTestRun(MIN, MAX, fileName);
                Close();
                
            }
                
        }
    }
}
