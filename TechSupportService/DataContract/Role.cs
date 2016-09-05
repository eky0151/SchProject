using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TechSupportService.DataContract
{
    [DataContract]
    public enum Role
    {
        [EnumMember]
        HelpDesk,
        [EnumMember]
        Admin,
        [EnumMember]
        Technician,
        [EnumMember]
        Boss
    }
}