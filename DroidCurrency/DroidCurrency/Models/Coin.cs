using System;
using System.Collections.Generic;
using System.Text;

namespace DroidCurrency.Models
{
    [Serializable]
    public class Coin
    {
        public string Name { get; set; }
        public string CharCode { get; set; }
        public decimal LocalCurrencyValue { get; set; }
        public string IconFileName { get; set; }
        public bool PaperMoney { get; set; }
        public bool Increased { get; set; }
        public bool Stable { get; set; }
    }
}
