using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WarehouseAuto.Script
{
    internal class WindowController
    {
        [DllImport("user32.dll")] public static extern bool SetForegroundWindow(IntPtr hWnd);
        [System.Runtime.InteropServices.DllImport("User32.dll")] public static extern bool ShowWindow(IntPtr handle, int nCmdShow);
        [DllImport("user32.dll")] public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")] public static extern uint GetWindowThreadProcessId(IntPtr hwnd, IntPtr proccess);
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)] public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public static void Fouce1C(object sender, EventArgs e)
        {

            IntPtr hWnd = WindowController.FindWindow("TscShellContainerClass", null);

            WindowController.ShowWindow(hWnd, 9);
            WindowController.SetForegroundWindow(hWnd);

            Thread.Sleep(500);
        }
    }

}
