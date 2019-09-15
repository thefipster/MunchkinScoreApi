namespace TheFipster.Munchkin.GameDomain.Messages
{
    public abstract class PlayerMessage : GameMessage
    {
        public Player Player { get; set; }
    }
}
