using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StocksWebApp.Models;

namespace StocksWebApp.Controllers
{
    public class TopStockPerformanceController : Controller
    {
        public IActionResult Index()
        {
            var topStockPerformanceViewModel = generateDummyData();

            //Default filter values
            topStockPerformanceViewModel.kpi = topStockPerformanceViewModel.kpiDict.Keys.FirstOrDefault();
            topStockPerformanceViewModel.timePeriod = 30;

            return View("index", topStockPerformanceViewModel);
        }
        public IActionResult Filter()
        {
            var kpiSM = Request.Form["kpiSelectMenu"];
            var tp = int.Parse(Request.Form["timePeriod"]);

            var topStockPerformanceViewModel = generateDummyData();

            //Filters
            topStockPerformanceViewModel.kpi = kpiSM;
            topStockPerformanceViewModel.timePeriod = tp;

            //TODO UPDATE THE RESULTS!
            topStockPerformanceViewModel.companiesKPI.First().rank = 99;

            return View("index", topStockPerformanceViewModel);
        }


        private TopStockPerformanceViewModel generateDummyData() {
            var dummyData = new TopStockPerformanceViewModel();



            dummyData.kpiDict = new Dictionary<string, string>();
            dummyData.kpiDict["Big1"] = "WOWZA1";
            dummyData.kpiDict["Big2"] = "WOWZA2";

            dummyData.timePeriod = 30; // 30 days

            dummyData.companiesKPI = new List<KPIInfo>();
            dummyData.companiesKPI.Add(
                new KPIInfo()
                {
                    rank = 1,
                    companyAbriv = "ABC",
                    companyName = "ABC-Company",
                    value = 5345.54
                });
            dummyData.companiesKPI.Add(
                new KPIInfo()
                {
                    rank = 2,
                    companyAbriv = "DEF",
                    companyName = "DEF-Company",
                    value = 45.54
                });
            dummyData.companiesKPI.Add(
                new KPIInfo()
                {
                    rank = 3,
                    companyAbriv = "GHI",
                    companyName = "GHI-Company",
                    value = 56.66
                });


            return dummyData;
        }
    }
}