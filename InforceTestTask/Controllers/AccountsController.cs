using Microsoft.AspNetCore.Mvc;

namespace InforceTestTask.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
