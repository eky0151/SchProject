namespace DbAndRepository.Repostirories
{
    using DbAndRepository.GenericsEFRepository;
    using DbAndRepository.IRepositories;
    using System;
    using System.Data.Entity;

    public class FilesRepository : GenericsRepository<Files>, IFileRepository
    {
        private Files files;

        public FilesRepository(DbContext newDb) : base(newDb)
        {
        }

        public override void Delete(int id)
        {
            files = GetById(id);
            database.Set<Files>().Remove(files);
            database.Entry<Files>(files).State = EntityState.Deleted;
            database.SaveChanges();
        }

        public override async Files GetById(int id)
        {
            files = await Get(i => i.ID == id).FirstOrDefaultAsync();
            return files;
        }

        public override void Update(Files entityToModify)
        {
            database.Entry(GetById(entityToModify.ID)).CurrentValues.SetValues(entityToModify);
            database.Entry<Files>(entityToModify).State = EntityState.Modified;
            database.SaveChanges();
        }
    }
}
