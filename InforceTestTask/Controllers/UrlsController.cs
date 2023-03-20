using Microsoft.AspNetCore.Mvc;

namespace InforceTestTask.Controllers
{
    public class UrlsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
