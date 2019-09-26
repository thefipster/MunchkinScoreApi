using System;
using System.Collections.Generic;
using System.Text;

namespace TheFipster.Munchkin.GameDomain
{
    public class CardCollection
    {
        public const string Dungeons = "dungeons";
        public const string Races = "races";
        public const string Classes = "classes";
        public const string Monsters = "monsters";
        public const string Curses = "curses";
        public const string Genders = "genders";

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
