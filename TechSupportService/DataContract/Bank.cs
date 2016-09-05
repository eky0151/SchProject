using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TechSupportService
{
    [DataContract]
    public enum Bank
    {
        [EnumMember]
        MagyarNemzetiBank,
        [EnumMember]
        Erste,
        [EnumMember]
        OrszágosTakarékPénztár,
        [EnumMember]
        OTP
    }
}