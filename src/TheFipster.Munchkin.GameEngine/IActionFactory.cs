using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameEngine.Actions;

namespace TheFipster.Munchkin.GameEngine
{
    public interface IActionFactory
    {
        IGameAction CreateActionFrom(GameMessage message, Scoreboard board);
    }
}
