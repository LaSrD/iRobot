using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
/// <summary>
/// 主端作为服务器
/// </summary>
namespace iBuaa_1_Debug_v1
{
    public partial class Teleoperation : Form
    {
        TCPserver server = new TCPserver();
        TCPclient client = new TCPclient();
        Thread severR;  //
        Thread clientR;
        Thread updatedata; //控件信息更新
        bool serverStatr = false; //工作在主端模式
        bool clientStatr = false; //工作在从端模式
        /// <summary>
        /// 接收的主端从端数据
        /// </summary>
        double[] RemoteAngle = { 0, 0, 0, 0, 0 };
        public static double[] RemoteAngle_ = { 0, 0, 0, 0, 0 };  // 静态对象
        string[] DeviceInfo = new string[4];
        public Teleoperation()
        {
            InitializeComponent();
            comboxModel.Items.Add("主端模式");
            comboxModel.Items.Add("从端模式");
            txtbdip.Text = server.GetLocalIp();

            updatedata = new Thread(new ThreadStart(Updatedata));
            updatedata.IsBackground = true;
         //   updatedata.Start();

            txtMip.Text = "192.168.31.203";
            txtMport.Text = "65532";
        }

        private void comboxModel_TextChanged(object sender, EventArgs e)
        {
            btonStart.Enabled = true;
            if (comboxModel.Text == "主端模式")
            {
                txtSport.Text = "65532";
                labZHUport.ForeColor = Color.Gray;
                labZHUip.ForeColor = Color.Gray;
                txtSport.Enabled = true;
                txtMport.Enabled = false;
                txtMip.Enabled = false;
                btonStart.Text = "启动";
            }
            else
            {
                labZHUport.ForeColor = Color.Black;
                labZHUip.ForeColor = Color.Black;
                txtSport.Enabled = false;
                txtMport.Enabled = true;
                txtMip.Enabled = true;
                btonStart.Text = "连接主端";
            }
        }
        /// <summary>
        /// 开始按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btonStart_Click(object sender, EventArgs e)
        {
            if(comboxModel.Text == "主端模式")
            {
                server.SocketServer(int.Parse(txtSport.Text));
                labstatie.Text = "✔";
                labstatie.ForeColor = Color.Green;
                serverStatr = true;

                groupBox3.Text = "从机信息";
                labconnect.Text = "主从通信连接成功";
                severR = new Thread(new ThreadStart(severRin));
                severR.Priority = ThreadPriority.Highest;
                severR.Start();
            }
            else // 从端模式
            {

                client.SocketConnect(txtMip.Text, int.Parse(txtMport.Text));
                labstatie.Text = "✔";
                labstatie.ForeColor = Color.Green;
                clientStatr = true;

                groupBox3.Text = "主机信息";
                labconnect.Text = "主从通信连接成功";
                clientR = new Thread(new ThreadStart(clientRin));
                clientR.Priority = ThreadPriority.Highest;
                clientR.Start();

                //client.SendMsg(EnDiscode.SerialNumEncode(iBuaaDebug.buaaDevice.DeviceInfo.Serial_Num));
                //client.SendMsg(EnDiscode.ProcotolEncode(iBuaaDebug.buaaDevice.DeviceInfo.Procotol_type));
                //client.SendMsg(EnDiscode.HwTypeEncode(iBuaaDebug.buaaDevice.DeviceInfo.hw_type));
                //client.SendMsg(EnDiscode.CtrlNumEncode(iBuaaDebug.buaaDevice.DeviceInfo.Ctrl_Num));
            }
        }
        /// <summary>
        /// 服务器（主端）控制线程
        /// </summary>
        private void severRin()
        {
            while (true)
            {
                #region 设备信息发送显示
                //Thread.Sleep(100);
                if (server.Recivebyte == null)
                    continue;
                //设备信息
                if (server.Recivebyte[0] == 0xC1 || server.Recivebyte[0] == 0xC2 ||
                    server.Recivebyte[0] == 0xC3 || server.Recivebyte[0] == 0xC4)
                {
                    DeviceInfo[0] = EnDiscode.SerialNumDiscode(server.Recivebyte) == null ? DeviceInfo[0] : EnDiscode.SerialNumDiscode(server.Recivebyte);
                    DeviceInfo[1] = EnDiscode.ProcotolDiscode(server.Recivebyte) == null ? DeviceInfo[1] : EnDiscode.ProcotolDiscode(server.Recivebyte);
                    DeviceInfo[2] = EnDiscode.HwTypeDiscode(server.Recivebyte)== null ? DeviceInfo[2] : EnDiscode.HwTypeDiscode(server.Recivebyte); 
                    DeviceInfo[3] = EnDiscode.CtrlNumDiscode(server.Recivebyte) == null ? DeviceInfo[3] : EnDiscode.CtrlNumDiscode(server.Recivebyte);
                    //server.ServerSendData(EnDiscode.SerialNumEncode(iBuaaDebug.buaaDevice.DeviceInfo.Serial_Num));
                    //server.ServerSendData(EnDiscode.ProcotolEncode(iBuaaDebug.buaaDevice.DeviceInfo.Procotol_type));
                    //server.ServerSendData(EnDiscode.HwTypeEncode(iBuaaDebug.buaaDevice.DeviceInfo.hw_type));
                    //server.ServerSendData(EnDiscode.CtrlNumEncode(iBuaaDebug.buaaDevice.DeviceInfo.Ctrl_Num));
                    server.Recivebyte = null;
                }
                #endregion
                
                #region 接收从端角度发送控制指令
                //接收客服端发送的角度数据
                double[] Slaveangle = EnDiscode.JointAngleDiscode(server.Recivebyte);
                if (Slaveangle != null)
                {
                    RemoteAngle = Slaveangle;
                    RemoteAngle_ = RemoteAngle;
                }
                //if(!iBuaaDebug.buaaDevice.isSame(Slaveangle))
                //{
                //    double[] temp = { };
                //    iBuaaDebug.buaaDevice.GetRobotAngle();
                //    byte[] data = EnDiscode.JointAngleEncode(iBuaaDebug.buaaDevice.RobotAnglesToArray());
                //    server.ServerSendData(data);
                //}
                #endregion
            }
        }
        /// <summary>
        /// 客服端（从端）控制线程
        /// </summary>
        private void clientRin()
        {
            while (true)
            {
                //Thread.Sleep(100);
                #region 从端反馈角度给主端
                double[] Slaveangle =
                {
                    //iBuaaDebug.buaaDevice.RobotAngle_WirstR,
                    //iBuaaDebug.buaaDevice.RobotAngle_Wirst,
                    //iBuaaDebug.buaaDevice.RobotAngle_LowArm,
                    //iBuaaDebug.buaaDevice.RobotAngle_Uparm,
                    //iBuaaDebug.buaaDevice.RobotAngle_Base
                };
                byte[] data = EnDiscode.JointAngleEncode(Slaveangle);
                client.SendMsg(data);
                #endregion

                #region 主端设备信息
                if (client.ReciveData == null)
                    continue;

                if (client.ReciveData[0] == 0xC1 || client.ReciveData[0] == 0xC2 ||
                     client.ReciveData[0] == 0xC3 || client.ReciveData[0] == 0xC4)
                {
                    DeviceInfo[0] = EnDiscode.SerialNumDiscode(client.ReciveData) == null ? DeviceInfo[0] : EnDiscode.SerialNumDiscode(client.ReciveData);
                    DeviceInfo[1] = EnDiscode.ProcotolDiscode(client.ReciveData) == null ? DeviceInfo[1] : EnDiscode.ProcotolDiscode(client.ReciveData);
                    DeviceInfo[2] = EnDiscode.HwTypeDiscode(client.ReciveData) == null ? DeviceInfo[2] : EnDiscode.HwTypeDiscode(client.ReciveData);
                    DeviceInfo[3] = EnDiscode.CtrlNumDiscode(client.ReciveData) == null ? DeviceInfo[3] : EnDiscode.CtrlNumDiscode(client.ReciveData);
                }
                #endregion

                double[] Jointangle = EnDiscode.JointAngleDiscode(client.ReciveData); // 接收主端数据
                if (Jointangle != null && Jointangle.Length == 5) {

                    RemoteAngle = Jointangle; // 更新对方角度
                    RemoteAngle_ = RemoteAngle;
                    //从臂减速比为8
                    //iBuaaDebug.buaaDevice.AbsoAngleControl(Jointangle,10);
                } 
            }
        }

