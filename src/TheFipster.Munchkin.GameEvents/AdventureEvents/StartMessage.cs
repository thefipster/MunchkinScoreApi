using TheFipster.Munchkin.GameDomain.Events;

namespace TheFipster.Munchkin.GameEvents
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
