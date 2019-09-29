using System.Collections;
using System.Collections.Generic;

namespace TheFipster.Munchkin.GameDomain.Messages
{
    public abstract class GameSwitchMessage<T> : GameMessage
    {
        protected GameSwitchMessage()
        {
            Add = new List<T>();
            Remove = new List<T>();
        }

        public IList<T> Add { get; set; }
        public IList<T> Remove { get; set; }
    }
}