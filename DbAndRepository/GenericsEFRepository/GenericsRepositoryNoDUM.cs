namespace DbAndRepository.GenericsEFRepository
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Linq.Expressions;

    public abstract class GenericsRepositoryNoDUM<TEntity> : IGenericsRepositoryNoDUM<TEntity> where TEntity : class
    {
        protected DbContext database;

        protected GenericsRepositoryNoDUM(DbContext newDb)
        {
            if (newDb == null)
                throw new ArgumentNullException(nameof(newDb));

            database = newDb;
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public IQueryable<TEntity> GetAll()
        {
            return database.Set<TEntity>();
        }

        public abstract TEntity GetById(int id);

        public void Insert(TEntity newEntity)
        {
            database.Set<TEntity>().Add(newEntity);
            database.Entry(newEntity).State = EntityState.Added;
            try
            {
                database.SaveChanges();

            }
            catch (DbEntityValidationException e)
            {
                var newException = new FormattedDbEntityValidationException(e);
                throw newException;
            }
        }

        public void Dispose()
        {
            database.Dispose();
        }
    }
}
