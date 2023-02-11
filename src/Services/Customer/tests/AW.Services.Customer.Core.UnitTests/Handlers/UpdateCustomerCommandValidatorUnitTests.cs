using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Handlers.UpdateCustomer;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.Services.SharedKernel.ValueTypes;
using AW.SharedKernel.UnitTesting;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class UpdateCustomerCommandValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task TestValidate_ValidCommand_NoValidationError(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            UpdateCustomerCommandValidator sut,
            string name,
            string accountNumber
        )
        {
            //Arrange
            accountNumber = accountNumber[..10];

            var command = new UpdateCustomerCommand(
                new StoreCustomerDto
                {
                    AccountNumber = accountNumber
                }
            );

            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new Entities.StoreCustomer(name, accountNumber));

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldNotHaveValidationErrorFor(command => command.Customer);
            result.ShouldNotHaveValidationErrorFor(command => command.Customer!.AccountNumber);
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_WithoutCustomer_ValidationError(
            UpdateCustomerCommandValidator sut
        )
        {
            //Arrange
            var command = new UpdateCustomerCommand(null);

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Customer)
                .WithErrorMessage("Customer is required");
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_WithEmptyAccountNumber_ValidationError(
            UpdateCustomerCommandValidator sut
        )
        {
            //Arrange
            var command = new UpdateCustomerCommand(new StoreCustomerDto());

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Customer!.AccountNumber)
                .WithErrorMessage("Account number is required");
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_WithAccountNumberTooLong_ValidationError(
            UpdateCustomerCommandValidator sut,
            string accountNumber
        )
        {
            //Arrange
            var command = new UpdateCustomerCommand(
                new StoreCustomerDto {  AccountNumber = accountNumber }
            );

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Customer!.AccountNumber)
                .WithErrorMessage("Account number must not exceed 10 characters");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_CustomerNotFound_ValidationError(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            UpdateCustomerCommandValidator sut,
            string accountNumber,
            List<CustomerAddressDto> addresses,
            string contactType,
            List<PersonPhoneDto> phoneNumbers
        )
        {
            //Arrange
            var customerDto = new StoreCustomerDto
            {
                AccountNumber = accountNumber[..10],
                Addresses = addresses,
                Contacts = new List<StoreCustomerContactDto>
                {
                    new StoreCustomerContactDto
                    {
                        ContactType = contactType,
                        ContactPerson = new PersonDto
                        {
                            EmailAddresses = new List<PersonEmailAddressDto>
                            {
                                new PersonEmailAddressDto
                                {
                                    EmailAddress = EmailAddress.Create("test@test.com").Value
                                }
                            },
                            PhoneNumbers = phoneNumbers
                        }
                    }
                }
            };

            var command = new UpdateCustomerCommand(
                customerDto
            );

            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.StoreCustomer?)null);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.Customer!.AccountNumber)
                .WithErrorMessage("Customer does not exist");
        }
    }
}
