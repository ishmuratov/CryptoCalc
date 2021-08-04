using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DroidCurrency
{
    static class CryptoCoins
    {
        public static string GetPrice(string coinName)
        {
            try
            {
                var request = WebRequest.Create($"https://coinmarketcap.com/currencies/{coinName}/");
                using (var responses = request.GetResponse())
                {
                    using (var streams = responses.GetResponseStream())
                    using (var readers = new StreamReader(streams))
                    {
                        //в переменной html наш сайт
                        string html = readers.ReadToEnd();
                        Match match = Regex.Match(html, @"priceValue(.+?)<\/div>", RegexOptions.Singleline);
                        List<string> log = new List<string>();
                        while (match.Success)
                        {
                            string answer = match.Value;
                            return DeleteTags(answer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "";
        }

        static string DeleteTags(string input)
        {
            int startIndex = input.IndexOf('>');
            return input.Substring(startIndex + 1, input.Length - startIndex - 1).Replace("</div>", string.Empty);
        }
    }
}
