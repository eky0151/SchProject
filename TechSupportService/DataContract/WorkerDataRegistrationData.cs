using System.Runtime.Serialization;
using System.Threading;
using TechSupportService.DataContract;

namespace TechSupportService
{
    [DataContract]
    public class WorkerDataRegistrationData:WorkerData
    { 
        [DataMember]
        public string PassWD { get; private set; }
        [DataMember]
        public bool Technician { get; private set; }
        public WorkerDataRegistrationData(string fullName, string username, string email, string phone, string address, string profilePicture, Status status, Bank bank, string bankAccount, Role role, string passWd, bool technician) : base(fullName, username, email, phone, address, profilePicture, status, bank, bankAccount, role)
        {
            PassWD = passWd;
            Technician = technician;
        }
    }
}