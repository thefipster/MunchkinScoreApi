using TheFipster.Munchkin.CardStash.Repository.Implementations;
using TheFipster.Munchkin.CardStash.Repository.Models;
using TheFipster.Munchkin.CardStash.Repository.Persistance;
using Xunit;

namespace TheFipster.Munchkin.CardStash.Repository.Unittest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var writer = new Writer<Monster>();
            var writerAuditer = new WriteAuditing<Monster>(writer);

            var monster = new Monster("Zerschmetterling", 7);
            writerAuditer.Save(monster);
        }
    }
}
