using System;
using System.Collections.Generic;

namespace TheFipster.Munchkin.GameDomain.Abstractions
{
    public interface ITypeInventory
    {
        IEnumerable<Type> Get();
    }
}
