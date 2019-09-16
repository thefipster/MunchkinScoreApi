namespace TheFipster.Munchkin.GameDomain.Messages
{
    public abstract class PlayerMessage : GameModifierMessage
    {
        public Player Player { get; set; }
    }
}
