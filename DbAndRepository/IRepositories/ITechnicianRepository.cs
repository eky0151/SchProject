namespace DbAndRepository.IRepositories
{
    using DbAndRepository.GenericsEFRepository;
    using System.Collections.Generic;

    public interface ITechnicianRepository : IGenericsRepository<Technician>
    {
        List<TechWorks> GetWorksByTechnician(int id);
        int GetAvailableTechnicianCount();
        Technician GetAvailableTechnician();
        Technician GetByName(string name);

    }
}
