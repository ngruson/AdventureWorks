using AW.Services.Customer.Core.Handlers.CreateIndividualCustomerPhone;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class CreateIndividualCustomerPhoneCommandValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task no_validation_errors_given_valid_command(
            CreateIndividualCustomerPhoneCommandValidator sut,
            CreateIndividualCustomerPhoneCommand command
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
            CreateIndividualCustomerPhoneCommandValidator sut,
            Phone phone
        )
        {
            //Arrange
            var command = new CreateIndividualCustomerPhoneCommand(
                Guid.Empty,
                phone
            );

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.CustomerId);
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_phonenumbertype(
            CreateIndividualCustomerPhoneCommandValidator sut,
            Guid customerId,
            string phoneNumber
        )
        {
            //Arrange
            var command = new CreateIndividualCustomerPhoneCommand(
                customerId,
                new Phone(string.Empty, phoneNumber)
            );

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.Phone.PhoneNumberType);
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_phonenumber(
            CreateIndividualCustomerPhoneCommandValidator sut,
            Guid customerId,
            string phoneNumberType
        )
        {
            //Arrange
            var command = new CreateIndividualCustomerPhoneCommand(
                customerId,
                new Phone(phoneNumberType, string.Empty)
            );

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.Phone.PhoneNumber);
        }
    }
}
