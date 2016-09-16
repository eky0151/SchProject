namespace DbAndRepository.Repostirories
{
    using DbAndRepository.GenericsEFRepository;
    using DbAndRepository.IRepositories;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class FilesRepository : GenericsRepository<Files>, IFileRepository
    {
        private Files files;

        public FilesRepository(DbContext newDb) : base(newDb)
        {
        }

        public override void Delete(int id)
        {
            files =  GetById(id);
            database.Set<Files>().Remove(files);
            database.Entry<Files>(files).State = EntityState.Deleted;
            database.SaveChanges();
        }

        public override Files GetById(int id)
        {
            List<Files> a = new List<Files>(GetAll());
            return  a.Find(i => i.ID == id);
        }

        public override void Update(Files entityToModify)
        {
            database.Entry(GetById(entityToModify.ID)).CurrentValues.SetValues(entityToModify);
            database.Entry<Files>(entityToModify).State = EntityState.Modified;
            database.SaveChanges();
        }
    }
}
