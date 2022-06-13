using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iBuaa_1_Debug_v1
{

    /// <summary>
    /// 嵌入外部exe
    /// </summary>
    public class EmbeddedExeTool
    {
        [DllImport("User32.dll", EntryPoint = "SetParent")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        IntPtr WindowHandle = IntPtr.Zero;
        private const int WS_THICKFRAME = 262144;
        private const int WS_BORDER = 8388608;
        private const int GWL_STYLE = -16;
        private const int WS_CAPTION = 0xC00000;
        private Process proApp = null;
        private Control ContainerControl = null;

        private const int WS_VISIBLE = 0x10000000;
        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
        private static extern IntPtr SetWindowLongPtr32(HandleRef hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", CharSet = CharSet.Auto)]
        private static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, int dwNewLong);

        private IntPtr SetWindowLong(HandleRef hWnd, int nIndex, int dwNewLong)
        {
            if (IntPtr.Size == 4)
            {
                return SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
            }
            return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
        }
        /// <summary>
        /// 加载外部exe程序到程序容器中
        /// </summary>
        /// <param name="control">要显示exe的容器控件</param>
        /// <param name="exepath">exe的完整绝对路径</param>
        public void LoadEXE(Control control, string exepath)
        {
            ContainerControl = control;
            control.SizeChanged += Control_SizeChanged;

            ProcessStartInfo info = new ProcessStartInfo(exepath); // 进程

            info.WindowStyle = ProcessWindowStyle.Minimized;
            info.UseShellExecute = false;
            info.CreateNoWindow = false;

            proApp = Process.Start(info);

            Application.Idle += Application_Idle;

            EmbedProcess(proApp, control);
        }
        /// <summary>
        /// 结束exe
        /// </summary>
        /// <param name="exename"></param>
        public void KillEXE(string exename)
        {
            Process[] pr = Process.GetProcessesByName(exename);
            foreach(Process Pro in pr)
            {
                if (Pro.ProcessName == exename)
                    Pro.Kill();
            }
        }
        /// <summary>
        /// 加载外部exe程序到程序容器中
        /// </summary>
        /// <param name="form">要显示exe的窗体</param>
        /// <param name="exepath">exe的完整绝对路径</param>
        public void LoadEXE(Form form, string exepath)
        {
            ContainerControl = form;
            form.SizeChanged += Control_SizeChanged;
            proApp = new Process();
            proApp.StartInfo.UseShellExecute = false;
            proApp.StartInfo.CreateNoWindow = false;
            proApp.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            proApp.StartInfo.FileName = exepath;
            proApp.StartInfo.Arguments = Process.GetCurrentProcess().Id.ToString();
            proApp.Start();
            Application.Idle += Application_Idle;
            EmbedProcess(proApp, form);
        }
        /// <summary>
        /// 确保应用程序嵌入此容器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Application_Idle(object sender, EventArgs e)
        {
            if (this.proApp == null || this.proApp.HasExited)
            {
                this.proApp = null;
                Application.Idle -= Application_Idle;
                return;
            }
            if (proApp.MainWindowHandle == IntPtr.Zero) return;
            Application.Idle -= Application_Idle;
            EmbedProcess(proApp, ContainerControl);
        }
        /// <summary>
        /// 将指定的程序嵌入指定的控件
        /// </summary>
        private void EmbedProcess(Process app, Control control)
        {
            // Get the main handle
            if (app == null || app.MainWindowHandle == IntPtr.Zero || control == null) return;
            try
            {
                // Put it into this form
                SetParent(app.MainWindowHandle, control.Handle);
                // Remove border and whatnot               
                SetWindowLong(new HandleRef(this, app.MainWindowHandle), GWL_STYLE, WS_VISIBLE);
                ShowWindow(app.MainWindowHandle, (int)ProcessWindowStyle.Maximized); // 最大化
                MoveWindow(app.MainWindowHandle, 0, 0, control.Width, control.Height, true);
            }
            catch (Exception ex3)
            {
                Console.WriteLine(ex3.Message);
            }
        }
        /// <summary>
        /// 嵌入容器大小改变事件
        /// </summary>
        private void Control_SizeChanged(object sender, EventArgs e)
        {
            if (proApp == null)
            {
                return;
            }

            if (proApp.MainWindowHandle != IntPtr.Zero && ContainerControl != null)
            {
                MoveWindow(proApp.MainWindowHandle, 0, 0, ContainerControl.Width, ContainerControl.Height, true);
            }
        }
    }
}
