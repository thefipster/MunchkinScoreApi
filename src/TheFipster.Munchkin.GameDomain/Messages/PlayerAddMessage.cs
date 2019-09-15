namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class PlayerAddMessage : PlayerMessage
    {
        public PlayerAddMessage() { }

        public PlayerAddMessage(Player player)
            => Player = player;

        public PlayerAddMessage(string name, string gender)
            => Player = new Player(name, gender);

        public override void ApplyTo(GameState state)
            => state.Players.Add(Player);

        public override void Undo(GameState state)
            => state.Players.Remove(Player);
    }
}
