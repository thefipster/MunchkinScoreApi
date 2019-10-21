using System;
using TheFipster.Munchkin.Gaming.Domain.Enums;
using TheFipster.Munchkin.Gaming.Domain.Events;

namespace TheFipster.Munchkin.Gaming.Events
{
    public class FightBoosterMessage : GameMessage
    {
        public Guid? PlayerId { get; set; }
        public FightParty Party { get; set; }
        public int Bonus { get; set; }
    }
}
