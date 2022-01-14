
using SqliteDatabase.Repositories;
using System;

namespace IotReworkDataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        int SaveChanges();
    }
}
