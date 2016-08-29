namespace DbAndRepository.IRepositories
{
    using GenericsEFRepository;
    using System.Windows.Media.Imaging;

    public interface ILoginDataRepository : IGenericsRepository<LoginData>
    {
        bool Authenticate(string username, string password, out string fullName, out string role);

        BitmapImage GetPicture(string userName);
    }
}
