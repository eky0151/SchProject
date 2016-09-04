using System.Runtime.Serialization;

namespace TechSupportService
{
    [DataContract]
    public class UsernameValidationResult
    {
        [DataMember]
        public bool Valid { get; set; }
        [DataMember]
        public string ProfilePicture { get; set; }
    }
}