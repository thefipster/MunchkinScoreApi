using System;
using System.Collections.Generic;
using System.Text;

namespace TheFipster.Munchkin.GameDomain
{
    public class CardCollection
    {
        public CardCollection()
        {
            Cards = new List<string>();
        }

        public CardCollection(string collectionName)
            : this(collectionName, new List<string>()) { }

        public CardCollection(string collectionName, List<string> cards)
        {
            Id = collectionName;
            Cards = cards;
        }

        public string Id { get; set; }
        public List<string> Cards { get; set; }
    }
}
