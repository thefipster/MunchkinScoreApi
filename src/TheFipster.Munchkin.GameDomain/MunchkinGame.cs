using System;
using System.Collections.Generic;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameDomain
{
    public class MunchkinGame
    {
        public MunchkinGame()
        {
            Id = Guid.NewGuid();
            State = new GameState();
            Protocol = new List<GameMessage>();
        }

        public Guid Id { get; set; }

        public GameState State { get; set; }

        public IEnumerable<GameMessage> Protocol { get; set; }
    }
}
