using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.TechSupportServiceReference;

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
        public ActionResult Registration(string username, string fullname, string password, string email)
        {
            using (TechSupportService1Client client = new TechSupportService1Client())
            {
                client.Open();
                client.RegisterNewUser(fullname, email, username, password);
            }

            return View(new RegisterModel(username, fullname, password, email));
        }
    }
}