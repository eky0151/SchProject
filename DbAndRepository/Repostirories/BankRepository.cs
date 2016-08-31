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

        public RegUser GetBankByWorker(Worker worker)
        {
            return database.Set<RegUser>().FirstOrDefault(i => i.ID == worker.BankID);
        }

        public override void Delete(int id)
        {
            bank = GetById(id);
            database.Set<RegUser>().Remove(bank);
            database.Entry<RegUser>(bank).State = EntityState.Deleted;
            database.SaveChanges();
        }

        public override RegUser GetById(int id)
        {
            //return database.Set<RegUser>().FirstOrDefault(x => x.ID == id);
            throw new System.NotImplementedException();
        }

        public override void Update(RegUser entityToModify)
        {
            database.Entry(GetById(entityToModify.ID)).CurrentValues.SetValues(entityToModify);
            database.Entry<RegUser>(entityToModify).State = EntityState.Modified;
            database.SaveChanges();
        }
    }
}
