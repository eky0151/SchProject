using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

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