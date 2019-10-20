using TheFipster.Munchkin.Gaming.Domain.Events;

namespace TheFipster.Munchkin.Gaming.Events
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
