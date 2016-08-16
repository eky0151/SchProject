namespace DbAndRepository.GenericsEFRepository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public interface IGenericsRepository<TEntity> : IDisposable where TEntity : class
    {
        void Insert(TEntity newEntity);

        void Delete(int id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToModify);

        IQueryable<TEntity> GetAll();

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        TEntity GetById(int id);
    }
}
