using LiteDB;
using System;
using System.Linq;
using TheFipster.Munchkin.Gaming.Domain;
using TheFipster.Munchkin.Gaming.Domain.Exceptions;

namespace TheFipster.Munchkin.Gaming.Storage.LiteDb
{
    public class PlayerStore : IPlayerStore
    {
        private readonly LiteCollection<GameMaster> _collection;

        public PlayerStore(IRepository<GameMaster> repository)
        {
            var repo = repository;
            _collection = repo.GetCollection();
        }

        public void Add(GameMaster gameMaster) =>
            _collection.Upsert(gameMaster);

        public GameMaster Get(Guid playerId) =>
            _collection.Find(x => x.Id == playerId).FirstOrDefault()
            ?? throw new UnknownPlayerException();

        public GameMaster GetByExternalId(string externalId) =>
            _collection.Find(x => x.ExternalId == externalId).FirstOrDefault()
            ?? throw new UnknownPlayerException();

        public GameMaster Register(string name, string externalId, string email = null)
        {
            var gameMaster = new GameMaster(name, externalId, email);
            _collection.Insert(gameMaster);
            return gameMaster;
        }
    }
}
