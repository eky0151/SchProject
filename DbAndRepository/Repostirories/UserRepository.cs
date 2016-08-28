using DbAndRepository.GenericsEFRepository;
using DbAndRepository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Data.Entity;
using System.Windows.Media;

namespace DbAndRepository.Repostirories
{
    class UserRepository : GenericsRepository<RegUser>, IUserRepository
    {
        public UserRepository(DbContext newDb) : base(newDb)
        {
        }

        public bool Authenticate(string username, string password, out string fullName)
        {
            fullName = string.Empty;
            RegUser r = Get(i => i.Username == username && i.Password == password).FirstOrDefault();
            if (r == null)
                return false;
            fullName = Get(i => r.ID == i.ID).Select(i => i.Fullname).FirstOrDefault();
            return true;

        }

        public override void Delete(int id)
        {
            RegUser r = GetById(id);
            database.Set<RegUser>().Remove(r);
            database.Entry<RegUser>(r).State = EntityState.Modified;
            database.SaveChanges();
        }

        public override RegUser GetById(int id)
        {
            return database.Set<RegUser>().FirstOrDefault(x => x.ID == id);
        }

        public BitmapImage GetPicture(string userName)
        {
            return (BitmapImage)new ImageSourceConverter().ConvertFrom((Get(i => i.Username == userName).FirstOrDefault().Picture));
        }

        public override void Update(RegUser entityToModify)
        {
            database.Entry(GetById(entityToModify.ID)).CurrentValues.SetValues(entityToModify);
            database.Entry(entityToModify).State = EntityState.Modified;
            database.SaveChanges();
        }
    }
}
