using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class HomeModel
    {
        [Required(ErrorMessage = "Username Is Empty")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password Is Empty")]
        public string Password { get; set; }

        public HomeModel(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}