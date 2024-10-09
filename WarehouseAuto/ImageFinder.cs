using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Linq;
using System.IO.Ports;
using System.Runtime.InteropServices;
using WarehouseAuto.Properties;
using WarehouseAuto.Script;
using System.Diagnostics;


namespace WarehouseAuto
{
    public class ImageFinder
    {
        public static float SizeCoef = 0.75f;

        public static void Start()
        {
            ClickButton(Resources.InvoiceListInactive);

        }

        public static void ClickButton(Bitmap bitmap)
        {
            using (var screenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))
            {
                using (var g = Graphics.FromImage(screenCapture))
                {
                    g.CopyFromScreen(0, 0, 0, 0, screenCapture.Size);
                }

                using (var buttonImage = new Bitmap(bitmap))
                {
                    var result = FindImageOnScreen(screenCapture, buttonImage);
                    if (result != null)
                    {
                        int clickX = (int)(result.Value.X / SizeCoef) + buttonImage.Width / 2;
                        int clickY = (int)(result.Value.Y / SizeCoef) + buttonImage.Height / 2;

                        MacrosSystem.MoveLeftClick(clickX, clickY);
                    }
                    else
                    {
                        Console.WriteLine("Button not found.");
                    }
                }
            }
        }

        public static void ClickButton(System.Drawing.Point point)
        {
            MacrosSystem.MoveLeftClick(point.X, point.Y);
        }

        public static async Task<bool> FindAndClickAsync(int timeout, Bitmap image, Action<System.Drawing.Point> onFoundAction, int interval = 100)
        {
            // Поиск изображения
            Rect? found = await FindCountAsync(timeout, image, interval);

            // Если найдено, выполняем действие с Point
            if (found != null)
            {
                // Получаем центр Rect как Point
                System.Drawing.Point center = new System.Drawing.Point((int)(found.Value.X / SizeCoef) + image.Width / 2, (int)(found.Value.Y / SizeCoef) + image.Height / 2);
                onFoundAction(center);
                return true; // Возвращаем true, если изображение найдено
            }

            return false; // Возвращаем false, если изображение не найдено
        }

        public static OpenCvSharp.Point GetImagePosition(Bitmap bitmap)
        {
            using (var screenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))
            {
                using (var g = Graphics.FromImage(screenCapture))
                {
                    g.CopyFromScreen(0, 0, 0, 0, screenCapture.Size);
                }

                using (var buttonImage = new Bitmap(bitmap))
                {
                    var result = FindImageOnScreen(screenCapture, buttonImage);
                    if (result != null)
                    {
                        int clickX = (int)(result.Value.X / SizeCoef) + buttonImage.Width / 2;
                        int clickY = (int)(result.Value.Y / SizeCoef) + buttonImage.Height / 2;

                        return new OpenCvSharp.Point(clickX, clickY);
                    }
                    else
                    {
                        Console.WriteLine("Iamge not found.");
                        return new OpenCvSharp.Point(0, 0);
                    }
                }
            }
        }

        public static Rect? Find(Bitmap bitmap)
        {
            using (var screenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))
            {
                using (var g = Graphics.FromImage(screenCapture))
                {
                    g.CopyFromScreen(0, 0, 0, 0, screenCapture.Size);
                }

                using (var buttonImage = new Bitmap(bitmap))
                {
                    // Здесь вызываем метод, который ищет изображение на экране и возвращает его позицию
                    var result = FindImageOnScreen(screenCapture, buttonImage);
                    return result; // Возвращаем позицию или null, если не найдено
                }
            }
        }

        private static Bitmap ResizeBitmap(Bitmap bmp, double scale)
        {
            int newWidth = (int)(bmp.Width * scale);
            int newHeight = (int)(bmp.Height * scale);
            var resizedBmp = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(resizedBmp))
            {
                g.DrawImage(bmp, 0, 0, newWidth, newHeight);
            }
            return resizedBmp;
        }

        public static bool FindCount(int count, int time, Bitmap tempalte)
        {
            for (int i = 0; i < count; i++)
            {
                var result = Find(tempalte);

                if (result != null)
                {
                    return true;
                }
                else
                {
                    Thread.Sleep(time);
                }
            }

            return false;
        }

        public static async Task<Rect?> FindCountAsync(int timeout, Bitmap template, int interval = 100)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            while (stopwatch.ElapsedMilliseconds < timeout)
            {
                // Ищем изображение
                var position = Find(template);
                if (position != null)
                {
                    return position; // Возвращаем позицию изображения
                }

                await Task.Delay(interval); // Ожидание перед следующей проверкой
            }

            return null; // Если изображение не найдено, возвращаем null
        }

        public static bool UnFindCount(int count, int time, Bitmap tempalte)
        {
            for (int i = 0; i < count; i++)
            {
                var result = ImageFinder.Find(tempalte);

                if (result == null)
                {
                    return true;
                }
                else
                {
                    Thread.Sleep(time);
                }
            }

            return false;
        }

        public static Rect? FindImageOnScreen(Bitmap screen, Bitmap template)
        {
            screen = ResizeBitmap(screen, SizeCoef);
            template = ResizeBitmap(template, SizeCoef);


            using (var screenMat = screen.ToMat())
            using (var templateMat = template.ToMat())
            using (var result = new Mat())
            {
                Cv2.MatchTemplate(screenMat, templateMat, result, TemplateMatchModes.CCoeffNormed);
                Cv2.MinMaxLoc(result, out double minVal, out double maxVal, out OpenCvSharp.Point minLoc, out OpenCvSharp.Point maxLoc);

                if (maxVal >= 0.8) // Adjust the threshold as necessary
                {
                    return new Rect(maxLoc.X, maxLoc.Y, template.Width, template.Height);
                }
                else
                {
                    return null;
                }
            }
        }
    }


}
