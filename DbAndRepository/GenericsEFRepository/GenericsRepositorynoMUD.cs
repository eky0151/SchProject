using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DbAndRepository.GenericsEFRepository
{
    public abstract class GenericsRepositorynoMUD<TEntity> : IGenericsRepositryNoDUM<TEntity> where TEntity : class
    {
        protected DbContext database;

        protected GenericsRepositorynoMUD(DbContext newDb)
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
