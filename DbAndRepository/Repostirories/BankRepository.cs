namespace DbAndRepository.Repostirories
{
    using System.Data.Entity;
    using GenericsEFRepository;
    using IRepositories;
    using System.Linq;

    public class BankRepository : GenericsRepository<Bank>, IBankRepository
    {
        private RegUser bank;

        public BankRepository(DbContext newDb) : base(newDb)
        {
        }

        public Worker GetBankByWorker(Worker worker)
        {
            return database.Set<Worker>().FirstOrDefault(i => i.ID == worker.BankID);
        }

        public override void Delete(int id)
        {
            bank = GetById(id);
            database.Set<Bank>().Remove(bank);
            database.Entry<Bank>(bank).State = EntityState.Deleted;
            database.SaveChanges();
        }

        public override Bank GetById(int id)
        {
            //return database.Set<RegUser>().FirstOrDefault(x => x.ID == id);
            throw new System.NotImplementedException();
        }

        public override void Update(Bank entityToModify)
        {
            database.Entry(GetById(entityToModify.ID)).CurrentValues.SetValues(entityToModify);
            database.Entry<Bank>(entityToModify).State = EntityState.Modified;
            database.SaveChanges();
        }
    }
}
