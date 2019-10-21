using System;

namespace TheFipster.Munchkin.Gaming.Domain.Exceptions
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
