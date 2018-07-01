namespace WPPS_DEMO
{
    partial class QueryOpt
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RealTimePower = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShortTimePower = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PeriodTimePower = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Time,
            this.RealTimePower,
            this.ShortTimePower,
            this.PeriodTimePower});
            this.dataGridView1.Location = new System.Drawing.Point(13, 66);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(550, 286);
            this.dataGridView1.TabIndex = 9;
            // 
            // Time
            // 
            this.Time.DataPropertyName = "Time";
            this.Time.HeaderText = "时间";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            this.Time.Width = 140;
            // 
            // RealTimePower
            // 
            this.RealTimePower.DataPropertyName = "RealTimePower";
            this.RealTimePower.HeaderText = "实时功率";
            this.RealTimePower.Name = "RealTimePower";
            this.RealTimePower.ReadOnly = true;
            this.RealTimePower.Width = 120;
            // 
            // ShortTimePower
            // 
            this.ShortTimePower.DataPropertyName = "ShortTimePower";
            this.ShortTimePower.HeaderText = "超短期功率";
            this.ShortTimePower.Name = "ShortTimePower";
            this.ShortTimePower.ReadOnly = true;
            this.ShortTimePower.Width = 120;
            // 
            // PeriodTimePower
            // 
            this.PeriodTimePower.DataPropertyName = "PeriodTimePower";
            this.PeriodTimePower.HeaderText = "短期功率";
            this.PeriodTimePower.Name = "PeriodTimePower";
            this.PeriodTimePower.ReadOnly = true;
            this.PeriodTimePower.Width = 120;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(196, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "~~~";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(412, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(234, 22);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(133, 21);
            this.textBox2.TabIndex = 11;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(47, 22);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(134, 21);
            this.textBox1.TabIndex = 10;
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Location = new System.Drawing.Point(13, 378);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(550, 200);
            this.textBox3.TabIndex = 12;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // QueryOpt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 590);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "QueryOpt";
            this.Text = "QueryOpt";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn RealTimePower;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShortTimePower;
        private System.Windows.Forms.DataGridViewTextBoxColumn PeriodTimePower;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox3;
    }
}