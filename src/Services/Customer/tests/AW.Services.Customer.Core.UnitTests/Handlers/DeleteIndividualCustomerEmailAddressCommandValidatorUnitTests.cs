using AW.Services.Customer.Core.Handlers.DeleteIndividualCustomerEmailAddress;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class DeleteIndividualCustomerEmailAddressCommandValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task no_validation_errors_given_valid_command(
            DeleteIndividualCustomerEmailAddressCommandValidator sut,
            DeleteIndividualCustomerEmailAddressCommand command
        )
        {
            //Arrange

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_customerid(
            DeleteIndividualCustomerEmailAddressCommandValidator sut
        )
        {
            //Arrange
            var command = new DeleteIndividualCustomerEmailAddressCommand(
                Guid.Empty,
                Guid.NewGuid()
            );

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.CustomerId);
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_emailaddressid(
            DeleteIndividualCustomerEmailAddressCommandValidator sut
        )
        {
            //Arrange
            var command = new DeleteIndividualCustomerEmailAddressCommand(
                Guid.NewGuid(),
                Guid.Empty
            );

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.EmailAddressId);
        }
    }
}
