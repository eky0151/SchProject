using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TechSupportService.DataContract;

namespace TechSupportService
{
    [ServiceContract]
    public interface ITechSupportService1
    {
        [OperationContract]
        LoginResult TechnicianLogin(string username, string passWD);

        [OperationContract]
        List<WorkerData> HelpDeskWorkerList();

        [OperationContract]
        List<CustomerData> CustomerList();

        [OperationContract]
        void UploadSolvedQuestion(SolvedQuestion question);

        [OperationContract]
        bool UserLogin(string username, string password);

        [OperationContract]
        string GetProfilePicture(string username);

        [OperationContract]
        string GetUserProfilePicture(string username);

        [OperationContract(IsOneWay = true)]
        void RegisterNewUser(string fullName, string email, string userName, string password);

    }
}
