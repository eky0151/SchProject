namespace DbAndRepository.Repostirories
{
    using DbAndRepository.GenericsEFRepository;
    using DbAndRepository.IRepositories;
    using System;
    using System.Linq;
    using System.Data.Entity;
    using System.Collections.Generic;

    public class BugreportRepository : GenericsRepository<Bugreport>, IBugreportRepository
    {
        private Bugreport bg;

        public BugreportRepository(DbContext newDb) : base(newDb)
        {
        }

        public override void Delete(int id)
        {
            bg = GetById(id);
            database.Set<Bugreport>().Remove(bg);
            database.Entry<Bugreport>(bg).State = EntityState.Deleted;
            database.SaveChanges();
        }

        public override Bugreport GetById(int id)
        {
            return database.Set<Bugreport>().FirstOrDefault(x => x.ID == id);
        }

        public List<string> GetFilesByBugreportID(int id)
        {
            return (from i in database.Set<Files>()
                    where i.ID == id
                    select i.File).ToList<string>();
        }

        public override void Update(Bugreport entityToModify)
        {
            throw new System.NotImplementedException();
            //do not modify
        }
    }
}
