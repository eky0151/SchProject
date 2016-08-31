namespace DbAndRepository.IRepositories
{
    using GenericsEFRepository;

    public interface IBankRepository : IGenericsRepository<Bank>
    {
        Worker GetBankByWorker(Worker worker);
    }
}
