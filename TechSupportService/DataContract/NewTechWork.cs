using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TechSupportService.DataContract
{
    [DataContract]
    public class NewTechWork
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public int TechID { get; set; }

        [DataMember]
        public DateTime TimeOrdered { get; set; }
    }
}