using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CameraHandle = System.Int32;
using MVSDK;
using System.Drawing;
using System.Drawing.Imaging;
using MvApi = MVSDK.MvApi;
using System.Windows.Forms;

namespace iBuaa_1_Debug_v1
{

    class camera
    {
        static public IntPtr m_Grabber = IntPtr.Zero;
        static private CameraHandle m_hCamera = 0;

        static protected tSdkCameraDevInfo m_DevInfo;
        static public ColorPalette m_GrayPal;
        static public bool InitCamera()
        {
            CameraSdkStatus status = 0;
            tSdkCameraDevInfo[] DevList;
            MvApi.CameraEnumerateDevice(out DevList);
            int NumDev = (DevList != null ? DevList.Length : 0);
            if (NumDev < 1)
            {
                MessageBox.Show("未扫描到相机");
                return false;
            }
            else if (NumDev == 1)
            {
                status = MvApi.CameraGrabber_Create(out m_Grabber, ref DevList[0]);
            }
            else
            {
                status = MvApi.CameraGrabber_CreateFromDevicePage(out m_Grabber);
            }

            if (status == 0)
            {
                MvApi.CameraGrabber_GetCameraDevInfo(m_Grabber, out m_DevInfo);
                MvApi.CameraGrabber_GetCameraHandle(m_Grabber, out m_hCamera);
                MvApi.CameraCreateSettingPage(m_hCamera,IntPtr.Zero, m_DevInfo.acFriendlyName, null, (IntPtr)0, 0);

                // 黑白相机设置ISP输出灰度图像
                // 彩色相机ISP默认会输出BGR24图像
                tSdkCameraCapbility cap;
                MvApi.CameraGetCapability(m_hCamera, out cap);
                if (cap.sIspCapacity.bMonoSensor != 0)
                {
                    MvApi.CameraSetIspOutFormat(m_hCamera, (uint)MVSDK.emImageFormat.CAMERA_MEDIA_TYPE_MONO8);

                    // 创建灰度调色板
                    Bitmap Image = new Bitmap(1, 1, PixelFormat.Format8bppIndexed);
                    m_GrayPal = Image.Palette;
                    for (int Y = 0; Y < m_GrayPal.Entries.Length; Y++)
                        m_GrayPal.Entries[Y] = Color.FromArgb(255, Y, Y, Y);
                }
                MvApi.CameraGrabber_StartLive(m_Grabber);


            }
            else
            {
                MessageBox.Show(String.Format("打开相机失败，原因：{0}", status));
            }
            return true;
        }
        static public void StartCamera()
        {
            if (m_Grabber != IntPtr.Zero)
                MvApi.CameraGrabber_StartLive(m_Grabber);
        }
        static public void StopCamera()
        {
            if (m_Grabber != IntPtr.Zero)
                MvApi.CameraGrabber_StopLive(m_Grabber);
        }
        static public void CreatCamerSetPage()
        {
            if (m_Grabber != IntPtr.Zero)
                MvApi.CameraShowSettingPage(m_hCamera, 1);
        }
        static public void CloseCamrea()
        {
            MvApi.CameraGrabber_Destroy(m_Grabber);
        }
    }
}
