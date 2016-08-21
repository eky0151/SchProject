namespace DbAndRepository.IRepositories
{
    using GenericsEFRepository;

    interface IBankRepository : IGenericsRepository<Bank>
    {
        Bank GetBankByWorker(Worker worker);
    }
}
