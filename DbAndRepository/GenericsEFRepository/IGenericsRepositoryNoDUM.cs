namespace DbAndRepository.GenericsEFRepository
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IGenericsRepositoryNoDUM<TEntity> : IDisposable where TEntity : class
        {
            void Insert(TEntity newEntity);

            IQueryable<TEntity> GetAll();

            IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

            TEntity GetById(int id);
        }   
}
