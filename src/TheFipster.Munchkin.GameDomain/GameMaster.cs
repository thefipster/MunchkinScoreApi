using System;
using System.Collections.Generic;

namespace TheFipster.Munchkin.GameDomain
{
    public class GameMaster : Player
    {
        public GameMaster()
        {
            Id = Guid.NewGuid();
            PlayerPool = new List<Player>();
        }

        public GameMaster(Player player, string email) : this()
        {
            Id = player.Id;
            Name = player.Name;
            Gender = player.Gender;
            Email = email;
        }

        public string Email { get; set; }
        public ICollection<Player> PlayerPool { get; set; }
    }
}
