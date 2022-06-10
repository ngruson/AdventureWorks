using AW.UI.Web.Store.ViewModels.Annotations;
using FluentAssertions;
using Xunit;

namespace AW.UI.Web.Store.UnitTests.ViewModels.Annotations
{
    public class CardExpirationAttributeUnitTests
    {
        [Fact]
        public void IsValid_ValueIsValidExpDate_ReturnsTrue()
        {
            //Arrange
            var sut = new CardExpirationAttribute();

            //Act
            var result = sut.IsValid("01/30");

            //Assert
            result.Should().Be(true);
        }

        [Fact]
        public void IsValid_ValueIsNull_ReturnsFalse()
        {
            //Arrange
            var sut = new CardExpirationAttribute();

            //Act
            var result = sut.IsValid(null);

            //Assert
            result.Should().Be(false);
        }

        [Fact]
        public void IsValid_ValueHasInvalidYear_ReturnsFalse()
        {
            //Arrange
            var sut = new CardExpirationAttribute();

            //Act
            var result = sut.IsValid("xx/30");

            //Assert
            result.Should().Be(false);
        }

        [Fact]
        public void IsValid_ValueHasInvalidMonth_ReturnsFalse()
        {
            //Arrange
            var sut = new CardExpirationAttribute();

            //Act
            var result = sut.IsValid("01/xx");

            //Assert
            result.Should().Be(false);
        }
    }
}