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
            var person = new Person.Person
            {
                FirstName = "Orlando",
                MiddleName = "N.",
                LastName = "Gee"
            };

            //Act
            string personName = person.FullName;

            //Assert
            personName.Should().Be("Orlando N. Gee");
        }

        [Fact]
        public void PersonFullName_PersonWithoutMiddleName()
        {
            //Arrange
            var person = new Person.Person
            {
                FirstName = "Orlando",
                LastName = "Gee"
            };

            //Act
            string personName = person.FullName;

            //Assert
            personName.Should().Be("Orlando Gee");
        }

        [Fact]
        public void PersonFullName_PersonFirstNameOnly()
        {
            //Arrange
            var person = new Person.Person
            {
                FirstName = "Orlando"
            };

            //Act
            string personName = person.FullName;

            //Assert
            personName.Should().Be("Orlando");
        }
    }
}