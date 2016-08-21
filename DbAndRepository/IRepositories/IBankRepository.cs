namespace DbAndRepository.IRepositories
{
    using GenericsEFRepository;

    public interface IBankRepository : IGenericsRepository<Bank>
    {
        Bank GetBankByWorker(Worker worker);
    }
}
