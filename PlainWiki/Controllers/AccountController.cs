using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlainWiki.Controllers
{
    public class AccountController : Controller
    {
        //Because the Signout Page from Microsoft returned an Error we redirect to the index here
        [HttpGet]
        public IActionResult SignOut(string page)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
