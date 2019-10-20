using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Munchkin.Gaming.Domain.Events;

namespace TheFipster.Munchkin.Gaming.Domain
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

        public Hero GetHero(Guid playerId) => Score.Heroes.First(hero => hero.Player.Id == playerId);
    }
}
