using System;

namespace TheFipster.Munchkin.Gaming.Domain.Exceptions
{
    public class UnknownMonsterException : Exception
    {
        public UnknownMonsterException(string message) : base(message) { }
    }
}
