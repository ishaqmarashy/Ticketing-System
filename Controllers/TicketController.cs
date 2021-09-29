using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Ticketing_System.Models;

namespace Ticketing_System.Controllers
{
    public class TicketController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProcessTicket(ErrorViewModel ticket)
        {
            if (ticket.ShowRequestId)
                return PartialView("TicketsDetail", ticket);
            return View("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
