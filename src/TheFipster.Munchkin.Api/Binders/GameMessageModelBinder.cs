using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TheFipster.Munchkin.Api.Binders
{
    public class GameMessageModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var body = bindingContext.ActionContext.HttpContext.Request.Body;
            var json = string.Empty;
            using (var stream = new StreamReader(body))
            {
                json = stream.ReadToEnd();
            }

            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }
    }
}
