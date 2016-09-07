using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApp.Models
{
    public class UserModel
    {
        public string Username { get; set; }

        public static string ChatText { get; set; }

        public UserModel(string username)
        {
            this.Username = username;
            ChatText = "The chat is now open";
        }
    }
}