using System.Diagnostics;
using System.Runtime.Serialization;
using TechSupportService.DataContract;

namespace TechSupportService
{
    [DataContract]
    public class WorkerData
    {
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
        public Bank Bank { get; private set; }
        [DataMember]
        public string BankAccount { get; private set; }
        [DataMember]
        public Role Role { get; private set; }

        public WorkerData(string fullName, string username, string email, string phone, string address, string profilePicture, Status status, Bank bank, string bankAccount, Role role)
        {
            FullName = fullName;
            Username = username;
            Email = email;
            Phone = phone;
            Address = address;
            ProfilePicture = profilePicture;
            Status = status;
            Bank = bank;
            BankAccount = bankAccount;
            Role = role;
        }
    }
}