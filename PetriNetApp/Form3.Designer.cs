namespace PetriNetApp
{
    partial class Form3
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
            this.min_change = new System.Windows.Forms.NumericUpDown();
            this.max_change = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_testfile = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.min_change)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.max_change)).BeginInit();
            this.SuspendLayout();
            // 
            // min_change
            // 
            this.min_change.Location = new System.Drawing.Point(65, 42);
            this.min_change.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.min_change.Name = "min_change";
            this.min_change.Size = new System.Drawing.Size(70, 22);
            this.min_change.TabIndex = 0;
            this.min_change.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // max_change
            // 
            this.max_change.Location = new System.Drawing.Point(200, 42);
            this.max_change.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.max_change.Name = "max_change";
            this.max_change.Size = new System.Drawing.Size(69, 22);
            this.max_change.TabIndex = 1;
            this.max_change.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "MIN:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(153, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "MAX:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(256, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Set buffers\' MIN and MAX test capacity.";
            // 
            // textBox_testfile
            // 
            this.textBox_testfile.Location = new System.Drawing.Point(176, 79);
            this.textBox_testfile.Name = "textBox_testfile";
            this.textBox_testfile.Size = new System.Drawing.Size(117, 22);
            this.textBox_testfile.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(216, 107);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 32);
            this.button1.TabIndex = 7;
            this.button1.Text = "Start test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Save result as .xlsx file:";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 144);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox_testfile);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.max_change);
            this.Controls.Add(this.min_change);
            this.Name = "Form3";
            this.Text = "Test";
            ((System.ComponentModel.ISupportInitialize)(this.min_change)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.max_change)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown min_change;
        private System.Windows.Forms.NumericUpDown max_change;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_testfile;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
    }
}