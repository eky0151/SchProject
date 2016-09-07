﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using DbAndRepository;
using TechSupportService.DataContract;

namespace TechSupportService
{
    [ServiceContract]
    public interface ITechSupportServiceSecure1
    {

        [OperationContract]
        LoginResult GetWorkerData();

        [OperationContract(IsOneWay = true)]
        void RegisterNewStaffMember(WorkerDataRegistrationData regData);

        [OperationContract]
        List<CustomerLoginData> LastCustomerList();

        [OperationContract]
        List<WorkerData> StaffList();

        [OperationContract(IsOneWay = true)]
        void ChangeWorkerStatus(string username, Status status);

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
        List<CustomerData> LastCustomers();

        [OperationContract]
        CustomerData GetCustomer(string username);



    }
}