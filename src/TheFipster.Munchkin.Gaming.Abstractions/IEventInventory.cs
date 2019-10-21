using System;
using System.Collections.Generic;
using TheFipster.Munchkin.Gaming.Domain;
using TheFipster.Munchkin.Gaming.Domain.Events;

namespace TheFipster.Munchkin.Gaming.Abstractions
{
    public interface IEventInventory
    {
        IEnumerable<Type> GetMessageTypes();
        IEnumerable<Type> GetActionTypes();
        GameAction GetActionFromMessage(GameMessage msg, Game game);
    }
}
