using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text.RegularExpressions;
using System.Globalization;
using WarehouseAuto.Properties;
using System.Timers;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;
using System.Text;
using System.Diagnostics;
using WarehouseAuto.Script;

namespace WarehouseAuto
{
    public partial class StandartMode : Form
    {
        public KeyboardHook keyboardHook = new KeyboardHook();

        public string PasteMode = "+{INSERT}";

        public List<Data> Datas = new List<Data>()
                {
                    new Data("Основное"),

                    new Data("Москва местами"),
                    new Data("Складочная"),
                    new Data("Дубровка"),
                    new Data("Томилино"),
                    new Data("Сверхсрочки"),

                    new Data("БИО"),

                    new Data("Москва ЕЛИ местами"),
                    new Data("Складочная ЕЛИ"),
                    new Data("Дубровка ЕЛИ"),
                    new Data("Томилино ЕЛИ"),

                    new Data("Хабаровск местами"),
                    new Data("Хабаровск мешок"),
                    new Data("Южный местами"),
                    new Data("Южный мешок"),
                    new Data("Камчатка местами"),
                    new Data("Камчатка мешок"),

                    new Data("Артем места"),
                    new Data("Артем мешок"),

                    new Data("Флагман"),
                    new Data("Флагман мешок"),

                    new Data("Арсеньев"),
                    new Data("Спасск"),
                    new Data("Лесозаводск"),
                    new Data("Дальнереченск"),
                    new Data("Дальнегорск"),

                    new Data("Москва АМО-ПРЕСС паллет"),
                    new Data("Москва АМО-ПРЕСС мешок"),

                    new Data("Южный ДВ ТЭК паллет"),
                    new Data("Южный ДВ ТЭК мешок"),

                    new Data("Камчатка ДВ ТЭК паллет"),
                    new Data("Камчатка ДВ ТЭК мешок"),

                    new Data("Магадан ДВ ТЭК паллет"),
                    new Data("Магадан ДВ ТЭК мешок"),

                    new Data("Дополнительно 1"),
                    new Data("Дополнительно 2"),
                    new Data("Дополнительно 3"),
                    new Data("Дополнительно 4"),
                    new Data("Дополнительно 5"),
                };

        public ProgramInfo ProgramInfo = new ProgramInfo();

        public bool CancelAdd = false;

        private SerialPort port = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);

        private SerialPort port2 = new SerialPort("COM7", 9600, Parity.None, 8, StopBits.One);

        public bool AutoAddPause = true;

        public WaitForPixelColor WaitForPixelColor = new WaitForPixelColor();
       
        public ImageFinder OtherProgram = new ImageFinder();

        public int ContainerID;
        public string ContainerName;
        public bool ButtonsPanelOpen = false;

        public KeyboardHook KeyboardHook;
        public Hooks Hooks;

        public string SavePath;

        //ФУНКЦИИ

        public StandartMode()
        {
            

            SavePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Console.WriteLine(SavePath);

            KeyboardHook KeyboardHook = new KeyboardHook();
            Hooks Hooks = new Hooks();

            keyboardHook.Init(this, Hooks);
            Hooks.Init(keyboardHook, this);

            //System.Windows.Forms.Application.Run();



            TopMost = true;

            SetTimer();

            SaveManager.LoadDataFromFile(SavePath, ref Datas, ref ProgramInfo);

            SaveManager.SaveDataToFile(SavePath, ref Datas, ref ProgramInfo);

            Console.WriteLine(ProgramInfo.Password);
            Console.WriteLine(ProgramInfo.COMPort);

            InitializeComponent();

            if (ProgramInfo.COMPort == "" || ProgramInfo.COMPort == string.Empty || ProgramInfo.COMPort == null)
            {
                ProgramInfo.COMPort = "COM3";
            }

            port = new SerialPort(ProgramInfo.COMPort, 9600, Parity.None, 8, StopBits.One);

            comText.Text = ProgramInfo.COMPort;

            SerialPortProgram();
        }

        public void SaveData()
        {
            SaveManager.SaveDataToFile(SavePath, ref Datas, ref ProgramInfo);
        }

        public string GetTimeNow()
        {
            string timeFormat = DateTime.Now.ToString("HH:mm:ss");

            return timeFormat;
        }

        private void StandartMode_Load(object sender, EventArgs e)
        {
            Location = new Point(1600, 625);
            Size = new Size(267, 454);

            FieldsUpdate();
        }

        private void StandartMode_FormClosing(object sender, FormClosingEventArgs e)
        {
            keyboardHook.Unhook();
        }

        private void SetPortButton_Click(object sender, EventArgs e)
        {
            port.Close();

            port.DataReceived -= new SerialDataReceivedEventHandler(port_DataReceived);

            port = new SerialPort(comText.Text, 9600, Parity.None, 8, StopBits.One);

            SerialPortProgram();

            ProgramInfo.COMPort = comText.Text;

            SaveManager.SaveDataToFile(SavePath, ref Datas, ref ProgramInfo);
        }

