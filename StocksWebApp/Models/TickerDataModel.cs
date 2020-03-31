using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksWebApp.Models
{
    public class TickerDataModel
    {
        public int Time { get; set; }
        public int Price { get; set; }
        public int Size { get; set; }
        public string Exchange { get; set; }
        public string SaleCondition { get; set; }
        public int Suspicious { get; set; }

    }
}
