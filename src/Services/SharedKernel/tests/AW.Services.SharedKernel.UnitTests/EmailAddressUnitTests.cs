using AW.Services.SharedKernel.ValueTypes;
using FluentAssertions;
using FluentAssertions.CSharpFunctionalExtensions;
using Xunit;

namespace AW.Services.SharedKernel.UnitTests
{
    public class EmailAddressUnitTests
    {
        [Fact]
        public void Create_ValidEmailAddress_Success()
        {
            //Arrange

            //Act
            var result = EmailAddress.Create("test@test.com");

            //Assert
            result.Should().BeSuccess();
        }

        [Fact]
        public void Create_EmptyEmailAddress_Failure()
        {
            //Arrange

            //Act
            var result = EmailAddress.Create("");

            //Assert
            result.Should().BeFailure();
        }

        [Fact]
        public void Create_NoAddSign_Failure()
        {
            //Arrange

            //Act
            var result = EmailAddress.Create("testtest.com");

            //Assert
            result.Should().BeFailure();
        }

        [Fact]
        public void Create_WithSpace_Failure()
        {
            //Arrange

            //Act
            var result = EmailAddress.Create("t est@test.com");

            //Assert
            result.Should().BeFailure();
        }
    }
}