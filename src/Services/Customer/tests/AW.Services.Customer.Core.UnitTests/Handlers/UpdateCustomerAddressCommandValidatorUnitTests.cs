using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Handlers.UpdateCustomerAddress;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentValidation.TestHelper;
using Moq;
using System.Linq;
using System.Threading;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class UpdateCustomerAddressCommandValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public void TestValidate_ValidCommand_NoValidationError(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            Entities.StoreCustomer customer,
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.AccountNumber = "1";

            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(customer);

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldNotHaveValidationErrorFor(command => command.AccountNumber);
            result.ShouldNotHaveValidationErrorFor(command => command.CustomerAddress);
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_WithEmptyAccountNumber_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.AccountNumber = null;

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.AccountNumber)
                .WithErrorMessage("Account number is required");
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_WithAccountNumberTooLong_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.AccountNumber)
                .WithErrorMessage("Account number must not exceed 10 characters");
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_CustomerNotFound_ValidationError(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.StoreCustomer)null);

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.AccountNumber)
                .WithErrorMessage("Customer does not exist");
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_WithoutCustomerAddress_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.CustomerAddress = null;

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress)
                .WithErrorMessage("Customer address is required");
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_WithoutAddressType_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.CustomerAddress.AddressType = "";

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.AddressType)
                .WithErrorMessage("Address type is required");
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_WithoutAddress_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.CustomerAddress.Address = null;

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address)
                .WithErrorMessage("Address is required");
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_WithoutAddressLine1_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.CustomerAddress.Address.AddressLine1 = "";

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address.AddressLine1)
                .WithErrorMessage("Address line 1 is required");
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_AddressLine1TooLong_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.CustomerAddress.Address.AddressLine1 =
                command.CustomerAddress.Address.AddressLine1 +
                command.CustomerAddress.Address.AddressLine1;

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address.AddressLine1)
                .WithErrorMessage("Address line 1 must not exceed 60 characters");
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_AddressLine2TooLong_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.CustomerAddress.Address.AddressLine2 =
                command.CustomerAddress.Address.AddressLine2 +
                command.CustomerAddress.Address.AddressLine2;

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address.AddressLine2)
                .WithErrorMessage("Address line 2 must not exceed 60 characters");
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_WithoutPostalCode_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.CustomerAddress.Address.PostalCode = "";

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address.PostalCode)
                .WithErrorMessage("Postal code is required");
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_PostalCodeTooLong_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address.PostalCode)
                .WithErrorMessage("Postal code must not exceed 15 characters");
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_WithoutCity_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.CustomerAddress.Address.City = "";

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address.City)
                .WithErrorMessage("City is required");
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_CityTooLong_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address.City)
                .WithErrorMessage("City must not exceed 30 characters");
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_WithoutStateProvince_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.CustomerAddress.Address.StateProvinceCode = "";

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address.StateProvinceCode)
                .WithErrorMessage("State/province is required");
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_StateProvinceTooLong_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address.StateProvinceCode)
                .WithErrorMessage("State/province must not exceed 3 characters");
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_WithoutCountry_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            command.CustomerAddress.Address.CountryRegionCode = "";

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address.CountryRegionCode)
                .WithErrorMessage("Country is required");
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_CountryTooLong_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress.Address.CountryRegionCode)
                .WithErrorMessage("Country must not exceed 3 characters");
        }

        [Theory]
        [AutoMoqData]
        public void TestValidate_AddressAlreadyExists_ValidationError(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            Entities.StoreCustomer customer,
            UpdateCustomerAddressCommandValidator sut,
            UpdateCustomerAddressCommand command,
            Entities.CustomerAddress customerAddress
        )
        {
            //Arrange
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

            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(customer);

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command)
                .WithErrorMessage("Address must be unique");
        }
    }
}