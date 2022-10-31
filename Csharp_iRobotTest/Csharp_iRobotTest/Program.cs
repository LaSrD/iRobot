using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRobotDLLforCsharp;
using System.Threading;

namespace Csharp_iRobotTest
{
    class Program
    {
        static IROBOT m_irobot = new IROBOT("COM6");
        static Thread thread;
        static void Main(string[] args)
        {
            int str = Console.Read();
            if (str == 'a')
            {
                string msg = m_irobot.Connect();
                if (msg != string.Empty)
                    Console.Write(msg);
            }
            if(m_irobot.isConnect())
            {
                m_irobot.Info();
                Console.Write(m_irobot.Serial_Num);
                Console.Write(m_irobot.Procotol_Type);
                Console.Write(m_irobot.HW_Type);
                Console.Write(m_irobot.Ctrl_Num);
                thread = new Thread(new ThreadStart(intance));
                thread.Start();
            }
            while(true)
            {
                int button = Console.Read();
                if (button == 'l')
                    m_irobot.LockBaseR(); // 
            }
               
        }
        static void intance()
        {
            while (true)
            {
                m_irobot.OutputForce();
            }
        }
    }
}
