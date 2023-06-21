using AW.Services.Customer.Core.Handlers.DeleteCustomer;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class DeleteCustomerCommandValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task no_validation_errors_given_valid_command(
            DeleteCustomerCommandValidator sut,
            DeleteCustomerCommand command
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
        public async Task validation_error_given_no_objectid(
            DeleteCustomerCommandValidator sut
        )
        {
            //Arrange
            var command = new DeleteCustomerCommand(Guid.Empty);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.ObjectId);
        }
    }
}
