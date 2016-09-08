using DbAndRepository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbAndRepository.GenericsEFRepository;
using System.Data.Entity;

namespace DbAndRepository.Repostirories
{
    class ClientLogsRepository : GenericsRepository<ClientLogs>, IClientLogsRepository
    {
        public ClientLogsRepository(DbContext newDb) : base(newDb)
        {
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
            //do not delete
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

        public override void Update(ClientLogs entityToModify)
        {
            throw new NotImplementedException();
            //do not update
        }
    }
}
