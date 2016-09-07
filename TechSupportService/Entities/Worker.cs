using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechSupportService.Entities
{
    public class Worker
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int? BankID { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public string ProfilePicture { get; set; }
    }
}