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
        public int TechnicianID { get; private set; }
        [DataMember]
        public string Location { get; private set; }
        [DataMember]
        public TechnicianStatus TechnicianStatus { get; private set; }

        public static explicit operator TechnicianData(Technician t)
        {
            TechnicianData data = new TechnicianData(t.Worker.FullName, t.Worker.LoginData.SingleOrDefault().Username, t.Worker.Email, t.Worker.Phone, t.Worker.Address, t.Worker.ProfilePicture, (TechSupportService.Status)Enum.Parse(typeof(Status), t.Worker.Status), (DataContract.Role)Enum.Parse(typeof(Role), t.Worker.LoginData.SingleOrDefault().Urole), t.WorkerID, t.Lastlocation.ToString(), (TechnicianStatus)Enum.Parse(typeof(TechnicianStatus), t.Available), t.ID);
            return data;
        }
        public static TechnicianData ConverTechnicianData(Technician t)
        {
            TechnicianData data=new TechnicianData(t.Worker.FullName,t.Worker.LoginData.FirstOrDefault().Username,t.Worker.Email,t.Worker.Phone,t.Worker.Address,t.Worker.ProfilePicture,(TechSupportService.Status)Enum.Parse(typeof(Status),t.Worker.Status),(DataContract.Role)Enum.Parse(typeof(Role),t.Worker.LoginData.FirstOrDefault().Urole),t.WorkerID,t.Lastlocation.Latitude+"$"+t.Lastlocation.Longitude,(TechnicianStatus)Enum.Parse(typeof(TechnicianStatus),t.Available),t.ID);
            return data;
        }

        public TechnicianData(string fullName, string username, string email, string phone, string address, string profilePicture, Status status, Role role, int id,  string location, TechnicianStatus technicianStatus,int technicianId=0) : base(fullName, username, email, phone, address, profilePicture, status, role, id)
        {
            TechnicianID = technicianId;
            Location = location;
            TechnicianStatus = technicianStatus;
        }
    }
}