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
        public IActionResult SearchTicket(){
            return View();
        }
        public IActionResult ProcessTicket(ErrorViewModel search)
        {
            DbAccess db = new DbAccess(_configuration);
            string queryStr = " SELECT * FROM UTICKETS WHERE UTICKETS.ID=\"" + search.RequestId + "\";";
            JArray Jar = db.Get(queryStr);
            if ((BuildTable(Jar, null, null)) == "Invalid Input")
                ViewData["data"] = "<div>Could not Find Ticket</div>";
            else ViewData["data"] = (BuildTable(Jar, null, null));

            return View();
        }
        [Authorize(Roles = "ADMIN")]
        [Route("Ticket/Delete/{id}")]
        [HttpGet]
        public  Boolean Delete(string id)
        {
            DbAccess db = new DbAccess(_configuration);
            String queryStr = "DELETE FROM UID WHERE UID.TICKETS_ID="+id+";" +
                              "DELETE FROM TICKETS WHERE TICKETS.ID="+id+";";
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
            var id= TempData["id"];
            string queryString = "UPDATE TICKETS SET COMPLAINT=\"" + ticket.RequestId
                                                                   + "\" WHERE ID=" + id + ";";
            if (db.Post(queryString))
                TempData["update"] = "Ticket ID:" + id + " Has Been Updated";
            else
                TempData["update"] = ticket.RequestId+" Failed to Update Ticket ID:" + id;
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
                TempData["update"] = queryString +ticket.State+ " Failed to Update Ticket ID:" + id;
            return RedirectToAction("MyTicketsAdmin");
        }
        public IActionResult CreateTicket() {
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
                ViewData["data"] = "Ticket Created Successfully! Your Tickets is ID: "+ ticketID;
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
        public IActionResult MyTickets() {
            DbAccess db = new DbAccess(_configuration);
            string queryString= "SELECT * FROM UTICKETS WHERE UTICKETS.UNAME=\"" + User.Identity.Name+"\";";
            JArray Jar=db.Get(queryString);
            if ((BuildTable(Jar, null, null)) == "Invalid Input")
                ViewData["data"] = "<div>You Have no Tickets</div>";
            else ViewData["data"] = (BuildTable(Jar, 1, 0));
            return View();
        }
        //access= 0 update 1 delete 2 update&delete chooses buttons
        //postfix specifies if there is a button
        [Authorize(Roles = "ADMIN")]
        public IActionResult MyTicketsAdmin()
        {
            DbAccess db = new DbAccess(_configuration);
            string queryString = "SELECT * FROM UTICKETS;";
            JArray Jar = db.Get(queryString);
            if ((BuildTable(Jar, null, null)) == "Invalid Input")
                ViewData["data"] = "<div>You Have no Tickets</div>";
            else ViewData["data"] = (BuildTable(Jar, 1, 2));
            return View("AdminAllTickets");
        }

        public static string BuildTable(JArray Jar,int? postfix, int? access)
        {
            if (postfix == null)
                access = null;
            else if(access == null) 
                postfix = null;
            else if(access<0&&access>=3){
                access = null;
                postfix = null;}
            try
            {
                var result = " <table id=\"myTable\" class=\"table table-bordered\" style=\"height: 100 %; width: 100 %;\" >   <thead>";
                int i = 0;
                result += " <tr> ";
                result += " <th scope=\"col\"> ROW </th>";
                Boolean postFixed = postfix != null;
                var identifier = "";
                var identifierMod = "";
                var identifierLoc = postfix;
                foreach (var col in Jar[0])
                {
                    identifier = col.ToString().Replace("\"", "").Split(":")[0];
                    if (i == identifierLoc)
                        identifierMod = identifier;
                    i++;
                    result += " <th scope=\"col\"> " + identifier + "</th>";
                }
                if (postFixed)
                {
                    if (postFixed)
                    {
                        result += " <th scope=\"row\">";
                        if (access == 0)
                            result += "UPDATE "+identifierMod;
                        else if (access == 1)
                            result += "DELETE "+ identifierMod;
                        else result += "UPDATE TICKET BY " + identifierMod+ " </th><th>DELETE TICKET BY " + identifierMod;
                        result += "</th>";
                    }
                }
                i = 0;
                identifierMod = "";
                result += " </tr>  </thead>  <tbody>";
                foreach (var row in Jar)
                {
                    
                    result += " <tr id=\"" + i + "\">  ";
                    result += " <th id=\""+i+"\" scope=\"row\">" + i + "</th>";
                    int ii = 0;
                    foreach (var col in row)
                    {
                        if (ii == identifierLoc) 
                            identifierMod = col.Last.ToString();
                        result += " <th  scope=\"row\">" + col.Last + "</th>";
                        ii++;
                    }
                    if (postFixed)
                    {
                        result += " <th scope=\"row\">";
                        if (access == 0)
                            result += "<button id=\""+ identifierMod + "\" name=\""+i+ "\" value=\"UPDATE\">UPDATE</button>";
                        else if (access == 1)
                            result += "<button id=\"" + identifierMod + "\" name=\"" + i + "\" value=\"DELETE\">DELETE</button>";
                        else result += "<button  id=\"" + identifierMod + "\" name=\"" + i + "\" value=\"UPDATE\">UPDATE</button> " +
                                       "</th><th> <button id=\"" + identifierMod + "\" name=\"" + i + "\" value=\"DELETE\">DELETE</button>";
                        result += "</th>";
                    }
                    result += " </tr>";
                    i++;
                }
                return result += "  </tbody> </table>";
            }
            catch (Exception)
            {
                return "Invalid Input";
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
