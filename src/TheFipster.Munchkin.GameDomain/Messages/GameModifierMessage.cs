using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public abstract class GameModifierMessage : GameMessage
    {
        public GameModifierMessage() { }

        public GameModifierMessage(Guid gameId, Modifier modifier) : base(gameId)
        {
            Modifier = modifier;
        }

        public Modifier Modifier { get; set; }
    }
}
