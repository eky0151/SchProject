using System.Runtime.Serialization;
using System.Threading;
using TechSupportService.DataContract;

namespace TechSupportService
{
    [DataContract]
    public class WorkerDataRegistrationData:WorkerData
    {
        [DataMember]
        public Bank Bank { get; private set; }
        [DataMember]
        public string BankAccount { get; private set; }
        [DataMember]
        public string PassWD { get; private set; }
        [DataMember]
        public bool Technician { get; private set; }

        public WorkerDataRegistrationData(string fullName, string username, string email, string phone, string address, string profilePicture, Status status, Role role, Bank bank, string bankAccount, string passWd, bool technician) : base(fullName, username, email, phone, address, profilePicture, status, role)
        {
            Bank = bank;
            BankAccount = bankAccount;
            PassWD = passWd;
            Technician = technician;
        }
    }
}