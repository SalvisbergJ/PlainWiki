using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlainWiki.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
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
                _context.Add(objUser);
                    await _context.SaveChangesAsync();
                    var db = await _context.UserProfile.FindAsync();
                    if(db.UserName.Equals(objUser.UserName)&&db.Password.Equals(objUser.Password))
                    {
                        var obj = objUser.UserName;
                        if (obj != null)
                        {
                            HttpContext.Session("UserName") = obj.ToString();
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
