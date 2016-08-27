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
        [Required(ErrorMessage = "Username Is Empty")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password Is Empty")]
        public string Password { get; set; }
    }
}