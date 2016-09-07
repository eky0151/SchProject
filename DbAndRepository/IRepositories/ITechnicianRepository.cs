using DbAndRepository.GenericsEFRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAndRepository.IRepositories
{
    public interface ITechnicianRepository : IGenericsRepository<Technician>
    {
        List<TechWorks> GetWorksByTechnician(int id);
        int GetAvailableTechnicianCount();
        Technician GetAvailableTechnician();
    }
}
