namespace DbAndRepository.IRepositories
{
    using GenericsEFRepository;

    public interface ILoginDataRepository : IGenericsRepository<LoginData>
    {
        bool Authenticate(LoginData login, out string fullName);
    }
}