        private void Teleoperation_FormClosing(object sender, FormClosingEventArgs e)
        {
            severR.Abort();
            clientR.Abort();
            updatedata.Abort();
            this.Dispose();
        }

        private void send_Click(object sender, EventArgs e)
        {
            byte[] data = Encoding.Default.GetBytes(txtSMsg.Text);
            if (CheckHEX.Checked == true)
            {
                if (serverStatr)
                {

                }
                else if (clientStatr)
                {

                }
            }
            else
            {
                if (serverStatr)
                {

                    server.ServerSendData(data);
                }
                else if (clientStatr)
                {
                    client.SendMsg(data);
                }
            }
        }

        private void txtSMsg_TextChanged(object sender, EventArgs e)
        {
            if (txtSMsg.Text != null)
                send.Enabled = true;
            else
                send.Enabled = false;
                    
        }
        //控件更新线程入口
        private void Updatedata()
        {
            while (true)
            {
                if (iBuaaDebug.m_iRobot == null)
                    continue;
                Thread.Sleep(100);
                //设备信息
                //DeviceStatic.Log(txtseries, DeviceInfo[0]);
                //DeviceStatic.Log(txtprocotol, DeviceInfo[1]);
                //DeviceStatic.Log(txthwtype, DeviceInfo[2]);
                //DeviceStatic.Log(txtcrtlnum, DeviceInfo[3]);

                DeviceStatic.Log(txtjointangle1, iBuaaDebug.m_iRobot.BaseRangle);
                DeviceStatic.Log(txtjointangle2, iBuaaDebug.m_iRobot.UarmSangle);
                DeviceStatic.Log(txtjointangle3, iBuaaDebug.m_iRobot.LarmSangle);
                DeviceStatic.Log(txtjointangle4, iBuaaDebug.m_iRobot.LarmRangle);
                DeviceStatic.Log(txtjointangle5, iBuaaDebug.m_iRobot.WristSangle);
                DeviceStatic.Log(txtjointangle6, iBuaaDebug.m_iRobot.WristRangle);

                //对方
                DeviceStatic.Log(txtSangle1, this.RemoteAngle[4]);
                DeviceStatic.Log(txtSangle2, this.RemoteAngle[3]);
                DeviceStatic.Log(txtSangle3, this.RemoteAngle[2]);
                DeviceStatic.Log(txtSangle4, this.RemoteAngle[1]);
                DeviceStatic.Log(txtSangle5, this.RemoteAngle[0]);
            }
        }
    }

}