        private void SerialPortProgram()
        {
            try
            {
                port.Open();
            }
            catch
            {
                MessageBox.Show("Такого порта не существует");
                return;
            }

            port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            LanguageSwitcher.SetEnglish();

            if (OldMode.Checked)
            {
                port2.WriteLine(port.ReadExisting());
                return;
            }

            if (EmulateCheckBox.Checked)
            {
                PasteMode = "^{v}";
                HIDPaste();

                return;
            }
            else
            {
                PasteMode = "+{INSERT}";
            }

            string text = port.ReadExisting();

            int containerID = -1;
            int.TryParse(text, out containerID);

            if (containerID >= 0 && containerID <= 60 && text.Length <= 3)
            {
                if (containerID == 50)
                {
                    Action action2 = (() =>
                    {
                        EnterInvoice.Items.RemoveAt(0);
                    });

                    EnterInvoice.Invoke(action2);

                    return;
                }

                SelectContainerID(containerID);
                FieldsUpdate();

                return;
            }

            Console.WriteLine(text.Remove(text.Length - 1, 1));

            Action action = (() =>
            {
                EnterInvoice.Items.Insert(0, text.Remove(text.Length - 1, 1));
            });

            Datas[ContainerID].EnterInvoices.Insert(0, new ScanInfo(GetTimeNow(), text.Remove(text.Length - 1, 1)));

            if (Reaction.Checked)
            {
                if (AutoAddPause)
                {
                    AutoAddPause = false;

                    Action action2 = (() =>
                    {
                        AutoAddButton.Text = "Пауза";
                        this.BackColor = Color.Orange;
                    });

                    AutoAddButton.Invoke(action2);

                    AsyncAddInvoice();
                }
            }

            if (EnterInvoice.InvokeRequired)
            {
                EnterInvoice.Invoke(action);
            }
            else
            {
                action();
            }

            TextCountUpdate2();

            SaveManager.SaveDataToFile(SavePath, ref Datas, ref ProgramInfo);
        }

        [STAThread]
        public async void HIDPaste()
        {
            await STATask.Run(() =>
            {
                string text = port.ReadExisting();

                LanguageSwitcher.SetEnglish();

                Paste(text.Remove(text.Length - 1, 1));

                HistroyAdd(text.Remove(text.Length - 1, 1));
                SaveManager.SaveDataToFile(SavePath, ref Datas, ref ProgramInfo);
            });
        }


        public void TextCountUpdate2()
        {
            Action action = (() =>
            {
                InvoceCountText.Text = EnterInvoice.Items.Count.ToString();
            });

            InvoceCountText.Invoke(action);
        }

        public void SelectContainerID(int containerID)
        {
            Action action = (() =>
            {
                ContainerComboBox.SelectedIndex = containerID;
            });

            ContainerComboBox.Invoke(action);
        }


        [STAThread]
        private void AutoAddButton_Click(object sender, EventArgs e)
        {
            if (!AutoAddPause)
            {
                SetPause();
            }
            else
            {
                if (EnterInvoice.Items.Count == 0)
                    return;

                AutoAddPause = false;
                AutoAddButton.Text = "Пауза";

                this.BackColor = Color.Orange;

                IntPtr hWnd = WindowController.FindWindow("TscShellContainerClass", null);
                WindowController.ShowWindow(hWnd, 9);
                WindowController.SetForegroundWindow(hWnd);

                AsyncAddInvoice();
            }
        }

        public void SetPause()
        {
            AutoAddPause = true;

            Action action = (() =>
            {
                AutoAddButton.Text = "Воспроизведение";
                this.BackColor = SystemColors.Control;
            });

            AutoAddButton.Invoke(action);
        }

        [STAThread]
        public async void AsyncAddInvoice()
        {
            await STATask.Run(() =>
            {
                Thread.Sleep(300);

                for (Int32 i = 0; i <= EnterInvoice.Items.Count; i++)
                {
                    if (AutoAddPause)
                        return;

                    bool search = false;

                    if (AutoMark.Checked)
                    {
                        NewPriyemka(ref search);
                    }
                    else
                    {
                        Priyemka(ref search);
                    }

                    if (search)
                        break;

                    TextCountUpdate2();

                    i = 0;
                }

                SetPause();
            });
        }

        private System.Timers.Timer aTimer;
        public bool MarkAuto = false;

        private void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += SetState;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        public void SetState(Object source, ElapsedEventArgs e)
        {
            if (ImageFinder.Find(Resources.SmaovivozWindow))
            {
                ChangeStateText("Самовывоз");
            }
            else if(ImageFinder.Find(Resources.DeliveryWindow))
            {
                ChangeStateText("Выдача на курьера");
            }
            else if (ImageFinder.Find(Resources.VidadtNaPeremeshenie))
            {
                ChangeStateText("ВыдатьНаПеремещение");
            }
            else if (ImageFinder.Find(Resources.RaskonsWindow))
            {
                ChangeStateText("РасКонс");
            }
            else if (ImageFinder.Find(Resources.InvoiceListWindow))
            {
                ChangeStateText("Список накладных");
            }
            else if (ImageFinder.Find(Resources.MarkButton))
            {
                ChangeStateText("МАРК окно");
            }
            else if (ImageFinder.Find(Resources.Marking))
            {
                ChangeStateText("МАРК");
            }
            else if (ImageFinder.Find(Resources.Consolidation))
            {
                ChangeStateText("КОНС");
            }
            else if (ImageFinder.Find(Resources.Prihod))
            {
                ChangeStateText("Приход");
            }
            else if (ImageFinder.Find(Resources.Spisanie))
            {
                ChangeStateText("Списание");
            }
            else
            {
                ChangeStateText("Отсутствует");
            }
        }

        public void ChangeStateText(string text)
        {
            Action action = (() =>
            {
                MStateText.Text = text;
            });

            MStateText.Invoke(action);
        }

