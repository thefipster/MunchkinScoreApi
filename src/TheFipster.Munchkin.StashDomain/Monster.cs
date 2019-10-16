namespace TheFipster.Munchkin.StashDomain
{
    public class Monster
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

        public string Name { get; set; }
        public int Level { get; set; }
        public int Treasures { get; set; }
    }
}
