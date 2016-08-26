namespace DbAndRepository.IRepositories
{
    using GenericsEFRepository;
    using System.Windows.Media.Imaging;

    public interface ILoginDataRepository : IGenericsRepository<LoginData>
    {
        bool Authenticate(LoginData login, out string fullName);

        BitmapImage GetPicture(string userName);
    }
}