        public void NewPriyemka(ref bool search)
        {
            if (MStateText.Text == "Выдача на курьера")
            {
                LanguageSwitcher.SetEnglish();

                ImageFinder.ClickButton(Resources.DliveryBarcodeEnter);

                Paste(EnterInvoice.Items[EnterInvoice.Items.Count - 1].ToString());

                HistroyFirstAdd();
                LastInvoceRemove();
                SaveManager.SaveDataToFile(SavePath, ref Datas, ref ProgramInfo);

                Thread.Sleep(1000);

                if (ImageFinder.FindCount(30, 100, Resources.NewButtonCheck))
                {
                    search = false;

                    return;
                }

                search = true;

                return;
            }
            else if (MStateText.Text == "Список накладных")
            {
                LanguageSwitcher.SetEnglish();

                Thread.Sleep(300);

                if (EnterInvoice.Items[0].ToString().Contains("#PC#"))
                {
                    SendKeys.SendWait("^{u}");
                    ImageFinder.FindCount(8, 250, Resources.GMXSearch);
                    Paste(EnterInvoice.Items[EnterInvoice.Items.Count - 1].ToString());
                    ImageFinder.UnFindCount(8, 250, Resources.GMXSearch);
                    SendKeys.SendWait("{Enter}");
                }
                else
                {
                    SendKeys.SendWait("{F4}");
                    ImageFinder.FindCount(8, 250, Resources.NumberForSearch);
                    Paste(EnterInvoice.Items[EnterInvoice.Items.Count - 1].ToString());
                    ImageFinder.UnFindCount(8, 250, Resources.NumberForSearch);
                    SendKeys.SendWait("{Enter}");
                }

                //Paste(EnterInvoice.Items[EnterInvoice.Items.Count - 1].ToString());


                HistroyFirstAdd();
                //LastInvoceRemove();
                SaveManager.SaveDataToFile(SavePath, ref Datas, ref ProgramInfo);

                search = true;

                return;
            }
            else if (MStateText.Text == "МАРК")
            {
                ImageFinder.ClickButton(Resources.InvoiceAdd);

                LanguageSwitcher.SetEnglish();

                Paste(EnterInvoice.Items[EnterInvoice.Items.Count - 1].ToString());

                HistroyFirstAdd();
                LastInvoceRemove();
                SaveManager.SaveDataToFile(SavePath, ref Datas, ref ProgramInfo);

                Thread.Sleep(400);

                if (AutoMark.Checked)
                {
                    if (ImageFinder.FindCount(30, 100, Resources.MarkButton))
                    {
                        ImageFinder.ClickButton(Resources.MarkButton);

                        if (ImageFinder.UnFindCount(8, 250, Resources.SaveButton))
                        {
                            search = false;

                            return;
                        }
                    }
                }

                search = true;

                return;
            }
            else if (MStateText.Text == "РасКонс")
            {
                ImageFinder.ClickButton(Resources.EnterInvoice);

                Paste(EnterInvoice.Items[EnterInvoice.Items.Count - 1].ToString());

                HistroyFirstAdd();
                LastInvoceRemove();
                SaveManager.SaveDataToFile(SavePath, ref Datas, ref ProgramInfo);

                Thread.Sleep(700);

                SendKeys.SendWait("{Tab}");

                Thread.Sleep(500);

                if (ImageFinder.FindCount(30, 100, Resources.RasConsCheck))
                {
                    search = false;

                    return;
                }

                search = true;

                return;
            }
            else if (MStateText.Text == "Приход" || MStateText.Text == "Списание")
            {
                SendKeys.SendWait("{F7}");
                ImageFinder.FindCount(20, 100, Resources.SelectOjbectTypeWindow);

                SendKeys.SendWait("{Enter 3}");

                ImageFinder.FindCount(20, 100, Resources.EnterInvoceWindow);

                Paste(EnterInvoice.Items[EnterInvoice.Items.Count - 1].ToString(), 200);

                HistroyFirstAdd();
                LastInvoceRemove();
                SaveManager.SaveDataToFile(SavePath, ref Datas, ref ProgramInfo);

                ImageFinder.UnFindCount(40, 100, Resources.EnterInvoiceDot);
            }
        }

