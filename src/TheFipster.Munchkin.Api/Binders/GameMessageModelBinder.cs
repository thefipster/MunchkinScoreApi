using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TheFipster.Munchkin.GameDomain;

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

            var jToken = JToken.Parse(json);
            var types = new MessageInventory().Get();
            var msgType = jToken["type"].Value<string>();

            var type = types.FirstOrDefault(x => x.Name == msgType);

            if (type != null)
            {
                var msg = JsonConvert.DeserializeObject(json, type);
                bindingContext.Result = ModelBindingResult.Success(msg);
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }
           
            return Task.CompletedTask;
        }
    }
}
