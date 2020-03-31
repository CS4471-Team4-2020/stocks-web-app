using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ChoETL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StocksWebApp.Models;

namespace StocksWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        static HttpClient client = new HttpClient();

        /// <summary>
        /// Gets the consumer scores by retailer id and return id
        /// </summary>
        /// <param name="retailerId"></param>
        /// <param name="returnId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");

            //var path = Path.Combine(
            //            Directory.GetCurrentDirectory(), "wwwroot",
            //            file.FileName);

            var result = string.Empty;

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                result = reader.ReadToEnd();
            }

            result = "Time,Price,Size,Exchange,SaleCondition,Suspicious\r\n" + result;


            StringBuilder sb = new StringBuilder();
            using (var p = ChoCSVReader.LoadText(result)
                .WithFirstLineHeader())
            {
                using (var w = new ChoJSONWriter(sb))
                    w.Write(p);
            }

            var jsonR = sb.ToString();
            jsonR = jsonR.Trim().Replace("\r", string.Empty);
            jsonR = jsonR.Trim().Replace("\n", string.Empty);
            jsonR = jsonR.Replace(Environment.NewLine, string.Empty);
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "https://cs4471-uploadexportservice.azurewebsites.net/api/HttpTrigger1?code=Rwp7t88s15SELSgp4LjZXYxmCM86bFt4HWnNRv/wvlGaPxZ/p4lXEg==", jsonR);
            response.EnsureSuccessStatusCode();

            return new EmptyResult();
        }

        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "csv", Path.GetFileName(path));
        }
    }
}
