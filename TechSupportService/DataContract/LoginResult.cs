using System.Runtime.Serialization;
using System.Security;

namespace TechSupportService
{
    [DataContract]
    public class LoginResult
    {
        [DataMember]
        public bool Valid { get; set; }

        [DataMember]
        public string Role { get; set; }

        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public SecureString UUID { get; set; }

    }
}