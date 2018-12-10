using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AminAndUserRole.Models;

namespace AminAndUserRole.Controllers
{
    public class UsersController : Controller
    {
        private InvertoryContext db = new InvertoryContext();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Password,ConfirmPassword,Address,PostalCode,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Role = new Role { Id = 2 };
                db.Users.Add(user);
                db.Entry(user.Role).State = EntityState.Unchanged;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Password,ConfirmPassword,Address,PostalCode,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //Code himself for role

        public ActionResult Login()
        {
            HttpCookie hc = Request.Cookies["login"];
            if (hc != null)
            {
                User user = new UserData().GetUser(hc.Values["usr"], hc.Values["pass"]);
                if (user != null)
                {
                    hc.Expires = DateTime.Today.AddDays(2);
                    Session.Add(Utility.CurrentUser, user);
                    if (user.IsinRole(Utility.Admin))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                   
        
                        return RedirectToAction("Index", "Home");   
                }
            }

            ViewBag.Controller = Request.QueryString["contr"];
            ViewBag.Act = Request.QueryString["act"];
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            User user = new UserData().GetUser(model.UserName, model.Password);
            if (user != null)
            {
                Session.Add(Utility.CurrentUser, user);
                string contr = Request.QueryString["c"];
                string act = Request.QueryString["a"];
                if (!string.IsNullOrEmpty(contr) && string.IsNullOrEmpty(act))
                {
                    return RedirectToAction(act, contr);
                }
                if (user.IsinRole(Utility.Admin))
                {
                    return RedirectToAction("Index", "Admin");
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Your LoginId and Password are Wrong..Please try Again!";
            }
            return View();
        }

    }
}
