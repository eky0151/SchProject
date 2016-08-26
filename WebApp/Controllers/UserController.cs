using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View("UserIndex");
        }

        public ActionResult Login()
        {
            //TODO: Check form input
            Session["UserID"] = 42;
            return RedirectToAction("Index");
        }
    }
}