namespace DbAndRepository.IRepositories
{
    using GenericsEFRepository;
    using System.Collections.Generic;

    interface ILogsRepository : IGenericsRepository<Logs>
    {
        List<Logs> GetByWorker(Worker worker);
    }
}
