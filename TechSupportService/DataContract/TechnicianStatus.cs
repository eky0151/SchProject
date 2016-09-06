using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TechSupportService.DataContract
{
    [DataContract]
    public enum TechnicianStatus
    {
        [EnumMember]
        FromCustomer,
        [EnumMember]
        AtCustomer,
        [EnumMember]
        Break,
        [EnumMember]
        Available
    }
}