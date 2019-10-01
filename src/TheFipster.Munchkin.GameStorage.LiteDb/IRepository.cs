using LiteDB;
using System;

namespace TheFipster.Munchkin.GameStorage.LiteDb
{
    public interface IRepository<T> : IDisposable
    {
        LiteCollection<T> GetCollection();
    }
}
