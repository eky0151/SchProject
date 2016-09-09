using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DbAndRepository;
using DbAndRepository.IRepositories;
using DbAndRepository.Repostirories;
using TechSupportService.DataContract;

namespace TechSupportService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TechSupportServiceCross" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TechSupportServiceCross.svc or TechSupportServiceCross.svc.cs at the Solution Explorer and start debugging.
    public class TechSupportServiceCross : ITechSupportServiceCross
    {
        private IWorkerRepository _workerRepository;
        private IRegUserRepository _regUserRepository;
        private ISolvedQuestionsRepository _solvedQuestionsRepository;

        public TechSupportServiceCross()
        {
            TechSupportDatabaseEntities db=new TechSupportDatabaseEntities();
            _workerRepository=new WorkerRepository(db);
            _regUserRepository=new RegUserRepository(db);
            _solvedQuestionsRepository=new SolvedQuestionsRepository(db);
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
            throw new NotImplementedException();
        }

        public void UploadSolvedQuestion(SolvedQuestion question)
        {
            _solvedQuestionsRepository.Insert(SolvedQuestion.SolvedQuestionToDB(question));
        }
    }
}
