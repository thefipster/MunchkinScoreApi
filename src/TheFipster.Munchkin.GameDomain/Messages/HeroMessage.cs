using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class HeroMessage : GameModifierMessage
    {
        public HeroMessage() { }

        public HeroMessage(Guid gameId, Hero hero, Modifier modifier) : base(gameId, modifier)
        {
            Hero = hero;
        }

        public Hero Hero { get; set; }
    }
}
