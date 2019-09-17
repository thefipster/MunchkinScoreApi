using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class DungeonAction : MessageAction, IGameAction
    {
        public DungeonAction(DungeonMessage message, Scoreboard board)
            : base(message, board) { }

        public new DungeonMessage Message => (DungeonMessage)base.Message;

        public Scoreboard Do()
        {
            switch (Message.Modifier)
            {
                case Modifier.Add:
                    return addDungeon();
                case Modifier.Remove:
                    return removeDungeon();
                default:
                    throw new InvalidModifierException();
            }
        }

        public Scoreboard Undo()
        {
            switch(Message.Modifier)
            {
                case Modifier.Add:
                    return removeDungeon();
                case Modifier.Remove:
                    return addDungeon();
                default:
                    throw new InvalidModifierException();
            }
        }

        public void Validate()
        {
            if (Message.Modifier == Modifier.Add && dungeonExists())
                throw new InvalidActionException("You are already in this dungeon.");

            if (Message.Modifier == Modifier.Remove && !dungeonExists())
                throw new InvalidActionException("Can't leave a dungeon that you have not entered.");
        }

        private bool dungeonExists() =>
            Board.Dungeons.Contains(Message.Dungeon);

        private Scoreboard addDungeon()
        {
            Board.Dungeons.Add(Message.Dungeon);
            return Board;
        }

        private Scoreboard removeDungeon()
        {
            Board.Dungeons.Remove(Message.Dungeon);
            return Board;
        }
    }
}
