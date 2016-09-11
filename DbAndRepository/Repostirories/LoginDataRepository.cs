namespace DbAndRepository.Repostirories
{
    using IRepositories;
    using GenericsEFRepository;
    using System.Data.Entity;
    using System.Linq;
    using System.Windows.Media.Imaging;
    using System.Windows.Media;
    using System.Threading.Tasks;
    using System;

    public class LoginDataRepository : GenericsRepository<LoginData>, ILoginDataRepository
    {
        private LoginData l;

        public LoginDataRepository(DbContext newDb) : base(newDb)
        {
        }

        public bool Authenticate(string username, string password)
        {
            l = Get(i => i.Username == username && i.Password == password).FirstOrDefault();
            if (l == null)
                return false;
            return true;
        }

        public void ChangePassWD(string username, string newPasswd)
        {
            var user = Get(x=>x.Username==username).FirstOrDefault();
            user.Password = newPasswd;
            Update(user);
        }

        public bool CheckUsername(string username)
        {
            var login = Get(x => x.Username == username).FirstOrDefault();
            if (login == null)
                return false;
            return true;
        }

        public override void Delete(int id)
        {
            l = GetById(id);
            database.Set<LoginData>().Remove(l);
            database.Entry<LoginData>(l).State = EntityState.Deleted;
            database.SaveChanges();
        }

        public override LoginData GetById(int id)
        {
            return database.Set<LoginData>().FirstOrDefault(x => x.ID == id);
        }


        public string GetPicture(string userName)
        {
            return Get(i => i.Username == userName).FirstOrDefault()?.Worker.ProfilePicture;
        }

        public override void Update(LoginData entityToModify)
        {
            database.Entry(GetById(entityToModify.ID)).CurrentValues.SetValues(entityToModify);
            database.Entry(entityToModify).State = EntityState.Modified;
            database.SaveChanges();
        }

        
    }
}