        public void Priyemka(ref bool search)
        {
            if (MStateText.Text == "Выдача на курьера")
            {
                LanguageSwitcher.SetEnglish();

                ImageFinder.ClickButton(Resources.DliveryBarcodeEnter);

                Paste(EnterInvoice.Items[EnterInvoice.Items.Count - 1].ToString(), 200);

                HistroyFirstAdd();
                LastInvoceRemove();
                SaveManager.SaveDataToFile(SavePath, ref Datas, ref ProgramInfo);

                Thread.Sleep(500);

                SendKeys.SendWait("+{Tab}");

                Paste("123", enter: false);

                if (ImageFinder.FindCount(30, 100, Resources.Checkkek))
                {
                    SendKeys.SendWait("^{Backspace}");

                    Thread.Sleep(200);

                    if (ImageFinder.FindCount(30, 100, Resources.DliveryBarcodeEnter))
                    {
                        search = false;

                        return;
                    }
                }

                search = true;

                return;
            }

            if (MStateText.Text == "ВыдатьНаПеремещение")
            {
                ImageFinder.ClickButton(Resources.InvoiceAdd);

                if (ImageFinder.FindCount(30, 100, Resources.EnterGMXNumber))
                {
                    Paste(EnterInvoice.Items[EnterInvoice.Items.Count - 1].ToString(), 200);

                    HistroyFirstAdd();
                    LastInvoceRemove();
                    SaveManager.SaveDataToFile(SavePath, ref Datas, ref ProgramInfo);

                    Thread.Sleep(500);

                    search = false;

                    return;
                }

                search = true;

                return;
            }


            if (MStateText.Text == "РасКонс")
            {
                ImageFinder.ClickButton(Resources.EnterInvoice);

                Paste(EnterInvoice.Items[EnterInvoice.Items.Count - 1].ToString());

                HistroyFirstAdd();
                LastInvoceRemove();
                SaveManager.SaveDataToFile(SavePath, ref Datas, ref ProgramInfo);

                Thread.Sleep(700);

                SendKeys.SendWait("{Tab}");

                Thread.Sleep(500);

                if (ImageFinder.FindCount(30, 100, Resources.RasConsCheck))
                {
                    search = false;

                    return;
                }

                search = true;

                return;
            }

            //Отмена если маркировка открыта
            if (WaitForPixelColor.GetColorAt(new Point(1170, 258)) == Color.FromArgb(255, 204, 204, 204))
            {
                SetPause();
                return;
            }

            //Поиск накладных
            if (WaitForPixelColor.GetColorAt(new Point(1342, 87)) == Color.FromArgb(255, 204, 204, 204))
            {
                //Нажимаем на кнопку накладной
                MacrosSystem.MoveLeftClick(454, 112);

                Thread.Sleep(100);

                //Определяем накладная это или ГМХ
                if (EnterInvoice.Items[0].ToString().Contains("#PC#"))
                {
                    SendKeys.SendWait("{Down 2}");
                }
                else
                {
                    //SendKeys.SendWait("{Down}");
                }

                Thread.Sleep(100);

                SendKeys.SendWait("{Enter}");

                //Ожидание когда откроется окно
                WaitForPixelColor.PollPixel(new Point(1342, 87), Color.FromArgb(255, 178, 178, 178), ref AutoAddPause);
                if (AutoAddPause) return;

                LanguageSwitcher.SetEnglish();

                Paste(EnterInvoice.Items[EnterInvoice.Items.Count - 1].ToString());

                //Автоматическое открытие накладной в случае появления ошибки, пока что не доделано, открываем ручками
                //Thread.Sleep(300);

                //if (WaitForPixelColor.GetColorAt(new Point(1573, 16)) == Color.FromArgb(255, 205, 205, 205))
                //{
                //    SendKeys.SendWait("{Enter}");
                //}

                HistroyFirstAdd();
                LastInvoceRemove();
                SaveManager.SaveDataToFile(SavePath, ref Datas, ref ProgramInfo);

                search = true;

                return;
            }

            //Нажимаем на кнопку добавления накладной
            MacrosSystem.MoveLeftClick(1720, 155);

            //Ожидаем появления окна с выбором типа добавления
            WaitForPixelColor.PollPixel(new Point(1392, 12), Color.FromArgb(255, 205, 205, 205), ref AutoAddPause);
            if (AutoAddPause) return;


            Thread.Sleep(200);

            //Определяем мешок это или накладная/гмх
            if (WaitForPixelColor.GetColorAt(new Point(913, 443)) == Color.FromArgb(255, 65, 48, 3))
            {
                if (EnterInvoice.Items[0].ToString().Contains("#F#"))
                {
                    MacrosSystem.MoveLeftClick(787, 510);
                }
                else
                {
                    MacrosSystem.MoveLeftClick(787, 476);
                }
            }
            
            if (WaitForPixelColor.GetColorAt(new Point(1706, 460)) != Color.FromArgb(255, 6, 6, 6))
            {
                Thread.Sleep(100);

                MacrosSystem.MoveLeftClick(1054, 650);

                WaitForPixelColor.PollPixel(new Point(1039, 405), Color.FromArgb(255, 178, 178, 178), ref AutoAddPause);
                if (AutoAddPause) return;
            }
            else if (WaitForPixelColor.GetColorAt(new Point(1706, 460)) == Color.FromArgb(255, 6, 6, 6))
            {
                //MessageBox.Show("Маркировка!");
            }

            LanguageSwitcher.SetEnglish();


            if (String.IsNullOrWhiteSpace(EnterInvoice.Items[0].ToString()))
            {
                Paste("Empty");
            }
            else
            {
                Paste(EnterInvoice.Items[EnterInvoice.Items.Count - 1].ToString());
            }

            HistroyFirstAdd();
            LastInvoceRemove();
            SaveManager.SaveDataToFile(SavePath, ref Datas, ref ProgramInfo);

            WaitForPixelColor.PollPixel(new Point(1364, 9), Color.FromArgb(255, 158, 158, 158), ref AutoAddPause);
        }

        public void HistroyFirstAdd()
        {
            Action action = (() =>
            {
                HistoryInvoice.Items.Insert(0, EnterInvoice.Items[EnterInvoice.Items.Count - 1].ToString());

            });

            HistoryInvoice.Invoke(action);

            //string timeFormat = DateTime.Now.ToString("HH:mm:ss");

            Datas[ContainerID].HistoryInvoces.Insert(0, Datas[ContainerID].EnterInvoices[Datas[ContainerID].EnterInvoices.Count-1]);
        }

        public void HistroyAdd(string text)
        {
            Action action = (() =>
            {
                HistoryInvoice.Items.Insert(0, text);

            });

            HistoryInvoice.Invoke(action);

            string timeFormat = DateTime.Now.ToString("HH:mm:ss");
            Datas[ContainerID].HistoryInvoces.Insert(0, new ScanInfo(timeFormat, text));
        }

        public void LastInvoceRemove()
        {
            Action action = (() =>
            {
                EnterInvoice.Items.RemoveAt(EnterInvoice.Items.Count - 1);
                Datas[ContainerID].EnterInvoices.RemoveAt(Datas[ContainerID].EnterInvoices.Count - 1);
            });

            EnterInvoice.Invoke(action);
        }

