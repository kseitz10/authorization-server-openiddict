using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace AuthorizationServer.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}