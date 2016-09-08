namespace DbAndRepository.Repostirories
{
    using System.Data.Entity;
    using GenericsEFRepository;
    using IRepositories;
    using System.Linq;
    using System.Collections.Generic;
    using System;

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

        public void RegisterNewWorker(string username, string urole,string passwd,string Workerstatus,string available,string address, string email,string fullName,string phone,string profilePicture, string bankName,string bankAccount,bool technician)
        {
            Bank b = new Bank
            {
                Name = bankName,
                Account = bankAccount
            };

            Technician t = null;
            if(technician == true)
            {
                t = new Technician
                {
                    Available = available           
                };
            }

            LoginData l = new LoginData
            {
                Username = username,
                Password = passwd,
                Urole = urole
            };

            Worker w = new Worker
            {
                FullName = fullName,
                Phone = phone,
                ProfilePicture = profilePicture,
                Email = email,
                Address = address,
                Bank = b,
                LoginData = new HashSet<LoginData> { l },
                Technician = new HashSet<Technician> { t },
                Status = Workerstatus
            };

            base.Insert(w);
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
