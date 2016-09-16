namespace DbAndRepository.IRepositories
{
    using GenericsEFRepository;
    using System.Collections.Generic;

    public interface ILoginLogsRepository : IGenericsRepositoryNoDUM<LoginLogs>
    {
        List<LoginLogs> GetByWorker(Worker worker);
    }
}
