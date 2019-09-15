using System;
using System.Collections.Generic;

namespace TheFipster.Munchkin.GameEngine.Model
{
    public class GameState
    {
        public GameState()
        {
            Dungeons = new List<string>();
            Players = new List<Player>();
        }

        public DateTime? Begin { get; set; }
        public DateTime? End { get; set; }
        public ICollection<string> Dungeons { get; set; }
        public ICollection<Player> Players { get; set; }
    }
}
