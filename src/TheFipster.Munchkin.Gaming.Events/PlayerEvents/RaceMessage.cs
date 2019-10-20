using System;
using System.Collections.Generic;
using TheFipster.Munchkin.Gaming.Domain.Events;

namespace TheFipster.Munchkin.Gaming.Events
{
    public class RaceMessage : GameSwitchMessage<string>
    {
        public static RaceMessage CreateAdd(int sequence, Guid playerId, IList<string> add)
        {
            return new RaceMessage
            {
                Sequence = sequence,
                PlayerId = playerId,
                Add = add
            };
        }

        public static RaceMessage CreateRemove(int sequence, Guid playerId, IList<string> remove)
        {
            return new RaceMessage
            {
                Sequence = sequence,
                PlayerId = playerId,
                Remove = remove
            };
        }

        public static RaceMessage Create(int sequence, Guid playerId, IList<string> add, IList<string> remove)
        {
            return new RaceMessage
            {
                Sequence = sequence,
                PlayerId = playerId,
                Add = add,
                Remove = remove
            };
        }

        public Guid PlayerId { get; set; }
    }
}
