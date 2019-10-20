using TheFipster.Munchkin.Gaming.Domain.Events;

namespace TheFipster.Munchkin.Gaming.Events
{
    public class StartMessage : GameMessage
    {
        public static StartMessage Create(int sequence)
        {
            return new StartMessage
            {
                Sequence = sequence
            };
        }
    }
}
