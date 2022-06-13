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

/// <summary>
/// 设备状态窗口
/// </summary>
namespace iBuaa_1_Debug_v1
{
    public partial class DeviceStatic : Form
    {
        
        Thread updatedata; // 更新电机状态线程
        public static bool BaseRLock = false;
        public static bool UarmSLock = false;
        public static bool LarmSLock = false;
        public static bool LarmRLock = false;
        public static bool WristarmSLock = false;
        public static bool WristarmRLock = false;
        public DeviceStatic()
        {
            InitializeComponent();
            updatedata = new Thread(new ThreadStart(Updatedata));
            updatedata.Priority = ThreadPriority.Normal;
            updatedata.Start();
        }
        private void Updatedata()
        {
            
            while (true)
            {

                Thread.Sleep(50);
                if (iBuaaDebug.m_iRobot == null)
                    continue;
               
                Log(txtjointangle1, iBuaaDebug.m_iRobot.BaseRangle);
                Log(txtjointangle2, iBuaaDebug.m_iRobot.UarmSangle);
                Log(txtjointangle3, iBuaaDebug.m_iRobot.LarmSangle);
                Log(txtjointangle4, iBuaaDebug.m_iRobot.LarmRangle);
                Log(txtjointangle5, iBuaaDebug.m_iRobot.WristSangle);
                Log(txtjointangle6, iBuaaDebug.m_iRobot.WristRangle);
                //电流
                Log(txtcurrent1, iBuaaDebug.m_iRobot.Current[5]);
                Log(txtcurrent2, iBuaaDebug.m_iRobot.Current[4]);
                Log(txtcurrent3, iBuaaDebug.m_iRobot.Current[3]);
                Log(txtcurrent4, iBuaaDebug.m_iRobot.Current[2]);
                Log(txtcurrent5, iBuaaDebug.m_iRobot.Current[1]);
                Log(txtcurrent6, iBuaaDebug.m_iRobot.Current[0]);

                //解算转矩显示  20220317
                //Log(txtTorque_1, iBuaaDebug.buaaDevice.Toruqe_Base);
                //Log(txtTorque_2, iBuaaDebug.buaaDevice.Toruqe_UpArm);
                //Log(txtTorque_3, iBuaaDebug.buaaDevice.Toruqe_LowArm);
                //Log(txtTorque_4, iBuaaDebug.buaaDevice.Toruqe_Wirst);
                //Log(txtTorque_5, iBuaaDebug.buaaDevice. Toruqe_WirstR);

                //末端姿态
                Log(txtpx, iBuaaDebug.m_iRobot.px);
                Log(txtpy, iBuaaDebug.m_iRobot.py);
                Log(txtpz, iBuaaDebug.m_iRobot.pz);
                Log(txtroll, iBuaaDebug.m_iRobot.roll);
                Log(txtpitch, iBuaaDebug.m_iRobot.pitch);
                Log(txtyaw, iBuaaDebug.m_iRobot.yaw);
                // 力
                Log(txtfx, iBuaaDebug.unity_Force[0]);
                Log(txtfy, iBuaaDebug.unity_Force[1]);
                Log(txtfz, iBuaaDebug.unity_Force[2]);
            }
        }
        public static void Log(TextBox textBox,double data)
        {
            if(textBox.InvokeRequired)
            {
                string str = data.ToString("F2");
                textBox.Invoke(new Action<string>(s =>
                {
                    textBox.Text = str;
                }), str);
            }
            return;
        }
        public static void Log(TextBox textBox, string str)
        {
            if (str == null)
                return;
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new Action<string>(s =>
                {
                    textBox.Text = str;
                }), str);
            }
            return;
        }
        public static void Log(Label textBox, string str)
        {
            if (str == null)
                return;
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new Action<string>(s =>
                {
                    textBox.Text = str;
                }), str);
            }
            return;
        }
        public static void Log(Label textBox,Color color)
        {
            if (color == null)
                return;
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new Action<Color>(s =>
                {
                    textBox.ForeColor = color;
                }), color);
            }
            return;
        }
        /// <summary>
        /// 温度报警
        /// </summary>
        /// <param name="data"></param>
        /// <param name="label"></param>
        public static void Wanning(int data,Label label)
        {
            if(data > 50)
            {
                Thread.Sleep(100);
                if (label.ForeColor == Color.Green)
                    label.ForeColor = Color.Red;
                else
                {
                    label.ForeColor = Color.Green;
                }
            }
            else if(data > 30)
            {
                Thread.Sleep(100);
                if (label.ForeColor == Color.Green)
                    label.ForeColor = Color.Orange;
                else
                {
                    label.ForeColor = Color.Green;
                }
            }
            else
            {
                label.ForeColor = Color.Green;
            }
        }
        private void DeviceStatic_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            updatedata.Abort();
            System.Environment.Exit(System.Environment.ExitCode);
            this.Dispose();
            this.Close();
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void btnlock1_Click(object sender, EventArgs e)
        {
            if(!BaseRLock)
            {
                btnlock1.Text = "UnLock";
                iBuaaDebug.m_iRobot.LockBaseR();
            }
            else
            {
                btnlock1.Text = "Lock";
                iBuaaDebug.m_iRobot.UnLockBaseR();
            }
            BaseRLock = !BaseRLock;
        }

        private void btnlock2_Click(object sender, EventArgs e)
        {
            if (!UarmSLock)
            {
                btnlock2.Text = "UnLock";
                iBuaaDebug.m_iRobot.LockUarmS();
            }
            else
            {
                btnlock2.Text = "Lock";
                iBuaaDebug.m_iRobot.UnLockUarmS();
            }
            UarmSLock = !UarmSLock;
        }

        private void btnlock3_Click(object sender, EventArgs e)
        {
            if (!LarmSLock)
            {
                btnlock3.Text = "UnLock";
                iBuaaDebug.m_iRobot.LockLarmS();
            }
            else
            {
                btnlock3.Text = "Lock";
                iBuaaDebug.m_iRobot.UnLockLarmS();
            }
            LarmSLock = !LarmSLock;
        }

        private void btnlock4_Click(object sender, EventArgs e)
        {
            if (!LarmRLock)
            {
                btnlock4.Text = "UnLock";
                iBuaaDebug.m_iRobot.LockLarmR();
            }
            else
            {
                btnlock4.Text = "Lock";
                iBuaaDebug.m_iRobot.UnLockLarmR();
            }
            LarmRLock = !LarmRLock;
        }

        private void btnlock5_Click(object sender, EventArgs e)
        {
            if (!WristarmSLock)
            {
                btnlock5.Text = "UnLock";
                iBuaaDebug.m_iRobot.LockWristS();
            }
            else {
                btnlock5.Text = "Lock";
                iBuaaDebug.m_iRobot.UnLockWristS();
            }
            WristarmSLock = !WristarmSLock;
        }

        private void btnlock6_Click(object sender, EventArgs e)
        {
            if (!WristarmRLock)
            {
                btnlock6.Text = "UnLock";
                iBuaaDebug.m_iRobot.LockWristR();
            }
            else
            {
                btnlock6.Text = "Lock";
                iBuaaDebug.m_iRobot.UnLockWristR();
            }
            WristarmRLock = !WristarmRLock;
        }
    }
}
