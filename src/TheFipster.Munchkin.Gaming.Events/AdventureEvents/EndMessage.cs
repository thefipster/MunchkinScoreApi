using TheFipster.Munchkin.Gaming.Domain.Events;

namespace TheFipster.Munchkin.Gaming.Events
{
    public class EndMessage : GameMessage
    {
        public static EndMessage Create(int sequence)
        {
            return new EndMessage
            {
                Sequence = sequence
            };
        }
    }
}
