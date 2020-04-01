using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StocksWebApp.Models
{
    public class TopStockPerformanceViewModel
    {
        public string kpi { get; set; }
        public Dictionary<string, string> kpiDict { get; set; }
        public int timePeriod { get; set; }
        public List<int> timePeriods { get{ return new List<int>() { 1, 7, 30, 365 }; }  }
        public List<KPIInfo> companiesKPI { get; set; }

    }
    public class KPIInfo {
        public int rank { get; set; }
        public string companyAbriv { get; set; }
        public string companyName { get; set; }
        public double value { get; set; }
    }
}
