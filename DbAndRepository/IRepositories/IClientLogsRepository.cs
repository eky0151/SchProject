namespace DbAndRepository.IRepositories
{
    using DbAndRepository.GenericsEFRepository;
    using System.Collections.Generic;

    interface IClientLogsRepository : IGenericsRepositoryNoDUM<ClientLogs>
    {
        List<ClientLogs> GetByUser(RegUser user);
    }
}
