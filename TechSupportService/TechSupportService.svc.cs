using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
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
        private readonly ISolvedQuestionsRepository _solvedQuestionsRepository;
        private readonly INewTechWorksRepository _newTechWorksRepository;
        private readonly ITechWorksRepository _techWorksRepository;

        public TechSupportService1()
        {
            TechSupportDatabaseEntities db = new TechSupportDatabaseEntities();
            _auth = new LoginDataRepository(db);
            _workerRepository = new WorkerRepository(db);
            _regUserRepository = new RegUserRepository(db);
            _solvedQuestionsRepository = new SolvedQuestionsRepository(db);
            _newTechWorksRepository = new NewTechWorksRepository(db);
            _techWorksRepository = new TechWorksRepository(db);
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
            return "";
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
                    return new LoginResult() { FullName = user.Worker.FullName, Role = (Role)Enum.Parse(typeof(Role), user.Urole), Valid = true };
            }
            return new LoginResult() { Valid = false };
        }

        public void UploadSolvedQuestion(SolvedQuestion question)
        {
            _solvedQuestionsRepository.Insert(SolvedQuestion.SolvedQuestionToDB(question));
        }

        public List<NewTechWork> GetMyNewTechworks(string username)
        {

            return _newTechWorksRepository.GetMyNewTechWorks(username).Select(Mapper.Map<NewTechWorks, NewTechWork>).ToList();
        }

        public async void SendMessageToSupport(string username,string sender, string message)
        {
            await AzureServiceBus.SendMessage(username,sender, message);
        }

        public List<Message> GetMyMessages(string username)
        {
           return AzureServiceBus.GetMessages(username);
        }
    }
}
