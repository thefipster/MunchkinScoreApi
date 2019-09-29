using System;

namespace TheFipster.Munchkin.GameDomain.Exceptions
{
    public class UnknownMonsterException : Exception
    {
        public UnknownMonsterException(string message) : base(message) { }
    }
}
