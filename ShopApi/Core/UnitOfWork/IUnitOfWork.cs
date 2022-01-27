
using SqliteDatabase.Repositories;
using System;

namespace ShopApi.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        int SaveChanges();
    }
}
