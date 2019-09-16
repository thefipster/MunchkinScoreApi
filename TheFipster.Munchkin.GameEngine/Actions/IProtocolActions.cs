using TheFipster.Munchkin.GameDomain;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public interface IProtocolActions
    {
        void Do(GameState state);

        void Undo(GameState state);
    }
}
