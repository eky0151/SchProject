using System;
using System.Collections.Generic;

namespace DbAndRepository.IRepositories
{
    using DbAndRepository.GenericsEFRepository;

    public interface INewTechWorksRepository : IGenericsRepository<NewTechWorks>
    {
        void AddNewTechWork(string address, string customerName, DateTime orderTime, int technicianID);

        List<NewTechWorks> GetMyNewTechWorks(string username);
    }
}
