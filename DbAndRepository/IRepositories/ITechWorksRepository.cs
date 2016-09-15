namespace DbAndRepository.IRepositories
{
    using DbAndRepository.GenericsEFRepository;
    using System.Collections.Generic;

    public interface ITechWorksRepository : IGenericsRepositoryNoDUM<TechWorks>
    {
        List<TechWorks> GetByTechnician(int id);
        void RegisterNewWork(TechWorks w);
    }
}
