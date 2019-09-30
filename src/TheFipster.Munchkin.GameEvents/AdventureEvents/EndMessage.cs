using TheFipster.Munchkin.GameDomain.Events;

namespace TheFipster.Munchkin.GameEvents
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
