using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using TheFipster.Munchkin.CardStash.Repository.Abstractions;

namespace TheFipster.Munchkin.CardStash.Repository.Decorators
{
    public class ReadLogger<Card> : IRead<Card>
    {
        private const string EntryMessage = "Calling {MethodName} for entity {CardType}";
        private const string FindOneMessage = EntryMessage + " with '{CardName}'";
        private const string ExitMessage = "{MethodName} for entity {CardType} found {CardResult}";

        private readonly IRead<Card> component;
        private readonly ILogger<ReadLogger<Card>> log;

        public ReadLogger(IRead<Card> reader, ILogger<ReadLogger<Card>> logger)
        {
            component = reader;
            log = logger;
        }

        public IEnumerable<Card> Find(Expression<Func<Card, bool>> filter)
        {
            log.LogTrace(EntryMessage, nameof(Find), typeof(Card).Name);
            var cards = component.Find(filter);
            log.LogTrace(ExitMessage, nameof(Find), typeof(Card).Name, string.Join(", ", cards));
            return cards;
        }

        public IEnumerable<Card> FindAll()
        {
            log.LogTrace(EntryMessage, nameof(FindAll), typeof(Card).Name);
            var cards = component.FindAll();
            log.LogTrace(ExitMessage, nameof(FindAll), typeof(Card).Name, string.Join(", ", cards));
            return cards;
        }

        public Card FindOne(string identifier)
        {
            log.LogTrace(FindOneMessage, nameof(FindOne), typeof(Card).Name, identifier);
            var card = component.FindOne(identifier);
            log.LogTrace(ExitMessage, nameof(FindOne), typeof(Card).Name, card);
            return card;
        }
    }
}
