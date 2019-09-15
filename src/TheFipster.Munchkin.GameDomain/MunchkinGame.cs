using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Munchkin.GameDomain.Exceptions;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameDomain
{
    public class MunchkinGame
    {
        private ICollection<GameMessage> protocol;

        private MunchkinGame(Guid id, DateTime createdAt, ICollection<GameMessage> protocol, GameState state)
        {
            Id = id;
            CreatedAt = createdAt;
            State = new GameState();
            this.protocol = protocol;
        }

        public MunchkinGame()
            : this(Guid.NewGuid(), DateTime.UtcNow, new List<GameMessage>(), new GameState()) { }

        public MunchkinGame(Guid id, DateTime createdAt, ICollection<GameMessage> protocol)
            : this(id, createdAt, protocol, new GameState())
        {
            foreach (var message in this.protocol)
                message.ApplyTo(State);
        }

        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public GameState State { get; set; }

        public IEnumerable<GameMessage> Protocol => protocol.ToList();

        public void AddMessage(GameMessage message)
        {
            protocol.Add(message);
            message.ApplyTo(State);
        }

        public void Undo()
        {
            if (!protocol.Any())
                throw new ProtocolEmptyException("Can't undo because the action protocol is empty.");

            var lastMessage = protocol.Last();
            lastMessage.Undo(State);
            protocol.Remove(lastMessage);
        }
    }
}
