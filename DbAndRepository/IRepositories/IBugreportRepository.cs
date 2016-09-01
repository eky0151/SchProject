using DbAndRepository.GenericsEFRepository;
using System.Collections.Generic;

namespace DbAndRepository.IRepositories
{
    public interface IBugreportRepository : IGenericsRepository<Bugreport>
    {
        List<byte[]> GetFilesByBugreportID(int id);
    }
}
