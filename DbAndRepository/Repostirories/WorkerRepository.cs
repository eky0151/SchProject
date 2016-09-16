namespace DbAndRepository.Repostirories
{
    using System.Data.Entity.Spatial;
    using System.Data.Entity;
    using GenericsEFRepository;
    using IRepositories;
    using System.Linq;
    using System.Collections.Generic;

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

        public void RegisterNewWorker(string username, string urole, string passwd, string Workerstatus, string address, string email, string fullName, string phone, string profilePicture, string bankName, string bankAccount)
        {
            Bank b = new Bank
            {
                Name = bankName,
                Account = bankAccount
            };

            Technician t = null;
            if (urole == "Technician")
            {
                t = new Technician
                {
                    Available = "Break",
                    Lastlocation= DbGeography.FromText("POINT(47.491358 19.075642)")
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

        public List<Worker> GetHelpDeskList()
        {
            return Get(x => x.LoginData.FirstOrDefault().Urole == "HelpDesk").ToList();
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
            return Get(x => x.LoginData.FirstOrDefault().Urole == "HelpDesk").Count(x => x.Status == "Available");
        }

        public void ChangePicture(int workerID, string picture)
        {
            var worker = GetById(workerID);
            worker.ProfilePicture = picture;
            Update(worker);
        }
    }
}
