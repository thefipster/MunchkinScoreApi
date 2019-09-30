using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Munchkin.GameDomain;

namespace TheFipster.Munchkin.GameStorage.Volatile
{
    public class VolatilePlayerStore : IPlayerStore
    {
        private List<GameMaster> _players;

        public VolatilePlayerStore()
        {
            _players = new List<GameMaster>();
        }

        public void Add(GameMaster gameMaster) => _players.Add(gameMaster);

        public GameMaster Get(string email) => _players.FirstOrDefault(player => player.Email == email);

        public GameMaster Get(Guid gameMasterId) => _players.FirstOrDefault(player => player.Id == gameMasterId);
    }
}
