namespace iBuaa_1_Debug_v1
{
    partial class WavesDisplay
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series13 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series14 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend8 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series15 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series16 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend9 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series17 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series18 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea10 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend10 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series19 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series20 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart3 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart4 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart5 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.master1 = new System.Windows.Forms.TextBox();
            this.slave1 = new System.Windows.Forms.TextBox();
            this.slave2 = new System.Windows.Forms.TextBox();
            this.master2 = new System.Windows.Forms.TextBox();
            this.slave3 = new System.Windows.Forms.TextBox();
            this.master3 = new System.Windows.Forms.TextBox();
            this.slave4 = new System.Windows.Forms.TextBox();
            this.master4 = new System.Windows.Forms.TextBox();
            this.slave5 = new System.Windows.Forms.TextBox();
            this.master5 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart5)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.LightGray;
            chartArea6.AxisX.MajorGrid.Enabled = false;
            chartArea6.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea6.AxisY.ScrollBar.Enabled = false;
            chartArea6.BackColor = System.Drawing.Color.LightGray;
            chartArea6.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.TileFlipX;
            chartArea6.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot;
            chartArea6.InnerPlotPosition.Auto = false;
            chartArea6.InnerPlotPosition.Height = 100F;
            chartArea6.InnerPlotPosition.Width = 95F;
            chartArea6.InnerPlotPosition.X = 5F;
            chartArea6.Name = "ChartArea1";
            chartArea6.Position.Auto = false;
            chartArea6.Position.Height = 100F;
            chartArea6.Position.Width = 100F;
            this.chart1.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chart1.Legends.Add(legend6);
            this.chart1.Location = new System.Drawing.Point(12, 18);
            this.chart1.Name = "chart1";
            series11.ChartArea = "ChartArea1";
            series11.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series11.Legend = "Legend1";
            series11.Name = "Master";
            series12.ChartArea = "ChartArea1";
            series12.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series12.Legend = "Legend1";
            series12.Name = "Slave";
            this.chart1.Series.Add(series11);
            this.chart1.Series.Add(series12);
            this.chart1.Size = new System.Drawing.Size(787, 102);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // chart2
            // 
            this.chart2.BackColor = System.Drawing.Color.LightGray;
            chartArea7.AxisX.MajorGrid.Enabled = false;
            chartArea7.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea7.AxisY.ScrollBar.Enabled = false;
            chartArea7.BackColor = System.Drawing.Color.LightGray;
            chartArea7.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.TileFlipX;
            chartArea7.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot;
            chartArea7.InnerPlotPosition.Auto = false;
            chartArea7.InnerPlotPosition.Height = 100F;
            chartArea7.InnerPlotPosition.Width = 95F;
            chartArea7.InnerPlotPosition.X = 5F;
            chartArea7.Name = "ChartArea1";
            chartArea7.Position.Auto = false;
            chartArea7.Position.Height = 100F;
            chartArea7.Position.Width = 100F;
            this.chart2.ChartAreas.Add(chartArea7);
            legend7.Name = "Legend1";
            this.chart2.Legends.Add(legend7);
            this.chart2.Location = new System.Drawing.Point(12, 125);
            this.chart2.Name = "chart2";
            series13.ChartArea = "ChartArea1";
            series13.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series13.Legend = "Legend1";
            series13.Name = "Master";
            series14.ChartArea = "ChartArea1";
            series14.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series14.Legend = "Legend1";
            series14.Name = "Slave";
            this.chart2.Series.Add(series13);
            this.chart2.Series.Add(series14);
            this.chart2.Size = new System.Drawing.Size(787, 102);
            this.chart2.TabIndex = 1;
            this.chart2.Text = "chart2";
            // 
            // chart3
            // 
            this.chart3.BackColor = System.Drawing.Color.LightGray;
            chartArea8.AxisX.MajorGrid.Enabled = false;
            chartArea8.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea8.AxisY.ScrollBar.Enabled = false;
            chartArea8.BackColor = System.Drawing.Color.LightGray;
            chartArea8.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.TileFlipX;
            chartArea8.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot;
            chartArea8.InnerPlotPosition.Auto = false;
            chartArea8.InnerPlotPosition.Height = 100F;
            chartArea8.InnerPlotPosition.Width = 95F;
            chartArea8.InnerPlotPosition.X = 5F;
            chartArea8.Name = "ChartArea1";
            chartArea8.Position.Auto = false;
            chartArea8.Position.Height = 100F;
            chartArea8.Position.Width = 100F;
            this.chart3.ChartAreas.Add(chartArea8);
            legend8.Name = "Legend1";
            this.chart3.Legends.Add(legend8);
            this.chart3.Location = new System.Drawing.Point(12, 232);
            this.chart3.Name = "chart3";
            series15.ChartArea = "ChartArea1";
            series15.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series15.Legend = "Legend1";
            series15.Name = "Master";
            series16.ChartArea = "ChartArea1";
            series16.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series16.Legend = "Legend1";
            series16.Name = "Slave";
            this.chart3.Series.Add(series15);
            this.chart3.Series.Add(series16);
            this.chart3.Size = new System.Drawing.Size(787, 102);
            this.chart3.TabIndex = 2;
            this.chart3.Text = "chart3";
            // 
            // chart4
            // 
            this.chart4.BackColor = System.Drawing.Color.LightGray;
            chartArea9.AxisX.MajorGrid.Enabled = false;
            chartArea9.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea9.AxisY.ScrollBar.Enabled = false;
            chartArea9.BackColor = System.Drawing.Color.LightGray;
            chartArea9.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.TileFlipX;
            chartArea9.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot;
            chartArea9.InnerPlotPosition.Auto = false;
            chartArea9.InnerPlotPosition.Height = 100F;
            chartArea9.InnerPlotPosition.Width = 95F;
            chartArea9.InnerPlotPosition.X = 5F;
            chartArea9.Name = "ChartArea1";
            chartArea9.Position.Auto = false;
            chartArea9.Position.Height = 100F;
            chartArea9.Position.Width = 100F;
            this.chart4.ChartAreas.Add(chartArea9);
            legend9.Name = "Legend1";
            this.chart4.Legends.Add(legend9);
            this.chart4.Location = new System.Drawing.Point(12, 339);
            this.chart4.Name = "chart4";
            series17.ChartArea = "ChartArea1";
            series17.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series17.Legend = "Legend1";
            series17.Name = "Master";
            series18.ChartArea = "ChartArea1";
            series18.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series18.Legend = "Legend1";
            series18.Name = "Slave";
            this.chart4.Series.Add(series17);
            this.chart4.Series.Add(series18);
            this.chart4.Size = new System.Drawing.Size(787, 102);
            this.chart4.TabIndex = 3;
            this.chart4.Text = "chart4";
            // 
            // chart5
            // 
            this.chart5.BackColor = System.Drawing.Color.LightGray;
            chartArea10.AxisX.MajorGrid.Enabled = false;
            chartArea10.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea10.AxisY.ScrollBar.Enabled = false;
            chartArea10.BackColor = System.Drawing.Color.LightGray;
            chartArea10.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.TileFlipX;
            chartArea10.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot;
            chartArea10.InnerPlotPosition.Auto = false;
            chartArea10.InnerPlotPosition.Height = 100F;
            chartArea10.InnerPlotPosition.Width = 95F;
            chartArea10.InnerPlotPosition.X = 5F;
            chartArea10.Name = "ChartArea1";
            chartArea10.Position.Auto = false;
            chartArea10.Position.Height = 100F;
            chartArea10.Position.Width = 100F;
            this.chart5.ChartAreas.Add(chartArea10);
            legend10.Name = "Legend1";
            this.chart5.Legends.Add(legend10);
            this.chart5.Location = new System.Drawing.Point(12, 446);
            this.chart5.Name = "chart5";
            series19.ChartArea = "ChartArea1";
            series19.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series19.Legend = "Legend1";
            series19.Name = "Master";
            series20.ChartArea = "ChartArea1";
            series20.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series20.Legend = "Legend1";
            series20.Name = "Slave";
            this.chart5.Series.Add(series19);
            this.chart5.Series.Add(series20);
            this.chart5.Size = new System.Drawing.Size(787, 102);
            this.chart5.TabIndex = 4;
            this.chart5.Text = "chart5";
            // 
            // master1
            // 
            this.master1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.master1.Location = new System.Drawing.Point(689, 64);
            this.master1.Name = "master1";
            this.master1.Size = new System.Drawing.Size(86, 21);
            this.master1.TabIndex = 5;
            // 
            // slave1
            // 
            this.slave1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.slave1.Location = new System.Drawing.Point(689, 88);
            this.slave1.Name = "slave1";
            this.slave1.Size = new System.Drawing.Size(86, 21);
            this.slave1.TabIndex = 6;
            // 
            // slave2
            // 
            this.slave2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.slave2.Location = new System.Drawing.Point(689, 199);
            this.slave2.Name = "slave2";
            this.slave2.Size = new System.Drawing.Size(86, 21);
            this.slave2.TabIndex = 8;
            // 
            // master2
            // 
            this.master2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.master2.Location = new System.Drawing.Point(689, 175);
            this.master2.Name = "master2";
            this.master2.Size = new System.Drawing.Size(86, 21);
            this.master2.TabIndex = 7;
            // 
            // slave3
            // 
            this.slave3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.slave3.Location = new System.Drawing.Point(689, 304);
            this.slave3.Name = "slave3";
            this.slave3.Size = new System.Drawing.Size(86, 21);
            this.slave3.TabIndex = 10;
            // 
            // master3
            // 
            this.master3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.master3.Location = new System.Drawing.Point(689, 280);
            this.master3.Name = "master3";
            this.master3.Size = new System.Drawing.Size(86, 21);
            this.master3.TabIndex = 9;
            // 
            // slave4
            // 
            this.slave4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.slave4.Location = new System.Drawing.Point(689, 411);
            this.slave4.Name = "slave4";
            this.slave4.Size = new System.Drawing.Size(86, 21);
            this.slave4.TabIndex = 12;
            // 
            // master4
            // 
            this.master4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.master4.Location = new System.Drawing.Point(689, 387);
            this.master4.Name = "master4";
            this.master4.Size = new System.Drawing.Size(86, 21);
            this.master4.TabIndex = 11;
            // 
            // slave5
            // 
            this.slave5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.slave5.Location = new System.Drawing.Point(689, 519);
            this.slave5.Name = "slave5";
            this.slave5.Size = new System.Drawing.Size(86, 21);
            this.slave5.TabIndex = 14;
            // 
            // master5
            // 
            this.master5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.master5.Location = new System.Drawing.Point(689, 495);
            this.master5.Name = "master5";
            this.master5.Size = new System.Drawing.Size(86, 21);
            this.master5.TabIndex = 13;
            // 
            // WavesDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 558);
            this.Controls.Add(this.slave5);
            this.Controls.Add(this.master5);
            this.Controls.Add(this.slave4);
            this.Controls.Add(this.master4);
            this.Controls.Add(this.slave3);
            this.Controls.Add(this.master3);
            this.Controls.Add(this.slave2);
            this.Controls.Add(this.master2);
            this.Controls.Add(this.slave1);
            this.Controls.Add(this.master1);
            this.Controls.Add(this.chart5);
            this.Controls.Add(this.chart4);
            this.Controls.Add(this.chart3);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.chart1);
            this.Name = "WavesDisplay";
            this.Text = "波形";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart4;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart5;
        private System.Windows.Forms.TextBox master1;
        private System.Windows.Forms.TextBox slave1;
        private System.Windows.Forms.TextBox slave2;
        private System.Windows.Forms.TextBox master2;
        private System.Windows.Forms.TextBox slave3;
        private System.Windows.Forms.TextBox master3;
        private System.Windows.Forms.TextBox slave4;
        private System.Windows.Forms.TextBox master4;
        private System.Windows.Forms.TextBox slave5;
        private System.Windows.Forms.TextBox master5;
    }
}