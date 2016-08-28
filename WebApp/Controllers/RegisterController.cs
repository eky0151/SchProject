using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View("RegisterIndex");
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registermodel)
        {
            return RedirectToAction("Success");
        }
    }
}