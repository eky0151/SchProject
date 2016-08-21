using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DbAndRepository.Repostirories;
using DbAndRepository;
using DbAndRepository.IRepositories;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DatabaseAndRepositoryTests
{
    [TestClass]
    public sealed class WorkerRepositoryTest
    {
        private IWorkerRepository workerRepo;
        private DatabaseEntities database;
        private List<Worker> workerList;

        #region Initialize and Cleanup
        [TestInitialize]
        public void InitializeTest()
        {
            database = new DatabaseEntities();
            workerRepo = new WorkerRepository(database);
            workerList = new List<Worker>(workerRepo.GetAll());
        }
        
        [TestCleanup]
        public void CleanUpTest()
        {
            database.Dispose();
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

    }
}
