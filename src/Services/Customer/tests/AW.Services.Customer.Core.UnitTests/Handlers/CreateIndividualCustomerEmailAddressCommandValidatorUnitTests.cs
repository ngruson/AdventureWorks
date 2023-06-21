using AW.Services.Customer.Core.Handlers.CreateIndividualCustomerEmailAddress;
using AW.Services.SharedKernel.ValueTypes;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class CreateIndividualCustomerEmailAddressCommandValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public void no_validation_errors_given_valid_command(
            CreateIndividualCustomerEmailAddressCommandValidator sut,
            CreateIndividualCustomerEmailAddressCommand command
        )
        {
            //Arrange

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [AutoMoqData]
        public void validation_error_given_no_customerid(
            CreateIndividualCustomerEmailAddressCommandValidator sut,
            EmailAddress emailAddress
        )
        {
            //Arrange
            var command = new CreateIndividualCustomerEmailAddressCommand(
                Guid.Empty,
                emailAddress
            );

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.CustomerId);
        }

        [Theory]
        [AutoMoqData]
        public void validation_error_given_no_emailaddress(
            CreateIndividualCustomerEmailAddressCommandValidator sut
        )
        {
            //Arrange

            var command = new CreateIndividualCustomerEmailAddressCommand(
                Guid.NewGuid(),
                EmailAddress.Create("")
            );

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.EmailAddress);
        }
    }
}
