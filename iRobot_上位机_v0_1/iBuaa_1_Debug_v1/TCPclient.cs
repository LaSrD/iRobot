using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iBuaa_1_Debug_v1
{
    class TCPclient
    {
        Socket clientsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Thread Revicethread;
        public byte[] ReciveData;
        public void SocketConnect(string ip,int port)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            try
            {
                clientsocket.Connect(endPoint);
            }
            catch (Exception ex)
            {

            }
            Revicethread = new Thread(new ThreadStart(ReceiveMsg));
            Revicethread.Priority = ThreadPriority.AboveNormal;
            Revicethread.Start();
            Revicethread.IsBackground = true;
        }
        /// <summary>
        /// 接受数据
        /// </summary>
        public void ReceiveMsg()
        {
            byte[] Databuffer = new byte[1024]; //接受缓冲区
            while (true)
            {
                int len = 0;
                try
                {
                    len = clientsocket.Receive(Databuffer, 0, Databuffer.Length, SocketFlags.None);
                }
                catch (Exception ex)
                {
                }
                ReciveData = Databuffer;
            }
        }
        public void SendMsg(byte[] data)
        {
            if (clientsocket.Connected)
            {
                clientsocket.Send(data, 0, data.Length, SocketFlags.None);
            }
        }
    }
}
