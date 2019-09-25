using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TheFipster.Munchkin.GameDomain;
using TheFipster.Munchkin.GameDomain.Messages;
using System.Collections.Generic;
using TheFipster.Munchkin.GameDomain.Exceptions;

namespace TheFipster.Munchkin.Api.Binders
{
    [ExcludeFromCodeCoverage]
    public class GameMessageModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var jArray = readJsonFromBody(bindingContext);
            tryParseMessagesToContext(bindingContext, jArray);
            return Task.CompletedTask;
        }

        private JArray readJsonFromBody(ModelBindingContext bindingContext)
        {
            var body = bindingContext.ActionContext.HttpContext.Request.Body;
            using (var stream = new StreamReader(body))
            {
                var json = stream.ReadToEnd();
                return JArray.Parse(json);
            }
        }

        private ModelBindingContext tryParseMessagesToContext(ModelBindingContext bindingContext, JArray jArray)
        {
            try
            {
                var messages = parseMessages(bindingContext, jArray).ToList();
                bindingContext.Result = ModelBindingResult.Success(messages);
            }
            catch (InvalidGameMessageException)
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }

            return bindingContext;
        }

        private static IEnumerable<GameMessage> parseMessages(ModelBindingContext bindingContext, JArray jArray)
        {
            foreach (var jToken in jArray)
                yield return parseMessage(jToken);
        }

        private static GameMessage parseMessage(JToken jToken)
        {
            var type = extractType(jToken);
            if (type == null)
                throw new InvalidGameMessageException();

            return (GameMessage)JsonConvert.DeserializeObject(jToken.ToString(), type);

        }

        private static System.Type extractType(JToken jToken)
        {
            var types = new MessageInventory().Get();
            var msgType = jToken["type"].Value<string>();
            return types.FirstOrDefault(t => t.Name == msgType);
        }
    }
}
