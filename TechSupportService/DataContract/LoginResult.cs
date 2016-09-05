using System.Runtime.Serialization;
using System.Security;
using TechSupportService.DataContract;

namespace TechSupportService
{
    [DataContract]
    public class LoginResult
    {
        [DataMember]
        public bool Valid { get; set; }

        [DataMember]
        public Role Role { get; set; }

        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public SecureString UUID { get; set; }

    }
}