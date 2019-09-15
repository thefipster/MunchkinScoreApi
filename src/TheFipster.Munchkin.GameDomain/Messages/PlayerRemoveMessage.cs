namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class PlayerRemoveMessage : PlayerMessage
    {
        public PlayerRemoveMessage() { }

        public PlayerRemoveMessage(Player player)
            => Player = player;

        public override void ApplyTo(GameState state)
            => state.Players.Remove(Player);

        public override void Undo(GameState state)
            => state.Players.Add(Player);
    }
}
