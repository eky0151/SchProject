using Microsoft.VisualStudio.TestTools.UnitTesting;
using DbAndRepository.Repostirories;
using DbAndRepository;
using DbAndRepository.IRepositories;
using System.Collections.Generic;
using System;

namespace DatabaseAndRepositoryTests
{
    [TestClass]
    public sealed class WorkerRepositoryTest
    {
        private IWorkerRepository workerRepo;
        private TechSupportDatabaseEntities db =  new TechSupportDatabaseEntities();
        private List<Worker> workerList;

        #region Initialize and Cleanup
        [TestInitialize]
        public void InitializeTest()
        {
            workerRepo = new WorkerRepository(db);
            workerList = new List<Worker>(workerRepo.GetAll());
        }

        [TestCleanup]
        public void CleanUpTest()
        {
            db.Dispose();
            workerRepo.Dispose();
            workerList = null;
        }
        #endregion

       

        [TestMethod]
        public void UpdateLogin()
        {
            LoginDataRepository r = new LoginDataRepository(db);
            List<LoginData> datas = new List<LoginData>(r.GetAll());
            datas[0].Password = "new password";

            r.Update(datas[0]);
        }

        [TestMethod]
        public void InserNewWorker()
        {
            IWorkerRepository wp = new WorkerRepository(db);
            
            wp.RegisterNewWorker("propa2", "HelpDesk", "propa2", "away", "Available", "propaUtca2", "proba@email.hu2", "Propa Propa2", "063010010210", null,
                "Propa Bank2", "100propaaccount2", false);
        }

        [TestMethod]
        public void SelectNewWorker()
        {
            IWorkerRepository wp = new WorkerRepository(db);
            Worker w = wp.GetById(5);

            

            Console.WriteLine();
        }

     


    }
}
