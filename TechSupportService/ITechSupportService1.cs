using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TechSupportService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITechSupportService1" in both code and config file together.
    [ServiceContract]
    public interface ITechSupportService1
    {

        [OperationContract]
        LoginResult Login(string username, string password);


        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class LoginResult
    {
        [DataMember]
        public bool Valid { get; set; }

        [DataMember]
        public string Role { get; set; }
    }
}
