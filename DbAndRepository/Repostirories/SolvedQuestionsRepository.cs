using DbAndRepository.GenericsEFRepository;
using DbAndRepository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using static System.Data.Entity.DbFunctions;

namespace DbAndRepository.Repostirories
{
    public class SolvedQuestionsRepository : GenericsRepository<SolvedQuestion>, ISolvedQuestionsRepository
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
            return Get(i => i.ID == id).FirstOrDefault();
        }

        public List<SolvedQuestion> GetByWorker(int id)
        {
            return Get(i => i.WorkerID == id).ToList();
        }

        public List<int> GetLastSevenDaysSolvedQuestions(out List<DateTime> d)
        {
            var dateCriteria = DateTime.Now.Date.AddDays(-7);
            var QueryDeatils = from i in GetAll()
                               where i.Timeanswered >= dateCriteria
                               group i by TruncateTime(i.Timeanswered) into g
                               select new
                               {
                                   Count = g.Count(),
                                   Time = (DateTime)g.Key
                               };


            d = new List<DateTime>();
            List<int> s = new List<int>();
            foreach (var j in QueryDeatils)
            {
                s.Add(j.Count);
                d.Add(j.Time);
            }

            return s;
        }

        public override void Update(SolvedQuestion entityToModify)
        {
            throw new NotImplementedException();
            //do not mod
        }
    }
}
