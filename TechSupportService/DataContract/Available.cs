using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TechSupportService.DataContract
{
    [DataContract]
    public enum Available
    {
        [EnumMember]
        FromCustomer,
        [EnumMember]
        InCustomer,
        [EnumMember]
        NotAvailable,
        [EnumMember]
        Break
    }
}