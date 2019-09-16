using TheFipster.Munchkin.GameDomain;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public interface IGameAction
    {
        Scoreboard Do();

        Scoreboard Undo();

        void Validate();
    }
}
