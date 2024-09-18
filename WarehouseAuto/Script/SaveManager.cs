using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseAuto.Script
{
    internal class SaveManager
    {

        public static void SaveDataToFile(string savePath, ref List<Data> datas, ref ProgramInfo programInfo)
        {
            string timestamp = DateTime.Now.ToString("dd.MM.yyyy");
            string fileName = $"{timestamp}.json";

            // Преобразуем объект в JSON
            string json = JsonConvert.SerializeObject(datas, Newtonsoft.Json.Formatting.Indented);
            string json2 = JsonConvert.SerializeObject(programInfo, Newtonsoft.Json.Formatting.Indented);

            // Сохраняем файл
            File.WriteAllText(savePath + "/" + fileName, json);
            File.WriteAllText(savePath + "/" + "programInfo.json", json2);
        }

        public static void LoadDataFromFile(string savePath, ref List<Data> data, ref ProgramInfo programInfo)
        {
            string timestamp = DateTime.Now.ToString("dd.MM.yyyy");
            string fileName = $"{timestamp}.json";

            if (File.Exists(savePath + "/programInfo.json"))
            {
                // Читаем JSON из файла
                string json = File.ReadAllText(savePath + "/programInfo.json");

                // Десериализуем JSON в объект класса Data
                programInfo = JsonConvert.DeserializeObject<ProgramInfo>(json);
            }
            else
            {
                programInfo = new ProgramInfo();
            }

            if (File.Exists(savePath + "/" + fileName))
            {
                // Читаем JSON из файла
                string json = File.ReadAllText(savePath + "/" + fileName);

                // Десериализуем JSON в объект класса Data
                data = JsonConvert.DeserializeObject<List<Data>>(json);
            }
            else
            {
                Console.WriteLine($"Файл {fileName} не найден.");

                data = new List<Data>()
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
            }
        }
    }

    [System.Serializable]
    public class Data
    {
        public string Name;

        public List<string> EnterInvoices = new List<string>();
        public List<ScanInfo> HistoryInvoces = new List<ScanInfo>();
        public List<string> Consolidations = new List<string>();

        public string GMTNumber;
        public string SealNumber;

        public Data() { }

        public Data(string name)
        {
            Name = name;
        }
    }

    public class ScanInfo
    {
        public string Time;
        public string Data;

        public ScanInfo(string time, string data)
        {
            Time = time;
            Data = data;
        }
    }

    [System.Serializable]
    public class ProgramInfo
    {
        public string COMPort;

        public string Password;
    }

}
