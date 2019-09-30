using TheFipster.Munchkin.GameDomain.Events;

namespace TheFipster.Munchkin.GameEvents
{
    public class FightEndMessage : GameMessage
    {
        public static FightEndMessage Create(int sequence, string result)
        {
            return new FightEndMessage
            {
                Sequence = sequence,
                Result = result
            };
        }

        public string Result { get; set; }
    }
}
