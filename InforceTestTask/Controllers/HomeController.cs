﻿using System.Diagnostics;
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

        public IActionResult Error(string message, int status)
        {
            return View(new ErrorVW { Message = message, Status = status });
        }
    }
}