using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 通信协议的编码解码
/// </summary>
namespace iBuaa_1_Debug_v1
{
    class EnDiscode
    {
        /// <summary>
        /// 关节角编码
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static byte[] JointAngleEncode(double[] angle)
        {
            return ElEncode(angle, 0xD1);
        }
        public static double[] JointAngleDiscode(byte[] data)
        {
            return ElDiscode(data, 0xD1);
        }
        public static byte[] UnityAngleEncode(double[] angle)
        {
            return ElEncode(angle, 0xAA);
        }
        /// <summary>
        /// 设备信息
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static byte[] IncreAngleEncode(double[] angle)
        {
            return ElEncode(angle, 0xD2);
        }
        public static double[] IncreAngleDiscode(byte[] data)
        {
            return ElDiscode(data, 0xD2);
        }
        public static byte[] SerialNumEncode(string str)
        {
            return StringEncode(str, 0xC1);
        }
        public static byte[] ProcotolEncode(string str)
        {
            return StringEncode(str, 0xC2);
        }
        public static byte[] HwTypeEncode(string str)
        {
            return StringEncode(str, 0xC3);
        }
        public static byte[] CtrlNumEncode(string str)
        {
            return StringEncode(str, 0xC4);
        }
        public static string SerialNumDiscode(byte[] data)
        {
            return StringDiscode(data,0xC1);
        }
        public static string ProcotolDiscode(byte[] data)
        {
            return StringDiscode(data, 0xC2);
        }
        public static string HwTypeDiscode(byte[] data)
        {
            return StringDiscode(data, 0xC3);
        }
        public static string CtrlNumDiscode(byte[] data)
        {
            return StringDiscode(data, 0xC4);
        }
        #region 私有成员
        private static byte[] ElEncode(double[] angle, byte id)
        {
            byte[] data = { id, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            int temp1 = (int)(angle[0] * 100.0f);
            int temp2 = (int)(angle[1] * 100.0f);
            int temp3 = (int)(angle[2] * 100.0f);
            int temp4 = (int)(angle[3] * 100.0f);
            int temp5 = (int)(angle[4] * 100.0f);
            int temp6 = (int)(angle[5] * 100.0f);

            data[1] = (byte)(temp1 & 0x00FF);
            data[2] = (byte)((temp1 >> 8) & 0x00FF);
            data[3] = (byte)(temp2 & 0x00FF);
            data[4] = (byte)((temp2 >> 8) & 0x00FF);
            data[5] = (byte)(temp3 & 0x00FF);
            data[6] = (byte)((temp3 >> 8) & 0x00FF);
            data[7] = (byte)(temp4 & 0x00FF);
            data[8] = (byte)((temp4 >> 8) & 0x00FF);
            data[9] = (byte)(temp5 & 0x00FF);
            data[10] = (byte)((temp5 >> 8) & 0x00FF);
            data[11] = (byte)(temp6 & 0x00FF);
            data[12] = (byte)((temp6 >> 8) & 0x00FF);

            return data;
        }
        private static double[] ElDiscode(byte[] data,byte id)
        {
            double[] angle = { 0, 0, 0, 0, 0 };
            if (data != null &&  data.Length >= 11 && data[0] == id)
            {
                short tempAngle = 0;
                for (int i = 2; i >= 1; --i)
                {
                    tempAngle <<= 8;
                    tempAngle |= (short)data[i];
                }
                angle[0] = tempAngle / 100.0f;

                tempAngle = 0;
                for (int i = 4; i >= 3; --i)
                {
                    tempAngle <<= 8;
                    tempAngle |= (short)data[i];
                }
                angle[1] = tempAngle / 100.0f;

                tempAngle = 0;
                for (int i = 6; i >= 5; --i)
                {
                    tempAngle <<= 8;
                    tempAngle |= (short)data[i];
                }
                angle[2] = tempAngle / 100.0f;

                tempAngle = 0;
                for (int i = 8; i >= 7; --i)
                {
                    tempAngle <<= 8;
                    tempAngle |= (short)data[i];
                }
                angle[3] = tempAngle / 100.0f;

                tempAngle = 0;
                for (int i = 10; i >= 9; --i)
                {
                    tempAngle <<= 8;
                    tempAngle |= (short)data[i];
                }
                angle[4] = tempAngle / 100.0f;
                return angle;
            }
            else
                return null;
        }
        private static byte[] StringEncode(string str, byte id)
        {
            byte[] temp = Encoding.Default.GetBytes(str);
            byte[] data = new byte[temp.Length + 1];
            data[0] = id;
            for(int i = 0; i < temp.Length; ++i)
            {
                data[i + 1] = temp[0];
            }
            return data;
        }
        private static  string StringDiscode(byte[] data,byte id)
        {
            if (data[0] != id) return null;
            return Encoding.Default.GetString(data, 1, data.Length - 1);
        }
        #endregion
    }
}
