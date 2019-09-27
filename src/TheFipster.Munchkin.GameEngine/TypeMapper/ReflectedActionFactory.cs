using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Abstractions;
using TheFipster.Munchkin.GameDomain.Messages;
using TheFipster.Munchkin.GameEngine.Actions;
using TheFipster.Munchkin.GameEngine.Exceptions;

namespace TheFipster.Munchkin.GameEngine
{
    public class ReflectedActionFactory : IActionFactory
    {
        private readonly IEnumerable<Type> _actionTypes;

        public ReflectedActionFactory(ITypeInventory actionInventory)
        {
            _actionTypes = actionInventory.Get();
        }

        public GameAction CreateActionFrom(GameMessage message, Game game)
        {
            var msgType = message.GetType().Name;
            var expectedActionType = msgType.Replace("Message", "Action");

            var matchedActionTypes = _actionTypes.FirstOrDefault(t => t.Name == expectedActionType);
            if (matchedActionTypes == null)
                throw new MissingActionException($"There was to action for message of type '{msgType}'. Tried '{expectedActionType}' but no luck.");

            return (GameAction)Activator.CreateInstance(matchedActionTypes, message, game);
        }
    }
}
