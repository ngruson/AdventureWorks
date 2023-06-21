using AW.Services.Customer.Core.Handlers.DeleteIndividualCustomerPhone;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class DeleteIndividualCustomerPhoneCommandValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task no_validation_errors_given_valid_command(
            DeleteIndividualCustomerPhoneCommandValidator sut,
            DeleteIndividualCustomerPhoneCommand command
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
            DeleteIndividualCustomerPhoneCommandValidator sut
        )
        {
            //Arrange
            var command = new DeleteIndividualCustomerPhoneCommand(
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
        public async Task validation_error_given_no_phoneid(
            DeleteIndividualCustomerPhoneCommandValidator sut
        )
        {
            //Arrange
            var command = new DeleteIndividualCustomerPhoneCommand(
                Guid.NewGuid(),
                Guid.Empty
            );

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.PhoneId);
        }
    }
}
