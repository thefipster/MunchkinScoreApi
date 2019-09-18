using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class DungeonMessage : GameModifierMessage
    {
        public DungeonMessage() { }

        public DungeonMessage(Guid gameId, string dungeon, Modifier modifier) : base(gameId, modifier)
        {
            Dungeon = dungeon;
        }

        public string Dungeon { get; set; }
    }
}
