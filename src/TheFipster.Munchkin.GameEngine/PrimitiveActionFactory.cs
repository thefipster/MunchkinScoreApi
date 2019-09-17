using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameEngine.Actions;

namespace TheFipster.Munchkin.GameEngine
{
    public class PrimitiveActionFactory : IActionFactory
    {
        public IGameAction CreateActionFrom(GameMessage msg, Scoreboard board)
        {
            switch (msg)
            {
                case HeroMessage heroMsg:
                    return new HeroAction(heroMsg, board);
                case DungeonMessage dungeonMsg:
                    return new DungeonAction(dungeonMsg, board);
                case LevelMessage lvlMsg:
                    return new LevelAction(lvlMsg, board);

                default:
                    throw new InvalidGameMessageException();
            }
        }
    }
}
