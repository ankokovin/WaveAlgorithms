namespace WaveAlgorithms
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
            this.bStart = new System.Windows.Forms.Button();
            this.rtbGraphStructure = new System.Windows.Forms.RichTextBox();
            this.bParseGraph = new System.Windows.Forms.Button();
            this.cbIsDirected = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // bStart
            // 
            this.bStart.Enabled = false;
            this.bStart.Location = new System.Drawing.Point(590, 292);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(75, 23);
            this.bStart.TabIndex = 0;
            this.bStart.Text = "Начать";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // rtbGraphStructure
            // 
            this.rtbGraphStructure.Location = new System.Drawing.Point(12, 12);
            this.rtbGraphStructure.Name = "rtbGraphStructure";
            this.rtbGraphStructure.Size = new System.Drawing.Size(141, 273);
            this.rtbGraphStructure.TabIndex = 1;
            this.rtbGraphStructure.Text = "";
            // 
            // bParseGraph
            // 
            this.bParseGraph.Location = new System.Drawing.Point(12, 314);
            this.bParseGraph.Name = "bParseGraph";
            this.bParseGraph.Size = new System.Drawing.Size(141, 27);
            this.bParseGraph.TabIndex = 2;
            this.bParseGraph.Text = "Парсить граф";
            this.bParseGraph.UseVisualStyleBackColor = true;
            this.bParseGraph.Click += new System.EventHandler(this.bParseGraph_Click);
            // 
            // cbIsDirected
            // 
            this.cbIsDirected.AutoSize = true;
            this.cbIsDirected.Location = new System.Drawing.Point(12, 291);
            this.cbIsDirected.Name = "cbIsDirected";
            this.cbIsDirected.Size = new System.Drawing.Size(102, 17);
            this.cbIsDirected.TabIndex = 3;
            this.cbIsDirected.Text = "Направленный";
            this.cbIsDirected.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(160, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(628, 274);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.cbIsDirected);
            this.Controls.Add(this.bParseGraph);
            this.Controls.Add(this.rtbGraphStructure);
            this.Controls.Add(this.bStart);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.RichTextBox rtbGraphStructure;
        private System.Windows.Forms.Button bParseGraph;
        private System.Windows.Forms.CheckBox cbIsDirected;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

