using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iBuaa_1_Debug_v1
{
    public partial class WavesDisplay : Form
    {
        int frame1 = 0;
        public WavesDisplay()
        {
            InitializeComponent();
          //  this.timer1.Start();
        }
        private void chartDisplay(System.Windows.Forms.DataVisualization.Charting.Chart chart, double master,double slave, int frame,TextBox textBox1, TextBox textBox2)
        {
            //chart.ChartAreas["ChartArea1"].AxisX.Maximum = 1000;
            chart.ChartAreas["ChartArea1"].AxisX.ScaleView.Size = 100;
            chart.ChartAreas["ChartArea1"].AxisX.ScaleView.Position = chart.Series["Master"].Points.Count - 100;

            chart.ChartAreas["ChartArea1"].AxisX.MajorGrid.IntervalOffset = 100;
            chart.Series["Master"].Points.AddXY(frame, master);
            chart.Series["Slave"].Points.AddXY(frame, slave);
            textBox1.Text = master.ToString("F2");
            textBox2.Text = slave.ToString("F2");

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (frame1++ > 0xffff)
            {
                frame1 = 0;
            };
            //chartDisplay(chart1, iBuaaDebug.buaaDevice.RobotAngle_Base, Teleoperation.RemoteAngle_[4], frame1,master1,slave1);
            //chartDisplay(chart2, iBuaaDebug.buaaDevice.RobotAngle_Uparm, Teleoperation.RemoteAngle_[3], frame1, master2, slave2);
            //chartDisplay(chart3, iBuaaDebug.buaaDevice.RobotAngle_LowArm, Teleoperation.RemoteAngle_[2], frame1, master3, slave3);
            //chartDisplay(chart4, iBuaaDebug.buaaDevice.RobotAngle_Wirst, Teleoperation.RemoteAngle_[1], frame1, master4, slave4);
            //chartDisplay(chart5, iBuaaDebug.buaaDevice.RobotAngle_WirstR, Teleoperation.RemoteAngle_[0], frame1, master5, slave5);
        }
    }
}
