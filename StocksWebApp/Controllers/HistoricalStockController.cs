using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StocksWebApp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StocksWebApp.Controllers
{
    public class HistoricalStockController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly Dictionary<string, string> companies = new Dictionary<string, string>()
        {
            { "ctcm", "CTC Media Inc" },
            { "dpm", "DCP Midstream Partners LP"},
            { "dx", "Dynex Capital Inc"},
            { "lit","Global X Funds"},
            { "npo", "Enpro Industries Inc" },
            { "nymx", "Nymox Pharmaceutical Corp"},
            { "pmm", "Putnam Managed Muni Income TR" },
            { "sbra", "Sabra Healthcare Reit Inc" },
            { "smrt", "Stein Mart Inc" },
            { "ttt", "Proshares Trust"  }
        };
        // GET: /<controller>/
        public IActionResult Index()
        {
            DateTime start = new DateTime(2011,1,13,0,0,0);
            DateTime end = new DateTime(2011,1,13,23,59,59);
            string company = "ctcm";

            var obj = GetContentAsync(company,start, end).Result;
            obj.CompanyDict = companies;
            return View(obj);
        }
        [HttpPost]
        public IActionResult Update(string Company, string StartDate, string EndDate)
        {
            var startDateObj = DateTime.ParseExact(StartDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            var endDateObj = DateTime.ParseExact(EndDate, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            var obj = GetContentAsync(Company, startDateObj, endDateObj).Result;
            obj.CompanyDict = companies;
            return Json(obj);
        }
        public async Task<HistoricalStockViewModel> GetContentAsync(string company, DateTime start, DateTime end)
        {

            var requestData = new RequestData()
            {
                Company = company,
                StartDate = start,
                EndDate = end

            };
            var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
            //var azureFuncString = "http://localhost:7071/api/Function1";
            var azureFuncString = "https://cs4471-historicalstockservice.azurewebsites.net/api/Function1";
            var response = await client.PostAsync(azureFuncString, content);
            var responseObj = await response.Content.ReadAsAsync<HistoricalStockViewModel>();
            return responseObj;
        }
    }

    public class RequestData
    {
        public string Company { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
