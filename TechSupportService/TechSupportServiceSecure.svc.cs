﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.ServiceModel;
using DbAndRepository;
using DbAndRepository.IRepositories;
using DbAndRepository.Repostirories;
using TechSupportService.DataContract;

namespace TechSupportService
{
    public class TechSupportServiceSecure1 : ITechSupportServiceSecure1
    {
        private readonly IBankRepository _bankRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IWorkerRepository _workerRepository;
        private readonly IBugreportRepository _bugsRepository;
        private readonly ILoginDataRepository _authRepository;
        private readonly IRegUserRepository _regUserRepository;
        private readonly ITechWorksRepository _techworksRepository;
        private readonly ITechnicianRepository _technicianRepository;
        private readonly INewTechWorksRepository _newTechWorksRepository;
        private readonly ISolvedQuestionsRepository _solvedQuestionsRepository;


        public TechSupportServiceSecure1()
        {
            var db = new TechSupportDatabaseEntities();
            _bankRepository = new BankRepository(db);
            _fileRepository = new FilesRepository(db);
            _workerRepository = new WorkerRepository(db);
            _bugsRepository = new BugreportRepository(db);
            _authRepository = new LoginDataRepository(db);
            _regUserRepository = new RegUserRepository(db);
            _techworksRepository = new TechWorksRepository(db);
            _technicianRepository = new TechninicanRepository(db);
            _newTechWorksRepository = new NewTechWorksRepository(db);
            _solvedQuestionsRepository = new SolvedQuestionsRepository(db);
        }



        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public void RegisterNewStaffMember(WorkerDataRegistrationData regData)
        {
            _workerRepository.RegisterNewWorker(regData.Username, Enum.GetName(typeof(Role),
                regData.Role), regData.PassWD, Enum.GetName(typeof(Status),
                regData.Status), regData.Address, regData.Email, regData.FullName,
                regData.Phone, regData.ProfilePicture, Enum.GetName(typeof(Bank), regData.Bank),
                regData.BankAccount);
        }

        public List<CustomerData> LastCustomerList()
        {
            return new List<CustomerData>(_regUserRepository.
                GetAll().
                OrderByDescending(i => i.Regtime).
                Select(i => (CustomerData)i));
        }

        public List<WorkerData> StaffList()
        {
            return _workerRepository.GetAll().ToList().ConvertAll(WorkerData.WorkerToWorkerData).ToList();
        }

        public void ChangeWorkerStatus(string username, Status status)
        {
            AzureServiceBus.SendStatusNotification(username, status);
            var loginData = _authRepository.Get(x => x.Username == username).SingleOrDefault();
            if (loginData != null)
                loginData.Worker.Status =
                    Enum.GetName(typeof(Status), status);
        }

