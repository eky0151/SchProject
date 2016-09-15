namespace DbAndRepository.Repostirories
{
    using DbAndRepository.GenericsEFRepository;
    using DbAndRepository.IRepositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;
    using static System.Data.Entity.DbFunctions;

    public class SolvedQuestionsRepository : GenericsRepositoryNoDUM<SolvedQuestion>, ISolvedQuestionsRepository
    {
        public SolvedQuestionsRepository(DbContext newDb) : base(newDb)
        {
        }

        public override SolvedQuestion GetById(int id)
        {
            return Get(i => i.ID == id).FirstOrDefault();
        }

        public List<SolvedQuestion> GetByWorker(int id)
        {
            return Get(i => i.WorkerID == id).ToList();
        }

        public List<int> GetLastSevenDaysSolvedQuestions(out List<DateTime> d, out List<KeyValuePair<string, int>> byName)
        {
            var dateCriteria = DateTime.Now.Date.AddDays(-7);
            var all = GetAll();
            var QueryDeatils = from i in all
                               where i.Timeanswered >= dateCriteria
                               group i by TruncateTime(i.Timeanswered) into g
                               select new
                               {
                                   Count = g.Count(),
                                   Time = (DateTime)g.Key
                               };

            var query2 = from i in all
                         where i.Timeanswered >= dateCriteria
                         group i by i.Worker.FullName into g
                         select new
                         {
                             Count = g.Count(),
                             Name = g.Key
                         };

            byName = new List<KeyValuePair<string, int>>();

            foreach (var p in query2)
            {
                byName.Add(new KeyValuePair<string, int>(p.Name, p.Count));
            }


            d = new List<DateTime>();
            List<int> s = new List<int>();
            foreach (var j in QueryDeatils)
            {
                s.Add(j.Count);
                d.Add(j.Time);
            }

            return s;
        }
    }
}
