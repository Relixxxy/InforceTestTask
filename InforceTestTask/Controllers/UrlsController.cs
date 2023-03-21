using InforceTestTask.Services.Interfaces;
using InforceTestTask.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InforceTestTask.Controllers
{
    [Authorize]
    public class UrlsController : Controller
    {
        private readonly IUrlsService _urlsService;

        public UrlsController(IUrlsService urlsService)
        {
            _urlsService = urlsService;
        }

        public async Task<IActionResult> Index()
        {
            var urls = await _urlsService.GetUrlsListAsync();
            return View(urls);
        }

        public async Task<IActionResult> UrlInfo(int id)
        {
            var url = await _urlsService.GetUrlAsync(id);
            return View(url);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUrlVM url)
        {
            var result = await _urlsService.AddUrlAsync(url.OriginalUrl, User.Identity!.Name!);

            return RedirectToAction(nameof(Index));
        }
    }
}
