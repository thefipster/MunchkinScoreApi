using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TheFipster.Munchkin.GameDomain.UnitTest.Models
{
    public class CardCollectionTest
    {
        [Fact]
        public void CreateNewObject_ResultsInInitializedList_Test()
        {
            // Arrange & Act
            var collection = new CardCollection();

            // Assert
            collection.Cards.Should().NotBeNull();
            collection.Cards.Should().BeEmpty();
        }

        [Fact]
        public void CreatedNewObjectWithName_ResultsInSetId_Test()
        {
            // Arrange
            var name = "evil monsters";

            // Act
            var collection = new CardCollection(name);

            // Assert
            collection.Id.Should().BeEquivalentTo(name);
        }

        [Fact]
        public void CreatedNewObjectWithCollection_ResultsInSetCards_Test()
        {
            // Arrange
            var name = "evil monsters";
            var monster1 = "Zerschmetterling";
            var monster2 = "Topfpflanze";
            var cards = new List<string> { monster1, monster2 };

            // Act
            var collection = new CardCollection(name, cards);

            // Assert
            collection.Cards.Should().BeEquivalentTo(monster1, monster2);
        }
    }
}
