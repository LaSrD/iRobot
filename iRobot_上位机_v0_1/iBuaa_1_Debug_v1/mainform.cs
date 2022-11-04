using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MVSDK;
using System.Drawing.Imaging;
using IRobotDLLforCsharp;

namespace iBuaa_1_Debug_v1
{
    public partial class iBuaaDebug : Form
    {
        /// <summary>
        /// 设备状态窗口
        /// </summary>
        public static DeviceStatic device = new DeviceStatic();
        public static ForceRender forceRender = new ForceRender();
        WavesDisplay Wavesdisplay = new WavesDisplay();
        positionControl positionControl = new positionControl();
        Teleoperation teleoperation = new Teleoperation();
        private float x; // 窗体x
        private float y;// 窗体y
        private bool cameraform = false; // 摄像机窗体显示与否
        private bool camerastop = false; // 摄像机开始停止
        static protected pfnCameraGrabberFrameCallback m_FrameCallback;

        /// <summary>
        /// 
        /// </summary>
        public static IROBOT m_iRobot;
        //public static PortControl portControl = new PortControl();
        private Thread TranmitThread2;      //控制指令发送线程
        private Thread unitySeverSendThread;//虚拟环境数据交换
        private Thread PortReadThread;      //串口接收线程
        private Thread updatedata;          //控件信息更新
        private string Handleinfo = string.Empty;
        private bool isBaseRLock = false;
        private bool isLock = false;
        private bool SceneChangeFlag = false;   // 演示场景切换标识
        private bool SceneFlag = true;          // 场景标识， true 为 机械臂演示场景 、 false 为 力反馈演示场景
        public static bool deviceEnable = false; //
        private Object handleinfoLock = new Object();  // 锁
        /// <summary>
        /// TCP 服务器
        /// </summary>
        TCPserver server = new TCPserver();
        public static double[] unity_Force = { 0, 0, 0 };                       // 接收回传的力数据
        /// <summary>
        /// unity 窗口
        /// </summary>
        EmbeddedExeTool embeddedExeTool = new EmbeddedExeTool();
        public iBuaaDebug()
        {
            InitializeComponent();
            x = this.Width;
            y = this.Height;
            SetControls.SetTag(this);

            //初始化设备

            //串口
            getsystemport();

            device.TopLevel = false;
            device.Dock = System.Windows.Forms.DockStyle.Fill;
            device.FormBorderStyle = FormBorderStyle.None;
            menu_panel.Controls.Add(device);
            device.Show();

            SceneChangeFlag = false;
            SceneFlag = true;

            //相机
            m_FrameCallback = m_FrameCallback = new pfnCameraGrabberFrameCallback(CameraGrabberFrameCallback);
        }
        #region 获取系统com
        private void getsystemport()
        {
            string[] str = SerialPort.GetPortNames();
            if(str == null)
            {
                txtInfo.Text = string.Format("{0}\r\t{1}", txtInfo.Text, "没找到串口");
                return;
            }
            else
            {
                foreach(string s in str)
                {
                    comboport.Items.Add(s);
                    cbbox_handle.Items.Add(s); 
                }
            }
        }
        #endregion
        private void 设备状态ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wavepan.Visible = false;
            menu_panel.Controls.Clear();
            device.TopLevel = false;
            device.Dock = System.Windows.Forms.DockStyle.Fill;
            device.FormBorderStyle = FormBorderStyle.None;
            menu_panel.Controls.Add(device);
            device.Show();
        }

