namespace DbAndRepository.Repostirories
{
    using IRepositories;
    using GenericsEFRepository;
    using System.Data.Entity;
    using System.Linq;
    using System.Windows.Media.Imaging;
    using System.Windows.Media;
    using System.Threading.Tasks;

    public class LoginDataRepository : GenericsRepository<LoginData>, ILoginDataRepository
    {
        private LoginData l;

        public LoginDataRepository(DbContext newDb) : base(newDb)
        {
        }

        public bool Authenticate(string username, string password, out string fullName, out string role)
        {
            fullName = role = string.Empty;
            l = Get(i => i.Username == username && i.Password == password).FirstOrDefault();
            if (l == null)
                return false;

            fullName = (from i in database.Set<Worker>()
                        where i.ID == l.ID
                        select i.Fullname).ToString();
            role = l.Urole;

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

        //public async Task<BitmapImage> GetPicture(string userName)
        //{
        //     await Task.Run(() => (BitmapImage)new ImageSourceConverter().ConvertFrom((Get(i => i.Username == userName).FirstOrDefault().Worker.Picture)));
        //}

        public BitmapImage GetPicture(string userName)
        {
            return (BitmapImage)new ImageSourceConverter().ConvertFrom((Get(i => i.Username == userName).FirstOrDefault().Worker.Picture));
        }

        public override void Update(LoginData entityToModify)
        {
            database.Entry(GetById(entityToModify.ID)).CurrentValues.SetValues(entityToModify);
            database.Entry(entityToModify).State = EntityState.Modified;
            database.SaveChanges();
        }
    }
}
