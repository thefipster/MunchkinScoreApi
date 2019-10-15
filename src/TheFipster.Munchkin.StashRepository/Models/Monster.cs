namespace TheFipster.Munchkin.StashRepository.Models
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

        public string Name { get; }
        public int Level { get; }
        public int Treasures { get; }
    }
}
