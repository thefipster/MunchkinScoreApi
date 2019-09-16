namespace TheFipster.Munchkin.GameDomain.Messages
{
    public abstract class HeroMessage : GameModifierMessage
    {
        public Hero Hero { get; set; }
    }
}
