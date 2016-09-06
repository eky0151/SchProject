namespace DbAndRepository.IRepositories
{
    using GenericsEFRepository;
    using System.Windows.Media.Imaging;

    public interface ILoginDataRepository : IGenericsRepository<LoginData>
    {
        bool Authenticate(string username, string password);
        bool CheckUsername(string username);
        string GetPicture(string userName);
    }
}
