using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using DbAndRepository;

namespace TechSupportService.DataContract
{
    [DataContract]
    public class TechnicianData:WorkerData
    {
        [DataMember]
        public string Location { get; private set; }
        [DataMember]
        public TechnicianStatus TechnicianStatus { get; private set; }

        public static explicit operator TechnicianData(Technician t)
        {
            TechnicianData data=new TechnicianData(t.Worker.FullName,t.Worker.LoginData.SingleOrDefault().Username,t.Worker.Email,t.Worker.Phone,t.Worker.Address,t.Worker.ProfilePicture,(TechSupportService.Status)Enum.Parse(typeof(Status),t.Worker.Status),(DataContract.Role)Enum.Parse(typeof(Role),t.Worker.LoginData.SingleOrDefault().Urole),t.Lastlocation.ToString(),(TechnicianStatus)Enum.Parse(typeof(TechnicianStatus),t.Available));
            return data;
        }
        public TechnicianData(string fullName, string username, string email, string phone, string address, string profilePicture, Status status, Role role, string location, TechnicianStatus technicianStatus) : base(fullName, username, email, phone, address, profilePicture, status, role)
        {
            Location = location;
            TechnicianStatus = technicianStatus;
        }
    }
}