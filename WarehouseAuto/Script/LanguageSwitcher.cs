using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarehouseAuto.Script
{
    internal class LanguageSwitcher
    {
        [DllImport("user32.dll")] public static extern IntPtr GetKeyboardLayout(uint thread);

        public static CultureInfo GetCurrentKeyboardLayout()
        {
            try
            {
                IntPtr foregroundWindow = WindowController.GetForegroundWindow();
                uint foregroundProcess = WindowController.GetWindowThreadProcessId(foregroundWindow, IntPtr.Zero);
                int keyboardLayout = GetKeyboardLayout(foregroundProcess).ToInt32() & 0xFFFF;
                return new CultureInfo(keyboardLayout);
            }
            catch (Exception _)
            {
                return new CultureInfo(1033); // Assume English if something went wrong.
            }
        }

        public static void Switch_keyboard(string lang)
        {
            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(lang);
            InputLanguage inputLanguage = InputLanguage.FromCulture(cultureInfo);
            InputLanguage.CurrentInputLanguage = inputLanguage;
        }

        public static void SetEnglish()
        {
            if (GetCurrentKeyboardLayout().Name == "ru-RU")
            {
                Switch_keyboard("en-US");
            }
        }
    }
}
