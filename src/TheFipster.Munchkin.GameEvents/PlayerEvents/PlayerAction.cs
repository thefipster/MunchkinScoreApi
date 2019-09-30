using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Events;
using TheFipster.Munchkin.GameDomain.Exceptions;

namespace TheFipster.Munchkin.GameEvents
{
    public class PlayerAction : GameSwitchAction<Player>
    {
        public PlayerAction(PlayerMessage message, Game game)
            : base(message, game) { }

        public new PlayerMessage Message => (PlayerMessage)base.Message;

        public override void Validate()
        {
            base.Validate();

            if (heroExists())
                throw new InvalidActionException("Hero is already in the dungeon.");

            if (heroNotExists())
                throw new InvalidActionException("Hero isn't even in the dungeon.");
        }

        public override Game Do()
        {
            base.Do();

            if (IsAddMessage)
                addPlayer();

            if (IsRemoveMessage)
                removePlayer();

            return Game;
        }

        private bool heroExists() =>
            Message.Add.Select(x => x.Id).Any(x => Game.Score.Heroes.Select(y => y.Player.Id).Contains(x));

        private bool heroNotExists() =>
            Message.Remove.Select(x => x.Id).Any(x => !Game.Score.Heroes.Select(y => y.Player.Id).Contains(x));

        private void addPlayer()
        {
            foreach (var player in Message.Add)
                if (!Game.Score.Heroes.Any(hero => hero.Player.Id == player.Id))
                    Game.Score.Heroes.Add(new Hero(player));
        }

        private void removePlayer()
        {
            foreach (var player in Message.Remove)
                if (Game.Score.Heroes.Any(hero => hero.Player.Id == player.Id))
                    Game.Score.Heroes = Game.Score.Heroes.Where(x => x.Player.Id != player.Id).ToList();
        }
    }
}
