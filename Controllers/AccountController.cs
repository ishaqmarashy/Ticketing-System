using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Ticketing_System.Models;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using Ticketing_System.DataAccess;

namespace Ticketing_System.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        } 

        [Route("/Account/Login")]
        [HttpGet("Login")]
        public IActionResult Login (string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpPost("Login")]
        public async Task<IActionResult> ProcessLogin(LoginModel user,string? returnUrl)
        {
            string md5 = CreateMd5(user.Password);
            string queryStr = "SELECT * FROM USERS WHERE USERS.UNAME = \"" + user.Username +
                              "\" AND USERS.PASSWORD = \"" + md5 + "\";";
            DbAccess db = new DbAccess(_configuration);
            JArray resultJArray = db.Get(queryStr);
            if (resultJArray!=null&&resultJArray.HasValues)
            {
                TempData["username"] = user.Username;
                TempData["auth"] = md5;
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.Username));
                claims.Add(new Claim(ClaimTypes.Role, resultJArray[0]["AUTH"].ToString()));
                var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return RedirectToAction("MyTickets","Ticket");
            }
            else
                ViewData["data"] = "Incorrect Login Credentials";
            return View("Login");
        }
        [HttpPost("Register")]
        public IActionResult ProcessRegistration(LoginModel user)
        {
            if (!(string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Username)))
            {
                string QueryStr = "INSERT INTO USERS VALUES(\"" + user.Username + "\",\"" + CreateMd5(user.Password) +
                                  "\",\"USER\");";
                DbAccess db = new DbAccess(_configuration);
                ViewData["data"] = db.Post(QueryStr);
                if ((Boolean) ViewData["data"])
                    ViewData["data"] = "User Successfully Created";
                else{
                    ViewData["data"] = "Failed to Create User";
                    return View("Register");
                }
            }
            return View("Login");
        }
        //used for converting passwords into md5, which is stored into the db.
        [Authorize(Roles = "ADMIN")]
        [Route("Account/Delete/{id}")]
        [HttpGet]
        public Boolean Delete(string id)
        {
            DbAccess db = new DbAccess(_configuration);
            String queryStr = "DELETE FROM USERS WHERE UNAME=\"" + id + "\";";
            return db.Post(queryStr);
        }
        [Authorize(Roles = "ADMIN")]
        public IActionResult AllUsersAdmin()
        {
            DbAccess db = new DbAccess(_configuration);
            string queryStr = "SELECT * FROM USERS";
            JArray Jar=db.Get(queryStr);
            ViewData["data"]=TableBuilder.Build(Jar,0,1);
            return View();
        } 
        public static string CreateMd5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                    sb.Append(hashBytes[i].ToString("X2"));
                return sb.ToString();
            }
        }
        //opens mysql connection and makes a query, returning the result as a json.
    
    }
}