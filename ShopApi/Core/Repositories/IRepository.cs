using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SqliteDatabase.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> Filter(Expression<Func<T, bool>> predicate);
        T GetById(int id);
        T Get(Expression<Func<T, bool>> predicate);
        bool Any(Expression<Func<T, bool>> predicate);        
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
    }
}
