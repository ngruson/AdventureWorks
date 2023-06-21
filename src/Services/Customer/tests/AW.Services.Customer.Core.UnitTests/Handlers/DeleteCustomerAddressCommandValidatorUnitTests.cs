using AW.Services.Customer.Core.Handlers.DeleteCustomerAddress;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class DeleteCustomerAddressCommandValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task no_validation_errors_given_valid_command(
            DeleteCustomerAddressCommandValidator sut,
            DeleteCustomerAddressCommand command
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
            DeleteCustomerAddressCommandValidator sut
        )
        {
            //Arrange
            var command = new DeleteCustomerAddressCommand(
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
        public async Task validation_error_given_no_addressid(
            DeleteCustomerAddressCommandValidator sut
        )
        {
            //Arrange
            var command = new DeleteCustomerAddressCommand(
                Guid.NewGuid(),
                Guid.Empty
            );

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.AddressId);
        }
    }
}
