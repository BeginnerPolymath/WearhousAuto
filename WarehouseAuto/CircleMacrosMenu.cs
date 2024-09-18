using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WarehouseAuto
{
    public partial class CircleMacrosMenu : Form
    {
        private const int WH_MOUSE_LL = 14;
        private const int WM_XBUTTONDOWN = 0x020B;
        private const int XBUTTON1 = 0x0001;
        private const int XBUTTON2 = 0x0002;

        private static LowLevelMouseProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        public CircleMacrosMenu()
        {
            InitializeComponent();

            _hookID = SetHook(_proc);
        }

        private void CircleMacrosMenu_Load(object sender, EventArgs e)
        {

        }

        private void CircleMacrosMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnhookWindowsHookEx(_hookID);
        }

        private static IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_XBUTTONDOWN)
            {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

                // Определяем, какая боковая кнопка была нажата
                if ((hookStruct.mouseData >> 16) == XBUTTON1 || (hookStruct.mouseData >> 16) == XBUTTON2)
                {
                    // Получаем позицию курсора
                    Cursor.Position = new System.Drawing.Point(hookStruct.pt.x, hookStruct.pt.y);

                    // Показываем окно с кнопками в позиции курсора
                    ShowPopup(Cursor.Position);
                }
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private static void ShowPopup(System.Drawing.Point position)
        {
            // Создаем окно с кнопками и показываем его
            Form popup = new Form();
            popup.StartPosition = FormStartPosition.Manual;
            popup.Location = position;
            popup.Size = new System.Drawing.Size(200, 100);

            Button button1 = new Button() { Text = "Кнопка 1", Left = 10, Top = 10 };
            Button button2 = new Button() { Text = "Кнопка 2", Left = 10, Top = 40 };

            popup.Controls.Add(button1);
            popup.Controls.Add(button2);

            popup.Show();
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
