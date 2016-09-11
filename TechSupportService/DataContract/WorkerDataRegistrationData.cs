using System.Runtime.Serialization;
using System.Threading;
using TechSupportService.DataContract;

namespace TechSupportService
{
    [DataContract]
    public class WorkerDataRegistrationData : WorkerData
    {
        [DataMember]
        public Bank Bank { get; private set; }
        [DataMember]
        public string BankAccount { get; private set; }
        [DataMember]
        public string PassWD { get; private set; }

        public WorkerDataRegistrationData(string fullName, string username, string email, string phone, string address, string profilePicture, Status status, Role role, Bank bank, string bankAccount, string passWd, int id = 0) : base(fullName, username, email, phone, address, profilePicture, status, role, id)
        {
            Bank = bank;
            BankAccount = bankAccount;
            PassWD = passWd;
        }
    }
}