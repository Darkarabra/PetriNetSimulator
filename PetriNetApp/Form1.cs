using ClosedXML.Excel;
using Extensions;
using MathNet.Numerics.LinearAlgebra;
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

namespace PetriNetApp
{
    public partial class Form1 : Form, TimeChangeListener
    {

        Controller controller;
        DataTable dt;

        public Form1()
        {

            InitializeComponent();
            controller = new Controller();
            controller.chartForm = this;
            LoadTextBoxes();
            dt = new DataTable();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void LoadTextBoxes()
        {
            sequenceText.Text = controller.printSequence();
            BuffersText.Text = controller.printBuffers();
            InputsText.Text = controller.printInputs();

        }

        private void submitBtn_Click(object sender, EventArgs e)
        {

            controller.submitDefaultData((int)machinesInput.Value, (int)processInput.Value);
            sequenceText.Text = string.Empty;
            BuffersText.Clear();
            InputsText.Clear();
            
        }


        private void simulateBtn_Click(object sender, EventArgs e)
        {
            string error;
            if (controller.hasErrors(out error) == true)
            {
                MessageBox.Show(error, "Error");
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add("time");
                dt.Columns.Add("transition");

                chart.Series.Clear();
                chart.Update();
                chart.DataBindTable(controller.tab);
                chart.Series[0].Name = "Transitions";
                chart.Series[0].MarkerStep = 1;
                chart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                chart.Series[0].Points.DataBind(controller.tab, "time", "transition", "");
                var sortList = new List<sortAlgorithm>();
                if (checkBox1.Checked)
                    sortList.Add(sortAlgorithm.FIFO);
                if (checkBox2.Checked)
                    sortList.Add(sortAlgorithm.MCF);
                if (checkBox3.Checked)
                    sortList.Add(sortAlgorithm.STF);
                var result = controller.Simulate(sortList);

                if (result == true)
                    textArea.AppendText("SIMULATION COMPLETED", Color.ForestGreen);
                else
                    textArea.AppendText("SIMULATION FAILED", Color.Red);

                //XLWorkbook wb = new XLWorkbook();
                //wb.Worksheets.Add(dt, "wyniki");
                //var name = "trasitions";
                //wb.SaveAs(name+".xlsx");
            }

        }

        private void addToSeqBtn_Click(object sender, EventArgs e)
        {
            var result = controller.addMachineToProcess((int)SequenceAddP.Value, (int)SequenceAddM.Value, (int)SequenceAddTime.Value);
            if (result == false)
                MessageBox.Show("Machine or process number out of range.");
            else
                sequenceText.Text = controller.printSequence();
        }

        private void clearSeqBtn_Click(object sender, EventArgs e)
        {
            controller.clearProcessesSequence();
            sequenceText.Text = string.Empty;
        }

        private void BufferCSaveBtn_Click(object sender, EventArgs e)
        {
            var result = controller.defineBufferCapacity((int)BufferNumber.Value, (int)BufferCapacity.Value);
            if (result == false)
                MessageBox.Show("Buffer number can't be higher than number of machines.");
            else
                BuffersText.Text = controller.printBuffers();

        }

        private void InputValueSaveBtn_Click(object sender, EventArgs e)
        {
            var result = controller.defineInputValue((int)InputNumber.Value, (int)InputValue.Value);
            if (result == false)
                MessageBox.Show("Input number can't be higher than number of processes.");
            else
                InputsText.Text = controller.printInputs();
        }

        private void sampleDataBtn_Click(object sender, EventArgs e)
        {
            controller.sampleData();
            LoadTextBoxes();
            machinesInput.Value = controller.Machines;
            processInput.Value = controller.ProcessesAmount;

        }

        public void onTimeChanged(int time, int transition)
        {
            DataRow dr = dt.NewRow();
            dr["time"] = time;
            dr["transition"] = transition;
            dt.Rows.Add(dr);
            chart.Series[0].Points.AddXY(time, transition);
            chart.Update();
            textArea.Clear();
            textArea.AppendText("t: " + time);
            textArea.AppendText(Environment.NewLine);
            textArea.AppendText("Firing transition: " + transition, Color.Orange);
            textArea.AppendText(Environment.NewLine);
            foreach (var t in controller.timer.Timers)
            {
                var color = Color.Green;
                if (t.Time > 0)
                    color = Color.Red;
                textArea.AppendText("M" + t.MachineNumber + ": P" + t.ProcessNumber, color);
                textArea.AppendText(Environment.NewLine);
            }
            textArea.Update();
            if (!simulationFast.Checked)
                System.Threading.Thread.Sleep(500);

        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            DataTable sim = new DataTable();
            dt = new DataTable();
            dt.Columns.Add("time");
            dt.Columns.Add("transition");

            sim.Columns.Add("B1");
            sim.Columns.Add("B2");
            sim.Columns.Add("B3");
            sim.Columns.Add("FIFO");
            sim.Columns.Add("MF");
            sim.Columns.Add("SF");
            int MIN = 5;
            int MAX = 10;
            for (int b1 = MIN; b1 <= MAX; b1++)
            {
                for(int b2 = MIN; b2 <= MAX; b2++)
                {
                    for (int b3 = MIN; b3 <= MAX; b3++)
                    {
                        controller.defineBufferCapacity(1, b1);
                        controller.defineBufferCapacity(2, b2);
                        controller.defineBufferCapacity(3, b3);
                        int FIFO = controller.Simulate(sortAlgorithm.FIFO);
                        int MF = controller.Simulate(sortAlgorithm.MCF);
                        int SF = controller.Simulate(sortAlgorithm.STF);
                        DataRow dr = sim.NewRow();
                        dr["B1"] = b1;
                        dr["B2"] = b2;
                        dr["B3"] = b3;
                        dr["FIFO"] = FIFO;
                        dr["MF"] = MF;
                        dr["SF"] = SF;
                        sim.Rows.Add(dr);
                    }
                }
            }
            XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(sim, "wyniki");
            wb.SaveAs("test2.xlsx");
            Console.Out.WriteLine("done");
            
        }

    }
}
