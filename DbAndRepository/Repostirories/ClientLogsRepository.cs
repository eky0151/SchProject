namespace DbAndRepository.Repostirories
{
    using DbAndRepository.IRepositories;
    using System.Collections.Generic;
    using System.Linq;
    using DbAndRepository.GenericsEFRepository;
    using System.Data.Entity;

    public class ClientLogsRepository : GenericsRepositoryNoDUM<ClientLogs>, IClientLogsRepository
    {
        public ClientLogsRepository(DbContext newDb) : base(newDb)
        {
        }

        public override ClientLogs GetById(int id)
        {
            return database.Set<ClientLogs>().FirstOrDefault(i => i.ID == id);
        }

        public List<ClientLogs> GetByUser(RegUser user)
        {
            return (from i in database.Set<ClientLogs>()
                    let name = user.Username
                    where i.Clientname == name
                    select i).ToList();
        }
    }
}
