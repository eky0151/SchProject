namespace DbAndRepository.IRepositories
{
    using GenericsEFRepository;

    public interface IWorkerRepository : IGenericsRepository<Worker>
    {
        int GetAvailableHelpDeskCount();

        Worker GetAvailableHelpDesk();

        void RegisterNewWorker(string username, string urole, string passwd,string Workerstatus, string available, string address, string email, string fullName, string phone, string profilePicture, string bankName, string bankAccount, bool technician);
    }
}
