using System.Collections.Generic;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public class GameSwitchMessage<T> : GameSwitchMessage
    {
        public new List<T> Add { get; set; }
        public new List<T> Remove { get; set; }
    }


    public class GameSwitchMessage : GameMessage
    {
        public List<object> Add { get; set; }
        public List<object> Remove { get; set; }
    }
}
