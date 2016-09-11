﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebApp.TechSupportServiceReference;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            using (TechSupportService1Client client = new TechSupportService1Client())
            {               
                if (client.UserLogin(username, password))
                {                   
                    return View(new UserModel(username));
                }
                else
                {
                    return View("~/Views/Home/index.cshtml"); //TODO: incorrect username or password
                }
            }
        }

        [HttpPost]
        public ActionResult Send(string name, string message)
        {
            UserModel.ChatText += String.Format("{0} {1}: {2}", Environment.NewLine, name, message);

            return View("~/Views/User/Login.cshtml");
        }

    }
}