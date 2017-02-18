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
using System.Windows.Forms.DataVisualization.Charting;

namespace PetriNetApp
{
    public partial class Form1 : Form, TimeChangeListener
    {

        Controller controller;
        DataTable dt;
        int machines;
        int processes;
        List<sortAlgorithm> sortList;
        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();
        public Form1()
        {

            InitializeComponent();
            controller = new Controller();
            controller.chartForm = this;
            LoadTextBoxes();
            dt = new DataTable();

            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.WorkerReportsProgress = true;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void LoadTextBoxes()
        {
            sequenceText.Text = controller.printSequence();
            BuffersText.Text = controller.printBuffers();
            InputsText.Text = controller.printInputs();
            machines = controller.Machines;
            processes = controller.ProcessesAmount;

        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            var newMachines = (int)machinesInput.Value;
            int newProcesses = (int)processInput.Value;
            bool clear = false;
            if (newMachines < machines || newProcesses < processes)
            {
                var confirm = MessageBox.Show("Submitting will cause loss of other parameters. Are you sure?", "Warning", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    sequenceText.Text = string.Empty;
                    BuffersText.Clear();
                    InputsText.Clear();
                    clear = true;
                }
            }
            controller.submitDefaultData((int)machinesInput.Value, (int)processInput.Value, clear);
            machines = newMachines;
            processes = newProcesses;
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
                simulateBtn.Enabled = false;
                stopBtn.Enabled = true;

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
                sortList = new List<sortAlgorithm>();
                if (radioButton_FIFO.Checked)
                    sortList.Add(sortAlgorithm.FIFO);
                if (radioButton_MCF.Checked)
                    sortList.Add(sortAlgorithm.MCF);
                if (radioButton_SOF.Checked)
                    sortList.Add(sortAlgorithm.STF);

                backgroundWorker.RunWorkerAsync();
                

                //XLWorkbook wb = new XLWorkbook();
                //wb.Worksheets.Add(dt, "wyniki");
                //var name = "trasitions11";
                //wb.SaveAs(name + ".xlsx");
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
            var confirm = MessageBox.Show("Are you sure that you want to clear sequences?", "Warning", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                controller.clearProcessesSequence();
                sequenceText.Text = string.Empty;
                sequenceText.Text = controller.printSequence();
            }
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

        public void onTimeChanged(int time, int transition, string machinesPercentage)
        {
            DataRow dr = dt.NewRow();
            dr["time"] = time;
            dr["transition"] = transition;
            if(transition != 0)
                dt.Rows.Add(dr);
            this.Invoke(new MethodInvoker(delegate {
                chart.Series[0].Points.AddXY(time, transition);
                chart.Update();
                textArea.Clear();
                textArea.AppendText(machinesPercentage + "\n" + "t: " + time + "\n");
                textArea.AppendText("Firing transition: " + transition + "\n", Color.Orange);

                foreach (var t in controller.timer.Timers)
                {
                    var color = Color.Green;
                    if (t.Time > 0)
                        color = Color.Red; 
                    textArea.AppendText("M" + t.MachineNumber + ": P" + t.ProcessNumber, color);
                    textArea.AppendText("           ");
                    textArea.AppendText(controller.printCurrentBufferProcesses(t.MachineNumber) + "\n", Color.Blue);
                }
                textArea.Update();
                if (!simulationFast.Checked)
                    System.Threading.Thread.Sleep(500);
            }));
            

        }

        private void testBtn_Click(object sender, EventArgs e)
        {
            int MIN = 5;
            int MAX = 10;
            var confirm = MessageBox.Show($"Test Buffers 1 - 3 for capacities {MIN} to {MAX}. It may take few minutes a while. Are you sure?" , "Warning" ,MessageBoxButtons.YesNo);
            if(confirm == DialogResult.Yes)
            {
                bool test = true;
                simulateBtn.Enabled = false;
                testBtn.Enabled = false;
                stopBtn.Enabled = true;
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
               
                for (int b1 = MIN; b1 <= MAX; b1++)
                {
                    for (int b2 = MIN; b2 <= MAX; b2++)
                    {
                        for (int b3 = MIN; b3 <= MAX; b3++)
                        {
                            controller.defineBufferCapacity(1, b1);
                            controller.defineBufferCapacity(2, b2);
                            controller.defineBufferCapacity(3, b3);
                            int FIFO = controller.Simulate(sortAlgorithm.FIFO, test);
                            int MF = controller.Simulate(sortAlgorithm.MCF, test);
                            int SF = controller.Simulate(sortAlgorithm.STF, test);
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

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = new simResult(controller.Simulate(sortList));
            
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool result = (e.Result as simResult).result;
            if (result == true)
                textArea.AppendText("SIMULATION COMPLETED", Color.ForestGreen);
            else if(controller.stopped)
                textArea.AppendText("SIMULATION STOPPED BY USER", Color.DarkOrange);
            else
                textArea.AppendText("SIMULATION FAILED", Color.Red);
            simulateBtn.Enabled = true;
            stopBtn.Enabled = false;
            save_btn.Enabled = true;
        }
        private class simResult
        {
            public bool result { get; set; }
            public simResult(bool value)
            {
                result = value;
            }
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            controller.onBreakEvent();
            backgroundWorker.CancelAsync();
            simulateBtn.Enabled = true;
            stopBtn.Enabled = false;
        }

        private void chart_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;

            prevPosition = pos;
            tooltip.RemoveAll();
            
            var results = chart.HitTest(pos.X, pos.Y, false, ChartElementType.DataPoint);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    var point = result.Object as DataPoint;
                    if (point != null)
                    {
                        var pointX = result.ChartArea.AxisX.ValueToPixelPosition(point.XValue);
                        var pointY = result.ChartArea.AxisY.ValueToPixelPosition(point.YValues[0]);

                        if (Math.Abs(pos.X - pointX) < 3 &&
                            Math.Abs(pos.Y - pointY) < 3)
                        {
                            tooltip.Show($"transition = {point.YValues[0]}, time = {point.XValue}", this.chart,
                                            pos.X, pos.Y - 15);
                        }
                    }
                }
            }
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            var dialog = new Form2(dt);
            dialog.ShowDialog(this);
        }
    }
}
