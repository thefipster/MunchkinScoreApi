using System;

namespace TheFipster.Munchkin.Api.Common.Exceptions
{
    public class DecoratedComponentMissingException : Exception
    {
        public DecoratedComponentMissingException(Type type)
            : base($"Can't find component {type.Name} for decoration.") { }
    }
}
