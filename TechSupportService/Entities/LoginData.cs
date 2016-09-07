using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechSupportService.Entities
{
    public class LoginData
    {
        public int ID { get; set; }
        public int WorkerID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Urole { get; set; }
    }
}