        private void EnterInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            LanguageSwitcher.SetEnglish();


            if (e.KeyCode.ToString() == "Down")
            {
                Color color = WaitForPixelColor.GetColorAt(new Point(MacrosSystem.GetCursorPosition().X, MacrosSystem.GetCursorPosition().Y));

                string a = MacrosSystem.GetCursorPosition().X + ", " + MacrosSystem.GetCursorPosition().Y;

                MessageBox.Show(a + " Color: " + WaitForPixelColor.GetColorAt(new Point(MacrosSystem.GetCursorPosition().X, MacrosSystem.GetCursorPosition().Y)));
                Clipboard.SetText($"{MacrosSystem.GetCursorPosition().X} {MacrosSystem.GetCursorPosition().Y} {WaitForPixelColor.GetColorAt(new Point(MacrosSystem.GetCursorPosition().X, MacrosSystem.GetCursorPosition().Y))}");

                Clipboard.SetText($"WaitForPixelColor.PollPixel(new Point({a}), Color.FromArgb({color.A}, {color.R}, {color.G}, {color.B}));");
            }

            if ((e.KeyCode == Keys.V && e.Control))
            {
                LanguageSwitcher.SetEnglish();

                if (Reaction.Checked)
                {
                    IntPtr hWnd = WindowController.FindWindow("TscShellContainerClass", null);
                    WindowController.ShowWindow(hWnd, 9);
                    WindowController.SetForegroundWindow(hWnd);
                }



                string[] invoices = Regex.Split(Clipboard.GetText(), "\r\n|\r|\n");

                foreach (var invoice in invoices)
                {
                    EnterInvoice.Items.Add(invoice);
                    Datas[ContainerID].EnterInvoices.Add(new ScanInfo(GetTimeNow(), invoice));
                }

                if (string.IsNullOrEmpty(EnterInvoice.Items[EnterInvoice.Items.Count - 1].ToString()))
                {
                    EnterInvoice.Items.RemoveAt(EnterInvoice.Items.Count - 1);
                    Datas[ContainerID].EnterInvoices.RemoveAt(Datas[ContainerID].EnterInvoices.Count - 1);
                }

                if (Reaction.Checked)
                {
                    if (AutoAddPause)
                    {
                        AutoAddPause = false;

                        Action action2 = (() =>
                        {
                            AutoAddButton.Text = "Пауза";
                        });

                        this.BackColor = Color.Orange;

                        AutoAddButton.Invoke(action2);

                        AsyncAddInvoice();
                    }

                }
                TextCountUpdate2();

                Console.WriteLine(EnterInvoice.Items.Count);
            }

            if ((e.KeyCode == Keys.X && e.Control))
            {
                ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(EnterInvoice);
                selectedItems = EnterInvoice.SelectedItems;

                if (EnterInvoice.SelectedIndex != -1)
                {
                    string text = "";
                    for (int i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        if (i > 0)
                            text += selectedItems[i].ToString() + "\n";
                        else
                            text += selectedItems[i].ToString();
                    }

                    Clipboard.SetText(text);

                    for (int i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        EnterInvoice.Items.Remove(selectedItems[i]);
                    }

                    TextCountUpdate2();
                }
            }

            ControlAList(e, EnterInvoice);

            ControlCList(e, EnterInvoice);

            if (e.KeyCode.ToString() == "Insert")
            {
                string invoices = "";

                foreach (var invoice in EnterInvoice.Items)
                {
                    invoices += invoice + "\n";
                }

                Clipboard.SetText(invoices);
            }

            if (e.KeyCode.ToString() == "End")
            {
                EnterInvoice.Items.Clear();
                TextCountUpdate2();
            }

            if (e.KeyCode.ToString() == "Delete" || e.KeyCode.ToString() == Keys.Back.ToString())
            {
                ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(EnterInvoice);
                selectedItems = EnterInvoice.SelectedItems;

                if (EnterInvoice.SelectedIndex != -1 && EnterInvoice.SelectedIndex < EnterInvoice.Items.Count)
                {
                    for (int i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        //Datas[ContainerID].EnterInvoices.Remove(new ScanInfo(GetTimeNow(), selectedItems[i].ToString()));
                        Datas[ContainerID].EnterInvoices.RemoveAt(i);

                        EnterInvoice.Items.Remove(selectedItems[i]);
                    }

                    TextCountUpdate2();

                    SaveManager.SaveDataToFile(SavePath, ref Datas, ref ProgramInfo);
                }
            }

