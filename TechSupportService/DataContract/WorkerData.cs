using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using DbAndRepository;
using TechSupportService.DataContract;

namespace TechSupportService
{
    [DataContract]
    public class WorkerData
    {
        [DataMember]
        public int ID { get; private set; }
        [DataMember]
        public string FullName { get; private set; }
        [DataMember]
        public string Username { get; private set; }
        [DataMember]
        public string Email { get; private set; }
        [DataMember]
        public string Phone { get; private set; }
        [DataMember]
        public string Address { get; private set; }
        [DataMember]
        public string ProfilePicture { get; private set; }
        [DataMember]
        public Status Status { get; private set; }
        [DataMember]
        public Role Role { get; private set; }

        public static WorkerData WorkerToWorkerData(Worker worker)
        {
            WorkerData data=new WorkerData(worker.FullName,worker.LoginData.FirstOrDefault().Username,worker.Email,worker.Phone,worker.Address,worker.LoginData.FirstOrDefault().Urole, (Status)Enum.Parse(typeof(Status), worker.Status), (Role)Enum.Parse(typeof(Role), worker.LoginData.FirstOrDefault().Urole), worker.ID);
            return data;
        }
        public static explicit operator WorkerData(LoginData w)
        {
            WorkerData data = new WorkerData(w.Worker.FullName, w.Username, w.Worker.Email, w.Worker.Phone, w.Worker.Address, w.Worker.ProfilePicture, (Status)Enum.Parse(typeof(Status), w.Worker.Status), (Role)Enum.Parse(typeof(Role), w.Urole),w.WorkerID);
            return data;
        }

        public WorkerData( string fullName, string username, string email, string phone, string address, string profilePicture, Status status, Role role,int id=0)
        {
            ID = id;
            FullName = fullName;
            Username = username;
            Email = email;
            Phone = phone;
            Address = address;
            ProfilePicture = profilePicture;
            Status = status;
            Role = role;
        }
    }
}