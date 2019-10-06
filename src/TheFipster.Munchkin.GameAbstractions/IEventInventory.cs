using System;
using System.Collections.Generic;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Events;

namespace TheFipster.Munchkin.GameAbstractions
{
    public interface IEventInventory
    {
        IEnumerable<Type> GetMessageTypes();
        IEnumerable<Type> GetActionTypes();
        GameAction GetActionFromMessage(GameMessage msg, Game game);
    }
}
