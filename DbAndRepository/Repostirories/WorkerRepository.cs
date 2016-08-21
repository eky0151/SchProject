namespace DbAndRepository.Repostirories
{
    using System.Data.Entity;
    using GenericsEFRepository;
    using IRepositories;
    using System.Linq;

    public class WorkerRepository : GenericsRepository<Worker>, IWorkerRepository
    {
        public WorkerRepository(DbContext newDb) : base(newDb)
        {
        }

        public override void Delete(int id)
        {
            database.Set<Worker>().Remove(GetById(id));
            database.SaveChanges();
        }

        public override Worker GetById(int id)
        {
            return database.Set<Worker>().FirstOrDefault(x => x.ID == id);
        }

        public override void Update(Worker entityToModify)
        {
            database.Entry(GetById(entityToModify.ID)).CurrentValues.SetValues(entityToModify);
            database.SaveChanges();
        }
    }
}
