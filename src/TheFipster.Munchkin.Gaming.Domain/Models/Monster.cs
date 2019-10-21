namespace TheFipster.Munchkin.Gaming.Domain
{
    public class Monster
    {
        public const string Collection = "monsters";

        public Monster() { }

        public Monster(string name)
        {
            Name = name;
        }

        public Monster(string name, int level) : this(name)
        {
            Level = level;
        }

        public string Name { get; set; }
        public int? Level { get; set; }
        public int? Treasures { get; set; }
        public int? LevelGain { get; set; }
    }
}
