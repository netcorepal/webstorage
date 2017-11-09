using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreWeb.Models;
using NetCorePal.WebStorage;
namespace NetCoreWeb.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(WebStorageProvider provider)
        {

        }

        public IActionResult Index([FromServices]WebStorageProvider provider)
        {
            var file = provider.GetFile("/f/abc.txt");
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
