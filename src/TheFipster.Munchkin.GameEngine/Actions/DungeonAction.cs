using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class DungeonAction : MessageSwitchAction
    {
        public DungeonAction(DungeonMessage message, Game game)
            : base(message, game) { }

        public new DungeonMessage Message => (DungeonMessage)base.Message;

        public override Game Do()
        {
            base.Do();
            if (IsAddMessage)
                addDungeon();

            if (IsRemoveMessage)
                removeDungeon();

            return Game;
        }

        private void addDungeon()
        {
            foreach (var dungeon in Message.Add)
                if (!Game.Score.Dungeons.Contains(dungeon))
                    Game.Score.Dungeons.Add(dungeon);
        }

        private void removeDungeon()
        {
            foreach (var dungeon in Message.Remove)
                if (Game.Score.Dungeons.Contains(dungeon))
                    Game.Score.Dungeons.Remove(dungeon);
        }
    }
}
