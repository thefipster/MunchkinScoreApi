using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TheFipster.Munchkin.GameDomain.Abstractions;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameDomain
{
    public class MessageInventory : ITypeInventory
    {
        public IEnumerable<Type> Get()
        {
            var types = Assembly
                .GetAssembly(typeof(GameMessage))
                .GetTypes()
                .Where(
                    myType => myType.IsClass
                    && !myType.IsAbstract
                    && typeof(GameMessage).IsAssignableFrom(myType));

            return types;
        }
    }
}
