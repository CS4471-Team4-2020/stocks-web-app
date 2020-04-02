using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StocksWebApp.Models;
//using Microsoft.Azure.CosmosDB.Table;

namespace StocksWebApp.Controllers
{
    public class StockSummaryController : Controller
    {
        static HttpClient client = new HttpClient();

        public IActionResult Index()
        {
            {
                ViewBag.Stockname = "Amzn";
                ViewBag.fiftylow = 1944.01;
                ViewBag.fiftyhigh = 1993.02;
                ViewBag.marketCap = "970.59B";
                ViewBag.priceEarnRatio = 84.80;
            };
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(IFormCollection formCollection)
        {
            foreach (var key in formCollection.Keys)
            {
                ViewBag.DisplayStock = formCollection["nameOfStock"];
            }


            var name = formCollection["nameOfStock"].ToString();

            var msg = new StockNameModel();
            msg.stockName = name;

            var content = new StringContent(JsonConvert.SerializeObject(msg), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("https://cs4471-stocksummaryandcomparisonservice.azurewebsites.net/api/StockSummary", content);
            response.EnsureSuccessStatusCode();

            if (!response.IsSuccessStatusCode)
            {
                return View();
            }
            var data = JsonConvert.DeserializeObject<StockSummaryInfo>(response.Content.ReadAsStringAsync().Result);

            ViewBag.lowdata = data.low;
            ViewBag.highdata = data.high;
            ViewBag.marketCap = data.marketCap;
            ViewBag.priceEarnRatio = data.priceToEarnRatio;
            return View();
        }
    }

}
