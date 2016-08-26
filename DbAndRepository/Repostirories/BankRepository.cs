namespace DbAndRepository.Repostirories
{
    using System.Data.Entity;
    using GenericsEFRepository;
    using IRepositories;
    using System.Linq;

    public class BankRepository : GenericsRepository<Bank>, IBankRepository
    {
        private Bank bank;

        public BankRepository(DbContext newDb) : base(newDb)
        {
        }

        public Bank GetBankByWorker(Worker worker)
        {
            return database.Set<Bank>().FirstOrDefault(i => i.ID == worker.BankID);
        }

        public override void Delete(int id)
        {
            bank = GetById(id);
            database.Set<Bank>().Remove(b);
            database.Entry<Bank>(b).State = EntityState.Deleted;
            database.SaveChanges();
        }

        public override Bank GetById(int id)
        {
            return database.Set<Bank>().FirstOrDefault(x => x.ID == id);
        }

        public override void Update(Bank entityToModify)
        {
            database.Entry(GetById(entityToModify.ID)).CurrentValues.SetValues(entityToModify);
            database.Entry<Bank>(entityToModify).State = EntityState.Modified;
            database.SaveChanges();
        }
    }
}
