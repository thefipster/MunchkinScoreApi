using System.Collections.Generic;
using TheFipster.Munchkin.Gaming.Domain;
using TheFipster.Munchkin.Gaming.Domain.Events;

namespace TheFipster.Munchkin.Gaming.Events
{
    public class PlayerMessage : GameSwitchMessage<Player>
    {
        public static PlayerMessage CreateAdd(int sequence, IList<Player> add)
        {
            return new PlayerMessage
            {
                Sequence = sequence,
                Add = add
            };
        }

        public static PlayerMessage CreateRemove(int sequence, IList<Player> remove)
        {
            return new PlayerMessage
            {
                Sequence = sequence,
                Remove = remove
            };
        }

        public static PlayerMessage Create(int sequence, IList<Player> add, IList<Player> remove)
        {
            return new PlayerMessage
            {
                Sequence = sequence,
                Add = add,
                Remove = remove
            };
        }
    }
}
