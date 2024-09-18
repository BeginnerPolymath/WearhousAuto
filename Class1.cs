using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarehouseAuto.Properties;

namespace WarehouseAuto
{
    public partial class StandartMode : Form
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct MousePoint
        {
            public int X, Y;
            public MousePoint(int x, int y) { X = x; Y = y; }
        }

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out MousePoint lpMousePoint);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("user32.dll")] static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")] static extern uint GetWindowThreadProcessId(IntPtr hwnd, IntPtr proccess);
        [DllImport("user32.dll")] static extern IntPtr GetKeyboardLayout(uint thread);

        public CultureInfo GetCurrentKeyboardLayout()
        {
            try
            {
                IntPtr foregroundWindow = GetForegroundWindow();
                uint foregroundProcess = GetWindowThreadProcessId(foregroundWindow, IntPtr.Zero);
                int keyboardLayout = GetKeyboardLayout(foregroundProcess).ToInt32() & 0xFFFF;
                return new CultureInfo(keyboardLayout);
            }
            catch
            {
                return new CultureInfo(1033); // Assume English if something went wrong.
            }
        }

        [Flags]
        public enum MouseEventFlags
        {
            LeftDown = 0x00000002,
            LeftUp = 0x00000004,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Move = 0x00000001,
            Absolute = 0x00008000,
            RightDown = 0x00000008,
            RightUp = 0x00000010
        }

        public enum EState
        {
            Priyem,
            Rascons,
            Cons
        }

        private List<Data> Datas = new List<Data> { new Data(), new Data(), new Data() };
        private EState State = EState.Priyem;
        private bool CancelAdd = false;
        private SerialPort port = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
        private bool AutoAddPause = true;
        private WaitForPixelColor WaitForPixelColor = new WaitForPixelColor();

        public StandartMode()
        {
            InitializeComponent();
            SerialPortProgram();
            TopMost = true;
        }

        private void StandartMode_Load(object sender, EventArgs e)
        {
            Location = new Point(1600, 625);
        }

        private void SerialPortProgram()
        {
            try
            {
                port.Open();
                port.DataReceived += Port_DataReceived;
            }
            catch
            {
                MessageBox.Show("Такого порта не существует");
            }
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (GetCurrentKeyboardLayout().Name == "ru-RU")
                SwitchKeyboard("en-US");

            if (EmulateCheckBox.Checked)
            {
                Kaki();
                return;
            }

            string text = port.ReadExisting().TrimEnd();

            Invoke(new Action(() =>
            {
                EnterInvoice.Items.Insert(0, text);
                Datas[(int)State].EnterInvoices.Insert(0, text);
            }));

            if (Reaction.Checked && AutoAddPause)
            {
                AutoAddPause = false;
                Invoke(new Action(() =>
                {
                    AutoAddButton.Text = "Пауза";
                    this.BackColor = Color.Orange;
                }));
                AsyncAddInvoice();
            }

            UpdateInvoiceCount();
        }

        public static void SetCursorPosition(int x, int y) => SetCursorPos(x, y);
        public static void SetCursorPosition(MousePoint point) => SetCursorPos(point.X, point.Y);
        public static MousePoint GetCursorPosition() => GetCursorPos(out var point) ? point : new MousePoint(0, 0);
        public static void MouseEvent(MouseEventFlags value)
        {
            var position = GetCursorPosition();
            mouse_event((int)value, position.X, position.Y, 0, 0);
        }

        public static void SwitchKeyboard(string lang)
        {
            var cultureInfo = CultureInfo.CreateSpecificCulture(lang);
            var inputLanguage = InputLanguage.FromCulture(cultureInfo);
            InputLanguage.CurrentInputLanguage = inputLanguage;
        }

        private async void Kaki()
        {
            await Task.Run(() =>
            {
                var text = port.ReadExisting().TrimEnd();
                Clipboard.SetText(text);
                if (GetCurrentKeyboardLayout().Name == "ru-RU")
                {
                    SwitchKeyboard("en-US");
                    Thread.Sleep(25);
                }
                SendKeys.SendWait("+{INSERT}");
                Thread.Sleep(50);
                SendKeys.SendWait("{Enter}");
            });
        }

        private void UpdateInvoiceCount()
        {
            Invoke(new Action(() =>
            {
                InvoceCountText.Text = EnterInvoice.Items.Count.ToString();
            }));
        }

        private void Priyem_Click(object sender, EventArgs e) => SwitchState(EState.Priyem, "Приёмка");
        private void Rascon_Click(object sender, EventArgs e) => SwitchState(EState.Rascons, "Расконсолидация");
        private void Cons_Click(object sender, EventArgs e) => SwitchState(EState.Cons, "Консолидация");

        private void SwitchState(EState newState, string stateText)
        {
            if (State == newState) return;

            State = newState;
            StateText.Text = stateText;
            EnterInvoice.Items.Clear();
            HistoryInvoice.Items.Clear();

            foreach (var invoice in Datas[(int)newState].EnterInvoices)
                EnterInvoice.Items.Add(invoice);

            foreach (var invoice in Datas[(int)newState].HistoryInvoces)
                HistoryInvoice.Items.Add(invoice);
        }

        private void AutoAddButton_Click(object sender, EventArgs e)
        {
            AutoAddPause = !AutoAddPause;
            AutoAddButton.Text = AutoAddPause ? "Воспроизведение" : "Пауза";
            this.BackColor = AutoAddPause ? SystemColors.Control : Color.Orange;

            if (!AutoAddPause && EnterInvoice.Items.Count > 0)
                AsyncAddInvoice();
        }

        private void SetPause()
        {
            AutoAddPause = true;
            Invoke(new Action(() =>
            {
                AutoAddButton.Text = "Воспроизведение";
                this.BackColor = SystemColors.Control;
            }));
        }

        private async void AsyncAddInvoice()
        {
            await Task.Run(() =>
            {
                Thread.Sleep(300);
                for (int i = 0; i < EnterInvoice.Items.Count && !AutoAddPause; i++)
                {
                    switch (State)
                    {
                        case EState.Priyem:
                            Priyemka();
                            break;
                        case EState.Rascons:
                            RasCons(i);
                            break;
                        case EState.Cons:
                            // Consolidation logic
                            break;
                    }
                    UpdateInvoiceCount();
                }
                SetPause();
            });
        }

        private void Priyemka()
        {
            if (WaitForPixelColor.GetColorAt(new Point(1170, 258)) == Color.FromArgb(255, 204, 204, 204))
            {
                SetPause();
                return;
            }

            if (WaitForPixelColor.GetColorAt(new Point(1342, 87)) == Color.FromArgb(255, 204, 204, 204))
            {
                MoveLeftClick(454, 112);
                Thread.Sleep(100);
                SendKeys.SendWait(EnterInvoice.Items[0].ToString().Contains("#PC#") ? "{Down 3}" : "{Down}");
                Thread.Sleep(100);
                SendKeys.SendWait("{Enter}");

                WaitForPixelColor.PollPixel(new Point(1342, 87), Color.FromArgb(255, 178, 178, 178), ref AutoAddPause);
                if (AutoAddPause) return;

                Clipboard.SetText(EnterInvoice.Items[EnterInvoice.Items.Count - 1].ToString());

                if (GetCurrentKeyboardLayout().Name == "ru-RU")
                {
                    SwitchKeyboard("en-US");
                    Thread.Sleep(25);
                }

                SendKeys.SendWait("+{INSERT}");
                Thread.Sleep(100);
                SendKeys.SendWait("{Enter}");

                HistroyAdd();
                EnterRemove();

                return;
            }

            MoveLeftClick(1720, 155);
            WaitForPixelColor.PollPixel(new Point(1392, 12), Color.FromArgb(255, 205, 205, 205), ref AutoAddPause);
        }

        private void RasCons(int index)
        {
            if (WaitForPixelColor.GetColorAt(new Point(1220, 304)) == Color.FromArgb(255, 0, 0, 0))
            {
                SetPause();
                return;
            }

            MoveLeftClick(314, 352);
            Thread.Sleep(200);
            SendKeys.SendWait(EnterInvoice.Items[index].ToString());
            Thread.Sleep(200);
            SendKeys.SendWait("{Enter}");

            WaitForPixelColor.PollPixel(new Point(1220, 304), Color.FromArgb(255, 0, 0, 0), ref AutoAddPause);
            if (AutoAddPause) return;

            HistroyAdd();
            EnterRemove();
        }

        private void MoveLeftClick(int x, int y)
        {
            SetCursorPos(x, y);
            Thread.Sleep(100);
            MouseEvent(MouseEventFlags.LeftDown);
            Thread.Sleep(100);
            MouseEvent(MouseEventFlags.LeftUp);
        }

        private void HistroyAdd()
        {
            Invoke(new Action(() =>
            {
                var invoice = EnterInvoice.Items[0].ToString();
                HistoryInvoice.Items.Insert(0, invoice);
                Datas[(int)State].HistoryInvoces.Insert(0, invoice);
            }));
        }

        private void EnterRemove()
        {
            Invoke(new Action(() =>
            {
                EnterInvoice.Items.RemoveAt(0);
            }));
        }
    }

    public class WaitForPixelColor
    {
        public Color GetColorAt(Point location)
        {
            using (Bitmap bitmap = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(location, Point.Empty, new Size(1, 1));
                }
                return bitmap.GetPixel(0, 0);
            }
        }

        public void PollPixel(Point location, Color targetColor, ref bool pauseFlag)
        {
            while (GetColorAt(location) != targetColor)
            {
                Thread.Sleep(100);
                if (pauseFlag)
                    break;
            }
        }
    }

    public class Data
    {
        public List<string> EnterInvoices { get; set; } = new List<string>();
        public List<string> HistoryInvoces { get; set; } = new List<string>();
    }
}