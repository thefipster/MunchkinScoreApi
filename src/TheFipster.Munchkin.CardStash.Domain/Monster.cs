namespace TheFipster.Munchkin.CardStash.Domain
{
    public class Monster : Card
    {
        public Monster()
        {

        }

        public Monster(string name, int level)
        {
            Name = name;
            Level = level;
        }

        public Monster(string name, int level, int treasures)
        {
            Name = name;
            Level = level;
            Treasures = treasures;
        }

        public int? Level { get; set; }
        public int? Treasures { get; set; }
    }
}
