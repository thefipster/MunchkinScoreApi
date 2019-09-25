using TheFipster.Munchkin.GameDomain;

namespace TheFipster.Munchkin.GameEngine.UnitTest.Helper
{
    public class PlayerFactory
    {
        public static Player CreateMale(string name) => new Player(name, "male");
        public static Player CreateFemale(string name) => new Player(name, "female");
    }
}
