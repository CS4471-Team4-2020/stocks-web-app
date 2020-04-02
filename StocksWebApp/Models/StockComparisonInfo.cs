using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksWebApp.Models
{
    public class StockComparisonInfo
    {
        public StockComparisonInfo()
        {
            StockSummaries = new List<StockSummaryInfo>();
        }

        public List<StockSummaryInfo> StockSummaries { get; set; }
    }
}
