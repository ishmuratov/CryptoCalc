using DroidCurrency.Helpers;
using DroidCurrency.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroidCurrency.Models
{
    [Serializable]
    public class DataBase : IData
    {
        public List<Coin> Data { get; set; }

        public DataBase()
        {
            Data = new List<Coin>();
        }
    }
}