        public void SendBugreport(string message, List<string> file)
        {
            AzureServiceBus.SendAppBugNotification(message);
            int? sender =
                _authRepository.Get(x => x.Username == ServiceSecurityContext.Current.PrimaryIdentity.Name)
                    .SingleOrDefault()?.Worker.ID;
            _bugsRepository.Insert(new Bugreport() { Date = DateTime.Now, Message = message, Sender = sender ?? 0 });
            var bug = _bugsRepository.Get(x => x.Message == message).SingleOrDefault().ID;
            foreach (string s in file)
            {
                _fileRepository.Insert(new Files() { BugreportID = bug, File = s });
            }
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public void ChangeWorkerPassWD(string username, string newPassWD)
        {
            var user = _authRepository.Get(x => x.Username == username).SingleOrDefault();
            if (user != null)
            {
                user.Password = newPassWD;
                _authRepository.Update(user);
            }
        }

        public void ChangePassWD(string newPassWD)
        {
            var user =
                _authRepository.Get(x => x.Username == ServiceSecurityContext.Current.PrimaryIdentity.Name).FirstOrDefault();
            if (user == null) return;
            user.Password = newPassWD;
            _authRepository.Update(user);
        }

        public WorkerData GetWorker(string username)
        {
            return (WorkerData)_authRepository.Get(x => x.Username == username).SingleOrDefault();
        }

        public List<TechnicianData> TechnicianList()
        {
            return _authRepository.Get(x => x.Urole == "Technician")
                .Select(x => x.Worker.Technician.FirstOrDefault())
                .ToList().ConvertAll(TechnicianData.ConverTechnicianData).ToList();
        }

        public List<TechnicianData> GetAvailableTechnician()
        {
            return
                _technicianRepository.Get(x => x.Available == "Available")
                    .ToList()
                    .ConvertAll(TechnicianData.ConverTechnicianData)
                    .ToList();
        }

        public void AddNewSolvedQuestion(SolvedQuestion solved)
        {
            _solvedQuestionsRepository.Insert(SolvedQuestion.SolvedQuestionToDB(solved));
        }

        public List<SolvedQuestion> SolvedQuestionList(uint Page)
        {
            return _solvedQuestionsRepository.Get(x => x.ID < Page).ToList().Cast<SolvedQuestion>().ToList();
        }

        public List<TechWork> GetTechWorks()
        {
            return _techworksRepository.GetAll().ToList().Cast<TechWork>().ToList();
        }

        public List<TechWork> NewTechWorks()
        {
            return _techworksRepository.GetAll().OrderBy(i => i.Finish).ToList().Cast<TechWork>().ToList();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Technician")]
        public void ChangeTechnicianStatus(TechnicianStatus status)
        {
            var res =
                _technicianRepository.Get(
                    x =>
                        x.Worker.LoginData.FirstOrDefault().Username ==
                        ServiceSecurityContext.Current.PrimaryIdentity.Name).FirstOrDefault();
            if (res == null) return;
            res.Available = Enum.GetName(typeof(TechnicianStatus), status);
            _technicianRepository.Update(res);
        }

        public void RegisterTechWork(TechWork work)
        {
            _techworksRepository.Insert(new TechWorks()
            {
                Customeraddress = work.Address,
                Customername = work.Customer.FullName,
                Finish = work.Finish,
                TechID = work.Technician.TechnicianID,
                Price = work.Price,
                Start = work.Start
            });
        }

        public CustomerData LastCustomer()
        {
            var z = _regUserRepository.GetAll().Count();
            RegUser u = _regUserRepository.GetById(z - 1);
            return ReturnCustomer(u);
        }

        public CustomerData GetCustomer(string username)
        {
            RegUser z = _regUserRepository.Get(i => i.Username == username).FirstOrDefault();
            return ReturnCustomer(z);
        }


        #region Private_Methods

        private CustomerData ReturnCustomer(RegUser u)
        {
            return new CustomerData
            {
                Email = u.Email,
                FullName = u.Fullname,
                ID = u.ID,
                Phone = u.Phonenumber,
                Picture = u.Picture,
                Points = u.Points,
                RegTime = u.Regtime,
                UserName = u.Username
            };
        }

        #endregion


        public List<int> GetLastSevedDaysSolves(out List<DateTime> dates, out List<KeyValuePair<string, int>> byName)
        {
            byName = new List<KeyValuePair<string, int>>();
            List<KeyValuePair<string, int>> temp;
            dates = new List<DateTime>();
            List<DateTime> temp1;
            List<int> z = _solvedQuestionsRepository.GetLastSevenDaysSolvedQuestions(out temp1, out temp);
            byName = temp;
            dates = temp1;
            return z;

        }

        public LoginResult Login()
        {
            string name = ServiceSecurityContext.Current.PrimaryIdentity.Name;
            var worker = _authRepository.Get(x => x.Username == name).SingleOrDefault()?.Worker;
            worker.Status = "Working";
            if (worker.LoginData.FirstOrDefault()?.Urole == "Technician")
            {
                worker.Technician.FirstOrDefault().Available = "Available";
            }
            _workerRepository.Update(worker);
            AzureServiceBus.SendWorkerLoginData(name);
            AzureServiceBus.SendStatusNotification(name, Status.Working);
            return new LoginResult()
            {
                FullName = worker.FullName,
                Role = (Role)Enum.Parse(typeof(Role), worker.LoginData.FirstOrDefault().Urole)
            };
        }

        public void ChangeMyStatus(Status newStatus)
        {
            string name = ServiceSecurityContext.Current.PrimaryIdentity.Name;
            var worker = _authRepository.Get(x => x.Username == name).SingleOrDefault()?.Worker;
            if (worker != null)
            {
                worker.Status = "Away";
                _workerRepository.Update(worker);
                AzureServiceBus.SendStatusNotification(name, Status.Away);
            }

        }

        public void ChangeMyPassWD(string newPass)
        {
            _authRepository.ChangePassWD(ServiceSecurityContext.Current.PrimaryIdentity.Name, newPass);
        }

        public void ChangeMyPicture(string picture)
        {
            var loginData = _authRepository.Get(x => x.Username == ServiceSecurityContext.Current.PrimaryIdentity.Name).FirstOrDefault();
            if (loginData != null)
                _workerRepository.ChangePicture(loginData.WorkerID, picture);
        }

        public bool CheckMyPassWD(string passWD)
        {
            var data =
                _authRepository.Get(
                        x => x.Username == ServiceSecurityContext.Current.PrimaryIdentity.Name && x.Password == passWD)
                    .FirstOrDefault();
            return data != null;
        }

        public List<int> GetLastMonthRegistratedUsers(out List<DateTime> Dates)
        {
            Dates = new List<DateTime>();
            List<DateTime> t;
            List<int> a = _regUserRepository.GetLastMonthRegistratedUsers(out t);
            Dates = t;
            return a;
        }

        public async void SendMessageToTechnician(string username, string sender, string message)
        {
            await AzureServiceBus.SendMessageToTechnician(username, sender, message);
        }

        public void InsertNewTechWorks(NewTechWork d)
        {
            _newTechWorksRepository.Insert(new DbAndRepository.NewTechWorks
            {
                CustomerName = d.CustomerName,
                Address = d.Address,
                TechID = d.TechID,
                TimeOrdered = d.TimeOrdered
            });
        }
    }
}
