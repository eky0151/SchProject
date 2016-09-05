using DbAndRepository.GenericsEFRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAndRepository.IRepositories
{
    public interface ITechWorksRepository : IGenericsRepository<TechWorks>
    {
        List<TechWorks> GetByTechnician(int id);
    }
}
