using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DbAndRepository.Repostirories;
using DbAndRepository;
using DbAndRepository.IRepositories;

namespace DatabaseAndRepositoryTests
{
    [TestClass]
    public sealed class RepositoryTest
    {
        [TestInitialize]
        public void InitializeTest()
        {
            DatabaseEntities database = new DatabaseEntities();
            IWorkerRepository workerRepo = new WorkerRepository(database);

        }
        

        [TestMethod]
        public void TestWorker()
        {
            DatabaseEntities
        }
    }
}
