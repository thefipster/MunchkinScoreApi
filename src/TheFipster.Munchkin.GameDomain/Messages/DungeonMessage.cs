using System.Collections.Generic;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class DungeonMessage : GameSwitchMessage<string>
    {
        public static DungeonMessage CreateAdd(int sequence, IList<string> add)
        {
            return new DungeonMessage
            {
                Sequence = sequence,
                Add = add
            };
        }

        public static DungeonMessage CreateRemove(int sequence, IList<string> remove)
        {
            return new DungeonMessage
            {
                Sequence = sequence,
                Remove = remove
            };
        }

        public static DungeonMessage Create(int sequence, IList<string> add, IList<string> remove)
        {
            return new DungeonMessage
            {
                Sequence = sequence,
                Add = add,
                Remove = remove
            };
        }
    }
}
