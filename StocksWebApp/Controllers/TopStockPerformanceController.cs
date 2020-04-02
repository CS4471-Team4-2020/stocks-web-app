using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StocksWebApp.Models;

namespace StocksWebApp.Controllers
{
    public class TopStockPerformanceController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly Dictionary<string, string> kpiDict = new Dictionary<string, string>()
        {
            { "closing", "Highest Closing Price" },
            { "high", "Highest High Price"},
            { "low", "Lowest Low Price"},
            { "market cap","Highest Market Capital"},
            { "price to earn ratio", "Lowest Price to Earning Ratio" }
        };

        public IActionResult Index()
        {

            //Default filter values
            string kpi = kpiDict.Keys.FirstOrDefault();
            int days = 30;

            //Get dates in the specified time period
            var endDateTime = DateTime.ParseExact("08/12/2011 00:00:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);  //Latest end date noted in the database
            var startDateTime = endDateTime.AddDays(-days);

            //Query function for list of companies in specified order
            var topStockPerformanceViewModel = GetContentAsync(kpi, startDateTime, endDateTime).Result;

            //Fill out the remaining ViewModel fields
            topStockPerformanceViewModel.kpi = kpi;
            topStockPerformanceViewModel.timePeriod = days;
            topStockPerformanceViewModel.kpiDict = kpiDict;

            return View("index", topStockPerformanceViewModel);
        }
        public IActionResult Filter()
        {

            //Filter values
            var kpiSM = Request.Form["kpiSelectMenu"];
            var days = int.Parse(Request.Form["timePeriod"]);

            //Generate the time period specified by filter
            var endDateTime = DateTime.ParseExact("08/12/2011 00:00:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);  //Latest end date noted in the database
            var startDateTime = endDateTime.AddDays(-days);

            //Query function for list of companies in specified order
            var topStockPerformanceViewModel = GetContentAsync(kpiSM, startDateTime, endDateTime).Result;

            //Fill out the remaining ViewModel fields
            topStockPerformanceViewModel.kpi = kpiSM;
            topStockPerformanceViewModel.timePeriod = days;
            topStockPerformanceViewModel.kpiDict = kpiDict;

            return View("index", topStockPerformanceViewModel);
        }


        public async Task<TopStockPerformanceViewModel> GetContentAsync(string kpi, DateTime start, DateTime end)
        {

            var requestData = new PerformanceRequestData()
            {
                Kpi = kpi,
                StartDate = start,
                EndDate = end

            };
            var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            //var azureFuncString = "http://localhost:7071/api/QueryData";
            var azureFuncString = "https://cs4471-topstockperformerservice.azurewebsites.net/api/QueryData";
            var response = await client.PostAsync(azureFuncString, content);
            var responseObj = await response.Content.ReadAsAsync<TopStockPerformanceViewModel>();
            return responseObj;
        }

        private class PerformanceRequestData
        {
            public string Kpi { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

    }
}