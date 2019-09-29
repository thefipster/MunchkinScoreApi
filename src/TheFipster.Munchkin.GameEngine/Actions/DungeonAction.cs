using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class DungeonAction : GameSwitchAction<string>
    {
        public DungeonAction(DungeonMessage message, Game game)
            : base(message, game) { }

        public new DungeonMessage Message => (DungeonMessage)base.Message;

        public override void Validate()
        {
            base.Validate();

            if (dungeonExists())
                throw new InvalidActionException("Dungeon is already active.");

            if (dungeonExistsNot())
                throw new InvalidActionException("Dungeon is not part of the game.");
        }

        public override Game Do()
        {
            base.Do();

            if (IsAddMessage)
                addDungeon();

            if (IsRemoveMessage)
                removeDungeon();

            return Game;
        }

        private bool dungeonExists() =>
            Message.Add.Any(x => Game.Score.Dungeons.Contains(x));

        private bool dungeonExistsNot() =>
            Message.Remove.Any(x => !Game.Score.Dungeons.Contains(x));

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
