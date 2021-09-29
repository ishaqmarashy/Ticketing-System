using System;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc;
using Ticketing_System.Models;
using System.Text;

namespace Ticketing_System.Controllers
{
    public class Login : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProcessRegistration(LoginModel user)
        {
            if (!(string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Username)))
            {
                String QueryStr = "INSERT INTO USERS values(\"" + user.Username + "\",\"" + CreateMd5(user.Password) + "\",\"USER\");";
                String reader = new SQLcon().MakeRequest(QueryStr);
                ViewData["data"] = reader;
                return View("Index");
            }
            return View("Index");
        }
        public static string CreateMd5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}