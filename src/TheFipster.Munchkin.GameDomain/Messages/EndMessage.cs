namespace TheFipster.Munchkin.GameDomain.Messages
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
