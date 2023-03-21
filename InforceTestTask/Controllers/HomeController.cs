using System.Diagnostics;
using InforceTestTask.Models;
using Microsoft.AspNetCore.Mvc;

namespace InforceTestTask.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorVW { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}