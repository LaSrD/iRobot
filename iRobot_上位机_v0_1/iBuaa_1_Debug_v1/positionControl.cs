using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Threading;

namespace iBuaa_1_Debug_v1
{
    public partial class positionControl : Form
    {
        double[] angle = new double[5];
        private string filename = "joint_record.txt";
        private Thread thread_position;
        private double[][] angle_control;
        private int length;
        public positionControl()
        { 
            InitializeComponent();
            txt_jointdata.ReadOnly = true;
            txt_jointdata.ContextMenuStrip = strip_cleartxt;
            thread_position = new Thread(new ThreadStart(position_send));

            thread_position.IsBackground = true;
            ReadToTxt(txt_jointdata, "joint_record.txt");

        }
        #region 单关节位置控制
        private void trBangle1_ValueChanged(object sender, EventArgs e)
        {
            txtAngle1.Text = (trBangle1.Value / 100.0f).ToString();
        }

        private void trBangle2_ValueChanged(object sender, EventArgs e)
        {
            txtAngle2.Text = (trBangle2.Value / 100.0f).ToString();
        }
        private void trBangle3_ValueChanged(object sender, EventArgs e)
        {
            txtAngle3.Text = (trBangle3.Value / 100.0f).ToString();
        }
        private void trBangle4_ValueChanged(object sender, EventArgs e)
        {
            txtAngle4.Text = (trBangle4.Value / 100.0f).ToString();
        }
        private void trBangle5_ValueChanged(object sender, EventArgs e)
        {
            txtAngle5.Text = (trBangle5.Value / 100.0f).ToString();
        }
        private void txt_tr(TextBox textBox,TrackBar trackBar)
        {
            float outrest = 0;
            bool tryans  = float.TryParse(textBox.Text, out outrest);
            if (tryans) {
                    int temp = System.Math.Min((int)(outrest * 100), trackBar.Maximum);
                    temp = System.Math.Max(temp, trackBar.Minimum);
                    trackBar.Value = temp;
                    angle[0] = double.Parse(txtAngle5.Text);
                    angle[1] = double.Parse(txtAngle4.Text);
                    angle[2] = double.Parse(txtAngle3.Text);
                    angle[3] = double.Parse(txtAngle2.Text);
                    angle[4] = double.Parse(txtAngle1.Text);
                }
        }

        private void txtAngle1_TextChanged(object sender, EventArgs e)
        {
            txt_tr(txtAngle1, trBangle1);

        }
        private void txtAngle2_TextChanged(object sender, EventArgs e)
        {
            txt_tr(txtAngle2, trBangle2);
        }
        private void txtAngle3_TextChanged(object sender, EventArgs e)
        {
            txt_tr(txtAngle3, trBangle3);
        }
        private void txtAngle4_TextChanged(object sender, EventArgs e)
        {
            txt_tr(txtAngle4, trBangle4);
        }

        private void txtAngle5_TextChanged(object sender, EventArgs e)
        {
            txt_tr(txtAngle5, trBangle5);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //iBuaaDebug.buaaDevice.AbsoAngleControl(angle,10);
        }
        #endregion

        #region record and play

        private void button2_Click(object sender, EventArgs e)
        {
            string temp = "";
            //temp += iBuaaDebug.buaaDevice.RobotAngle_WirstR.ToString() + "\t";
            //temp += iBuaaDebug.buaaDevice.RobotAngle_Wirst.ToString() + "\t";
            //temp += iBuaaDebug.buaaDevice.RobotAngle_LowArm.ToString() + "\t";
            //temp += iBuaaDebug.buaaDevice.RobotAngle_Uparm.ToString() + "\t";
            //temp += iBuaaDebug.buaaDevice.RobotAngle_Base.ToString() + "\n";
            wirteTofile(temp, filename);
            AppendText(txt_jointdata, temp + "\t"  + DateTime.Now.ToShortTimeString() + "\r\n");
        }
        private void 清除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileStream fs = File.Open(Application.StartupPath + @"\" + filename,FileMode.Truncate);
            fs.Close();
            ClearTextBox(txt_jointdata); 
        }
        // 从text 上获取角度来控制机械臂位置
        private void button3_Click(object sender, EventArgs e)
        {
            if (txt_jointdata.Text == string.Empty)
                return;
            angle_control = new double[txt_jointdata.Lines.Length][]; 
            for (int index = 0; index < txt_jointdata.Lines.Length; index += 2)
            {
                angle_control[index] = GetAngleFromTex(txt_jointdata, index);
                //while (!iBuaaDebug.buaaDevice.isSame(angle_control)) ;
            }
            length = angle_control.Length;
            if (!thread_position.IsAlive)
                thread_position.Start();
        }
        private void position_send()
        {
            int index = 0;
            while(true)
            {
                Thread.Sleep(50);
                //while (!iBuaaDebug.buaaDevice.isSame(angle_control[index]))
                //{
                //    iBuaaDebug.buaaDevice.AbsoAngleControl(angle_control[index], 10);
                //}
                if(index + 2 > length)
                    break;
                index += 2;
            }
        }
        #endregion

        static public void wirteTofile(string str,string filename)
        {
            StreamWriter streamWriter;
            FileInfo file = new FileInfo(filename);
            streamWriter = file.AppendText();
            streamWriter.WriteLine(str);
            streamWriter.Flush();
            streamWriter.Close();
        }
        private void ReadToTxt(TextBox text, string filename,string note = "")
        {
            StreamReader sr = new StreamReader(filename,false);
            text.AppendText(note);
            while(sr.Peek() != -1)
            {
                string angle = sr.ReadLine();
                text.AppendText(angle + "\r\n");           
            }
            sr.Close();
            
        }
        private void ClearTextBox(TextBox text)
        {
            text.Clear();
        }
        private void AppendText(TextBox text,string str)
        {
            text.AppendText(str);
        }
        private double[] GetAngleFromTex(TextBox textBox,int index)
        {
            double[] angle = { 0, 0, 0, 0, 0 };
            if (textBox.Text == string.Empty || index > textBox.Lines.Length)
                return angle;
            else
            {
                string temp = textBox.Lines[index];
                string[] str = temp.Split('\t');
                if(str.Length >= 5)
                {
                    angle[0] = double.Parse(str[0]);
                    angle[1] = double.Parse(str[1]);
                    angle[2] = double.Parse(str[2]);
                    angle[3] = double.Parse(str[3]);
                    angle[4] = double.Parse(str[4]);
                }
            }
            return angle;
        }
    }
}
 