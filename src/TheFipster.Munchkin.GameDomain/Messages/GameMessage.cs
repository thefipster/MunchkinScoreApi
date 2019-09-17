using Newtonsoft.Json;
using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public abstract class GameMessage
    {
        private string type;

        protected GameMessage()
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
        }

        protected GameMessage(Guid gameId) :this()
        {
            GameId = gameId;
        }

        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public DateTime Timestamp { get; set; }

        public string Type
        {
            set => type = value;
            get => type ?? GetType().Name;
        }
    }
}
