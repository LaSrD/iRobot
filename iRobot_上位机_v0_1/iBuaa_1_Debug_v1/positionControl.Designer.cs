namespace iBuaa_1_Debug_v1
{
    partial class positionControl
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAngle5 = new System.Windows.Forms.TextBox();
            this.trBangle5 = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAngle3 = new System.Windows.Forms.TextBox();
            this.trBangle4 = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAngle4 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAngle2 = new System.Windows.Forms.TextBox();
            this.trBangle2 = new System.Windows.Forms.TrackBar();
            this.trBangle3 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAngle1 = new System.Windows.Forms.TextBox();
            this.trBangle1 = new System.Windows.Forms.TrackBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txt_jointdata = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.strip_cleartxt = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.清除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trBangle5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trBangle4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trBangle2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trBangle3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trBangle1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.strip_cleartxt.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtAngle5);
            this.groupBox1.Controls.Add(this.trBangle5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtAngle3);
            this.groupBox1.Controls.Add(this.trBangle4);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtAngle4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtAngle2);
            this.groupBox1.Controls.Add(this.trBangle2);
            this.groupBox1.Controls.Add(this.trBangle3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtAngle1);
            this.groupBox1.Controls.Add(this.trBangle1);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 306);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "关节控制";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 269);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(315, 31);
            this.button1.TabIndex = 15;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 230);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "关节5";
            // 
            // txtAngle5
            // 
            this.txtAngle5.Location = new System.Drawing.Point(245, 227);
            this.txtAngle5.Name = "txtAngle5";
            this.txtAngle5.Size = new System.Drawing.Size(77, 21);
            this.txtAngle5.TabIndex = 13;
            this.txtAngle5.Text = "0.00";
            this.txtAngle5.TextChanged += new System.EventHandler(this.txtAngle5_TextChanged);
            // 
            // trBangle5
            // 
            this.trBangle5.AutoSize = false;
            this.trBangle5.Location = new System.Drawing.Point(64, 227);
            this.trBangle5.Maximum = 18000;
            this.trBangle5.Minimum = -18000;
            this.trBangle5.Name = "trBangle5";
            this.trBangle5.Size = new System.Drawing.Size(175, 28);
            this.trBangle5.TabIndex = 12;
            this.trBangle5.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trBangle5.ValueChanged += new System.EventHandler(this.trBangle5_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "关节4";
            // 
            // txtAngle3
            // 
            this.txtAngle3.Location = new System.Drawing.Point(245, 127);
            this.txtAngle3.Name = "txtAngle3";
            this.txtAngle3.Size = new System.Drawing.Size(77, 21);
            this.txtAngle3.TabIndex = 10;
            this.txtAngle3.Text = "0.00";
            this.txtAngle3.TextChanged += new System.EventHandler(this.txtAngle3_TextChanged);
            // 
            // trBangle4
            // 
            this.trBangle4.AutoSize = false;
            this.trBangle4.Location = new System.Drawing.Point(64, 177);
            this.trBangle4.Maximum = 10000;
            this.trBangle4.Minimum = -10000;
            this.trBangle4.Name = "trBangle4";
            this.trBangle4.Size = new System.Drawing.Size(175, 28);
            this.trBangle4.TabIndex = 9;
            this.trBangle4.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trBangle4.ValueChanged += new System.EventHandler(this.trBangle4_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "关节3";
            // 
            // txtAngle4
            // 
            this.txtAngle4.Location = new System.Drawing.Point(245, 177);
            this.txtAngle4.Name = "txtAngle4";
            this.txtAngle4.Size = new System.Drawing.Size(77, 21);
            this.txtAngle4.TabIndex = 7;
            this.txtAngle4.Text = "0.00";
            this.txtAngle4.TextChanged += new System.EventHandler(this.txtAngle4_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "关节2";
            // 
            // txtAngle2
            // 
            this.txtAngle2.Location = new System.Drawing.Point(245, 77);
            this.txtAngle2.Name = "txtAngle2";
            this.txtAngle2.Size = new System.Drawing.Size(77, 21);
            this.txtAngle2.TabIndex = 4;
            this.txtAngle2.Text = "0";
            this.txtAngle2.TextChanged += new System.EventHandler(this.txtAngle2_TextChanged);
            // 
            // trBangle2
            // 
            this.trBangle2.AutoSize = false;
            this.trBangle2.Location = new System.Drawing.Point(64, 77);
            this.trBangle2.Maximum = 0;
            this.trBangle2.Minimum = -9000;
            this.trBangle2.Name = "trBangle2";
            this.trBangle2.Size = new System.Drawing.Size(175, 28);
            this.trBangle2.TabIndex = 3;
            this.trBangle2.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trBangle2.ValueChanged += new System.EventHandler(this.trBangle2_ValueChanged);
            // 
            // trBangle3
            // 
            this.trBangle3.AutoSize = false;
            this.trBangle3.Location = new System.Drawing.Point(64, 127);
            this.trBangle3.Maximum = 10000;
            this.trBangle3.Minimum = -10000;
            this.trBangle3.Name = "trBangle3";
            this.trBangle3.Size = new System.Drawing.Size(175, 28);
            this.trBangle3.TabIndex = 3;
            this.trBangle3.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trBangle3.ValueChanged += new System.EventHandler(this.trBangle3_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "关节1";
            // 
            // txtAngle1
            // 
            this.txtAngle1.Location = new System.Drawing.Point(245, 27);
            this.txtAngle1.Name = "txtAngle1";
            this.txtAngle1.Size = new System.Drawing.Size(77, 21);
            this.txtAngle1.TabIndex = 1;
            this.txtAngle1.Text = "0.00";
            this.txtAngle1.TextChanged += new System.EventHandler(this.txtAngle1_TextChanged);
            // 
            // trBangle1
            // 
            this.trBangle1.AutoSize = false;
            this.trBangle1.Location = new System.Drawing.Point(64, 27);
            this.trBangle1.Maximum = 18000;
            this.trBangle1.Minimum = -18000;
            this.trBangle1.Name = "trBangle1";
            this.trBangle1.Size = new System.Drawing.Size(175, 28);
            this.trBangle1.TabIndex = 0;
            this.trBangle1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trBangle1.ValueChanged += new System.EventHandler(this.trBangle1_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.txt_jointdata);
            this.groupBox2.Location = new System.Drawing.Point(0, 312);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(331, 237);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Record-Play";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(166, 199);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(148, 32);
            this.button3.TabIndex = 2;
            this.button3.Text = "Play";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 199);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(148, 32);
            this.button2.TabIndex = 1;
            this.button2.Text = "Record";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txt_jointdata
            // 
            this.txt_jointdata.Location = new System.Drawing.Point(12, 20);
            this.txt_jointdata.Multiline = true;
            this.txt_jointdata.Name = "txt_jointdata";
            this.txt_jointdata.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_jointdata.Size = new System.Drawing.Size(299, 173);
            this.txt_jointdata.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // strip_cleartxt
            // 
            this.strip_cleartxt.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.清除ToolStripMenuItem});
            this.strip_cleartxt.Name = "strip_cleartxt";
            this.strip_cleartxt.Size = new System.Drawing.Size(101, 26);
            // 
            // 清除ToolStripMenuItem
            // 
            this.清除ToolStripMenuItem.Name = "清除ToolStripMenuItem";
            this.清除ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.清除ToolStripMenuItem.Text = "清除";
            this.清除ToolStripMenuItem.Click += new System.EventHandler(this.清除ToolStripMenuItem_Click);
            // 
            // positionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 551);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "positionControl";
            this.Text = "positionControl";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trBangle5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trBangle4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trBangle2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trBangle3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trBangle1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.strip_cleartxt.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAngle1;
        private System.Windows.Forms.TrackBar trBangle1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAngle5;
        private System.Windows.Forms.TrackBar trBangle5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAngle3;
        private System.Windows.Forms.TrackBar trBangle4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAngle4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAngle2;
        private System.Windows.Forms.TrackBar trBangle2;
        private System.Windows.Forms.TrackBar trBangle3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_jointdata;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ContextMenuStrip strip_cleartxt;
        private System.Windows.Forms.ToolStripMenuItem 清除ToolStripMenuItem;
    }
}