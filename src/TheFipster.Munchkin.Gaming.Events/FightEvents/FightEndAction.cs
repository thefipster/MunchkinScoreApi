using TheFipster.Munchkin.Gaming.Domain;
using TheFipster.Munchkin.Gaming.Domain.Events;
using TheFipster.Munchkin.Gaming.Domain.Exceptions;

namespace TheFipster.Munchkin.Gaming.Events
{
    public class FightEndAction : GameAction
    {
        public FightEndAction(GameMessage message, Game game)
            : base(message, game) { }

        public new FightEndMessage Message => (FightEndMessage)base.Message;

        public override Game Do()
        {
            Game.Score.Fight = null;
            return base.Do();
        }

        public override void Validate()
        {
            base.Validate();

            if (Game.Score.Fight == null)
                throw new InvalidActionException("What a pacifist, want's to end a fight thats not even there.");
        }
    }
}
