using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View("UserIndex");
        }

        [HttpPost]
        public ActionResult Login(HomeModel homemodel)
        {
            return View("UserIndex");
        }
    }
}