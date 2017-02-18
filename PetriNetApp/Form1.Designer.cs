namespace PetriNetApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.submitBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.simulateBtn = new System.Windows.Forms.Button();
            this.machinesInput = new System.Windows.Forms.NumericUpDown();
            this.processInput = new System.Windows.Forms.NumericUpDown();
            this.SequenceAddM = new System.Windows.Forms.NumericUpDown();
            this.SequenceAddP = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.addToSeqBtn = new System.Windows.Forms.Button();
            this.ClearSeqBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.SequenceAddTime = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.BufferNumber = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.BufferCapacity = new System.Windows.Forms.NumericUpDown();
            this.BufferCSaveBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sequenceText = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.InputsText = new System.Windows.Forms.TextBox();
            this.BuffersText = new System.Windows.Forms.TextBox();
            this.InputValueSaveBtn = new System.Windows.Forms.Button();
            this.InputValue = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.InputNumber = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.sampleDataBtn = new System.Windows.Forms.Button();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.textArea = new System.Windows.Forms.RichTextBox();
            this.simulationFast = new System.Windows.Forms.CheckBox();
            this.testBtn = new System.Windows.Forms.Button();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.stopBtn = new System.Windows.Forms.Button();
            this.radioButton_FIFO = new System.Windows.Forms.RadioButton();
            this.radioButton_MCF = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioButton_SOF = new System.Windows.Forms.RadioButton();
            this.save_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.machinesInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.processInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SequenceAddM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SequenceAddP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SequenceAddTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BufferNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BufferCapacity)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InputValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InputNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // submitBtn
            // 
            this.submitBtn.Location = new System.Drawing.Point(168, 53);
            this.submitBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.submitBtn.Name = "submitBtn";
            this.submitBtn.Size = new System.Drawing.Size(69, 27);
            this.submitBtn.TabIndex = 0;
            this.submitBtn.Text = "Submit";
            this.submitBtn.UseVisualStyleBackColor = true;
            this.submitBtn.Click += new System.EventHandler(this.submitBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Machines";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Processes";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Sequences:";
            // 
            // simulateBtn
            // 
            this.simulateBtn.Location = new System.Drawing.Point(317, 123);
            this.simulateBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.simulateBtn.Name = "simulateBtn";
            this.simulateBtn.Size = new System.Drawing.Size(75, 30);
            this.simulateBtn.TabIndex = 9;
            this.simulateBtn.Text = "Simulate";
            this.simulateBtn.UseVisualStyleBackColor = true;
            this.simulateBtn.Click += new System.EventHandler(this.simulateBtn_Click);
            // 
            // machinesInput
            // 
            this.machinesInput.Location = new System.Drawing.Point(91, 26);
            this.machinesInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.machinesInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.machinesInput.Name = "machinesInput";
            this.machinesInput.Size = new System.Drawing.Size(64, 22);
            this.machinesInput.TabIndex = 10;
            this.machinesInput.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // processInput
            // 
            this.processInput.Location = new System.Drawing.Point(91, 57);
            this.processInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.processInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.processInput.Name = "processInput";
            this.processInput.Size = new System.Drawing.Size(64, 22);
            this.processInput.TabIndex = 11;
            this.processInput.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // SequenceAddM
            // 
            this.SequenceAddM.Location = new System.Drawing.Point(203, 21);
            this.SequenceAddM.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SequenceAddM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SequenceAddM.Name = "SequenceAddM";
            this.SequenceAddM.Size = new System.Drawing.Size(45, 22);
            this.SequenceAddM.TabIndex = 12;
            this.SequenceAddM.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // SequenceAddP
            // 
            this.SequenceAddP.Location = new System.Drawing.Point(80, 20);
            this.SequenceAddP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SequenceAddP.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SequenceAddP.Name = "SequenceAddP";
            this.SequenceAddP.Size = new System.Drawing.Size(45, 22);
            this.SequenceAddP.TabIndex = 13;
            this.SequenceAddP.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(132, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 17);
            this.label4.TabIndex = 14;
            this.label4.Text = "Machine:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "Process:";
            // 
            // addToSeqBtn
            // 
            this.addToSeqBtn.Location = new System.Drawing.Point(365, 16);
            this.addToSeqBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.addToSeqBtn.Name = "addToSeqBtn";
            this.addToSeqBtn.Size = new System.Drawing.Size(53, 27);
            this.addToSeqBtn.TabIndex = 16;
            this.addToSeqBtn.Text = "Add";
            this.addToSeqBtn.UseVisualStyleBackColor = true;
            this.addToSeqBtn.Click += new System.EventHandler(this.addToSeqBtn_Click);
            // 
            // ClearSeqBtn
            // 
            this.ClearSeqBtn.Location = new System.Drawing.Point(365, 78);
            this.ClearSeqBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ClearSeqBtn.Name = "ClearSeqBtn";
            this.ClearSeqBtn.Size = new System.Drawing.Size(52, 27);
            this.ClearSeqBtn.TabIndex = 17;
            this.ClearSeqBtn.Text = "Clear";
            this.ClearSeqBtn.UseVisualStyleBackColor = true;
            this.ClearSeqBtn.Click += new System.EventHandler(this.clearSeqBtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(255, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 17);
            this.label6.TabIndex = 18;
            this.label6.Text = "Time:";
            // 
            // SequenceAddTime
            // 
            this.SequenceAddTime.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.SequenceAddTime.Location = new System.Drawing.Point(304, 20);
            this.SequenceAddTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SequenceAddTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SequenceAddTime.Name = "SequenceAddTime";
            this.SequenceAddTime.Size = new System.Drawing.Size(45, 22);
            this.SequenceAddTime.TabIndex = 19;
            this.SequenceAddTime.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 17);
            this.label7.TabIndex = 20;
            this.label7.Text = "Buffer:";
            // 
            // BufferNumber
            // 
            this.BufferNumber.Location = new System.Drawing.Point(67, 31);
            this.BufferNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BufferNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BufferNumber.Name = "BufferNumber";
            this.BufferNumber.Size = new System.Drawing.Size(45, 22);
            this.BufferNumber.TabIndex = 21;
            this.BufferNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(119, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 17);
            this.label8.TabIndex = 22;
            this.label8.Text = "Capacity:";
            // 
            // BufferCapacity
            // 
            this.BufferCapacity.Location = new System.Drawing.Point(191, 31);
            this.BufferCapacity.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BufferCapacity.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.BufferCapacity.Name = "BufferCapacity";
            this.BufferCapacity.Size = new System.Drawing.Size(45, 22);
            this.BufferCapacity.TabIndex = 23;
            this.BufferCapacity.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // BufferCSaveBtn
            // 
            this.BufferCSaveBtn.Location = new System.Drawing.Point(243, 28);
            this.BufferCSaveBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BufferCSaveBtn.Name = "BufferCSaveBtn";
            this.BufferCSaveBtn.Size = new System.Drawing.Size(68, 26);
            this.BufferCSaveBtn.TabIndex = 24;
            this.BufferCSaveBtn.Text = "Save";
            this.BufferCSaveBtn.UseVisualStyleBackColor = true;
            this.BufferCSaveBtn.Click += new System.EventHandler(this.BufferCSaveBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sequenceText);
            this.groupBox1.Controls.Add(this.SequenceAddP);
            this.groupBox1.Controls.Add(this.SequenceAddM);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.SequenceAddTime);
            this.groupBox1.Controls.Add(this.ClearSeqBtn);
            this.groupBox1.Controls.Add(this.addToSeqBtn);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(21, 161);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(473, 128);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Processes";
            // 
            // sequenceText
            // 
            this.sequenceText.Location = new System.Drawing.Point(112, 53);
            this.sequenceText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sequenceText.Name = "sequenceText";
            this.sequenceText.ReadOnly = true;
            this.sequenceText.Size = new System.Drawing.Size(248, 51);
            this.sequenceText.TabIndex = 20;
            this.sequenceText.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.submitBtn);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.machinesInput);
            this.groupBox2.Controls.Add(this.processInput);
            this.groupBox2.Location = new System.Drawing.Point(21, 48);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(473, 94);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "General";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.InputsText);
            this.groupBox3.Controls.Add(this.BuffersText);
            this.groupBox3.Controls.Add(this.InputValueSaveBtn);
            this.groupBox3.Controls.Add(this.InputValue);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.InputNumber);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.BufferNumber);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.BufferCSaveBtn);
            this.groupBox3.Controls.Add(this.BufferCapacity);
            this.groupBox3.Location = new System.Drawing.Point(21, 304);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(473, 108);
            this.groupBox3.TabIndex = 28;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Values";
            // 
            // InputsText
            // 
            this.InputsText.Location = new System.Drawing.Point(317, 69);
            this.InputsText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.InputsText.Name = "InputsText";
            this.InputsText.ReadOnly = true;
            this.InputsText.Size = new System.Drawing.Size(141, 22);
            this.InputsText.TabIndex = 20;
            // 
            // BuffersText
            // 
            this.BuffersText.Location = new System.Drawing.Point(317, 28);
            this.BuffersText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BuffersText.Name = "BuffersText";
            this.BuffersText.ReadOnly = true;
            this.BuffersText.Size = new System.Drawing.Size(141, 22);
            this.BuffersText.TabIndex = 20;
            // 
            // InputValueSaveBtn
            // 
            this.InputValueSaveBtn.Location = new System.Drawing.Point(243, 68);
            this.InputValueSaveBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.InputValueSaveBtn.Name = "InputValueSaveBtn";
            this.InputValueSaveBtn.Size = new System.Drawing.Size(68, 26);
            this.InputValueSaveBtn.TabIndex = 29;
            this.InputValueSaveBtn.Text = "Save";
            this.InputValueSaveBtn.UseVisualStyleBackColor = true;
            this.InputValueSaveBtn.Click += new System.EventHandler(this.InputValueSaveBtn_Click);
            // 
            // InputValue
            // 
            this.InputValue.Location = new System.Drawing.Point(191, 71);
            this.InputValue.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.InputValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.InputValue.Name = "InputValue";
            this.InputValue.Size = new System.Drawing.Size(45, 22);
            this.InputValue.TabIndex = 28;
            this.InputValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(132, 78);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 17);
            this.label10.TabIndex = 27;
            this.label10.Text = "Value:";
            // 
            // InputNumber
            // 
            this.InputNumber.Location = new System.Drawing.Point(67, 71);
            this.InputNumber.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.InputNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.InputNumber.Name = "InputNumber";
            this.InputNumber.Size = new System.Drawing.Size(45, 22);
            this.InputNumber.TabIndex = 26;
            this.InputNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 17);
            this.label9.TabIndex = 25;
            this.label9.Text = "Input:";
            // 
            // sampleDataBtn
            // 
            this.sampleDataBtn.Location = new System.Drawing.Point(373, 12);
            this.sampleDataBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sampleDataBtn.Name = "sampleDataBtn";
            this.sampleDataBtn.Size = new System.Drawing.Size(108, 30);
            this.sampleDataBtn.TabIndex = 29;
            this.sampleDataBtn.Text = "Sample data";
            this.sampleDataBtn.UseVisualStyleBackColor = true;
            this.sampleDataBtn.Click += new System.EventHandler(this.sampleDataBtn_Click);
            // 
            // chart
            // 
            chartArea3.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart.Legends.Add(legend3);
            this.chart.Location = new System.Drawing.Point(536, 36);
            this.chart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chart.Name = "chart";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series3.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            this.chart.Series.Add(series3);
            this.chart.Size = new System.Drawing.Size(757, 369);
            this.chart.TabIndex = 30;
            this.chart.Text = "chart1";
            this.chart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart_MouseMove);
            // 
            // textArea
            // 
            this.textArea.Location = new System.Drawing.Point(536, 445);
            this.textArea.Name = "textArea";
            this.textArea.Size = new System.Drawing.Size(757, 130);
            this.textArea.TabIndex = 34;
            this.textArea.Text = "";
            // 
            // simulationFast
            // 
            this.simulationFast.AutoSize = true;
            this.simulationFast.Location = new System.Drawing.Point(191, 129);
            this.simulationFast.Name = "simulationFast";
            this.simulationFast.Size = new System.Drawing.Size(124, 21);
            this.simulationFast.TabIndex = 35;
            this.simulationFast.Text = "Fast simulation";
            this.simulationFast.UseVisualStyleBackColor = true;
            // 
            // testBtn
            // 
            this.testBtn.Location = new System.Drawing.Point(295, 12);
            this.testBtn.Name = "testBtn";
            this.testBtn.Size = new System.Drawing.Size(75, 30);
            this.testBtn.TabIndex = 36;
            this.testBtn.Text = "Test";
            this.testBtn.UseVisualStyleBackColor = true;
            this.testBtn.Click += new System.EventHandler(this.testBtn_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // stopBtn
            // 
            this.stopBtn.Enabled = false;
            this.stopBtn.Location = new System.Drawing.Point(392, 123);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(75, 30);
            this.stopBtn.TabIndex = 37;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // radioButton_FIFO
            // 
            this.radioButton_FIFO.AutoSize = true;
            this.radioButton_FIFO.Checked = true;
            this.radioButton_FIFO.Location = new System.Drawing.Point(11, 30);
            this.radioButton_FIFO.Name = "radioButton_FIFO";
            this.radioButton_FIFO.Size = new System.Drawing.Size(59, 21);
            this.radioButton_FIFO.TabIndex = 38;
            this.radioButton_FIFO.TabStop = true;
            this.radioButton_FIFO.Text = "FIFO";
            this.radioButton_FIFO.UseVisualStyleBackColor = true;
            // 
            // radioButton_MCF
            // 
            this.radioButton_MCF.AutoSize = true;
            this.radioButton_MCF.Location = new System.Drawing.Point(11, 57);
            this.radioButton_MCF.Name = "radioButton_MCF";
            this.radioButton_MCF.Size = new System.Drawing.Size(179, 21);
            this.radioButton_MCF.TabIndex = 39;
            this.radioButton_MCF.TabStop = true;
            this.radioButton_MCF.Text = "Machine connected first";
            this.radioButton_MCF.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radioButton_SOF);
            this.groupBox5.Controls.Add(this.radioButton_FIFO);
            this.groupBox5.Controls.Add(this.radioButton_MCF);
            this.groupBox5.Controls.Add(this.stopBtn);
            this.groupBox5.Controls.Add(this.simulationFast);
            this.groupBox5.Controls.Add(this.simulateBtn);
            this.groupBox5.Location = new System.Drawing.Point(21, 428);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(473, 159);
            this.groupBox5.TabIndex = 41;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Simulation";
            // 
            // radioButton_SOF
            // 
            this.radioButton_SOF.AutoSize = true;
            this.radioButton_SOF.Location = new System.Drawing.Point(11, 84);
            this.radioButton_SOF.Name = "radioButton_SOF";
            this.radioButton_SOF.Size = new System.Drawing.Size(180, 21);
            this.radioButton_SOF.TabIndex = 40;
            this.radioButton_SOF.TabStop = true;
            this.radioButton_SOF.Text = "Shortest operations first";
            this.radioButton_SOF.UseVisualStyleBackColor = true;
            // 
            // save_btn
            // 
            this.save_btn.Enabled = false;
            this.save_btn.Location = new System.Drawing.Point(1169, 410);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(124, 29);
            this.save_btn.TabIndex = 41;
            this.save_btn.Text = "Save transitions";
            this.save_btn.UseVisualStyleBackColor = true;
            this.save_btn.Click += new System.EventHandler(this.save_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1316, 592);
            this.Controls.Add(this.save_btn);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.testBtn);
            this.Controls.Add(this.textArea);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.sampleDataBtn);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "PetriNet Simulator";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.machinesInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.processInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SequenceAddM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SequenceAddP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SequenceAddTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BufferNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BufferCapacity)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.InputValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InputNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button submitBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button simulateBtn;
        private System.Windows.Forms.NumericUpDown machinesInput;
        private System.Windows.Forms.NumericUpDown processInput;
        private System.Windows.Forms.NumericUpDown SequenceAddM;
        private System.Windows.Forms.NumericUpDown SequenceAddP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button addToSeqBtn;
        private System.Windows.Forms.Button ClearSeqBtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown SequenceAddTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown BufferNumber;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown BufferCapacity;
        private System.Windows.Forms.Button BufferCSaveBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox InputsText;
        private System.Windows.Forms.TextBox BuffersText;
        private System.Windows.Forms.Button InputValueSaveBtn;
        private System.Windows.Forms.NumericUpDown InputValue;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown InputNumber;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RichTextBox sequenceText;
        private System.Windows.Forms.Button sampleDataBtn;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.RichTextBox textArea;
        private System.Windows.Forms.CheckBox simulationFast;
        private System.Windows.Forms.Button testBtn;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.RadioButton radioButton_FIFO;
        private System.Windows.Forms.RadioButton radioButton_MCF;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioButton_SOF;
        private System.Windows.Forms.Button save_btn;
    }
}

