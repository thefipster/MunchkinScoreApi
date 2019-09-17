using Microsoft.AspNetCore.Mvc.ModelBinding;
using TheFipster.Munchkin.GameDomain.Messages;

namespace TheFipster.Munchkin.Api.Binders
{
    public class GameMessageModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(GameMessage))
                return new GameMessageModelBinder();

            return null;
        }
    }
}
