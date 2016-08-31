namespace DbAndRepository.IRepositories
{
    using GenericsEFRepository;

    public interface IBankRepository : IGenericsRepository<RegUser>
    {
        RegUser GetBankByWorker(Worker worker);
    }
}
