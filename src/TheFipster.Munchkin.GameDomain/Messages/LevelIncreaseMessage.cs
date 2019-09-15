using System;
using System.Linq;
using TheFipster.Munchkin.GameDomain.Exceptions;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class LevelIncreaseMessage : LevelMessage
    {
        public LevelIncreaseMessage() { }

        public LevelIncreaseMessage(Guid playerId, int levelChange, string reason = null)
        {
            PlayerId = playerId;
            Delta = levelChange;
            Reason = reason;
        }

        public override void ApplyTo(GameState state)
        {
            if (!state.Players.Any(player => player.Id == PlayerId))
                throw new UnknownPlayerException(PlayerId);

            state.Players.First(p => p.Id == PlayerId).Level += Delta;
        }

        public override void Undo(GameState state)
        {
            if (!state.Players.Any(player => player.Id == PlayerId))
                throw new UnknownPlayerException(PlayerId);

            state.Players.First(p => p.Id == PlayerId).Level -= Delta;
        }
    }
}
