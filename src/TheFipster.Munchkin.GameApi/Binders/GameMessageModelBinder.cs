using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using TheFipster.Munchkin.GameAbstractions;
using TheFipster.Munchkin.GameDomain.Events;
using TheFipster.Munchkin.GameDomain.Exceptions;

namespace TheFipster.Munchkin.GameApi.Binders
{
    [ExcludeFromCodeCoverage]
    public class GameMessageModelBinder : IModelBinder
    {
        private readonly IEventInventory _eventInventory;

        public GameMessageModelBinder(IEventInventory eventInventory)
        {
            _eventInventory = eventInventory;
        }

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
                var messages = parseMessages(jArray).ToList();
                bindingContext.Result = ModelBindingResult.Success(messages);
            }
            catch (InvalidGameMessageException)
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }

            return bindingContext;
        }

        private IEnumerable<GameMessage> parseMessages(JArray jArray)
        {
            foreach (var jToken in jArray)
                yield return parseMessage(jToken);
        }

        private GameMessage parseMessage(JToken jToken)
        {
            var type = extractType(jToken);
            if (type == null)
                throw new InvalidGameMessageException();

            return (GameMessage)JsonConvert.DeserializeObject(jToken.ToString(), type);

        }

        private Type extractType(JToken jToken)
        {
            var types = _eventInventory.GetMessageTypes();
            var msgType = jToken["type"].Value<string>();
            return types.FirstOrDefault(t => t.Name == msgType);
        }
    }
}
