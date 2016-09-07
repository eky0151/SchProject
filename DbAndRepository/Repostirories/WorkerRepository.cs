﻿using System;

namespace DbAndRepository.Repostirories
{
    using System.Data.Entity;
    using GenericsEFRepository;
    using IRepositories;
    using System.Linq;

    public class WorkerRepository : GenericsRepository<Worker>, IWorkerRepository
    {
        private Worker w;

        public WorkerRepository(DbContext newDb) : base(newDb)
        {
        }

        public override void Delete(int id)
        {
            w = GetById(id);
            database.Set<Worker>().Remove(w);
            database.Entry<Worker>(w).State = EntityState.Deleted;
            database.SaveChanges();
        }

        public void RegisterNewWorker(string username,string passwd,string address, string email,string fullName,string phone,string profilePicture,string status, string bankName,string bankAccount,bool technician)
        {
            
        }

        public Worker GetAvailableHelpDesk()
        {
            return Get(x => x.LoginData.SingleOrDefault().Urole == "HelpDesk").FirstOrDefault();
        }

        public override Worker GetById(int id)
        {
            return database.Set<Worker>().FirstOrDefault(x => x.ID == id);
        }

        public override void Update(Worker entityToModify)
        {
            database.Entry(GetById(entityToModify.ID)).CurrentValues.SetValues(entityToModify);
            database.Entry<Worker>(entityToModify).State = EntityState.Modified;
            database.SaveChanges();
        }

        public int GetAvailableHelpDeskCount()
        {
            return
                Get(x => x.LoginData.SingleOrDefault().Urole == "HelpDesk").Count(x => x.Status == "Available");
        }

    }
}
