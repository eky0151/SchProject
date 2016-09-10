﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DbAndRepository;
using DbAndRepository.IRepositories;
using DbAndRepository.Repostirories;
using TechSupportService.DataContract;
using TechSupportService.ServiceContract;

namespace TechSupportService
{
    public class TechSupportServiceSecure1 : ITechSupportServiceSecure1
    {
        private readonly ILoginDataRepository _authRepository;
        private readonly IWorkerRepository _workerRepository;
        private readonly IBankRepository _bankRepository;
        private readonly ITechnicianRepository _technicianRepository;
        private readonly IBugreportRepository _bugsRepository;
        private readonly IFileRepository _fileRepository;
        private readonly ISolvedQuestionsRepository _solvedQuestionsRepository;
        private readonly ITechWorksRepository _techworksRepository;
        private readonly IRegUserRepository _regUserRepository;


        public TechSupportServiceSecure1()
        {
            TechSupportDatabaseEntities db = new TechSupportDatabaseEntities();
            _authRepository = new LoginDataRepository(db);
            _workerRepository = new WorkerRepository(db);
            _bankRepository = new BankRepository(db);
            _technicianRepository = new TechninicanRepository(db);
            _bugsRepository = new BugreportRepository(db);
            _fileRepository = new FilesRepository(db);
            _solvedQuestionsRepository = new SolvedQuestionsRepository(db);
            _techworksRepository = new TechWorksRepository(db);
            _regUserRepository = new RegUserRepository(db);
        }



        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public void RegisterNewStaffMember(WorkerDataRegistrationData regData)
        {
            _bankRepository.Insert(new DbAndRepository.Bank()
            {
                Account = regData.BankAccount,
                Name = Enum.GetName(typeof(Bank), regData.Bank)
            });
            var bank = _bankRepository.Get(x => x.Account == regData.BankAccount).FirstOrDefault();
            Worker wk = new Worker()
            {
                Address = regData.Address,
                BankID = bank.ID,
                Email = regData.Email,
                FullName = regData.FullName,
                Phone = regData.Phone,
                ProfilePicture = regData.ProfilePicture,
                Status = Enum.GetName(typeof(Status), regData.Status)
            };
            _workerRepository.Insert(wk);

            var workerdata =
                _workerRepository.Get(x => x.FullName.ToString() == regData.FullName.ToString() && x.BankID == bank.ID)
                    .FirstOrDefault();
            _authRepository.Insert(new LoginData()
            {
                Password = regData.PassWD,
                Urole = Enum.GetName(typeof(Role), regData.Role),
                Username = regData.Username,
                WorkerID = workerdata.ID
            });
            if (regData.Technician)
                _technicianRepository.Insert(new Technician() {Available = "Break", WorkerID = workerdata.ID});

        }

        public List<CustomerData> LastCustomerList()
        {
            return new List<CustomerData>(_regUserRepository.
                GetAll().
                OrderByDescending(i => i.Regtime).
                Select(i => (CustomerData) i));
        }

        public List<WorkerData> StaffList()
        {
            List<WorkerData> staffreturn = new List<WorkerData>();
            var staff = _workerRepository.GetAll().ToList();
            foreach (Worker worker in staff)
            {
                WorkerData data = new WorkerData(worker.FullName,
                    _authRepository.Get(x => x.Worker.ID == worker.ID).SingleOrDefault().Username, worker.Email,
                    worker.Phone, worker.Address, worker.ProfilePicture,
                    (Status) Enum.Parse(typeof(Status), worker.Status),
                    (Role)
                    Enum.Parse(typeof(Role), _authRepository.Get(x => x.Worker.ID == worker.ID).SingleOrDefault().Urole));
                staffreturn.Add(data);
            }

            return staffreturn;

        }

        public void ChangeWorkerStatus(string username, Status status)
        {
            _authRepository.Get(x => x.Username == username).SingleOrDefault().Worker.Status =
                Enum.GetName(typeof(Status), status);
        }

