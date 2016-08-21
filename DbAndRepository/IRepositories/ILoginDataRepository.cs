namespace DbAndRepository.IRepositories
{
    using GenericsEFRepository;

    interface ILoginDataRepository : IGenericsRepository<LoginData>
    {
        bool Authenticate(LoginData login, out string fullName);
    }
}
