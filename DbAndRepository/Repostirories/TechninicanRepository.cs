using DbAndRepository.GenericsEFRepository;
using DbAndRepository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DbAndRepository.Repostirories
{
    class TechninicanRepository : GenericsRepository<Technician>, ITechnicianRepository
    {
        public TechninicanRepository(DbContext newDb) : base(newDb)
        {
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override Technician GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(Technician entityToModify)
        {
            throw new NotImplementedException();
        }
    }
}
