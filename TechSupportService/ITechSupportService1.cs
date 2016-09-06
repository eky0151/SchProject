using System;
using System.Collections.Generic;
using System.Linq;
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

        [OperationContract(IsOneWay = true)]
        void RegisterNewUser(string fullName, string email, string userName, string password);

        [OperationContract]
        UsernameValidationResult UsernameValidation(string username);

        [OperationContract]
        void RegisterNewStaffMember(WorkerDataRegistrationData regData);

        [OperationContract]
        List<CustomerLoginData> LastCustomerList();

        [OperationContract]
        List<WorkerData> StaffList();

        [OperationContract]
        void ChangeWorkerStatus(string username, Status status);

        [OperationContract]
        void SendBugreport(string message, string sender, string file);

        [OperationContract]
        void ChangeWorkerPassWD(string uuid, string username, string newPassWD);



    }
}
