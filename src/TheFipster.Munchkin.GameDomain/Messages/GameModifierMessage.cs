using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public abstract class GameModifierMessage : GameMessage
    {
        public GameModifierMessage() { }

        public GameModifierMessage(Modifier modifier)
        {
            Modifier = modifier;
        }

        public Modifier Modifier { get; set; }
    }
}
