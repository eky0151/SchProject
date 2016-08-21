namespace DbAndRepository.Repostirories
{
    using IRepositories;
    using GenericsEFRepository;
    using System.Data.Entity;
    using System.Linq;

    public class LoginDataRepository : GenericsRepository<LoginData>, ILoginDataRepository
    {
        public LoginDataRepository(DbContext newDb) : base(newDb)
        {
        }

        public bool Authenticate(LoginData login, out string fullName)
        {
            fullName = string.Empty;
            if (GetById(login.ID) == null)
                return false;
            fullName = (from i in database.Set<Worker>()
                        where i.ID == login.ID
                        select i.Fullname).ToString();
            return true;
        }

        public override void Delete(int id)
        {
            database.Set<LoginData>().Remove(GetById(id));
            database.SaveChanges();
        }

        public override LoginData GetById(int id)
        {
            return database.Set<LoginData>().FirstOrDefault(x => x.ID == id);
        }

        public override void Update(LoginData entityToModify)
        {
            database.Entry(GetById(entityToModify.ID)).CurrentValues.SetValues(entityToModify);
            database.SaveChanges();
        }
    }
}