        public void SendBugreport(string message, List<string> file)
        {
            int? sender =
                _authRepository.Get(x => x.Username == ServiceSecurityContext.Current.PrimaryIdentity.Name)
                    .SingleOrDefault()?.Worker.ID;
            _bugsRepository.Insert(new Bugreport() {Date = DateTime.Now, Message = message, Sender = sender ?? 0});
            var bug = _bugsRepository.Get(x => x.Message == message).SingleOrDefault().ID;
            foreach (string s in file)
            {
                _fileRepository.Insert(new Files() {BugreportID = bug, File = s});
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
                _authRepository.Get(x => x.Username == ServiceSecurityContext.Current.PrimaryIdentity.Name)
                    .SingleOrDefault();
            if (user != null)
            {
                user.Password = newPassWD;
                _authRepository.Update(user);
            }
        }

        public WorkerData GetWorker(string username)
        {
            return (WorkerData) _authRepository.Get(x => x.Username == username).SingleOrDefault();
        }

        public List<TechnicianData> TechnicianList()
        {
            string role = Enum.GetName(typeof(Role), Role.Technician);
            var res = _authRepository.Get(x => x.Urole == "Technician")
                .Select(x => x.Worker.Technician.FirstOrDefault())
                .ToList();
            return res.ConvertAll(TechnicianData.ConverTechnicianData).ToList();
        }

        public List<TechnicianData> GetAvailableTechnician()
        {
            return
                _authRepository.Get(x => x.Urole == Enum.GetName(typeof(Role), Role.Technician))
                    .Where(x => (x.Worker.Technician.SingleOrDefault()).Available == "Available")
                    .Select(x => x.Worker.Technician.SingleOrDefault())
                    .Cast<TechnicianData>()
                    .ToList();
        }

        public void AddNewSolvedQuestion(SolvedQuestion solved)
        {
            _solvedQuestionsRepository.Insert(new DbAndRepository.SolvedQuestion()
            {
                Answer = solved.Answer,
                Category = solved.Category
            });
        }

        public List<SolvedQuestion> SolvedQuestionList(uint Page)
        {
            throw new NotImplementedException();
        }

        public List<TechWork> GetTechWorks()
        {
            return _techworksRepository.GetAll().Select(x => (TechWork) x).ToList();
        }

        public List<TechWork> NewTechWorks()
        {
            return new List<TechWork>(_techworksRepository.GetAll().OrderBy(i => i.Finish).Select(i => (TechWork) i));
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Technician")]
        public void ChangeTechnicianStatus(TechnicianStatus status)
        {
            var res = _authRepository.Get(x => x.Username == ServiceSecurityContext.Current.PrimaryIdentity.Name)?
                .SingleOrDefault()?
                .Worker.Technician.SingleOrDefault();
            if (res != null)
            {
                res.Available = Enum.GetName(typeof(TechnicianStatus), status);
                _technicianRepository.Update(res);
            }
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
            var z = _regUserRepository.GetAll().ToList();
            RegUser u = z[z.Count - 1];
            return ReturnCustomer(u);
        }

        public CustomerData GetCustomer(string username)
        {
            RegUser z = _regUserRepository.Get(i => i.Username == username).FirstOrDefault();
            //return (CustomerData)_regUserRepository.Get(x => x.Username == username).SingleOrDefault();
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
            Guid clientId = Guid.NewGuid();
            string name = ServiceSecurityContext.Current.PrimaryIdentity.Name;
            var worker = _authRepository.Get(x => x.Username == name).SingleOrDefault()?.Worker;
            worker.Status = "Working";
            _workerRepository.Update(worker);
            if (worker != null)
            {
                return new LoginResult()
                {
                    Identifier = clientId,
                    FullName = worker.FullName,
                    Role = (Role) Enum.Parse(typeof(Role), worker.LoginData.FirstOrDefault().Urole)
                };

            }
            return null;
        }
        
    }
}
