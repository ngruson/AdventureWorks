using AW.Services.Customer.Core.Handlers.CreateCustomerAddress;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class CreateCustomerAddressCommandValidatorUnitTests
    {

        [Theory]
        [AutoMoqData]
        public void no_validation_errors_given_valid_command(
            CreateCustomerAddressCommandValidator sut,
            CreateCustomerAddressCommand command
        )
        {
            //Arrange

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [AutoMoqData]
        public void validation_error_given_no_customer_id(
            CreateCustomerAddressCommandValidator sut,
            CustomerAddress customerAddress
        )
        {
            //Arrange
            var command = new CreateCustomerAddressCommand(
                Guid.Empty,
                customerAddress
            );

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.CustomerId);
        }

        [Theory]
        [AutoMoqData]
        public void validation_error_given_no_addressline1(
            CreateCustomerAddressCommandValidator sut,
            CreateCustomerAddressCommand command
        )
        {
            //Arrange
            command.CustomerAddress.Address.AddressLine1 = string.Empty;

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.CustomerAddress.Address.AddressLine1);
        }

        [Theory]
        [AutoMoqData]
        public void validation_error_given_no_addressline2(
            CreateCustomerAddressCommandValidator sut,
            CreateCustomerAddressCommand command
        )
        {
            //Arrange
            command.CustomerAddress.Address.AddressLine2 = string.Empty;

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.CustomerAddress.Address.AddressLine2);
        }

        [Theory]
        [AutoMoqData]
        public void validation_error_given_no_postalcode(
            CreateCustomerAddressCommandValidator sut,
            CreateCustomerAddressCommand command
        )
        {
            //Arrange
            command.CustomerAddress.Address.PostalCode = string.Empty;

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.CustomerAddress.Address.PostalCode);
        }

        [Theory]
        [AutoMoqData]
        public void validation_error_given_no_city(
            CreateCustomerAddressCommandValidator sut,
            CreateCustomerAddressCommand command
        )
        {
            //Arrange
            command.CustomerAddress.Address.City = string.Empty;

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.CustomerAddress.Address.City);
        }

        [Theory]
        [AutoMoqData]
        public void validation_error_given_no_stateprovincecode(
            CreateCustomerAddressCommandValidator sut,
            CreateCustomerAddressCommand command
        )
        {
            //Arrange
            command.CustomerAddress.Address.StateProvinceCode = string.Empty;

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.CustomerAddress.Address.StateProvinceCode);
        }

        [Theory]
        [AutoMoqData]
        public void validation_error_given_no_countryregioncode(
            CreateCustomerAddressCommandValidator sut,
            CreateCustomerAddressCommand command
        )
        {
            //Arrange
            command.CustomerAddress.Address.CountryRegionCode = string.Empty;

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.CustomerAddress.Address.CountryRegionCode);
        }

        [Theory]
        [AutoMoqData]
        public void validation_error_given_no_addresstype(
            CreateCustomerAddressCommandValidator sut,
            CreateCustomerAddressCommand command
        )
        {
            //Arrange
            command.CustomerAddress.AddressType = string.Empty;

            //Act
            var result = sut.TestValidate(command);

            //Assert
            result.ShouldHaveValidationErrorFor(_ => _.CustomerAddress.AddressType);
        }
    }
}
