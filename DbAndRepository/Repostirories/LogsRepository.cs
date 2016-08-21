namespace DbAndRepository.Repostirories
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using GenericsEFRepository;
    using IRepositories;
    using System.Linq;

    class LogsRepository : GenericsRepository<Logs>, ILogsRepository
    {
        public LogsRepository(DbContext newDb) : base(newDb)
        {
        }

        public override void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public override Logs GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Logs> GetByWorker(Worker worker)
        {
            return GetAll().Where(i => i.ID == worker.ID).ToList();
        }

        public override void Update(Logs entityToModify)
        {
            throw new System.NotImplementedException();
        }
    }
}
