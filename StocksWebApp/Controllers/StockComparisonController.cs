using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StocksWebApp.Controllers
{
    public class StockComparisonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}