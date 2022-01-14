using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SqliteDatabase.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        /// <summary>
        /// Ctor
        /// </summary>
        public Repository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentException("dbContext can not be null");

            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).Any();
        }

        public void Delete(T entity)
        {
            if (entity.GetType().GetProperty("IsDelete") != null)
            {
                T _entity = entity;

                _entity.GetType().GetProperty("IsDelete").SetValue(_entity, true);

                this.Update(_entity);
            }
            else
            {
                var dbEntityEntry = _dbContext.Entry(entity);

                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    _dbSet.Attach(entity);
                    _dbSet.Remove(entity);
                }
            }
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return;
            else
            {
                if (entity.GetType().GetProperty("IsDelete") != null)
                {
                    T _entity = entity;
                    _entity.GetType().GetProperty("IsDelete").SetValue(_entity, true);

                    this.Update(_entity);
                }
                else
                {
                    Delete(entity);
                }
            }
        }
     
        public IQueryable<T> Filter(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).FirstOrDefault();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
