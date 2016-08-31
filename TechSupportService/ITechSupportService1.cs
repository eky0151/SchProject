using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TechSupportService
{
    [ServiceContract]
    public interface ITechSupportService1
    {

        [OperationContract]
        LoginResult Login(string username, string password);

        [OperationContract]
        bool UserLogin(string username, string password);

    }
    [DataContract]
    public class LoginResult
    {
        [DataMember]
        public bool Valid { get; set; }

        [DataMember]
        public string Role { get; set; }
        [DataMember]
        public string FullName { get; set; }
    }


}
