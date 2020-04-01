using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksWebApp.Models
{
    public class HistoricalStockViewModel
    {
        public Dictionary<string,string> CompanyDict { get; set; }
        public List<string> Dates { get; set; }
        public List<double> Prices { get; set; }
        public double Low { get; set; }
        public double High { get; set; }
    }
}
