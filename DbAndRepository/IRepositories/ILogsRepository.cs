namespace DbAndRepository.IRepositories
{
    using GenericsEFRepository;
    using System.Collections.Generic;

    public interface ILogsRepository : IGenericsRepository<Logs>
    {
        List<Logs> GetByWorker(Worker worker);
    }
}
