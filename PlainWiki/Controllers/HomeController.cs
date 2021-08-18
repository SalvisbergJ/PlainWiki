using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlainWiki.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PlainWiki.Data;

namespace PlainWiki.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDataContext _context;

        public HomeController(ApplicationDataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HttpContext.Session.SetString("Test", "Ben Rules!");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserProfile objUser)

        {
            if (ModelState.IsValid)
            {
                {
                    var obj = _context.UserProfile.Where(a => a.UserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        HttpContext.Session.SetInt32("UserId", obj.UserId);
                        HttpContext.Session.SetString("UserName", obj.UserName.ToString());
                        Console.WriteLine(HttpContext.Session.GetInt32("UserId"));
                        Console.WriteLine(HttpContext.Session.GetString("UserName"));
                        return RedirectToAction("Index");
                        
                    }
                }
            }
            return View(objUser);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
