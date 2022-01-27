using Microsoft.EntityFrameworkCore;
using ShopApi.Db.Contexts;
using SqliteDatabase.Repositories;
using System;


namespace ShopApi.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;


        public UnitOfWork()
        {
            _dbContext = new SqliteContext();
        }


        public UnitOfWork(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext can not be null.");

            _dbContext = dbContext;
        }


        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(_dbContext);
        }

        public int SaveChanges()
        {
            try
            {
                return _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        #region IDisposable Members
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
