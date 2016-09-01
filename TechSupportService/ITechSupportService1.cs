using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace TechSupportService
{
    [ServiceContract]
    public interface ITechSupportService1
    {

        [OperationContract(AsyncPattern = true)]
        Task<LoginResult> LoginAsync(string username, string password);

        [OperationContract]
        bool UserLogin(string username, string password);

        [OperationContract]
        void RegisterNewUser(string fullName, string email, string userName, string password);



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
