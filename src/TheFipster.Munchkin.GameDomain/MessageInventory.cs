using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameDomain
{
    public class MessageInventory
    {
        public IEnumerable<Type> Get(params object[] constructorArgs)
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
