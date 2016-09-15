using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DbAndRepository;
using DbAndRepository.IRepositories;
using DbAndRepository.Repostirories;
using SupportBot.LUIS_Classes;
using SupportBot.TextAnalitycs_Classes;
using TechSupportService.DataContract;

namespace TechSupportService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TechSupportBotService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TechSupportBotService.svc or TechSupportBotService.svc.cs at the Solution Explorer and start debugging.
    public class TechSupportBotService : ITechSupportBotService
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly ITechnicianRepository _technicianRepository;
        private readonly ITechWorksRepository _techWorksRepository;
        private readonly ISolvedQuestionsRepository _solvedQuestionsRepository;


        public TechSupportBotService()
        {
            TechSupportDatabaseEntities db = new TechSupportDatabaseEntities();
            _workerRepository = new WorkerRepository(db);
            _technicianRepository = new TechninicanRepository(db);
            _techWorksRepository = new TechWorksRepository(db);
            _solvedQuestionsRepository = new SolvedQuestionsRepository(db);
        }
        public List<SolvedQuestion> FindSimilar(string question)
        {
            var res = LUIS.ParseUserInput(question).Result;
            Intentsresult topic = res.OrderByDescending(x => x.score).FirstOrDefault();
            if (topic != null && topic.score > 0.6)
            {
              var temp= _solvedQuestionsRepository.FindSimilarQuestions(question, TextAnalitycs.MakeRequests(question).Result.keyPhrases,
                    topic.Name).ToList();
                if (temp.Count > 0)
                {
                    return temp.ConvertAll(SolvedQuestion.DbToSolvedQuestion).ToList();
                }
            }
            return null;
        }

        public int GetAvailableHelpDesk()
        {
            return _workerRepository.GetAvailableHelpDeskCount();
        }

        public int GetAvailableTechnician()
        {
            return _technicianRepository.GetAvailableTechnicianCount();
        }

        public TechnicianData ResgisterNewTechWork(string location, CustomerData customer)
        {
            var technician = _technicianRepository.GetAvailableTechnician();
            _techWorksRepository.RegisterNewWork(new TechWorks() { Customeraddress = location, Customername = customer.FullName, TechID = technician.ID });
            return (TechnicianData)technician;
        }
    }
}
