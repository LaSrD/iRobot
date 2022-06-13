using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.IO.Ports;

public enum PORTSTATE : short {
    CONNECT = 0,
    DISCONNECT = 1,
    ERROR = 2
} 
///<summary>
///串口通信
///</summary>
public class PortControl
{
    /// <summary>
    /// 串口配置
    /// </summary>
    public static SerialPort connect = new SerialPort();
    public string portName = "COM10";       //串口名
    public int baudRate = 9600;             //波特率
    public Parity parity = Parity.None;     //效验位
    public int dataBits = 8;                //数据位
    public StopBits stopBits = StopBits.One;//停止位

    public bool connectFlag = false;        //连接标识
    public string ButtonInf = string.Empty;       //接收的数据    

    public PORTSTATE Portconnect(string PortName, int baudRate = 9600)
    {
        if (!connectFlag)
        {
            portName = PortName;
            try
            {
                if (!connect.IsOpen)
                {
                    connect.PortName = PortName;
                    connect.DataBits = dataBits;
                    connect.BaudRate = baudRate;
                    connect.Parity = parity;
                    connect.ReadTimeout = SerialPort.InfiniteTimeout;
                    try
                    {
                        connect.Open();
                    }
                    catch(Exception ex)
                    {
                        return PORTSTATE.ERROR;
                    }

                    connectFlag = true;
                    return PORTSTATE.CONNECT;
                }
                else
                {
                    connect.Close();
                    return PORTSTATE.ERROR;
                }
            }
            catch (Exception ee)
            {
                
            }
        }
        else
        {
            connect.Close();
            connectFlag = false;
            return PORTSTATE.DISCONNECT;
        }
        return PORTSTATE.ERROR;
    }
    /// <summary>
    /// 断开串口
    /// </summary>
    public void Close()
    {
        if (connectFlag)
        {
            connect.Close();
        }
    }
    /// <summary>
    /// 按行读取串口数据
    /// </summary>
    /// <returns></returns>
    public void ReadPort()
    {
        if (connect.IsOpen)
        {
            connect.NewLine = "\r\n";
            ButtonInf = connect.ReadLine();
        }
    }
}
