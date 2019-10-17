using System;

namespace TheFipster.Munchkin.DependencyInjection.Exceptions
{
    public class DecoratedComponentMissingException : Exception
    {
        public DecoratedComponentMissingException(Type type)
            : base($"Can't find component {type.Name} for decoration.") { }
    }
}
