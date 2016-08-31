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
    public class RegUserRepository : GenericsRepository<RegUser>, IRegUserRepository
    {
        private RegUser user;

        public RegUserRepository(DbContext newDb) : base(newDb)
        {
        }

        public bool Autenthicate(string userName, string password)
        {
            user = Get(i => i.Username == userName && i.Password == password).FirstOrDefault();
            if (user == null)
                return false;
            return true;
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
