using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class RegisterModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Email { get; set; }

        [Compare("Email", ErrorMessage = "Confirm email doesn't match, Type again !")]
        public string ConfirmEmail { get; set; }

        [Required]
        public HttpPostedFileBase UploadImage { get; set; }

        public RegisterModel(string username, string fullname, string password, string email, HttpPostedFileBase uploadimage)
        {
            this.Username = username;
            this.FullName = fullname;
            this.Password = password;
            this.Email = email;
            this.UploadImage = uploadimage;
        }
    }


}