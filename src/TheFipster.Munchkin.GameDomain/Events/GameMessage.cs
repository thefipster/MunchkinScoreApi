using System;

namespace TheFipster.Munchkin.GameDomain.Events
{
    public abstract class GameMessage
    {
        private string type;

        protected GameMessage()
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public Guid? OriginId { get; set; }
        public int Sequence { get; set; }
        public DateTime Timestamp { get; set; }

        public string Type
        {
            set => type = value;
            get => type ?? GetType().Name;
        }
    }
}
