using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iBuaa_1_Debug_v1
{
    class TCPserver
    {
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> CilentproxSocketlist = new List<Socket>();  //代理客户端socket的集合
        public byte[] Recivebyte;
        /// <summary>
        /// 服务器
        /// </summary>
        public void SocketServer(int port)
        {
            //本地地址
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(GetLocalIp()),port);
            try
            {
                //绑定ip
                server.Bind(iPEndPoint);
            }
            catch (Exception ex)
            {
                MessageBox.Show("启动失败" + ex.Message);
            }
            //开始监听
            server.Listen(5);  //连接等待队列

            //异步线程接受连接请求 
            ThreadPool.QueueUserWorkItem(new WaitCallback(AcceptClientConnect), server);
        }
        private void AcceptClientConnect(object server)
        {
            var Server = server as Socket;
            //代理套接字
            while (true)
            {
                var proxClient = Server.Accept(); //Accept()回阻塞线程
                CilentproxSocketlist.Add(proxClient);
                //异步线程接受客户端发来的信息
                ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveMsg), proxClient);
            }

        }
        public void ReceiveMsg(object clien)
        {
            var Client = clien as Socket;
            while (true)
            {
                int len = 0;
                byte[] Databuffer = new byte[1024]; //接受缓冲区
                try
                {
                    len = Client.Receive(Databuffer, 0, Databuffer.Length, SocketFlags.None);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    CilentproxSocketlist.Remove(Client);
                }

                if (len <= 0)
                {
                    Recivebyte = null;
                    CilentproxSocketlist.Remove(Client);
                    return;
                }
                else {
                    Recivebyte = Databuffer;
                }
                
            }
        }

        public string GetLocalIp()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            return AddressIP;
        }
        /// <summary>
        /// 服务器发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ServerSendData(byte[] data)
        {

            for (int i = 0; i < CilentproxSocketlist.Count; ++i)
            {
                if (
                    
                    
                    CilentproxSocketlist[i].Connected == true)
                {
                    try
                    {
                        CilentproxSocketlist[i].Send(data, 0, data.Length, SocketFlags.None);
                    }
                    catch(Exception ex)
                    {
                       
                    }
                }

            }

            //foreach (Socket proxClien in CilentproxSocketlist)
            //{
            //    if (proxClien.Connected == true)
            //    {
            //        proxClien.Send(data, 0, data.Length, SocketFlags.None);
            //    }
            //}
        }
    }
}
