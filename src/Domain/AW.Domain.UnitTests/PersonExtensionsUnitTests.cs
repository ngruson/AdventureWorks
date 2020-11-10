using AW.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Xunit;

namespace AW.Domain.UnitTests
{
    public class PersonExtensionsUnitTests
    {
        [Fact]
        public void PersonFullName_PersonWithMiddleName()
        {
            //Arrange
            var person = new PersonBuilder().WithTestValues().Build();

            //Act
            string personName = person.FullName;

            //Assert
            personName.Should().Be("Orlando N. Gee");
        }

        [Fact]
        public void PersonFullName_PersonWithoutMiddleName()
        {
            //Arrange
            var person = new PersonBuilder().WithTestValues().Build();
            person.MiddleName = "";

            //Act
            string personName = person.FullName;

            //Assert
            personName.Should().Be("Orlando Gee");
        }

        [Fact]
        public void PersonFullName_PersonFirstNameOnly()
        {
            //Arrange
            var person = new PersonBuilder().WithTestValues().Build();
            person.MiddleName = "";
            person.LastName = "";

            //Act
            string personName = person.FullName;

            //Assert
            personName.Should().Be("Orlando");
        }
    }
}