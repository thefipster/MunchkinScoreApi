using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class FightStartMessage : GameMessage
    {
        public static FightStartMessage Create(int sequence, Hero hero, Monster monster)
        {
            return new FightStartMessage
            {
                Sequence = sequence,
                Hero = hero,
                Monster = monster,
            };
        }

        public Hero Hero { get; set; }
        public Monster Monster { get; set; }
        public string Reason { get; set; }
    }
}
