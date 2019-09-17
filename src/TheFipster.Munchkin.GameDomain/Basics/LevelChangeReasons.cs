using System.Collections.Generic;

namespace TheFipster.Munchkin.GameDomain.Basics
{
    public class LevelChangeReasons
    {
        public static IEnumerable<string> Items => new[] { "Gold", "Kampf", "Erleuchtung" };
    }
}
