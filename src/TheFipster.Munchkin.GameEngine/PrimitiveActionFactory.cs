using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameEngine.Actions;

namespace TheFipster.Munchkin.GameEngine
{
    public class PrimitiveActionFactory : IActionFactory
    {
        public IGameAction CreateActionFrom(GameMessage msg, Game game)
        {
            switch (msg)
            {
                case HeroMessage heroMsg:
                    return new HeroAction(heroMsg, game);
                case DungeonMessage dungeonMsg:
                    return new DungeonAction(dungeonMsg, game);
                case LevelMessage lvlMsg:
                    return new LevelAction(lvlMsg, game);
                case StartMessage startMsg:
                    return new StartAction(startMsg, game);
                case EndMessage endMsg:
                    return new EndAction(endMsg, game);

                default:
                    throw new InvalidGameMessageException();
            }
        }
    }
}
