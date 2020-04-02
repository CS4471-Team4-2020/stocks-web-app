using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Collections.Generic;
using StocksWebApp.Models;
using Newtonsoft.Json;
using System.Text;

namespace StocksWebApp.Controllers
{
    public class StockComparisonController : Controller
    {
        //public static string UsrStockname1 = "insert default value here";

        static HttpClient client = new HttpClient();

        public IActionResult Index()
        {

            ViewBag.Stockname1 = "Amzn";
            ViewBag.fiftylow1 = 1944.01;
            ViewBag.fiftyhigh1 = 1993.02;
            ViewBag.marketCap1 = "970.59B";
            ViewBag.priceEarnRatio1 = 84.80;

            ViewBag.Stockname2 = "ACN";
            ViewBag.fiftylow2 = 137.15;
            ViewBag.fiftyhigh2 = 216.39;
            ViewBag.marketCap2 = "102.31B";
            ViewBag.priceEarnRatio2 = 20.08;

            ViewBag.Stockname3 = "AAPL";
            ViewBag.fiftylow3 = 170.27;
            ViewBag.fiftyhigh3 = 327.85;
            ViewBag.marketCap3 = "1.05T";
            ViewBag.priceEarnRatio3 = 18.98;

            ViewBag.Stockname4 = "FB";
            ViewBag.fiftylow4 = 137.10;
            ViewBag.fiftyhigh4 = 224.20;
            ViewBag.marketCap4 = "453.39B";
            ViewBag.priceEarnRatio4 = 24.75;

            ViewBag.Stockname5 = "MSFT";
            ViewBag.fiftylow5 = 118.38;
            ViewBag.fiftyhigh5 = 190.70;
            ViewBag.marketCap5 = "1.16T";
            ViewBag.priceEarnRatio5 = 26.53;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOne(IFormCollection formCollection)
        {
            //TODO should be in a list but too lazy
            ViewBag.UsrStockname1 = formCollection["nameOfStockOne"];
            ViewBag.UsrStockname2 = formCollection["nameOfStockTwo"];
            ViewBag.UsrStockname3 = formCollection["nameOfStockThree"];
            ViewBag.UsrStockname4 = formCollection["nameOfStockFour"];
            ViewBag.UsrStockname5 = formCollection["nameOfStockFive"];

            var msg = new StockNamesModel();
            msg.stockNames = new List<string>();

            if (!string.IsNullOrWhiteSpace(formCollection["nameOfStockOne"].ToString())) { msg.stockNames.Add(formCollection["nameOfStockOne"].ToString()); }
            if (!string.IsNullOrWhiteSpace(formCollection["nameOfStockTwo"].ToString())) { msg.stockNames.Add(formCollection["nameOfStockTwo"].ToString()); }
            if (!string.IsNullOrWhiteSpace(formCollection["nameOfStockThree"].ToString())) { msg.stockNames.Add(formCollection["nameOfStockThree"].ToString()); }
            if (!string.IsNullOrWhiteSpace(formCollection["nameOfStockFour"].ToString())) { msg.stockNames.Add(formCollection["nameOfStockFour"].ToString()); }
            if (!string.IsNullOrWhiteSpace(formCollection["nameOfStockFive"].ToString())) { msg.stockNames.Add(formCollection["nameOfStockFive"].ToString()); }

            var content = new StringContent(JsonConvert.SerializeObject(msg), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("https://cs4471-stocksummaryandcomparisonservice.azurewebsites.net/api/StockComparison", content);
            response.EnsureSuccessStatusCode();

            var stockSummaries = JsonConvert.DeserializeObject<StockComparisonInfo>(response.Content.ReadAsStringAsync().Result);

            //this view bag should a array/list but im lazy so whatever
            if (stockSummaries.StockSummaries.Count >= 1)
            {
                foreach (StockSummaryInfo element in stockSummaries.StockSummaries)
                {


                    if (element?.StockName != null)
                    {
                        if (string.Compare(element.StockName, formCollection["nameOfStockOne"].ToString()) == 0)
                        {
                            ViewBag.Usrfiftylow1 = element.low;
                            ViewBag.Usrfiftyhigh1 = element.high;
                            ViewBag.UsrmarketCap1 = element.marketCap;
                            ViewBag.UsrpriceEarnRatio1 = element.priceToEarnRatio;
                        }
                    }
                }

            }
            if (stockSummaries.StockSummaries.Count >= 2)
            {
                foreach (StockSummaryInfo element in stockSummaries.StockSummaries)
                {

                    if (element != null && element.StockName != null)
                    {
                        if (string.Compare(element.StockName, formCollection["nameOfStockTwo"].ToString()) == 0)
                        {
                            ViewBag.Usrfiftylow2 = element.low;
                            ViewBag.Usrfiftyhigh2 = element.high;
                            ViewBag.UsrmarketCap2 = element.marketCap;
                            ViewBag.UsrpriceEarnRatio2 = element.priceToEarnRatio;
                        }
                    }
                }
            }
            if (stockSummaries.StockSummaries.Count >= 3)
            {
                foreach (StockSummaryInfo element in stockSummaries.StockSummaries)
                {

                    if (element != null && element.StockName != null)
                    {
                        if (string.Compare(element.StockName, formCollection["nameOfStockThree"].ToString()) == 0)
                        {
                            ViewBag.Usrfiftylow3 = element.low;
                            ViewBag.Usrfiftyhigh3 = element.high;
                            ViewBag.UsrmarketCap3 = element.marketCap;
                            ViewBag.UsrpriceEarnRatio3 = element.priceToEarnRatio;
                        }
                    }
                }
            }
            if (stockSummaries.StockSummaries.Count >= 4)
            {
                foreach (StockSummaryInfo element in stockSummaries.StockSummaries)
                {

                    if (element != null && element.StockName != null)
                    {
                        if (string.Compare(element.StockName, formCollection["nameOfStockFour"].ToString()) == 0)
                        {
                            ViewBag.Usrfiftylow4 = element.low;
                            ViewBag.Usrfiftyhigh4 = element.high;
                            ViewBag.UsrmarketCap4 = element.marketCap;
                            ViewBag.UsrpriceEarnRatio4 = element.priceToEarnRatio;
                        }
                    }
                }
            }
            if (stockSummaries.StockSummaries.Count >= 5)
            {
                foreach (StockSummaryInfo element in stockSummaries.StockSummaries)
                {

                    if (element != null && element.StockName != null)
                    {
                        if (string.Compare(element.StockName, formCollection["nameOfStockFive"].ToString()) == 0)
                        {
                            ViewBag.Usrfiftylow5 = element.low;
                            ViewBag.Usrfiftyhigh5 = element.high;
                            ViewBag.UsrmarketCap5 = element.marketCap;
                            ViewBag.UsrpriceEarnRatio5 = element.priceToEarnRatio;
                        }
                    }
                }
            }

            return View();
        }
    }
}