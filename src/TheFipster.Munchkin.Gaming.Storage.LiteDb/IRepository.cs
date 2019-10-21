using LiteDB;
using System;

namespace TheFipster.Munchkin.Gaming.Storage.LiteDb
{
    public interface IRepository<T> : IDisposable
    {
        LiteCollection<T> GetCollection();
    }
}
