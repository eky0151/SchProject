using System.Data.Entity.Validation;

namespace DbAndRepository.GenericsEFRepository
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Entity;

    public abstract class GenericsRepository<TEntity> : IGenericsRepository<TEntity> where TEntity : class
    {
        protected DbContext database;

        protected GenericsRepository(DbContext newDb)
        {
            if (newDb == null)
                throw new ArgumentNullException(nameof(newDb));

            database = newDb;
        }

        public abstract void Delete(int id);
        public abstract TEntity GetById(int id);
        public abstract void Update(TEntity entityToModify);

        public void Delete(TEntity entityToDelete)
        {
            database.Set<TEntity>().Remove(entityToDelete);
            database.Entry<TEntity>(entityToDelete).State = EntityState.Deleted;
            database.SaveChanges();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public IQueryable<TEntity> GetAll()
        {
            return database.Set<TEntity>();
        }

        public virtual void Insert(TEntity newEntity)
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
