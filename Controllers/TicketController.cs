using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Ticketing_System.DataAccess;
using Ticketing_System.Models;

namespace Ticketing_System.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private readonly IConfiguration _configuration;
        public TicketController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult SearchTicket()
        {
            return View();
        }
        public IActionResult ProcessTicket(ErrorViewModel search)
        {
            DbAccess db = new DbAccess(_configuration);
            string queryStr = " SELECT * FROM UTICKETS WHERE UTICKETS.ID=\"" + search.RequestId + "\";";
            JArray Jar = db.Get(queryStr);
            queryStr = (TableBuilder.Build(Jar, null, null));
            if (queryStr == "Invalid Input")
                ViewData["data"] = "<div>No Such Ticket</div>";
            else ViewData["data"] = queryStr;
            return View();
        }
        [Authorize(Roles = "ADMIN")]
        [Route("Ticket/Delete/{id}")]
        [HttpGet]
        public Boolean Delete(string id)
        {
            DbAccess db = new DbAccess(_configuration);
            String queryStr = "DELETE FROM UID WHERE UID.TICKETS_ID=" + id + ";" +
                              "DELETE FROM TICKETS WHERE TICKETS.ID=" + id + ";";
            return db.Post(queryStr);
        }
        [Route("Ticket/Update/{id}")]
        [HttpGet]
        public IActionResult Update(string id)
        {
            ViewData["data"] = "Editing Ticket " + id + "'s Complaint";
            ViewData["id"] = id;
            TempData["id"] = id;
            return View("UpdateTicket");
        }

        [Authorize(Roles = "ADMIN")]
        [Route("Ticket/UpdateAdmin/{id}")]
        [HttpGet]
        public IActionResult UpdateAdmin(string id)
        {
            ViewData["data"] = "Editing Ticket " + id + "'s Complaint";
            TempData["id"] = id;
            ViewData["id"] = id;
            return View("UpdateAdminTicket");
        }
        public IActionResult ProcessUpdate(ErrorViewModel ticket)
        {
            DbAccess db = new DbAccess(_configuration);
            var id = TempData["id"];
            string queryString = "UPDATE TICKETS SET COMPLAINT=\"" + ticket.RequestId
                                                                   + "\" WHERE ID=" + id + ";";
            if (db.Post(queryString))
                TempData["update"] = "Ticket ID:" + id + " Has Been Updated";
            else
                TempData["update"] = ticket.RequestId + " Failed to Update Ticket ID:" + id;
            return RedirectToAction("MyTickets");
        }
        [Authorize(Roles = "ADMIN")]
        public IActionResult ProcessUpdateAdmin(TicketModel ticket)
        {
            DbAccess db = new DbAccess(_configuration);
            var id = TempData["id"];
            string queryString = "UPDATE TICKETS SET " +
                                 "COMPLAINT=\"" + ticket.Complaint + "\", " +
                                 "STATES_TSTATE=\"" + ticket.State + "\" " +
                                 "WHERE ID=" + id + ";";
            if (db.Post(queryString))
                TempData["update"] = "Ticket ID:" + id + " Has Been Updated";
            else
                TempData["update"] = queryString + ticket.State + " Failed to Update Ticket ID:" + id;
            return RedirectToAction("MyTicketsAdmin");
        }
        public IActionResult CreateTicket()
        {
            return View();
        }
        public IActionResult ProcessCreateTicket(ErrorViewModel ticket)
        {
            DbAccess db = new DbAccess(_configuration);
            String queryString = " INSERT INTO TICKETS (COMPLAINT,STATES_TSTATE) VALUES (\"" + ticket.RequestId + "\",\"OPEN\"); ";
            Boolean Succuss = db.Post(queryString);
            if (Succuss)
            {
                queryString = "SELECT * FROM TICKETS WHERE TICKETS.COMPLAINT =\"" + ticket.RequestId + "\" AND TICKETS.STATES_TSTATE=\"OPEN\";";
                JArray array = db.Get(queryString);
                string ticketID = array[0]["ID"].ToString();
                ViewData["data"] = "Ticket Created Successfully! Your Tickets is ID: " + ticketID;
                queryString = "INSERT INTO UID VALUES (" + ticketID + ",\"" + User.Identity.Name + "\");";
                Succuss = db.Post(queryString);
                if (Succuss)
                {
                    ViewData["data"] = ViewData["data"] + " and connected to user: " + User.Identity.Name;
                }
                else
                {
                    ViewData["data"] = "Empty Ticket";
                }
            }
            else
            {
                ViewData["data"] = "Failed to Create Ticket";
            }


            return View("CreateTicket");
        }
        [Route("Ticket/MyTickets")]
        public IActionResult MyTickets()
        {
            DbAccess db = new DbAccess(_configuration);
            string queryString = "SELECT * FROM UTICKETS WHERE UTICKETS.UNAME=\"" + User.Identity.Name + "\";";
            JArray Jar = db.Get(queryString);
            queryString = (TableBuilder.Build(Jar, 1, 0));
            if (queryString == "Invalid Input")
                ViewData["data"] = "<div>You Have no Tickets</div>";
            else ViewData["data"] = queryString;
            return View();
        }
        [Authorize(Roles = "ADMIN")]
        public IActionResult MyTicketsAdmin()
        {
            DbAccess db = new DbAccess(_configuration);
            string queryString = "SELECT * FROM UTICKETS;";
            JArray Jar = db.Get(queryString);
            queryString = (TableBuilder.Build(Jar, 1, 2));
            if (queryString == "Invalid Input")
                ViewData["data"] = "<div>There are Tickets</div>";
            else ViewData["data"] = queryString;
            return View("AdminAllTickets");
        }
    }
}