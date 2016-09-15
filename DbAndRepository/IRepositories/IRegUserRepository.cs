namespace DbAndRepository.IRepositories
{
    using DbAndRepository.GenericsEFRepository;
    using System;
    using System.Collections.Generic;

    public interface IRegUserRepository : IGenericsRepository<RegUser>
    {
        bool Autenthicate(string userName, string password);

        string GetPicture(string userName);

        List<int> GetLastMonthRegistratedUsers(out List<DateTime> times);

        RegUser GetUserByUsername(string username);

    }
}
