using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using TheFipster.Munchkin.Gaming.Abstractions;
using TheFipster.Munchkin.Gaming.Domain.Events;
using TheFipster.Munchkin.Gaming.Domain.Exceptions;

namespace TheFipster.Munchkin.Gaming.Api.Binders
{
    [ExcludeFromCodeCoverage]
    public class GameMessageModelBinder : IModelBinder
    {
        private readonly IEventInventory _eventInventory;

        public GameMessageModelBinder(IEventInventory eventInventory)
        {
            _eventInventory = eventInventory;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var jArray = await readJsonFromBodyAsync(bindingContext);
            tryParseMessagesToContext(bindingContext, jArray);
        }

        private async Task<JArray> readJsonFromBodyAsync(ModelBindingContext bindingContext)
        {
            var body = bindingContext.ActionContext.HttpContext.Request.Body;
            using (var stream = new StreamReader(body))
            {
                var json = await stream.ReadToEndAsync();
                return JArray.Parse(json);
            }
        }

        private void tryParseMessagesToContext(ModelBindingContext bindingContext, JArray jArray)
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
