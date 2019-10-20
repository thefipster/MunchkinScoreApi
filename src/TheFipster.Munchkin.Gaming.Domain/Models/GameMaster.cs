using System;
using System.Collections.Generic;

namespace TheFipster.Munchkin.Gaming.Domain
{
    public class GameMaster : Player
    {
        public GameMaster()
        {
            Id = Guid.NewGuid();
            PlayerPool = new List<Player>();
        }

        public GameMaster(Player player, string externalId) : this()
        {
            Name = player.Name;
            Gender = player.Gender;
            Id = player.Id;
            ExternalId = externalId;
        }

        public GameMaster(string name, string externalId, string email) : this()
        {
            Name = name;
            ExternalId = externalId;
            Email = email;
        }

        public string Email { get; set; }
        public string ExternalId { get; set; }

        public ICollection<Player> PlayerPool { get; set; }
    }
}
