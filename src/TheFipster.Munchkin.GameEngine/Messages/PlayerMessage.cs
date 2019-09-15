using TheFipster.Munchkin.GameEngine.Model;

namespace TheFipster.Munchkin.GameEngine.Messages
{
    public abstract class PlayerMessage : GameMessage
    {
        public Player Player { get; set; }
    }
}
