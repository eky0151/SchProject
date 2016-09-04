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
        }

        public override LoginLogs GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<LoginLogs> GetByWorker(Worker worker)
        {
            throw new System.NotImplementedException();
        }

        public override void Update(LoginLogs entityToModify)
        {
            throw new System.NotImplementedException();
        }
    }
}
