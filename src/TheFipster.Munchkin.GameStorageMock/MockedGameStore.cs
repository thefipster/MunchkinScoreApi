using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GamePersistance;

namespace TheFipster.Munchkin.GameStorageVolatile
{
    public class MockedGameStore : IGameStore
    {
        private IList<Game> games;

        public MockedGameStore() =>
            games = new List<Game>();

        public Game Get(Guid gameId) => 
            games.FirstOrDefault(x => x.Id == gameId) 
            ?? throw new UnknownGameException();

        public void Upsert(Game game)
        {
            if (games.Any(x => x.Id == game.Id))
                games.Remove(game);
            
            games.Add(game);
        }
    }
}
