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

        string GetPicture(string userName);

        List<int> GetLastMonthRegistratedUsers(out List<DateTime> times);

    }
}
