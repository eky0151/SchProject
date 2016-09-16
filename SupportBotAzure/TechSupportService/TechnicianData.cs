using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SupportBot
{
    public class TechnicianData
    {
        public Resgisternewtechworkresult ResgisterNewTechWorkResult { get; set; }
    }

    public class Resgisternewtechworkresult
    {
        public string Address { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int ID { get; set; }
        public string Phone { get; set; }
        public string ProfilePicture { get; set; }
        public int Role { get; set; }
        public int Status { get; set; }
        public string Username { get; set; }
        public string Location { get; set; }
        public int TechnicianID { get; set; }
        public int TechnicianStatus { get; set; }
    }

}