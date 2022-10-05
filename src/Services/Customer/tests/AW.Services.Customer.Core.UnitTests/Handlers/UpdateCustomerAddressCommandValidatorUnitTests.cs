using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Handlers.UpdateCustomerAddress;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentValidation.TestHelper;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class UpdateCustomerAddressCommandValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task TestValidate_ValidCommand_NoValidationError(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            Entities.StoreCustomer customer,
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.AccountNumber = "1";

            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(customer);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldNotHaveValidationErrorFor(command => command.AccountNumber);
            result.ShouldNotHaveValidationErrorFor(command => command.CustomerAddress);
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_WithEmptyAccountNumber_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.AccountNumber = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.AccountNumber)
                .WithErrorMessage("Account number is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_WithAccountNumberTooLong_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.AccountNumber)
                .WithErrorMessage("Account number must not exceed 10 characters");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_CustomerNotFound_ValidationError(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.AccountNumber = command.AccountNumber[..10];

            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.StoreCustomer)null);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.AccountNumber)
                .WithErrorMessage("Customer does not exist");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_WithoutCustomerAddress_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.AccountNumber = command.AccountNumber[..10];
            command.CustomerAddress = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress)
                .WithErrorMessage("Customer address is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_WithoutAddressType_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.AccountNumber = command.AccountNumber[..10];
            command.CustomerAddress.AddressType = "";

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.AddressType)
                .WithErrorMessage("Address type is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_WithoutAddress_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.AccountNumber = command.AccountNumber[..10];
            command.CustomerAddress.Address = null;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address)
                .WithErrorMessage("Address is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_WithoutAddressLine1_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.AccountNumber = command.AccountNumber[..10];
            command.CustomerAddress.Address.AddressLine1 = "";

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address.AddressLine1)
                .WithErrorMessage("Address line 1 is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_AddressLine1TooLong_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.AccountNumber = command.AccountNumber.Substring(0, 10);
            command.CustomerAddress.Address.AddressLine1 =
                command.CustomerAddress.Address.AddressLine1 +
                command.CustomerAddress.Address.AddressLine1;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address.AddressLine1)
                .WithErrorMessage("Address line 1 must not exceed 60 characters");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_AddressLine2TooLong_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.AccountNumber = command.AccountNumber.Substring(0, 10);
            command.CustomerAddress.Address.AddressLine2 =
                command.CustomerAddress.Address.AddressLine2 +
                command.CustomerAddress.Address.AddressLine2;

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address.AddressLine2)
                .WithErrorMessage("Address line 2 must not exceed 60 characters");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_WithoutPostalCode_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.AccountNumber = command.AccountNumber[..10];
            command.CustomerAddress.Address.PostalCode = "";

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address.PostalCode)
                .WithErrorMessage("Postal code is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_PostalCodeTooLong_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.AccountNumber = command.AccountNumber[..10];

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address.PostalCode)
                .WithErrorMessage("Postal code must not exceed 15 characters");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_WithoutCity_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.AccountNumber = command.AccountNumber[..10];
            command.CustomerAddress.Address.PostalCode = command.CustomerAddress.Address.PostalCode[..15];
            command.CustomerAddress.Address.City = "";

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address.City)
                .WithErrorMessage("City is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_CityTooLong_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.AccountNumber = command.AccountNumber[..10];
            command.CustomerAddress.Address.PostalCode = command.CustomerAddress.Address.PostalCode[..15];
            command.CustomerAddress.Address.City = command.CustomerAddress.Address.City[..30];
            command.CustomerAddress.Address.StateProvinceCode = command.CustomerAddress.Address.StateProvinceCode[..3];
            command.CustomerAddress.Address.CountryRegionCode = command.CustomerAddress.Address.CountryRegionCode[..3];

            command.CustomerAddress.Address.City = "a".PadRight(35, 'b');

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address.City)
                .WithErrorMessage("City must not exceed 30 characters");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_WithoutStateProvince_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.AccountNumber = command.AccountNumber[..10];
            command.CustomerAddress.Address.PostalCode = command.CustomerAddress.Address.PostalCode[..15];
            command.CustomerAddress.Address.City = command.CustomerAddress.Address.City[..30];
            command.CustomerAddress.Address.StateProvinceCode = "";

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address.StateProvinceCode)
                .WithErrorMessage("State/province is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_StateProvinceTooLong_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.AccountNumber = command.AccountNumber[..10];
            command.CustomerAddress.Address.PostalCode = command.CustomerAddress.Address.PostalCode[..15];
            command.CustomerAddress.Address.City = command.CustomerAddress.Address.City[..30];

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address.StateProvinceCode)
                .WithErrorMessage("State/province must not exceed 3 characters");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_WithoutCountry_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.AccountNumber = command.AccountNumber[..10];
            command.CustomerAddress.Address.PostalCode = command.CustomerAddress.Address.PostalCode[..15];
            command.CustomerAddress.Address.City = command.CustomerAddress.Address.City[..30];
            command.CustomerAddress.Address.StateProvinceCode = command.CustomerAddress.Address.StateProvinceCode[..3];
            command.CustomerAddress.Address.CountryRegionCode = "";

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address.CountryRegionCode)
                .WithErrorMessage("Country is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_CountryTooLong_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.AccountNumber = command.AccountNumber[..10];
            command.CustomerAddress.Address.PostalCode = command.CustomerAddress.Address.PostalCode[..15];
            command.CustomerAddress.Address.City = command.CustomerAddress.Address.City[..30];
            command.CustomerAddress.Address.StateProvinceCode = command.CustomerAddress.Address.StateProvinceCode[..3];

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address.CountryRegionCode)
                .WithErrorMessage("Country must not exceed 3 characters");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_AddressAlreadyExists_ValidationError(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            Entities.StoreCustomer customer,
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
            //Entities.CustomerAddress customerAddress
        )
        {
            //Arrange
            var customerAddress = new Entities.CustomerAddress(
                command.CustomerAddress.AddressType,
                new Entities.Address(
                    command.CustomerAddress.Address.AddressLine1,
                    command.CustomerAddress.Address.AddressLine2,
                    command.CustomerAddress.Address.PostalCode.Substring(0, 15),
                    command.CustomerAddress.Address.City.Substring(0, 30),
                    command.CustomerAddress.Address.StateProvinceCode.Substring(0, 3),
                    command.CustomerAddress.Address.CountryRegionCode.Substring(0, 3)
                )
            );
            
            customer.AddAddress(customerAddress);

            command.AccountNumber = "1";
            var address = customer.Addresses.ToList()[0];
            command.CustomerAddress.AddressType = address.AddressType;
            command.CustomerAddress.Address.AddressLine1 = address.Address.AddressLine1;
            command.CustomerAddress.Address.AddressLine2 = address.Address.AddressLine2;
            command.CustomerAddress.Address.PostalCode = address.Address.PostalCode;
            command.CustomerAddress.Address.City = address.Address.City;
            command.CustomerAddress.Address.StateProvinceCode = address.Address.StateProvinceCode;
            command.CustomerAddress.Address.CountryRegionCode = address.Address.CountryRegionCode;

            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(customer);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command)
                .WithErrorMessage("Address must be unique");
        }
    }
}