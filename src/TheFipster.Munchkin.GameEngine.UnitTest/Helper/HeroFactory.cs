using TheFipster.Munchkin.GameDomain;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Helper
{
    public class HeroFactory
    {
        public static Hero Create(string name, string gender)
        {
            var player = new Player(name, gender);
            return new Hero(player);
        }

        public static Hero CreateMaleHero(string name) => Create(name, "male");
        public static Hero CreateFemaleHero(string name) => Create(name, "female");
    }
}
