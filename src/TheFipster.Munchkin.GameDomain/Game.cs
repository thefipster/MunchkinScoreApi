using System;
using System.Collections.Generic;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameDomain
{
    public class Game
    {
        public Game()
        {
            Id = Guid.NewGuid();
            Created = DateTime.UtcNow;
            Score = new Scoreboard();
            Protocol = new List<GameMessage>();
        }

        public Guid Id { get; set; }
        public DateTime? Created { get; set; }
        public Scoreboard Score { get; set; }
        public IList<GameMessage> Protocol { get; set; }
    }
}
