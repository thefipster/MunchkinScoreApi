namespace TheFipster.Munchkin.GameDomain.Messages
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
