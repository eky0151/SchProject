namespace DbAndRepository.Repostirories
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using GenericsEFRepository;
    using IRepositories;
    using System.Linq;

    public class LoginLogsRepository : GenericsRepository<LoginLogs>, ILoginLogsRepository
    {
        public LoginLogsRepository(DbContext newDb) : base(newDb)
        {
        }

        public override void Delete(int id)
        {
            throw new System.NotImplementedException();
            //do not delete
        }

        public override LoginLogs GetById(int id)
        {
            return database.Set<LoginLogs>().FirstOrDefault(x => x.ID == id);
        }

        public List<LoginLogs> GetByWorker(Worker worker)
        {
            return (from i in database.Set<LoginLogs>()
                   let name = worker.LoginData.FirstOrDefault().Username
                   where i.LoginName == name
                   select i).ToList();
        }

        public override void Update(LoginLogs entityToModify)
        {
            throw new System.NotImplementedException();
            //do not update
        }
    }
}
