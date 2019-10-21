using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TheFipster.Munchkin.Gaming.Abstractions;
using TheFipster.Munchkin.Gaming.Domain.Events;

namespace TheFipster.Munchkin.Gaming.Api.Binders
{
    [ExcludeFromCodeCoverage]
    public class GameMessageModelBinderProvider : IModelBinderProvider
    {
        private IEventInventory _eventInventory;

        public GameMessageModelBinderProvider(IEventInventory eventInventory)
        {
            _eventInventory = eventInventory;
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(List<GameMessage>))
                return new GameMessageModelBinder(_eventInventory);

            return null;
        }
    }
}
