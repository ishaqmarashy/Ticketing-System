using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Ticketing_System.Models;

namespace Ticketing_System.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AccessDenied(string returnUrl)
        {
            ViewData["data"] = "Higher Authorization is Need to Access That Page";
            ViewData["ReturnUrl"] = returnUrl;
            return View("Index");
        }
    }
}
