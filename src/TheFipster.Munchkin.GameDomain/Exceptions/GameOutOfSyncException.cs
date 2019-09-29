using System;

namespace TheFipster.Munchkin.GameDomain.Exceptions
{
    public class GameOutOfSyncException : Exception
    {
        public GameOutOfSyncException(int lastReceivedSequence)
        {
            LastReceivedSequence = lastReceivedSequence;
        }

        public int LastReceivedSequence { get; set; }
    }
}
