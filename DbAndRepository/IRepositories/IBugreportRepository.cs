namespace DbAndRepository.IRepositories
{
    using DbAndRepository.GenericsEFRepository;
    using System.Collections.Generic;

    public interface IBugreportRepository : IGenericsRepository<Bugreport>
    {
        List<string> GetFilesByBugreportID(int id);
    }
}
