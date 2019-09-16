using System;
using System.Collections.Generic;

namespace TheFipster.Munchkin.GameDomain
{
    public class GameMaster : IPlayer
    {
        public GameMaster()
        {
            Id = Guid.NewGuid();
            PlayerPool = new List<Player>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public ICollection<Player> PlayerPool { get; set; }
    }
}
