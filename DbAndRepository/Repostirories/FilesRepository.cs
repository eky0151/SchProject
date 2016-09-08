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
