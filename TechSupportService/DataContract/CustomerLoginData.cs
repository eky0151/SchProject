using System.Runtime.Serialization;

namespace TechSupportService
{
    [DataContract]
    public class CustomerLoginData
    {
        public string FullName { get; private set; }
        public string ProfilePicture { get; private set; }

        public CustomerLoginData(string fullName, string profilePicture)
        {
            FullName = fullName;
            ProfilePicture = profilePicture;
        }
    }
}