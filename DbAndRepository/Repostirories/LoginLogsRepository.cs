namespace DbAndRepository.Repostirories
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using GenericsEFRepository;
    using IRepositories;
    using System.Linq;

    public class LoginLogsRepository : GenericsRepositoryNoDUM<LoginLogs>, ILoginLogsRepository
    {
        public LoginLogsRepository(DbContext newDb) : base(newDb)
        {
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
    }
}
