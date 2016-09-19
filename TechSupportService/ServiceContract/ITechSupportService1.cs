using System.Collections.Generic;
using System.ServiceModel;
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
        List<NewTechWork> GetMyNewTechworks(string username);

        [OperationContract]
        void UploadSolvedQuestion(SolvedQuestion question);

        [OperationContract]
        bool UserLogin(string username, string password);

        [OperationContract]
        string GetProfilePicture(string username);

        [OperationContract]
        string GetUserProfilePicture(string username);

        [OperationContract(IsOneWay = true)]
        void RegisterNewUser(string fullName, string email, string userName, string password, string profilePicture);

        [OperationContract(IsOneWay = true)]
        void SendMessageToSupport(string username, string sender, string message);

        [OperationContract]
        List<Message> GetMyMessages(string username);

        [OperationContract(IsOneWay = true)]
        void Logout(string username);

        [OperationContract(IsOneWay = true)]
        void ChangeMyStatus(string username, Status newStatus);

        [OperationContract(IsOneWay = true)]
        void ChangeMyTechnicianStatus(string username, TechnicianStatus status);


    }
}
