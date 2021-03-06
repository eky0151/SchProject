﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using DbAndRepository.Repostirories;
using DbAndRepository;
using DbAndRepository.IRepositories;
using System.Collections.Generic;
using System;
using System.Linq;

namespace DatabaseAndRepositoryTests
{
    [TestClass]
    public sealed class WorkerRepositoryTest
    {
        private IWorkerRepository workerRepo;
        private ITechWorksRepository techWorksRepo;
        private ITechnicianRepository technicianRepo;
        private TechSupportDatabaseEntities db =  new TechSupportDatabaseEntities();
        private List<Worker> workerList;
        private SupportService.TechSupportServiceSecure1Client host = new
            SupportService.TechSupportServiceSecure1Client();

        #region Initialize and Cleanup
        [TestInitialize]
        public void InitializeTest()
        {
            workerRepo = new WorkerRepository(db);
            techWorksRepo = new TechWorksRepository(db);
            technicianRepo = new TechninicanRepository(db);
            workerList = new List<Worker>(workerRepo.GetAll());
            //host.ClientCredentials.UserName.UserName = "Flynn";
            //host.ClientCredentials.UserName.Password = "sam";
            //host.Open();
        }

        [TestCleanup]
        public void CleanUpTest()
        {
            db.Dispose();
            workerRepo.Dispose();
            workerList = null;
            host.Close();
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
            
            //wp.RegisterNewWorker("propa2", "HelpDesk", "propa2", "away", "Available", "propaUtca2", "proba@email.hu2", "Propa Propa2", "063010010210", null,
            //    "Propa Bank2", "100propaaccount2", false);
        }

        [TestMethod]
        public void SelectNewWorker()
        {
            IWorkerRepository wp = new WorkerRepository(db);
            Worker w = wp.GetById(5);
            Assert.AreEqual("Propa Propa2", w.FullName);
        }

        [TestMethod]
        public void InsertTEchWorks()
        {
            List<Technician> techs = new List<Technician>(technicianRepo.GetAll());

            techWorksRepo.Insert(new TechWorks
            {
                TechID = techs[0].ID,
                Customeraddress = "1019, Bütyke Úrimalom utca 30",
                Customername = "Molnár András",
                Finish = DateTime.Now,
                Start = DateTime.Now.AddHours(-3),
                Price = 12400
            });

            List<TechWorks> works = new List<TechWorks>(techWorksRepo.GetAll());

            Assert.AreEqual("Molnár András", works[works.Count - 1].Customername);
        }

        [TestMethod]
        public void GetTEchnicianByName()
        {
            List<Technician> techs = technicianRepo.GetAll().ToList();
            string name = techs[0].Worker.FullName;

            Assert.AreEqual(name, technicianRepo.GetByName(name).Worker.FullName);
        }

        [TestMethod]
        public void GetCustomer()
        {
            SupportService.CustomerData d = host.LastCustomer();
            SupportService.CustomerData j = host.GetCustomer(d.UserName);
            Assert.AreNotEqual(null, j);
        }


        [TestMethod]
        public void GetCustomerByName()
        {
            SupportService.CustomerData d = host.GetCustomer("Bencee");
            Assert.AreNotEqual(null, d);
        }


        [TestMethod]
        public void GetLastSevenDaySolvedQuestions()
        {
            IRegUserRepository users = new RegUserRepository(db);

            List<DateTime> times;
            List<int> a  = users.GetLastMonthRegistratedUsers(out times);
        }

        [TestMethod]
        public void GetAvailableTechnicians()
        {
            Technician t = technicianRepo.GetAvailableTechnician();

            Assert.AreEqual(t, null);
        }
    }
}
