using System;
using TheFipster.Munchkin.GameDomain.Enums;
using TheFipster.Munchkin.GameDomain.Events;

namespace TheFipster.Munchkin.GameEvents
{
    public class FightBoosterMessage : GameMessage
    {
        public Guid? PlayerId { get; set; }
        public FightParty Party { get; set; }
        public int Bonus { get; set; }
    }
}
