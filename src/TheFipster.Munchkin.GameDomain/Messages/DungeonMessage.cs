namespace TheFipster.Munchkin.GameDomain.Messages
{
    public abstract class DungeonMessage : GameMessage
    {
        public string Dungeon { get; set; }
    }
}
