using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class GenderMessage : GameMessage
    {
        public static GenderMessage Create(int sequence, Guid playerId, string gender)
        {
            return new GenderMessage
            {
                Sequence = sequence,
                PlayerId = playerId,
                Gender = gender
            };
        }

        public Guid PlayerId { get; set; }
        public string Gender { get; set; }
    }
}
