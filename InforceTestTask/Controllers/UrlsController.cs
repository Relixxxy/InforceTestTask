using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InforceTestTask.Controllers
{
    [Authorize]
    public class UrlsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UrlInfo(int id)
        {
            return View();
        }
    }
}
