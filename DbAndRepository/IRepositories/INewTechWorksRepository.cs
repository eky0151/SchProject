using System;

namespace DbAndRepository.IRepositories
{
    using DbAndRepository.GenericsEFRepository;

    public interface INewTechWorksRepository : IGenericsRepository<NewTechWorks>
    {
        void AddNewTechWork(string address, string customerName, DateTime orderTime, int technicianID);
    }
}
