using LiteDB;
using System;
using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;

namespace TheFipster.Munchkin.GameStorage.LiteDb
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

        public GameMaster Get(string email) =>
            _collection.Find(x => x.Email == email).FirstOrDefault() 
            ?? throw new UnknownPlayerException();

        public GameMaster Get(Guid playerId) =>
            _collection.Find(x => x.Id == playerId).FirstOrDefault()
            ?? throw new UnknownPlayerException();
    }
}
