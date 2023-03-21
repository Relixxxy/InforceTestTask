using InforceTestTask.Services.Interfaces;
using InforceTestTask.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InforceTestTask.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAccountsService _accountsService;
        public AccountsController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM credentials)
        {
            if (ModelState.IsValid)
            {
                var errors = await _accountsService.RegisterAsync(credentials.Email, credentials.Password);

                if (errors is null)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(credentials);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM credentials, string returnUrl = null!)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountsService.LoginAsync(credentials.Email, credentials.Password, credentials.RememberMe);

                if (result)
                {
                    if (returnUrl == null || returnUrl == "/")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Username or Password incorrect");
                }
            }

            return View(credentials);
        }

        public async Task<IActionResult> LogOut()
        {
            await _accountsService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
