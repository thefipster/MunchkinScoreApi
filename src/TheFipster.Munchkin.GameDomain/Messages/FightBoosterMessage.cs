using System;
using TheFipster.Munchkin.GameDomain.Enums;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class FightBoosterMessage : GameMessage
    {
        public Guid? PlayerId { get; set; }
        public FightParty Party { get; set; }
        public int Bonus { get; set; }
    }
}
