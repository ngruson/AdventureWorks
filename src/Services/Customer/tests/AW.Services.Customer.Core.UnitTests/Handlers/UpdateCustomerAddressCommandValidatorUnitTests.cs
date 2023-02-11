using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Handlers.UpdateCustomerAddress;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentValidation.TestHelper;
using Moq;
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
            string accountNumber,
            CustomerAddressDto customerAddress
        )
        {
            //Arrange
            accountNumber = accountNumber[..10];
            var command = new UpdateCustomerAddressCommand(accountNumber, customerAddress);

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
            CustomerAddressDto customerAddress
        )
        {
            //Arrange
            var command = new UpdateCustomerAddressCommand(string.Empty, customerAddress);

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
            CustomerAddressDto customerAddress,
            string accountNumber
        )
        {
            //Arrange
            var command = new UpdateCustomerAddressCommand(accountNumber[..10], customerAddress);

            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.StoreCustomer?)null);

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
            string accountNumber
        )
        {
            //Arrange
            var command = new UpdateCustomerAddressCommand(accountNumber[..10], null);

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
            string accountNumber
        )
        {
            //Arrange
            var command = new UpdateCustomerAddressCommand(
                accountNumber[..10], 
                new CustomerAddressDto
                {
                    AddressType = "",
                    Address = new AddressDto()
                }
            );

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.AddressType)
                .WithErrorMessage("Address type is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_WithoutAddress_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            string accountNumber,
            string addressType
        )
        {
            //Arrange
            var customerAddress = new CustomerAddressDto 
            {
                AddressType = addressType
            };
            var command = new UpdateCustomerAddressCommand(accountNumber[..10], customerAddress);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address)
                .WithErrorMessage("Address is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_WithoutAddressLine1_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            string accountNumber,
            CustomerAddressDto customerAddress
        )
        {
            //Arrange
            customerAddress.Address!.AddressLine1 = "";
            var command = new UpdateCustomerAddressCommand(accountNumber[..10], customerAddress);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address!.AddressLine1)
                .WithErrorMessage("Address line 1 is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_AddressLine1TooLong_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            string accountNumber,
            CustomerAddressDto customerAddress
        )
        {
            //Arrange
            customerAddress.Address!.AddressLine1 =
                customerAddress.Address.AddressLine1 +
                customerAddress.Address.AddressLine1;

            var command = new UpdateCustomerAddressCommand(accountNumber[..10], customerAddress);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address!.AddressLine1)
                .WithErrorMessage("Address line 1 must not exceed 60 characters");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_AddressLine2TooLong_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            string accountNumber,
            CustomerAddressDto customerAddress
        )
        {
            //Arrange
            customerAddress.Address!.AddressLine2 =
                customerAddress.Address.AddressLine2 +
                customerAddress.Address.AddressLine2;

            var command = new UpdateCustomerAddressCommand(accountNumber[..10], customerAddress);            

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address!.AddressLine2)
                .WithErrorMessage("Address line 2 must not exceed 60 characters");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_WithoutPostalCode_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            string accountNumber,
            CustomerAddressDto customerAddress
        )
        {
            //Arrange
            customerAddress.Address!.PostalCode = "";
            var command = new UpdateCustomerAddressCommand(accountNumber[..10], customerAddress);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address!.PostalCode)
                .WithErrorMessage("Postal code is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_PostalCodeTooLong_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            string accountNumber,
            CustomerAddressDto customerAddress
        )
        {
            //Arrange
            var command = new UpdateCustomerAddressCommand(accountNumber[..10], customerAddress);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address!.PostalCode)
                .WithErrorMessage("Postal code must not exceed 15 characters");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_WithoutCity_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            string accountNumber,
            CustomerAddressDto customerAddress
        )
        {
            //Arrange
            customerAddress.Address!.PostalCode = customerAddress.Address!.PostalCode![..15];
            customerAddress.Address.City = "";
            var command = new UpdateCustomerAddressCommand(accountNumber[..10], customerAddress);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address!.City)
                .WithErrorMessage("City is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_CityTooLong_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            string accountNumber,
            CustomerAddressDto customerAddress
        )
        {
            //Arrange
            customerAddress.Address!.PostalCode = customerAddress.Address!.PostalCode![..15];
            customerAddress.Address.City = customerAddress.Address!.City![..30];
            customerAddress.Address.StateProvinceCode = customerAddress.Address!.StateProvinceCode![..3];
            customerAddress.Address.CountryRegionCode = customerAddress.Address!.CountryRegionCode![..3];
            customerAddress.Address.City = "a".PadRight(35, 'b');

            var command = new UpdateCustomerAddressCommand(accountNumber[..10], customerAddress);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address!.City)
                .WithErrorMessage("City must not exceed 30 characters");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_WithoutStateProvince_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            string accountNumber,
            CustomerAddressDto customerAddress
        )
        {
            //Arrange
            customerAddress.Address!.PostalCode = customerAddress.Address!.PostalCode![..15];
            customerAddress.Address.City = customerAddress.Address!.City![..30];
            customerAddress.Address.StateProvinceCode = "";

            var command = new UpdateCustomerAddressCommand(accountNumber[..10], customerAddress);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address!.StateProvinceCode)
                .WithErrorMessage("State/province is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_StateProvinceTooLong_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            string accountNumber,
            CustomerAddressDto customerAddress
        )
        {
            //Arrange
            customerAddress.Address!.PostalCode = customerAddress.Address!.PostalCode![..15];
            customerAddress.Address.City = customerAddress.Address!.City![..30];
            var command = new UpdateCustomerAddressCommand(accountNumber[..10], customerAddress);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address!.StateProvinceCode)
                .WithErrorMessage("State/province must not exceed 3 characters");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_WithoutCountry_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            string accountNumber,
            CustomerAddressDto customerAddress
        )
        {
            //Arrange
            customerAddress.Address!.PostalCode = customerAddress.Address!.PostalCode![..15];
            customerAddress.Address.City = customerAddress.Address!.City![..30];
            customerAddress.Address.StateProvinceCode = customerAddress.Address!.StateProvinceCode![..3];
            customerAddress.Address.CountryRegionCode = "";
            var command = new UpdateCustomerAddressCommand(accountNumber[..10], customerAddress);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address!.CountryRegionCode)
                .WithErrorMessage("Country is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_CountryTooLong_ValidationError(
            UpdateCustomerAddressCommandValidator sut,
            string accountNumber,
            CustomerAddressDto customerAddress
        )
        {
            //Arrange
            customerAddress.Address!.PostalCode = customerAddress.Address!.PostalCode![..15];
            customerAddress.Address.City = customerAddress.Address!.City![..30];
            customerAddress.Address.StateProvinceCode = customerAddress.Address!.StateProvinceCode![..3];
            var command = new UpdateCustomerAddressCommand(accountNumber[..10], customerAddress);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address!.CountryRegionCode)
                .WithErrorMessage("Country must not exceed 3 characters");
        }

        [Theory]
        [AutoMoqData]
        public async Task TestValidate_AddressAlreadyExists_ValidationError(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            Entities.StoreCustomer customer,
            UpdateCustomerAddressCommandValidator sut,
            string accountNumber,
            CustomerAddressDto customerAddressDto
        )
        {
            //Arrange
            var customerAddress = new Entities.CustomerAddress(
                customerAddressDto.AddressType!,
                new Entities.Address(
                    customerAddressDto.Address!.AddressLine1!,
                    customerAddressDto.Address.AddressLine2!,
                    customerAddressDto!.Address.PostalCode![..15],
                    customerAddressDto.Address.City![..30],
                    customerAddressDto.Address.StateProvinceCode![..3],
                    customerAddressDto.Address.CountryRegionCode![..3]
                )
            );
            
            customer.AddAddress(customerAddress);

            var address = customer.Addresses.ToList()[0]!;

            var command = new UpdateCustomerAddressCommand(
                accountNumber[..10], 
                new CustomerAddressDto
                {
                    AddressType = address.AddressType!,
                    Address = new AddressDto
                    {
                        AddressLine1 = address.Address!.AddressLine1,
                        AddressLine2 = address.Address!.AddressLine2,
                        PostalCode = address.Address!.PostalCode,
                        City = address.Address!.City,
                        StateProvinceCode = address.Address!.StateProvinceCode,
                        CountryRegionCode = address.Address!.CountryRegionCode
                    }
                }
            );

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