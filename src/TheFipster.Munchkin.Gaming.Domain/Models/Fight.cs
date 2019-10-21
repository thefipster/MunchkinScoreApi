using System;
using System.Collections.Generic;
using System.Linq;
using TheFipster.Munchkin.CardStash.Domain;

namespace TheFipster.Munchkin.Gaming.Domain
{
    public class Fight
    {
        private int totalMonsterScore;
        private int totalHeroesScore;

        public Fight()
        {
            Id = Guid.NewGuid();
            Heroes = new List<Hero>();
            Monsters = new List<Monster>();
        }

        public Fight(Hero Hero, Monster monster)
            : this()
        {
            Heroes.Add(Hero);
            Monsters.Add(monster);
        }

        public Guid Id { get; set; }
        public List<Hero> Heroes { get; set; }
        public List<Monster> Monsters { get; set; }
        public int MonsterBonus { get; set; }
        public int HeroesBonus { get; set; }

        public int TotalMonsterScore
        {
            set => totalMonsterScore = value;
            get
            {
                var basicLevel = Monsters.Sum(monster => monster.Level) ?? 0;
                var totalMonsterScore = basicLevel + MonsterBonus;
                if (this.totalMonsterScore != totalMonsterScore)
                    this.totalMonsterScore = totalMonsterScore;

                return this.totalMonsterScore;
            }
        }

        public int TotalHeroesScore
        {
            set => totalHeroesScore = value;
            get
            {
                var basicLevel = Heroes.Sum(hero => hero.Level);
                var basicBonus = Heroes.Sum(hero => hero.Bonus);
                var totalHeroesScore = basicLevel + basicBonus + HeroesBonus;
                if (this.totalHeroesScore != totalHeroesScore)
                    this.totalHeroesScore = totalHeroesScore;

                return this.totalHeroesScore;
            }
        }
    }
}
