namespace TheFipster.Munchkin.GameDomain.Messages
{
    public abstract class GameModifierMessage : GameMessage
    {
        public Modifier Modifier { get; set; }
    }
}
