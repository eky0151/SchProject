using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechSupportService.Entities
{
    public class Technician
    {
        public int ID { get; set; }
        public int WorkerID { get; set; }
        public string Available { get; set; }
        public System.Data.Entity.Spatial.DbGeography Lastlocation { get; set; }
    }
}