using System;
using System.Collections.Generic;
using System.Text;

namespace DroidCurrency
{
    static class DecimalHelper
    {
        public static decimal StringToDecimal(string input)
        {
            char splitter = input.Contains(".") ? '.' : ',';
            decimal result = 0;
            if (input.Contains(splitter.ToString()))
            {
                string[] tmpArray = input.Split(splitter);
                if (tmpArray.Length == 2)
                {
                    result = int.Parse(tmpArray[0]) + int.Parse(tmpArray[1]) / (decimal)Math.Pow(10, tmpArray[1].Length);
                }
            }
            return result;
        }
    }
}
