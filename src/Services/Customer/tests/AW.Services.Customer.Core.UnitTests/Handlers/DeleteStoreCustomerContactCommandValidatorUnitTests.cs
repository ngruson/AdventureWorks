using AW.Services.Customer.Core.Handlers.DeleteStoreCustomerContact;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class DeleteStoreCustomerContactCommandValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task no_validation_errors_given_valid_command(
            DeleteStoreCustomerContactCommandValidator sut,
            DeleteStoreCustomerContactCommand command
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
            DeleteStoreCustomerContactCommandValidator sut,
            Guid contactId
        )
        {
            //Arrange
            var command = new DeleteStoreCustomerContactCommand(
                Guid.Empty,
                contactId
            );

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.CustomerId);
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_contactid(
            DeleteStoreCustomerContactCommandValidator sut,
            Guid customerId
        )
        {
            //Arrange
            var command = new DeleteStoreCustomerContactCommand(
                customerId,
                Guid.Empty
            );

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.ContactId);
        }
    }
}
