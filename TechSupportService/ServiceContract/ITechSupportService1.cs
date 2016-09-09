using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace TechSupportService
{
    [ServiceContract]
    public interface ITechSupportService1
    {
        [OperationContract]
        bool UserLogin(string username, string password);

        [OperationContract]
        string GetProfilePicture(string username);

        [OperationContract]
        string GetUserProfilePicture(string username);

        [OperationContract(IsOneWay = true)]
        void RegisterNewUser(string fullName, string email, string userName, string password);

        [OperationContract]
        List<int> GetLastSevedDaysSolves(out List<DateTime> dates);
    }
}
