using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.GameApi.Binders
{
    [ExcludeFromCodeCoverage]
    public class GameMessageModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(List<GameMessage>))
                return new GameMessageModelBinder();

            return null;
        }
    }
}