        private void 位置控制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wavepan.Visible = false;
            menu_panel.Controls.Clear();
            positionControl.TopLevel = false;
            positionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            positionControl.FormBorderStyle = FormBorderStyle.None;
            menu_panel.Controls.Add(positionControl);
            positionControl.Show();
        }
        /// <summary>
        /// 连接设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openDevice_Click(object sender, EventArgs e)
        {
            #region 设备连接
            if (comboport.Text == String.Empty)
                return;
            m_iRobot = new IROBOT(comboport.Text);
            string msg = m_iRobot.Connect();
            if(msg == String.Empty)
            {
                labColorconnect.ForeColor = Color.Green;
                labInfoconnect.Text = "Connected";
                deviceEnable = true;
               
            }
            if (deviceEnable != true)
                return;

            // 设备信息
            m_iRobot.Info();
            txtseries.Text = m_iRobot.Serial_Num;
            txtprocotol.Text = m_iRobot.Procotol_Type;
            txthwtype.Text = m_iRobot.HW_Type;
            txtcrtlnum.Text = m_iRobot.Ctrl_Num;

            TranmitThread2 = new Thread(new ThreadStart(TranmitThreadentrance2));
            TranmitThread2.Priority = ThreadPriority.AboveNormal;
            TranmitThread2.IsBackground = true;
            TranmitThread2.Start();

            unitySeverSendThread = new Thread(new ThreadStart(unitySeverSendEntrance));
            unitySeverSendThread.Priority = ThreadPriority.Normal;
            unitySeverSendThread.IsBackground = true;
            unitySeverSendThread.Start();

            updatedata = new Thread(new ThreadStart(Updatedata));
            updatedata.Priority = ThreadPriority.Lowest;
            updatedata.IsBackground = true;
            updatedata.Start();

            // portconnect();


            //开启与unity通讯的服务器
            server.SocketServer(65532);
            #endregion

        }
        /// <summary>
        /// 力控制线程
        /// </summary>
        private void TranmitThreadentrance2()
        {
            while(deviceEnable == true)
            {
                //接收回传的三维力数据解码
                forcedecode();
                m_iRobot.State();
                m_iRobot.Angle();
                m_iRobot.Pose();
                
                double force_x = unity_Force[0] + ForceRender.Winform_force[0];
                double force_y = unity_Force[1] + ForceRender.Winform_force[1];
                double force_z = unity_Force[2] + ForceRender.Winform_force[2];

                if (Handleinfo != string.Empty)
                {
                    lock(handleinfoLock){
                        isLock = !isLock;
                        Handleinfo = string.Empty;
                    }
                    
                }
                //if (isLock)
                //{ 
                //    m_iRobot.Lock();
                //}
                //else
                //{
                //    m_iRobot.UnLock();
                //}
                m_iRobot.OutputForce(force_x, force_y, force_z,0,0,0);
            }
        }
        /// <summary>
        /// 接收unity回传数据 并解码
        /// 
        /// 
        /// 
        /// </summary>
        /// <returns></returns>
        private void forcedecode()
        {
            byte[] datas = server.Recivebyte;
            if(datas != null && datas[0] == 0xFD)
            {
                short temp = 0;
                for (int i = 2; i >= 1; --i)
                {
                    temp <<= 8;
                    temp |= (short)datas[i];
                }
                unity_Force[0] = temp / 1000.0f;

                temp = 0;
                for (int i = 4; i >= 3; --i)
                {
                    temp <<= 8;
                    temp |= (short)datas[i];
                }
                unity_Force[1] = temp  / 1000.0f;

                temp = 0;
                for (int i = 6; i >= 5; --i)
                {
                    temp <<= 8;
                    temp |= (short)datas[i];
                }
                unity_Force[2] = temp / 1000.0f ;
            }
            return;
        }
        /// <summary>
        /// unity 通信线程
        /// </summary>
        private void unitySeverSendEntrance()
        {
            while(deviceEnable == true)
            {
                Thread.Sleep(50);
                //上电信息
                byte[] datas = { 0xfc, 0x01 };
                server.ServerSendData(datas);
                //锁定信息
                byte[] datas_lock = { 0xEF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                if (DeviceStatic.BaseRLock)
                    datas_lock[1] = 0x01;
                if (DeviceStatic.UarmSLock)
                    datas_lock[2] = 0x01;
                if (DeviceStatic.LarmSLock)
                    datas_lock[3] = 0x01;
                if (DeviceStatic.LarmRLock)
                    datas_lock[4] = 0x01;
                if (DeviceStatic.WristarmSLock)
                    datas_lock[5] = 0x01;
                if (DeviceStatic.WristarmRLock)
                    datas_lock[6] = 0x01;
                server.ServerSendData(datas_lock);
                double[] angle = {  m_iRobot.BaseRangle,
                                    m_iRobot.UarmSangle,
                                    0 ,//m_iRobot.LarmSangle,
                                    m_iRobot.LarmRangle,
                                    m_iRobot.WristSangle,
                                    m_iRobot.WristRangle };
                datas = EnDiscode.UnityAngleEncode(angle);
                server.ServerSendData(datas);
            }       
        }
        /// <summary>
        /// 串口读取线程
        /// </summary>
        private void PortReadThreadint()
        {
            while(m_iRobot.isHandleConnect())
            {
                Handleinfo = m_iRobot.GetHandleInfo();
                if (txtInfo.InvokeRequired)
                {
                    txtInfo.Invoke(new Action<string>(s =>
                    {
                        string temp = string.Empty;
                        if (Handleinfo != "10000")
                            temp = "按键按下";
                        txtInfo.Text = temp;
                    }), Handleinfo);
                }
            }
        }
        private void Updatedata()
        {
            while (true)
            {
                Thread.Sleep(200);
                //温度
                DeviceStatic.Log(txttemp1, iBuaaDebug.m_iRobot.Temperature[5]);
                DeviceStatic.Wanning((int)iBuaaDebug.m_iRobot.Temperature[5], tempwanning1);
                DeviceStatic.Log(txttemp2, iBuaaDebug.m_iRobot.Temperature[4]);
                DeviceStatic.Wanning((int)iBuaaDebug.m_iRobot.Temperature[4], tempwanning2);
                DeviceStatic.Log(txttemp3, iBuaaDebug.m_iRobot.Temperature[3]);
                DeviceStatic.Wanning((int)iBuaaDebug.m_iRobot.Temperature[3], tempwanning3);
                DeviceStatic.Log(txttemp4, iBuaaDebug.m_iRobot.Temperature[2]);
                DeviceStatic.Wanning((int)iBuaaDebug.m_iRobot.Temperature[2], tempwanning4);
                DeviceStatic.Log(txttemp5, iBuaaDebug.m_iRobot.Temperature[1]);
                DeviceStatic.Wanning((int)iBuaaDebug.m_iRobot.Temperature[1], tempwanning5);
                DeviceStatic.Log(txttemp6, iBuaaDebug.m_iRobot.Temperature[0]);
                DeviceStatic.Wanning((int)iBuaaDebug.m_iRobot.Temperature[0], tempwanning6);

                if (!isLock)
                {
                    DeviceStatic.Log(label6, "操作状态：转矩随动");
                    DeviceStatic.Log(labbut1, "Operation");
                    DeviceStatic.Log(labbut2, "Mode!");
                    DeviceStatic.Log(labbut3, "⭕");
                    DeviceStatic.Log(labbut1, Color.FromArgb(0, 192, 0));
                    DeviceStatic.Log(labbut2, Color.FromArgb(0, 192, 0));
                    DeviceStatic.Log(labbut3, Color.FromArgb(0, 192, 0));
                }
                else
                {
                    DeviceStatic.Log(label6, "操作状态：位置锁定");
                    DeviceStatic.Log(labbut1, "Position");
                    DeviceStatic.Log(labbut2, "LOCK!");
                    DeviceStatic.Log(labbut3, "⛔");
                    DeviceStatic.Log(labbut1, Color.Red);
                    DeviceStatic.Log(labbut2, Color.Red);
                    DeviceStatic.Log(labbut3, Color.Red);
                }
            }
        }
        private void button_changeName_Click(object sender, EventArgs e)
        {



        }
        private void iBuaaDebug_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(updatedata != null && updatedata.IsAlive)
                updatedata.Abort();
            embeddedExeTool.KillEXE("iBuaa_1_Debug_v0.1");
            if (deviceEnable == true && unitySeverSendThread.IsAlive)
                unitySeverSendThread.Abort();
            System.Environment.Exit(System.Environment.ExitCode);
            this.Dispose();
            this.Close();
        }

        private void closeDevice_Click(object sender, EventArgs e)
        {
             m_iRobot.Close();
            labColorconnect.ForeColor = Color.Red;
            labInfoconnect.Text = "DisConnect";
        }

        private void 双机遥操作模式ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wavepan.Visible = false;
            menu_panel.Controls.Clear();
            teleoperation.TopLevel = false;
            teleoperation.Dock = System.Windows.Forms.DockStyle.Fill;
            teleoperation.FormBorderStyle = FormBorderStyle.None;
            menu_panel.Controls.Add(teleoperation);
            teleoperation.Show();
        }

        private void button_zhongli_Click(object sender, EventArgs e)
        {

            //定义一个文件打开控件
            OpenFileDialog ofd = new OpenFileDialog();
            //设置打开对话框的初始目录，默认目录为exe运行文件所在的路径
            ofd.InitialDirectory = Application.StartupPath;
            //设置打开对话框的标题
            ofd.Title = "请选择要打开的文件";
            //设置打开对话框可以多选
            ofd.Multiselect = true;
            //设置对话框打开的文件类型
            ofd.Filter = "|*.exe|所有文件|*.*";
            //设置文件对话框当前选定的筛选器的索引
            ofd.FilterIndex = 2;
            //设置对话框是否记忆之前打开的目录
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //获取用户选择的文件完整路径
                string filePath = ofd.FileName;
                //获取对话框中所选文件的文件名和扩展名，文件名不包括路径
                string fileName = ofd.SafeFileName;
                embeddedExeTool.LoadEXE(unitypanel, filePath);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            embeddedExeTool.KillEXE("iRobot_ar");
        }

        private void txttemp5_TextChanged(object sender, EventArgs e)
        {

        }

        private void txttemp2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txttemp1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txttemp3_TextChanged(object sender, EventArgs e)
        {

        }

        private void txttemp4_TextChanged(object sender, EventArgs e)
        {

        }

        //private void portconnect()
        //{
        //    string portname = comboport.Text;
        //    PORTSTATE pORTSTATE = PORTSTATE.CONNECT;
        //    try
        //    {
        //        pORTSTATE = portControl.Portconnect(portname);
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }

        //    if(pORTSTATE == PORTSTATE.CONNECT)
        //    {
        //        labhandle.ForeColor = Color.Green;
        //        labhandle2.Text = "HandleConnect";
        //        //string temp = txtInfo.Text;
        //        //txtInfo.Text = string.Format("{0}\r\t{1}", temp, "手柄连接成功");
        //    }
        //    else if (pORTSTATE == PORTSTATE.DISCONNECT)
        //    {
        //        labhandle.ForeColor = Color.Red;
        //        labhandle2.Text = "HandleDisConnect";
        //    }

        //}


        private void 力反馈演示ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            wavepan.Visible = false;
            menu_panel.Controls.Clear();
            forceRender.TopLevel = false;
            forceRender.Dock = System.Windows.Forms.DockStyle.Fill;
            forceRender.FormBorderStyle = FormBorderStyle.None;
            menu_panel.Controls.Add(forceRender);
            forceRender.Show();
        }

        private void 波形显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wavepan.Visible = true;
            wavepan.Controls.Clear();
            Wavesdisplay.TopLevel = false;
            Wavesdisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            Wavesdisplay.FormBorderStyle = FormBorderStyle.None;
            wavepan.Controls.Add(Wavesdisplay);
            Wavesdisplay.Show();
        }

        private void iBuaaDebug_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(cameraform)
            {
                this.Size = new Size(1160, 690);
                button2.Location = new Point(1132, 12);
                button2.Text = ">";
            }
            else
            {
                this.Size = new Size(1830, 690);
                button2.Location = new Point(1802, 12);
                button2.Text = "<";
            }
            cameraform = !cameraform;
        }
        #region 摄像机
        private void 打开相机ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(camera.InitCamera())
            {
                MvApi.CameraGrabber_SetRGBCallback(camera.m_Grabber, m_FrameCallback, IntPtr.Zero);
                timer_camera.Start();
            }
        }



        private void 停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(camerastop)
            {
                停止ToolStripMenuItem.Text = "播放";
                camera.StopCamera();
            }
            else
            {
                停止ToolStripMenuItem.Text = "停止";
                camera.StartCamera();
            }
            camerastop = !camerastop;
        }

        private void 相机设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            camera.CreatCamerSetPage();
        }
        private void CameraGrabberFrameCallback(
            IntPtr Grabber,
            IntPtr pFrameBuffer,
            ref tSdkFrameHead pFrameHead,
            IntPtr Context)
        {
            // 数据处理回调

            // 由于黑白相机在相机打开后设置了ISP输出灰度图像
            // 因此此处pFrameBuffer=8位灰度数据
            // 否则会和彩色相机一样输出BGR24数据

            // 彩色相机ISP默认会输出BGR24图像
            // pFrameBuffer=BGR24数据

            // 执行一次GC，释放出内存
            GC.Collect();

            // 由于SDK输出的数据默认是从底到顶的，转换为Bitmap需要做一下垂直镜像
            MvApi.CameraFlipFrameBuffer(pFrameBuffer, ref pFrameHead, 1);

            int w = pFrameHead.iWidth;
            int h = pFrameHead.iHeight;
            Boolean gray = (pFrameHead.uiMediaType == (uint)MVSDK.emImageFormat.CAMERA_MEDIA_TYPE_MONO8);
            Bitmap Image = new Bitmap(w, h,
                gray ? w : w * 3,
                gray ? PixelFormat.Format8bppIndexed : PixelFormat.Format24bppRgb,
                pFrameBuffer);

            // 如果是灰度图要设置调色板
            if (gray)
            {
                Image.Palette = camera.m_GrayPal;
            }

            this.Invoke((EventHandler)delegate
            {
                cameraPir.Image = Image;
                cameraPir.Refresh();
            });
        }
        #endregion

        private void updateList_Click(object sender, EventArgs e)
        {
            if (cbbox_handle.Text == String.Empty)
                return;
            string msg = m_iRobot.ConnectToHandle(cbbox_handle.Text);
            if (msg == String.Empty)
            {
                labhandle.ForeColor = Color.Green;
                labhandle2.Text = "Connected";
                txtInfo.Text = "手柄连接成功";


                PortReadThread = new Thread(new ThreadStart(PortReadThreadint));
                PortReadThread.IsBackground = true;
                PortReadThread.Start();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            byte[] data = { 0xff };
            server.ServerSendData(data);
        }
    }
}
