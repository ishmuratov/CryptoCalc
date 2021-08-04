using System;
using System.Collections.Generic;
using System.Text;

namespace DroidCurrency.Models
{
    public class Valute
    {
        public string ID { get; set; }
        public string NumCode { get; set; }
        public string CharCode { get; set; }
        public string Nominal { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Previous { get; set; }
    }
}
