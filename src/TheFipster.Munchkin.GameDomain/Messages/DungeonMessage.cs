namespace TheFipster.Munchkin.GameDomain.Messages
{
    public abstract class DungeonMessage : GameModifierMessage
    {
        public string Dungeon { get; set; }
    }
}
