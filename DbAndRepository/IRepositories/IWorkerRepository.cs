namespace DbAndRepository.IRepositories
{
    using System.Collections.Generic;
    using GenericsEFRepository;

    public interface IWorkerRepository : IGenericsRepository<Worker>
    {
        int GetAvailableHelpDeskCount();

        Worker GetAvailableHelpDesk();

        List<Worker> GetHelpDeskList();

        void ChangePicture(int workerID, string picture);

        void RegisterNewWorker(string username, string urole, string passwd, string Workerstatus, string address,
            string email, string fullName, string phone, string profilePicture, string bankName, string bankAccount);
    }
}
