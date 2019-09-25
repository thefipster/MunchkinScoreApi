using System;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class DungeonMessage : GameModifierMessage
    {
        public DungeonMessage() { }

        public DungeonMessage(string dungeon, Modifier modifier) : base(modifier)
        {
            Dungeon = dungeon;
        }

        public string Dungeon { get; set; }
    }
}
