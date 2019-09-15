using System.Collections.Generic;

namespace TheFipster.Munchkin.GameDomain
{
    public class LevelChangeReasons
    {
        public static IEnumerable<string> Items => new[] { "Gold", "Kampf", "Erleuchtung" };
    }
}
