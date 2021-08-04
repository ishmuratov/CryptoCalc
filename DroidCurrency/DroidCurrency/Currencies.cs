using DroidCurrency.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace DroidCurrency
{
    static class Currencies
    {
        public static string GetPrice(string charCode)
        {
            string jsonData = GetJSONData();

            dynamic array = JsonConvert.DeserializeObject(jsonData);
            string valuteArray = array["Valute"].ToString();
            var ValutesData = JsonConvert.DeserializeObject<Dictionary<string, Valute>>(valuteArray);
            foreach (var item in ValutesData)
            {
                if (item.Key == charCode)
                {
                    return item.Value.Value;
                }
            }
            return "";
        }

        private static string GetJSONData()
        {
            var request = WebRequest.Create($"http://www.cbr-xml-daily.ru/daily_json.js");
            using (var responses = request.GetResponse())
            {
                using (var streams = responses.GetResponseStream())
                using (var readers = new StreamReader(streams))
                {
                    //в переменной html наш сайт
                    string html = readers.ReadToEnd();
                    return html;
                }
            }
        }

    }
}
