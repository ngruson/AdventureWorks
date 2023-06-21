using Ardalis.Result;
using AW.Services.SharedKernel.ValueTypes;
using FluentAssertions;
using Xunit;

namespace AW.Services.SharedKernel.UnitTests
{
    public class EmailAddressUnitTests
    {
        [Fact]
        public void return_success_given_valid_emailaddress()
        {
            //Arrange

            //Act
            var result = EmailAddress.Create("test@test.com");

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void return_invalid_given_empty_emailaddress()
        {
            //Arrange

            //Act
            var result = EmailAddress.Create("");

            //Assert
            result.Status.Should().Be(ResultStatus.Invalid);
        }

        [Fact]
        public void return_invalid_given_no_add_sign()
        {
            //Arrange

            //Act
            var result = EmailAddress.Create("testtest.com");

            //Assert
            result.Status.Should().Be(ResultStatus.Invalid);
        }

        [Fact]
        public void return_invalid_given_emailaddress_with_space()
        {
            //Arrange

            //Act
            var result = EmailAddress.Create("t est@test.com");

            //Assert
            result.Status.Should().Be(ResultStatus.Invalid);
        }
    }
}
