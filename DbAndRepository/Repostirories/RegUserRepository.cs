using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DbAndRepository.GenericsEFRepository;
using DbAndRepository.IRepositories;

namespace DbAndRepository.Repostirories
{
    class RegUserRepository : GenericsRepository<RegUser>, IRegUserRepository
    {
        public RegUserRepository(DbContext newDb) : base(newDb)
        {
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override RegUser GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(RegUser entityToModify)
        {
            throw new NotImplementedException();
        }
    }
}
