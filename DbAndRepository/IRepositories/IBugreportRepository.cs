using DbAndRepository.GenericsEFRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAndRepository.IRepositories
{
    interface IBugreportRepository : IGenericsRepository<Bugreport>
    {
        List<byte[]> GetFilesByBugreportID(int id);
    }
}
