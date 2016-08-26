using DbAndRepository.GenericsEFRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAndRepository.IRepositories
{
    interface ITechnicianRepository : IGenericsRepository<Technician>
    {
        List<TechWorks> GetWorksByTechnician(int id);
    }
}
