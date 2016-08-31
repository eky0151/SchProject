using DbAndRepository.GenericsEFRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DbAndRepository.IRepositories
{
    public interface IRegUserRepository : IGenericsRepository<RegUser>
    {
        bool Autenthicate(string userName, string password);

        BitmapImage GetPicture(string userName);

    }
}
