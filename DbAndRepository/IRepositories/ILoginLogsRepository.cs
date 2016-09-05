namespace DbAndRepository.IRepositories
{
    using GenericsEFRepository;
    using System.Collections.Generic;

    public interface ILoginLogsRepository : IGenericsRepository<LoginLogs>
    {
        List<LoginLogs> GetByWorker(Worker worker);
    }
}
