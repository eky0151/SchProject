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
        private ILogsRepository logs;

        #region Initialize and Cleanup
        [TestInitialize]
        public void InitializeTest()
        {
            workerRepo = new WorkerRepository(db);
            workerList = new List<Worker>(workerRepo.GetAll());
            logs = new LogsRepository(db);
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
        public void ModifyWorkerData()
        {
            Worker w = workerList[0];
            string fullname = "John Doe";
            w.Fullname = fullname;
            workerRepo.Update(w);
            workerList = new List<Worker>(workerRepo.GetAll());
            Assert.AreEqual(fullname, workerList[0].Fullname);
        }

        [TestMethod]
        public void UpdateLogin()
        {
            LoginDataRepository r = new LoginDataRepository(db);
            List<LoginData> datas = new List<LoginData>(r.GetAll());
            datas[0].Password = "new password";

            r.Update(datas[0]);
        }

     


    }
}
