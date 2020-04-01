using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ChoETL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StocksWebApp.Controllers
{
    public class UploadController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
                Environment.GetEnvironmentVariable("UPLOAD_AZURE_FUNC_APP_URL "), jsonR);
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