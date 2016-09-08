using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using DbAndRepository;

namespace TechSupportService.DataContract
{
    [DataContract]
    public class CustomerData
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string FullName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public int Points { get; set; }
        [DataMember]
        public string Picture { get; set; }
        [DataMember]
        public string Phone { get; set; }
        [DataMember]
        public DateTime RegTime { get; set; }
        [DataMember]
        public string UserName { get; set; }

        public static explicit operator CustomerData(RegUser r)
        {
            CustomerData data = new CustomerData() { Email = r.Email, FullName = r.Fullname, Picture = r.Picture, Points = r.Points, RegTime = r.Regtime, ID = r.ID , UserName = r.Username, Phone = r.Phonenumber};
            return data;
        }

    }
}