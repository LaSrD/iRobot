using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iBuaa_1_Debug_v1
{
    public partial class ForceRender : Form
    {
        public static double[] Winform_force = new double[3];  // 控件控制的力 fx，fy，fz
        private Thread updatedata;
        public ForceRender()
        {
            InitializeComponent();
            updatedata = new Thread(new ThreadStart(Updatedata));
            updatedata.Start();


        }
        /// <summary>
        /// 控件信息更新线程
        /// </summary>
        private void Updatedata()
        {
            while (true)
            {
                if (iBuaaDebug.m_iRobot == null)
                    continue;
                Thread.Sleep(200);
                DeviceStatic.Log(txtjointangle1, iBuaaDebug.m_iRobot.BaseRangle);
                DeviceStatic.Log(txtjointangle2, iBuaaDebug.m_iRobot.UarmSangle);
                //DeviceStatic.Log(txtjointangle3, iBuaaDebug.m_iRobot.LarmSangle);
                DeviceStatic.Log(txtjointangle4, iBuaaDebug.m_iRobot.LarmRangle);
                DeviceStatic.Log(txtjointangle5, iBuaaDebug.m_iRobot.WristSangle);
                DeviceStatic.Log(txtjointangle6, iBuaaDebug.m_iRobot.WristRangle);

                //电流
                DeviceStatic.Log(txtcurrent1, iBuaaDebug.m_iRobot.Current[5]);
                DeviceStatic.Log(txtcurrent2, iBuaaDebug.m_iRobot.Current[4]);
               // DeviceStatic.Log(txtcurrent3, iBuaaDebug.m_iRobot.Current[3]);
                DeviceStatic.Log(txtcurrent4, iBuaaDebug.m_iRobot.Current[2]);
                DeviceStatic.Log(txtcurrent5, iBuaaDebug.m_iRobot.Current[1]);
                DeviceStatic.Log(txtcurrent6, iBuaaDebug.m_iRobot.Current[0]);

                ////解算转矩显示  20220317
                //DeviceStatic.Log(txtTorque_1, iBuaaDebug.buaaDevice.Toruqe_Base);
                //DeviceStatic.Log(txtTorque_2, iBuaaDebug.buaaDevice.Toruqe_UpArm);
                //DeviceStatic.Log(txtTorque_3, iBuaaDebug.buaaDevice.Toruqe_LowArm);
                //DeviceStatic.Log(txtTorque_4, iBuaaDebug.buaaDevice.Toruqe_Wirst);
                //DeviceStatic.Log(txtTorque_5, iBuaaDebug.buaaDevice.Toruqe_WirstR);

                // 力
            }
        }

        private void traTotxt(TextBox textBox,TrackBar trackBar)
        {
            float outres = 0;
            bool isfloat = float.TryParse(textBox.Text, out outres);
            if(isfloat)
            {
                int temp = System.Math.Min((int)(outres * 1000),trackBar.Maximum);
                temp = System.Math.Max(temp, trackBar.Minimum);
                trackBar.Value = temp;
               // textBox.Text = (temp / 1000.0f).ToString();
                Winform_force[0] = txtTodouble(txtfx);
                Winform_force[1] = txtTodouble(txtfy);
                Winform_force[2] = txtTodouble(txtfz);

               // MessageBox.Show("力反馈开始");
            }
        }
        private double txtTodouble(TextBox textBox)
        {
            double result;
            if(double.TryParse(textBox.Text,out result))
            {
                return result;
            }
            return 0.0;
            
        }
        private void trackBar_fx_ValueChanged(object sender, EventArgs e)
        {
            txtfx.Text = (trackBar_fx.Value / 1000.0f).ToString();
        }

        private void trackBar_fy_ValueChanged(object sender, EventArgs e)
        {
            txtfy.Text = (trackBar_fy.Value / 1000.0f).ToString();
        }
        private void trackBar_fz_ValueChanged(object sender, EventArgs e)
        {
            txtfz.Text = (trackBar_fz.Value / 1000.0f).ToString();
        }

        private void txtfx_TextChanged(object sender, EventArgs e)
        {
            traTotxt(txtfx, trackBar_fx);
        }

        private void txtfy_TextChanged(object sender, EventArgs e)
        {
            traTotxt(txtfy, trackBar_fy);
        }

        private void txtfz_TextChanged(object sender, EventArgs e)
        {
            traTotxt(txtfz, trackBar_fz);
        }
    }
}
