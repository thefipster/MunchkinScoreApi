using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class ClassMessage : GameModifierMessage
    {
        public ClassMessage() { }

        public ClassMessage(Guid playerId, string className, Modifier modifier)
            : base(modifier)
        {
            PlayerId = playerId;
            Class = className;
        }

        public Guid PlayerId { get; set; }
        public string Class { get; set; }
    }
}
