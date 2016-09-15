namespace DbAndRepository.Repostirories
{
    using DbAndRepository.GenericsEFRepository;
    using DbAndRepository.IRepositories;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;

    public class TechWorksRepository : GenericsRepositoryNoDUM<TechWorks>, ITechWorksRepository
    {
        public TechWorksRepository(DbContext newDb) : base(newDb)
        {
        }

        public override TechWorks GetById(int id)
        {
            return Get(i => i.ID == id).FirstOrDefault();
        }

        public List<TechWorks> GetByTechnician(int id)
        {
            return Get(i => i.TechID == id).ToList();
        }

        public void RegisterNewWork(TechWorks w)
        {
            base.Insert(w);
        }
    }
}
