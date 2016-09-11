namespace DbAndRepository.Repostirories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using DbAndRepository.GenericsEFRepository;
    using DbAndRepository.IRepositories;
    using System.Windows.Media.Imaging;
    using System.Windows.Media;
    using System.Collections.Generic;
    using static System.Data.Entity.DbFunctions;

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
            user = GetById(id);
            database.Set<RegUser>().Remove(user);
            database.Entry<RegUser>(user).State = EntityState.Deleted;
            database.SaveChanges();
        }

        public override RegUser GetById(int id)
        {
            return database.Set<RegUser>().FirstOrDefault(i => i.ID == id);
        }

        public List<int> GetLastMonthRegistratedUsers(out List<DateTime> times)
        {
            var criteria = DateTime.Now.AddDays(-30);
            var query = from i in GetAll()
                        where i.Regtime >= criteria
                        group i by TruncateTime(i.Regtime) into g
                        select new
                        {
                            Count = g.Count(),
                            Time = (DateTime)g.Key
                        };
            List<int> t = new List<int>();
            times = new List<DateTime>();

            foreach (var j in query)
            {
                t.Add(j.Count);
                times.Add(j.Time);
            }
            return t;
        }

        public string GetPicture(string userName)
        {
            return Get(i => i.Username == userName).FirstOrDefault()?.Picture;
        }

        public RegUser GetUserByUsername(string username)
        {
            return Get(i => i.Username == username).FirstOrDefault();
        }

        public override void Update(RegUser entityToModify)
        {
            database.Entry(GetById(entityToModify.ID)).CurrentValues.SetValues(entityToModify);
            database.Entry<RegUser>(entityToModify).State = EntityState.Modified;
            database.SaveChanges();
        }
    }
}
