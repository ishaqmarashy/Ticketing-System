using Microsoft.AspNetCore.Mvc;
using System;
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
        public IActionResult ProcessTicket(TicketInput search)
        {
            DbAccess db = new DbAccess(_configuration);
            string queryStr = " SELECT * FROM UTICKETS WHERE UTICKETS.ID=\"" + search.Complaint + "\";";
            JArray Jar = db.Get(queryStr);
            queryStr = (TableBuilder.Build(Jar, null, null));
            if (queryStr == "Invalid Input")
                ViewData["data"] = "<div>Ticket does not exist</div>";
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
            ViewData["data"] = "Editing ticket ID:" + id + "'s complaint";
            ViewData["id"] = id;
            TempData["id"] = id;
            return View("UpdateTicket");
        }

        [Authorize(Roles = "ADMIN")]
        [Route("Ticket/UpdateAdmin/{id}")]
        [HttpGet]
        public IActionResult UpdateAdmin(string id)
        {
            ViewData["data"] = "Editing ticket " + id + "'s Complaint";
            TempData["id"] = id;
            ViewData["id"] = id;
            return View("UpdateAdminTicket");
        }
        public IActionResult ProcessUpdate(TicketInput ticket)
        {
            DbAccess db = new DbAccess(_configuration);
            var id = TempData["id"];
            string queryString = "UPDATE TICKETS SET COMPLAINT=\"" + ticket.Complaint
                                                                   + "\" WHERE ID=" + id + ";";
            if (db.Post(queryString))
                TempData["update"] = "Ticket ID:" + id + " has been updated";
            else
                TempData["update"] = ticket.Complaint + " Failed to update ticket ID:" + id;
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
                TempData["update"] = "Ticket ID:" + id + " has been updated";
            else
                TempData["update"] = queryString + ticket.State + " Failed to Update Ticket ID:" + id;
            return RedirectToAction("MyTicketsAdmin");
        }
        public IActionResult CreateTicket()
        {
            return View();
        }
        public IActionResult ProcessCreateTicket(TicketInput ticket)
        {
            DbAccess db = new DbAccess(_configuration);
            String queryString = " INSERT INTO TICKETS (COMPLAINT,STATES_TSTATE) VALUES (\"" + ticket.Complaint + "\",\"OPEN\"); ";
            Boolean Succuss = db.Post(queryString);
            if (Succuss)
            {
                queryString = "SELECT * FROM TICKETS WHERE TICKETS.COMPLAINT =\"" + ticket.Complaint + "\" AND TICKETS.STATES_TSTATE=\"OPEN\";";
                JArray array = db.Get(queryString);
                string ticketID = array[0]["ID"].ToString();
                ViewData["data"] = "Ticket created successfully! Your tickets ID is: " + ticketID;
                queryString = "INSERT INTO UID VALUES (" + ticketID + ",\"" + User.Identity.Name + "\");";
                Succuss = db.Post(queryString);
                if (Succuss)
                {
                    ViewData["data"] = ViewData["data"] + " and connected to user: " + User.Identity.Name;
                }
                else
                {
                    ViewData["data"] = "empty ticket";
                }
            }
            else
            {
                ViewData["data"] = "failed to create ticket";
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
                ViewData["data"] = "<p>You have no tickets</p>";
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
                ViewData["data"] = "<p>There are no tickets</p>";
            else ViewData["data"] = queryString;
            return View("AdminAllTickets");
        }
    }
}