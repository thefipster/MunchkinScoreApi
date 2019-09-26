using TheFipster.Munchkin.GameDomain;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public interface IGameAction
    {
        Game Do();

        void Validate();
    }
}
