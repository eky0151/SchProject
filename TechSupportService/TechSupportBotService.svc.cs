using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DbAndRepository;
using DbAndRepository.IRepositories;
using DbAndRepository.Repostirories;
using TechSharedLibraries.LUIS_Classes;
using TechSharedLibraries.TextAnalitycs_Classes;
using TechSupportService.DataContract;

namespace TechSupportService
{
    public class TechSupportBotService : ITechSupportBotService
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly ITechnicianRepository _technicianRepository;
        private readonly INewTechWorksRepository _newTechWorksRepository;
        private readonly IRegUserRepository _regUserRepository;
        private readonly ISolvedQuestionsRepository _solvedQuestionsRepository;


        public TechSupportBotService()
        {
            TechSupportDatabaseEntities db = new TechSupportDatabaseEntities();
            _workerRepository = new WorkerRepository(db);
            _technicianRepository = new TechninicanRepository(db);
            _newTechWorksRepository = new NewTechWorksRepository(db);
            _solvedQuestionsRepository = new SolvedQuestionsRepository(db);
            _regUserRepository = new RegUserRepository(db);
        }
        public List<SolvedQuestion> FindSimilar(string question)
        {
            var res = LUIS.ParseUserInput(question).Result;
            Intentsresult topic = res.OrderByDescending(x => x.score).FirstOrDefault();
            if (topic != null && topic.score > 0.6)
            {
                var temp = _solvedQuestionsRepository.FindSimilarQuestions(question, TextAnalitycs.MakeRequests(question).Result.keyPhrases,
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

        public TechnicianData ResgisterNewTechWork(string location, string fullname)
        {
            var technician = _technicianRepository.GetAvailableTechnician();
            _newTechWorksRepository.AddNewTechWork(location, fullname, DateTime.Now, technician.ID);
            return (TechnicianData)technician;
        }
    }
}
