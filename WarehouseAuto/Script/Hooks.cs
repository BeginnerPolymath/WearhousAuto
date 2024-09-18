using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarehouseAuto.Properties;

namespace WarehouseAuto.Script
{
    public class Hooks
    {
        public StandartMode StandartMode;
        public KeyboardHook KeyboardHook;

        public void Init(KeyboardHook keyboardHook, StandartMode standartMode)
        {
            KeyboardHook = keyboardHook;
            StandartMode = standartMode;

            KeyboardHook.Hooks = this;
        }

        MaskedTextBox msk;

        public void WritePassword()
        {
            popup = new Form();

            popup.Size = new System.Drawing.Size(200, 200);

            msk = new MaskedTextBox() { Text = "", Left = 10, Top = 10 };

            popup.Controls.Add(msk);

            msk.TextChanged += UpdatePassword;

            popup.TopMost = true;
            popup.Show();
        }

        void UpdatePassword(object sender, EventArgs e)
        {
            StandartMode.ProgramInfo.Password = msk.Text;
            StandartMode.SaveData();
        }

        // Нестатический метод для обработки нажатия F12
        public void AutoEnterRDP()
        {
            string pathToExe = @"C:\Users\User\Desktop\Cargo.RDP";

            Process.Start(pathToExe);

            if (ImageFinder.FindCount(10, 1000, Resources.RDPPassword))
            {
                ImageFinder.ClickButton(Resources.RDPPassword);

                foreach (var _char in StandartMode.ProgramInfo.Password)
                {
                    SendWaitDelay("{" + $"{_char}" + "}", 1, 50);
                }

                SendWaitDelay("{Enter}", 1, 50);

                if (ImageFinder.FindCount(20, 1000, Resources._1CStartWindow))
                {
                    SendWaitDelay("{Enter}", 1, 50);
                }

            }
        }

        // Нестатический метод для обработки нажатия F12
        public void OnF12Pressed()
        {
            StandartMode.SetPause();
        }

        public Form popup;

        public void ShowPopup(System.Drawing.Point position)
        {
            if (popup != null)
                if (popup.IsHandleCreated && popup.Visible)
                {
                    popup.Close();
                    popup = null;
                    return;
                }

            int sizeX = 200;
            int sizeY = 200;

            popup = new Form();
            popup.StartPosition = FormStartPosition.Manual;
            popup.Location = new Point(position.X - (sizeX / 2), position.Y - (sizeY / 2));
            popup.Size = new System.Drawing.Size(sizeX, sizeY);

            List<Button> buttons = new List<Button>()
            {
                new Button() { Text = "ЗК Вечер", Left = 10, Top = 10 },
                new Button() { Text = "ОГ Вечер", Left = 10, Top = 40 },

                new Button() { Text = "ЗК Утро", Left = 10, Top = 70 },
                new Button() { Text = "ОГ Утро", Left = 10, Top = 100 },

                new Button() { Text = "ОКГСС", Left = 10, Top = 130 },
            };

            foreach (var button in buttons)
            {
                popup.Controls.Add(button);
                button.Click += new EventHandler(ClosePopup);
                button.Click += new EventHandler(WindowController.Fouce1C);
            }

            buttons[0].Click += new EventHandler(EndingConsolidationNightB);
            buttons[1].Click += new EventHandler(SendingConsolidationNightB);

            buttons[2].Click += new EventHandler(EndingConsolidationMorningB);
            buttons[3].Click += new EventHandler(SendingConsolidationMorningB);

            buttons[4].Click += new EventHandler(OKGCC);

            popup.FormBorderStyle = FormBorderStyle.None;
            popup.TopMost = true;
            popup.Show();
        }

        public void ClosePopup(object sender, EventArgs e)
        {
            popup.Close();
            popup = null;
            return;
        }



        public void EndingConsolidationNightB(object sender, EventArgs e)
        {
            EndingConsolidation("18:00:00");
        }

        public void EndingConsolidationMorningB(object sender, EventArgs e)
        {
            EndingConsolidation("10:00:00");
        }

        public void EndingConsolidation(string time)
        {
            string currentDateTime = DateTime.Now.ToString("dd.MM.yyyy " + time);

            ImageFinder.ClickButton(Resources.EndingConsolidationButton);

            if (ImageFinder.FindCount(10, 30, Resources.EndingConsolidation))
            {
                StandartMode.Paste(currentDateTime, 200);

                Thread.Sleep(500);

                ImageFinder.ClickButton(Resources.EndingButton);
            }
        }




        public void SendingConsolidationNightB(object sender, EventArgs e)
        {
            SendingConsolidation("18:30:00");
        }

        public void SendingConsolidationMorningB(object sender, EventArgs e)
        {
            SendingConsolidation("10:30:00");
        }

        public void SendingConsolidation(string time)
        {
            string currentDateTime = DateTime.Now.ToString("dd.MM.yyyy " + time);

            ImageFinder.ClickButton(Resources.SendingConsolidationButton);

            if (ImageFinder.FindCount(10, 30, Resources.SendingWindow))
            {
                StandartMode.Paste(currentDateTime, 200);

                Thread.Sleep(500);

                SendWaitDelay("{Tab}", 3, 200);

                Thread.Sleep(500);

                StandartMode.Paste("К168КТ125", 200);

                Thread.Sleep(500);

                ImageFinder.ClickButton(Resources.EndingButton);
            }
        }

        public void SendWaitDelay(string text, int count = 0, int delay = 0)
        {
            for (int i = 0; i < count; i++)
            {
                SendKeys.SendWait(text);

                Thread.Sleep(delay);
            }


        }

        public void OKGCC(object sender, EventArgs e)
        {
            SendWaitDelay("+{F10}", 1, 200);

            if (ImageFinder.FindCount(10, 30, Resources.OnBase))
            {
                SendWaitDelay("{Down}", 9, 50);

                SendWaitDelay("{Right}", 1, 50);

                SendWaitDelay("{Down}", 2, 50);

                SendWaitDelay("{Enter}", 1, 1000);

                SendWaitDelay("{Enter}", 1);
            }
        }
    }
}
