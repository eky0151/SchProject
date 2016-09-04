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
    public class TechWorksRepository : GenericsRepository<TechWorks>, ITechWorksRepository
    {
        public TechWorksRepository(DbContext newDb) : base(newDb)
        {
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override TechWorks GetById(int id)
        {
            return Get(i => i.ID == id).FirstOrDefault();
        }

        public List<TechWorks> GetByTechnician(int id)
        {
            return Get(i => i.TechID == id).ToList();
        }

        public override void Update(TechWorks entityToModify)
        {
            throw new NotImplementedException();
        }
    }
}
