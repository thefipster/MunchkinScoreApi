using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class PlayerMessage : GameModifierMessage
    {
        public PlayerMessage() { }

        public PlayerMessage(Player player, Modifier modifier) : base(modifier)
        {
            Player = player;
        }

        public Player Player { get; set; }
    }
}
