using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TheFipster.Munchkin.GameDomain.Abstractions;
using TheFipster.Munchkin.GameEngine.Actions;

namespace TheFipster.Munchkin.GameEngine
{
    public class ActionInventory : ITypeInventory
    {
        public IEnumerable<Type> Get()
        {
            var types = Assembly
                .GetAssembly(typeof(GameAction))
                .GetTypes()
                .Where(
                    myType => myType.IsClass
                    && !myType.IsAbstract
                    && typeof(GameAction).IsAssignableFrom(myType));

            return types;
        }
    }
}
