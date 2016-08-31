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
    class FilesRepository : GenericsRepository<Files>, IFileRepository
    {
        public FilesRepository(DbContext newDb) : base(newDb)
        {
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override Files GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(Files entityToModify)
        {
            throw new NotImplementedException();
        }
    }
}