            if (e.KeyCode.ToString() == "A")
            {
                CancelAdd = true;
            }
        }

       
        private void HistoryInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(HistoryInvoice);
            selectedItems = HistoryInvoice.SelectedItems;

            if (HistoryInvoice.SelectedIndex != -1)
            {
                SelectionCount.Text = selectedItems.Count.ToString();
            }
        }

        private void AddConsolidations_Click(object sender, EventArgs e)
        {
            for (Int32 i = 0; i < ConsoList.Items.Count; i++)
            {
                ImageFinder.ClickButton(Resources.EnterConsolidation);

                Thread.Sleep(100);

                Paste(ConsoList.Items[i].ToString());

                Thread.Sleep(400);

                if (!ImageFinder.FindCount(20, 100, Resources.EnterConsolidation))
                {
                    MessageBox.Show("Ошибка");
                    return;
                }
            }
        }

        private void ConsoList_KeyDown(object sender, KeyEventArgs e)
        {
            ControlVList(e, ConsoList);

            ControlCList(e, ConsoList);

            DeleteList(e, ConsoList);

            ControlAList(e, ConsoList);

            UpdateCosolidationCountText();
        }

        public void UpdateCosolidationCountText()
        {
            ConsolidationCount.Text = ConsoList.Items.Count.ToString();
        }

        private void HistoryInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            ControlCList(e, HistoryInvoice);

            ControlAList(e, HistoryInvoice);

            //DeleteList(e, HistoryInvoice);

            SelectionCount.Text = HistoryInvoice.SelectedItems.Count.ToString();
        }

        public void Paste(string text, int latency = 100, bool enter = true)
        {
            Clipboard.SetText(text);

            Thread.Sleep(50);

            SendKeys.SendWait(PasteMode);

            if (!enter)
            {
                return;
            }

            Thread.Sleep(latency);

            SendKeys.SendWait("{Enter}");
        }

        public void ControlCList(KeyEventArgs e, ListBox list)
        {
            if ((e.KeyCode == Keys.C && e.Control))
            {
                ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(list);
                selectedItems = list.SelectedItems;

                if (list.SelectedIndex != -1)
                {
                    string text = "";
                    for (int i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        if (i > 0)
                            text += selectedItems[i].ToString() + "\n";
                        else
                            text += selectedItems[i].ToString();
                    }

                    Clipboard.SetText(text);
                }
            }
        }

        public void ControlAList(KeyEventArgs e, ListBox list)
        {
            if((e.KeyCode == Keys.A && e.Control))
            {
                if (list.SelectedIndex != -1)
                {
                    for (global::System.Int32 i = 0; i < list.Items.Count; i++)
                    {
                        list.SetSelected(i, true);
                    }
                }
            }
        }

        public void ControlVList(KeyEventArgs e, ListBox list)
        {
            if ((e.KeyCode == Keys.V && e.Control))
            {
                LanguageSwitcher.SetEnglish();

                string[] invoices = Regex.Split(Clipboard.GetText(), "\r\n|\r|\n");

                foreach (var invoice in invoices)
                {
                    list.Items.Add(invoice);
                }
            }
        }

        public void DeleteList(KeyEventArgs e, ListBox list)
        {
            if (e.KeyCode.ToString() == "Delete" || e.KeyCode.ToString() == "Backspace")
            {
                ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(list);
                selectedItems = list.SelectedItems;

                if (list.SelectedIndex != -1 && list.SelectedIndex < list.Items.Count)
                {
                    for (int i = selectedItems.Count - 1; i >= 0; i--)
                    {
                        list.Items.Remove(selectedItems[i]);
                        //scanInfos.RemoveAt(i);
                    }
                }



                SaveManager.SaveDataToFile(SavePath, ref Datas, ref ProgramInfo);
            }
        }



        private void ButtonsPanel_Click(object sender, EventArgs e)
        {
            if (ButtonsPanelOpen)
            {
                Size = new Size(267, 454);
            }
            else
            {
                Size = new Size(467, 454);
            }

            ButtonsPanelOpen = !ButtonsPanelOpen;
        }

        


       

        private void ContainerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            containerIDUpDown.Value = ContainerComboBox.SelectedIndex;
            FieldsUpdate();
        }

        void FieldsUpdate()
        {
            Action action = (() =>
            {
                ContainerID = ContainerComboBox.SelectedIndex;
                ContainerName = ContainerComboBox.SelectedItem.ToString();

                EnterInvoice.Items.Clear();
                HistoryInvoice.Items.Clear();

                foreach (var data in Datas[ContainerID].EnterInvoices)
                {
                    EnterInvoice.Items.Add(data.Data);
                }

                foreach (var data in Datas[ContainerID].HistoryInvoces)
                {
                    HistoryInvoice.Items.Add(data.Data);
                }

                GTMField.Text = Datas[ContainerID].GMTNumber;
                SealField.Text = Datas[ContainerID].SealNumber;

                TextCountUpdate2();
            });

            ContainerComboBox.Invoke(action);
        }

        private void CreateConsolidation_Click(object sender, EventArgs e)
        {
            if (ContainerName == "Москва местами")
            {
                CreateCons("Курьер-Регион (Москва)", "АЭРОФЛОТ=ПОСТАВЩИК СПб", "Авиа", "Владивосток", "");
            }
            else if (ContainerName == "Москва ЕЛИ местами")
            {
                CreateCons("Курьер-Регион (Москва)", "АЭРОФЛОТ=ПОСТАВЩИК СПб", "Авиа", "Владивосток", "", "Литий");
            }
            else if (ContainerName == "БИО")
            {
                CreateCons("Курьер-Регион (Москва)", "Авиа-Карго Логистик новый(Владивосток)", "Авиа", "Владивосток", "");
            }

            else if (ContainerName == "Хабаровск местами")
            {
                CreateCons("КС Хабаровск", "АЭРО-ГРУЗ (Владивосток)", "Авиа", "Владивосток", "");
            }
            else if (ContainerName == "Южный местами")
            {
                CreateCons("КС Южно-Сахалинск", "АЭРО-ГРУЗ (Владивосток)", "Авиа", "Владивосток", "");
            }
            else if (ContainerName == "Камчатка местами")
            {
                CreateCons("КС Петропавловск-Камчатский", "АЭРО-ГРУЗ (Владивосток)", "Авиа", "Владивосток", "");
            }

            else if (ContainerName == "Артем места")
            {
                CreateCons("КС Артем", "КС Владивосток", "Авто", "Владивосток", "");
            }

            else if (ContainerName == "Флагман")
            {
                CreateCons("КС Хабаровск", "Флагман-Амур", "Авто", "Владивосток", "");
            }

            else if (ContainerName == "Арсеньев")
            {
                CreateCons("Раков Игорь", "ТК ЭНЕРГИЯ (Владивосток)", "Авто", "Владивосток", "", energy: true);
            }
            else if (ContainerName == "Спасск")
            {
                CreateCons("Герасимов Геннадий", "ТК ЭНЕРГИЯ (Владивосток)", "Авто", "Владивосток", "", energy: true);
            }
            else if (ContainerName == "Лесозаводск")
            {
                CreateCons("Маргач", "ТК ЭНЕРГИЯ (Владивосток)", "Авто", "Владивосток", "", energy: true);
            }
            else if (ContainerName == "Дальнереченск")
            {
                CreateCons("Синяйкин", "ТК ЭНЕРГИЯ (Владивосток)", "Авто", "Владивосток", "", energy: true);
            }
            else if (ContainerName == "Дальнегорск")
            {
                CreateCons("Кошуков", "ТК ЭНЕРГИЯ (Владивосток)", "Авто", "Владивосток", "", energy: true);
            }
        }

        private void CreatePackage_Click(object sender, EventArgs e)
        {
            if (ContainerName == "Складочная" || ContainerName == "Складочная ЕЛИ")
            {
                CreatePackageFunc("Склад Складочная", Resources.MOWButton, Resources.Package80_120, Resources.FastButton);
            }   
            else if (ContainerName == "Дубровка" || ContainerName == "Дубровка ЕЛИ")
            {
                CreatePackageFunc("Склад Дубровка", Resources.MOWButton, Resources.Package100_120, Resources.FastButton);
            }
            else if (ContainerName == "Томилино" || ContainerName == "Томилино ЕЛИ")
            {
                CreatePackageFunc("Склад Томилино", Resources.MOWButton, Resources.Package100_120, Resources.FastButton);
            }
            else if (ContainerName == "Сверхсрочки" || ContainerName == "БИО")
            {
                CreatePackageFunc("Склад Дубровка", Resources.MOWButton, Resources.Package55_105, Resources.VeryfastButton);
            }

            else if (ContainerName == "Хабаровск мешок")
            {
                CreatePackageFunc("Склад (Хабаровск г)", Resources.KHVButton, Resources.Package100_120, Resources.FastButton);
            }
            else if (ContainerName == "Южный мешок")
            {
                CreatePackageFunc("Склад (Южно-Сахалинск г)", Resources.UUSButton, Resources.Package80_120, Resources.FastButton);
            }
            else if (ContainerName == "Камчатка мешок")
            {
                CreatePackageFunc("Склад (Петропавловск-Камчатский г)", Resources.PPKButton, Resources.Package80_120, Resources.FastButton);
            }

            else if (ContainerName == "Артем мешок")
            {
                CreatePackageFunc("Склад (Петропавловск-Камчатский г)", Resources.PPKButton, Resources.Package80_120, Resources.FastButton);
            }
        }

        public bool StorageMOWCheck(string storage)
        {
            string[] storages = new string[] { "Склад Складочная", "Склад Дубровка", "Склад Томилино"};

            foreach (var strg in storages)
            {
                if (storage == strg)
                {
                    return true;
                }
            }

            return false;
        }

        void CreateCons(string giver, string transporter, string mode, string point, string point2, string comment = "", bool energy = false)
        {
            LanguageSwitcher.SetEnglish();

            //Нажимаем на кнопку создания консолидации
            ImageFinder.ClickButton(Resources.CreateConsolidationButton);

            Thread.Sleep(500);

            if (ImageFinder.FindCount(10, 30, Resources.GiverEnter))
            {
                //Кликаем на поле получателя
                ImageFinder.ClickButton(Resources.GiverEnter);

                Thread.Sleep(500);

                SendKeys.SendWait("{F4}");

                if (ImageFinder.FindCount(10, 100, Resources.Kontragents))
                {
                    SendKeys.SendWait("{Tab 3}");

                    Thread.Sleep(500);

                    SendKeys.SendWait("^f");

                    Thread.Sleep(1000);

                    Paste(giver, 500);

                    Thread.Sleep(1000);

                    SendKeys.SendWait("{Enter}");
                }


                Thread.Sleep(500);

                if(!energy)
                    SendKeys.SendWait("{Tab 1}");

                Thread.Sleep(500);

                Paste(transporter, 500);

                Thread.Sleep(1000);

                SendKeys.SendWait("{Tab 2}");

                Paste(mode);

                Thread.Sleep(500);

                Paste("Владивосток");

                Thread.Sleep(500);

                Paste(mode);

                Thread.Sleep(500);

                if (comment != "")
                {
                    SendKeys.SendWait("{Tab 3}");

                    Thread.Sleep(500);

                    Paste(comment, enter: false);
                }
            }
        }

        private void CloseEnergy_Click(object sender, EventArgs e)
        {
            CloseConsolidation(DateTime.Now.ToString("dd.MM.yyyy") + " 10:00:00", DateTime.Now.ToString("dd.MM.yyyy") + " 10:30:00", "А123ВЛ125", "Т754РН797");
        }

        public void CloseConsolidation(string datatime, string datatime2, string number, string number2)
        {
            //Закрытие консолидации

            ImageFinder.ClickButton(Resources.EndingConsolidationButton);

            ImageFinder.FindCount(10, 250, Resources.EndingConsolidation);

            Paste(datatime);

            Thread.Sleep(200);

            ImageFinder.ClickButton(Resources.EndingButton);

            Thread.Sleep(500);

            //Отправка консолидации

            ImageFinder.ClickButton(Resources.SendingConsolidationButton);

            ImageFinder.FindCount(10, 250, Resources.SendingWindow);

            Paste(datatime2);

            Thread.Sleep(200);

            if (ImageFinder.Find(Resources.NumberAutoTransfer))
            {
                ImageFinder.ClickButton(Resources.NumberAutoTransfer);

                Paste(number);

                Thread.Sleep(200);
            }



            ImageFinder.ClickButton(Resources.NumberAuto);

            Paste(number2);
        }

        public void CreatePackageFunc(string storage, Bitmap image, Bitmap package, Bitmap speed)
        {

            //Открываем окно выбора направления
            ImageFinder.ClickButton(Resources.CreatePackageButton);

            if (ImageFinder.FindCount(10, 200, Resources.GMTWindow))
            {
                Thread.Sleep(200);

                //Артем по другому делается

                SendKeys.SendWait("{Enter}");

                ImageFinder.FindCount(5, 500, image);

                ImageFinder.ClickButton(image);

                Thread.Sleep(200);

                if (StorageMOWCheck(storage))
                {
                    ImageFinder.FindCount(5, 250, Resources.GMTWindow);

                    SendKeys.SendWait("{Down 3}");

                    SendKeys.SendWait("{Enter}");   
                }

                //ImageFinder.ClickButton(Resources.MOWButtonInGMT);

                ImageFinder.FindCount(5, 250, Resources.CreateGMTWindow);

                Paste(storage);

                SendKeys.SendWait("{Tab}");

                SendKeys.SendWait("{F4}");

                ImageFinder.FindCount(5, 500, Resources.SelectPackageWindow);

                ImageFinder.ClickButton(package);

                Thread.Sleep(500);

                SendKeys.SendWait("{Tab}");

                SendKeys.SendWait("{F4}");

                ImageFinder.FindCount(5, 500, Resources.DelivySpeedWindow);

                ImageFinder.ClickButton(speed);

                Thread.Sleep(100);

                MacrosSystem.SetCursorPosition(1143, 788);
            }
        }

        private void containerIDUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (containerIDUpDown.Value < ContainerComboBox.Items.Count - 1)
            {
                ContainerComboBox.SelectedIndex = (int)containerIDUpDown.Value;
                FieldsUpdate();
            }
        }

        private void TextWindowButton_Click(object sender, EventArgs e)
        {
            TextPanel textPanel = new TextPanel();
            textPanel.Show();
            textPanel.TopMost = true;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            IntPtr hWnd = WindowController.FindWindow("TscShellContainerClass", null);
            WindowController.ShowWindow(hWnd, 9);
            WindowController.SetForegroundWindow(hWnd);

            Thread.Sleep(500);

            SendKeys.SendWait("^+W");

            Thread.Sleep(200);

            //SendKeys.SendWait("{Tab 15}");
            SendKeys.SendWait("{Tab 13}");

            Thread.Sleep(200);

            SendKeys.SendWait("{Enter}");

            Thread.Sleep(500);

            SendKeys.SendWait("{Right}");

            Thread.Sleep(500);

            SendKeys.SendWait("{Enter}");

            Thread.Sleep(500);

            /*
            Paste("10");
            SendKeys.SendWait("{Tab}");
            SendKeys.SendWait("{Enter}");

            Paste("20");
            SendKeys.SendWait("{Tab}");
            SendKeys.SendWait("{Enter}");

            Paste("30");
            */

            SendKeys.SendWait("{Tab 5}");

            Thread.Sleep(100);

            SendKeys.SendWait("{Enter}");

            Thread.Sleep(100);

            Paste("0.1");

            Thread.Sleep(100);

            SendKeys.SendWait("{Tab 3}");

            Thread.Sleep(100);

            SendKeys.SendWait("{Enter}");

            if (ImageFinder.UnFindCount(5, 250, Resources.MarkWindow))
            {
                MacrosSystem.MoveLeftClick(1075, 202);

                Thread.Sleep(500);

                SendKeys.SendWait("{Down}");
            }
        }

       

        private void GTMField_TextChanged(object sender, EventArgs e)
        {
            Datas[ContainerID].GMTNumber = GTMField.Text;

            SaveManager.SaveDataToFile(SavePath, ref Datas, ref ProgramInfo);
        }

        private void SealField_TextChanged(object sender, EventArgs e)
        {
            Datas[ContainerID].SealNumber = SealField.Text;

            SaveManager.SaveDataToFile(SavePath, ref Datas, ref ProgramInfo);
        }


        private void AutoOpenGMX_Click(object sender, EventArgs e)
        {
            if (GTMField.Text != string.Empty)
            {
                IntPtr hWnd = WindowController.FindWindow("TscShellContainerClass", null);
                WindowController.ShowWindow(hWnd, 9);
                WindowController.SetForegroundWindow(hWnd);

                Thread.Sleep(300);

                SendKeys.SendWait("{F7}");

                if (ImageFinder.FindCount(10, 30, Resources.GMTChoiceButton))
                {
                    ImageFinder.ClickButton(Resources.GMTChoiceButton);

                    Thread.Sleep(400);

                    SendKeys.SendWait("{Tab 2}");

                    Thread.Sleep(400);

                    SendKeys.SendWait("{Enter}");

                    //SwitchLanguageToEnglish();

                    Paste(GTMField.Text, 500);
                }
                else
                {
                    Paste(GTMField.Text, 500);
                }


            }
        }

        private void comText_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }

    

    
}


