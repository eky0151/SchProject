using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using DbAndRepository;
using TechSupportService.DataContract;
using TechSupportService.ServiceContract;

namespace TechSupportService
{
    [ServiceContract]
    public interface ITechSupportServiceSecure1
    {

        [OperationContract]
        LoginResult Login();

        [OperationContract(IsOneWay = true)]
        void RegisterNewStaffMember(WorkerDataRegistrationData regData);

        [OperationContract]
        List<CustomerData> LastCustomerList();

        [OperationContract]
        List<WorkerData> StaffList();

        [OperationContract(IsOneWay = true)]
        void ChangeWorkerStatus(string username, Status status);

        [OperationContract(IsOneWay = true)]
        void ChangeMyStatus(Status newStatus);

        [OperationContract(IsOneWay = true)]
        void SendBugreport(string message, List<string> file);

        [OperationContract(IsOneWay = true)]
        void ChangeWorkerPassWD(string username, string newPassWD);

        [OperationContract(IsOneWay = true)]
        void ChangePassWD(string passnewPassWD);

        [OperationContract]
        WorkerData GetWorker(string username);

        [OperationContract]
        List<TechnicianData> TechnicianList();

        [OperationContract]
        List<TechnicianData> GetAvailableTechnician();

        [OperationContract(IsOneWay = true)]
        void AddNewSolvedQuestion(SolvedQuestion solved);

        [OperationContract]
        List<SolvedQuestion> SolvedQuestionList(uint Page);

        [OperationContract]
        List<TechWork> GetTechWorks();

        [OperationContract]
        List<TechWork> NewTechWorks();

        [OperationContract(IsOneWay = true)]
        void ChangeTechnicianStatus(TechnicianStatus status);

        [OperationContract(IsOneWay = true)]
        void RegisterTechWork(TechWork work);

        [OperationContract]
        CustomerData LastCustomer();

        [OperationContract]
        CustomerData GetCustomer(string username);

        [OperationContract]
        List<int> GetLastSevedDaysSolves(out List<DateTime> dates, out List<KeyValuePair<string, int>> byName);

        [OperationContract(IsOneWay = true)]
        void ChangeMyPassWD(string newPass);

        [OperationContract(IsOneWay = true)]
        void ChangeMyPicture(string picture);

        List<int> GetLastMonthRegistratedUsers(out List<DateTime> Dates);

    }
}
