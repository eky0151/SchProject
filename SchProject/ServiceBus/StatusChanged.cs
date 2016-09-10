using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using SchProject.TechSupportSecure;

namespace TechSupportService.DataContract
{
    [DataContract]
    public class StatusChanged
    {
        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public Status NewStatus { get; set; }
    }
}