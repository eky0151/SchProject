using DbAndRepository.GenericsEFRepository;
using DbAndRepository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace DbAndRepository.Repostirories
{
    class SolvedQuestionsRepository : GenericsRepository<SolvedQuestion>, ISolvedQuestionsRepository
    {
        public SolvedQuestionsRepository(DbContext newDb) : base(newDb)
        {
        }

        public override void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override SolvedQuestion GetById(int id)
        {
            return Get(i => i.Id == id).FirstOrDefault();
        }

        public List<SolvedQuestion> GetByWorker(int id)
        {
            return Get(i => i.WorkerID == id).ToList();
        }

        public override void Update(SolvedQuestion entityToModify)
        {
            throw new NotImplementedException();
        }
    }
}
