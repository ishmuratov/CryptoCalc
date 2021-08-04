using DroidCurrency.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace DroidCurrency.Helpers
{
    class FileWorker
    {
        public static void SaveDataToFile(IData _data, string _filePath)
        {
            if (_data == null)
            {
                return;
            }

            try
            {
                using (var fs = new FileInfo(_filePath).Create())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(fs, _data);
                }
            }
            catch (Exception _ex)
            {
                //Logger.IOLog($"Ошибка. Файл {_filePath} не может быть сохранён. " + _ex.Message);
            }
        }

        public static IData LoadDataFromFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                return null;
            }

            try
            {
                using (var fs = new FileInfo(filePath).Open(FileMode.Open))
                {
                    var formatter = new BinaryFormatter();
                    return formatter.Deserialize(fs) as IData;
                }
            }
            catch (Exception _ex)
            {
                //Logger.IOLog($"Ошибка файла {filePath} : файл повреждён. " + _ex.Message);
            }
            return null;
        }

        public static void WriteTextToFile(string _data, string _filePath)
        {
            try
            {
                File.WriteAllText(_filePath, _data);
            }
            catch (Exception _ex)
            {
                //Logger.IOLog($"Ошибка. Файл {_filePath} не может быть сохранён. " + _ex.Message);
            }
        }

        public static string ReadTextFromFile(string _filePath)
        {
            try
            {
                return File.ReadAllText(_filePath);
            }
            catch (Exception _ex)
            {
                return string.Empty;
                //Logger.IOLog($"Ошибка. Файл {_filePath} не может быть сохранён. " + _ex.Message);
            }
        }

    }
}
