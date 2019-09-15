namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class DungeonAddMessage : DungeonMessage
    {
        public DungeonAddMessage() { }

        public DungeonAddMessage(string dungeon)
            => Dungeon = dungeon;

        public override void ApplyTo(GameState state)
            => state.Dungeons.Add(Dungeon);

        public override void Undo(GameState state)
            => state.Dungeons.Remove(Dungeon);
    }
}
