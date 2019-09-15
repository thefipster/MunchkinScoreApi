using TheFipster.Munchkin.GameEngine.Model;

namespace TheFipster.Munchkin.GameEngine.Messages
{
    public class DungeonRemoveMessage : DungeonMessage
    {
        public DungeonRemoveMessage() { }

        public DungeonRemoveMessage(string dungeon)
            => Dungeon = dungeon;

        public override void ApplyTo(GameState state)
            => state.Dungeons.Remove(Dungeon);

        public override void Undo(GameState state)
            => state.Dungeons.Add(Dungeon);
    }
}
