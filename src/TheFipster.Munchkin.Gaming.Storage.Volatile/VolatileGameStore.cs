using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Munchkin.Gaming.Domain;
using TheFipster.Munchkin.Gaming.Domain.Exceptions;

namespace TheFipster.Munchkin.Gaming.Storage.Volatile
{
    public class VolatileGameStore : IGameStore
    {
        private IList<Game> games;

        public VolatileGameStore() =>
            games = new List<Game>();

        public Game Get(Guid gameId) => 
            games.FirstOrDefault(game => game.Id == gameId) 
            ?? throw new UnknownGameException();

        public void Upsert(Game game)
        {
            if (games.Any(g => g.Id == game.Id))
                games.Remove(game);
            
            games.Add(game);
        }
    }
}
