﻿using DbAndRepository.GenericsEFRepository;
using System.Collections.Generic;

namespace DbAndRepository.IRepositories
{
    public interface IBugreportRepository : IGenericsRepository<Bugreport>
    {
        List<string> GetFilesByBugreportID(int id);
    }
}
