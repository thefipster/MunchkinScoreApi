using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameEngine.Actions
{
    public class PlayerAction : MessageSwitchAction
    {
        public PlayerAction(PlayerMessage message, Game game)
            : base(message, game) { }

        public new PlayerMessage Message => (PlayerMessage)base.Message;

        public override Game Do()
        {
            base.Do();
            if (IsAddMessage)
                addPlayer();

            if (IsRemoveMessage)
                removePlayer();

            return Game;
        }

        private void addPlayer()
        {
            foreach (var player in Message.Add)
                if (Game.Score.Heroes.Any(hero => hero.Player.Id == player.Id))
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
