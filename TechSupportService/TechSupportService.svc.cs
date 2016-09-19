using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DbAndRepository;
using DbAndRepository.IRepositories;
using DbAndRepository.Repostirories;
using TechSupportService.DataContract;

namespace TechSupportService
{
    public class TechSupportService1 : ITechSupportService1
    {
        private readonly ILoginDataRepository _auth;
        private readonly IWorkerRepository _workerRepository;
        private readonly IRegUserRepository _regUserRepository;
        private readonly ITechWorksRepository _techWorksRepository;
        private readonly ITechnicianRepository _technicianRepository;
        private readonly INewTechWorksRepository _newTechWorksRepository;
        private readonly ISolvedQuestionsRepository _solvedQuestionsRepository;

        public TechSupportService1()
        {
            var db = new TechSupportDatabaseEntities();
            _auth = new LoginDataRepository(db);
            _workerRepository = new WorkerRepository(db);
            _regUserRepository = new RegUserRepository(db);
            _techWorksRepository = new TechWorksRepository(db);
            _technicianRepository = new TechninicanRepository(db);
            _newTechWorksRepository = new NewTechWorksRepository(db);
            _solvedQuestionsRepository = new SolvedQuestionsRepository(db);
            AutoMapperConfig.Init();
        }
        public bool UserLogin(string username, string password)
        {
            bool success = _regUserRepository.Autenthicate(username, password);
            if (success)
                AzureServiceBus.SendCustomerLoginNotification((CustomerData)_regUserRepository.GetUserByUsername(username));
            return success;
        }

        public string GetProfilePicture(string username)
        {
            bool res = _auth.CheckUsername(username);
            string picture = null;
            if (res)
            {
                picture = _auth.GetPicture(username);
            }
            return picture;
        }
        public string GetUserProfilePicture(string username)
        {
            var picture = _regUserRepository.GetPicture(username);
            return picture ?? "";
        }
        public void RegisterNewUser(string fullName, string email, string userName, string password, string profilePicture)
        {
            _regUserRepository.Insert(new RegUser
            {
                Fullname = fullName,
                Email = email,
                Username = userName,
                Password = password,
                Points = 1,
                Picture = profilePicture,
                Regtime = DateTime.Now,
            });
        }
        public List<CustomerData> CustomerList()
        {
            return _regUserRepository.GetAll().Select(CustomerData.CustomerToCustomerData).ToList();
        }

        public List<WorkerData> HelpDeskWorkerList()
        {
            return _workerRepository.GetHelpDeskList().Select(WorkerData.WorkerToWorkerData).ToList();
        }

        public LoginResult TechnicianLogin(string username, string passWD)
        {
            if (_auth.Authenticate(username, passWD))
            {
                var user = _auth.Get(x => x.Username == username).FirstOrDefault();
                if (user != null)
                {
                    ChangeMyStatus(username, Status.Working);
                    ChangeMyTechnicianStatus(username, TechnicianStatus.Available);
                    return new LoginResult()
                    {
                        FullName = user.Worker.FullName,
                        Role = (Role)Enum.Parse(typeof(Role), user.Urole),
                        Valid = true
                    };
                }
            }
            return new LoginResult() { Valid = false };
        }

        public void Logout(string username)
        {
            ChangeMyStatus(username, Status.Away);
            ChangeMyTechnicianStatus(username, TechnicianStatus.Break);
        }
        public void ChangeMyStatus(string username, Status newStatus)
        {
            var user = _auth.Get(x => x.Username == username).FirstOrDefault();
            if (user != null)
            {
                user.Worker.Status = Enum.GetName(typeof(Status), newStatus);
                _auth.Update(user);
                AzureServiceBus.SendStatusNotification(username, newStatus);
            }
        }

        public void ChangeMyTechnicianStatus(string username, TechnicianStatus status)
        {
            var user = _auth.Get(x => x.Username == username).FirstOrDefault();
            var technician = user?.Worker.Technician.FirstOrDefault();
            if (technician != null)
            {
                technician.Available = Enum.GetName(typeof(TechnicianStatus), status);
                _technicianRepository.Update(technician);
            }
        }

        public void UploadSolvedQuestion(SolvedQuestion question)
        {
            _solvedQuestionsRepository.Insert(SolvedQuestion.SolvedQuestionToDB(question));
        }

        public List<NewTechWork> GetMyNewTechworks(string username)
        {

            return _newTechWorksRepository.GetMyNewTechWorks(username).Select(Mapper.Map<NewTechWorks, NewTechWork>).ToList();
        }

        public async void SendMessageToSupport(string username, string sender, string message)
        {
            await AzureServiceBus.SendMessage(username, sender, message);
        }

        public List<Message> GetMyMessages(string username)
        {
            return AzureServiceBus.GetMessages(username);
        }
    }
}
