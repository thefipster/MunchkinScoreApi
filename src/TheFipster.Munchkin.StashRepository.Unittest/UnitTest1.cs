using TheFipster.Munchkin.StashRepository.Implementations;
using TheFipster.Munchkin.StashRepository.Models;
using TheFipster.Munchkin.StashRepository.Persistance;
using Xunit;

namespace TheFipster.Munchkin.StashRepository.Unittest
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
