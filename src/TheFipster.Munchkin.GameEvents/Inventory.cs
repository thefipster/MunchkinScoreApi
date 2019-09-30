using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TheFipster.Munchkin.GameAbstractions;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Events;
using TheFipster.Munchkin.GameDomain.Exceptions;

namespace TheFipster.Munchkin.GameEvents
{
    public class Inventory : IEventInventory
    {
        private readonly IEnumerable<Type> _messageTypes;
        private readonly IEnumerable<Type> _actionTypes;

        public Inventory()
        {
            _messageTypes = GetMessageTypes();
            _actionTypes = GetActionTypes();
        }

        public GameAction GetActionFromMessage(GameMessage msg, Game game)
        {
            checkIfMsgTypeExists(msg);
            var actionType = getActionFromMessage(msg);
            return (GameAction)Activator.CreateInstance(actionType, msg, game);
        }

        public IEnumerable<Type> GetActionTypes() => 
            getAllTypesAssignableTo(typeof(GameAction));

        public IEnumerable<Type> GetMessageTypes() => 
            getAllTypesAssignableTo(typeof(GameMessage));

        private Type getActionFromMessage(GameMessage msg)
        {
            var expectedActionName = msg.GetType().Name.Replace("Message", "Action");
            var actionType = _actionTypes.FirstOrDefault(t => t.Name == expectedActionName);

            if (actionType == null)
                throw new MissingActionException($"Action for Message {msg.GetType().Name} was not found. I tried '{expectedActionName}' with no luck.");
            
            return actionType;
        }

        private void checkIfMsgTypeExists(GameMessage msg)
        {
            if (_messageTypes.All(t => t.Name != msg.GetType().Name))
                throw new MissingMessageException($"Message '{msg.GetType().Name}' was not found.");
        }

        private IEnumerable<Type> getAllTypesAssignableTo(Type thisType)
        {
            var types = Assembly
                .GetAssembly(GetType())
                .GetTypes()
                .Where(type => type.IsClass
                    && !type.IsAbstract
                    && thisType.IsAssignableFrom(type));

            return types;
        }
    }
}
