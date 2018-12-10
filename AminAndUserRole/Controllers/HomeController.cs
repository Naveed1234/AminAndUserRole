using AminAndUserRole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AminAndUserRole.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            InvertoryContext db = new InvertoryContext();
            User u = db.Users.First();
            return View(u);
        }
    }
}