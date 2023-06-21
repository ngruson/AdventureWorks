using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Handlers.UpdateCustomerAddress;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class UpdateCustomerAddressCommandValidatorUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task no_validation_errors_given_valid_command(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            UpdateCustomerAddressCommandValidator sut,
            Guid customerId,
            CustomerAddress customerAddress
        )
        {
            //Arrange
            customerAddress.Address!.PostalCode = customerAddress.Address.PostalCode![0..15];
            customerAddress.Address.City = customerAddress.Address.City![0..30];
            customerAddress.Address.StateProvinceCode = customerAddress.Address.StateProvinceCode![0..3];
            customerAddress.Address.CountryRegionCode = customerAddress.Address.CountryRegionCode![0..3];

            var command = new UpdateCustomerAddressCommand(customerId, customerAddress);

            customerRepoMock.Setup(x => x.AnyAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(true);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_accountnumber(
            UpdateCustomerAddressCommandValidator sut,
            CustomerAddress customerAddress
        )
        {
            //Arrange
            var command = new UpdateCustomerAddressCommand(Guid.Empty, customerAddress);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerId);
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_customer_notfound(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            UpdateCustomerAddressCommandValidator sut,
            CustomerAddress customerAddress,
            Guid customerId
        )
        {
            //Arrange
            var command = new UpdateCustomerAddressCommand(customerId, customerAddress);

            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.StoreCustomer?)null);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerId)
                .WithErrorMessage("Customer does not exist");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_customer_address(
            [Frozen] Mock<IRepository<Entities.Customer>> repository,
            UpdateCustomerAddressCommandValidator sut,
            Guid customerId
        )
        {
            //Arrange
            var command = new UpdateCustomerAddressCommand(customerId, null);

            repository.Setup(_ => _.AnyAsync(
                    It.IsAny<GetCustomerSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress)
                .WithErrorMessage("Customer address is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_address_type(
            [Frozen] Mock<IRepository<Entities.Customer>> repository,
            UpdateCustomerAddressCommandValidator sut,
            Guid customerId
        )
        {
            //Arrange
            var command = new UpdateCustomerAddressCommand(
                customerId, 
                new CustomerAddress
                {
                    AddressType = "",
                    Address = new Address()
                }
            );

            repository.Setup(_ => _.AnyAsync(
                    It.IsAny<GetCustomerSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.AddressType)
                .WithErrorMessage("Address type is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_address(
            [Frozen] Mock<IRepository<Entities.Customer>> repository,
            UpdateCustomerAddressCommandValidator sut,
            Guid customerId,
            string addressType
        )
        {
            //Arrange
            var customerAddress = new CustomerAddress 
            {
                AddressType = addressType
            };
            var command = new UpdateCustomerAddressCommand(customerId, customerAddress);

            repository.Setup(_ => _.AnyAsync(
                    It.IsAny<GetCustomerSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address)
                .WithErrorMessage("Address is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_addressline1(
            [Frozen] Mock<IRepository<Entities.Customer>> repository,
            UpdateCustomerAddressCommandValidator sut,
            Guid customerId,
            CustomerAddress customerAddress
        )
        {
            //Arrange
            customerAddress.Address!.AddressLine1 = "";
            var command = new UpdateCustomerAddressCommand(customerId, customerAddress);

            repository.Setup(_ => _.AnyAsync(
                    It.IsAny<GetCustomerSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address!.AddressLine1)
                .WithErrorMessage("Address line 1 is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_addressline1_toolong(
            [Frozen] Mock<IRepository<Entities.Customer>> repository,
            UpdateCustomerAddressCommandValidator sut,
            Guid customerId,
            CustomerAddress customerAddress
        )
        {
            //Arrange
            customerAddress.Address!.AddressLine1 =
                customerAddress.Address.AddressLine1 +
                customerAddress.Address.AddressLine1;

            var command = new UpdateCustomerAddressCommand(customerId, customerAddress);

            repository.Setup(_ => _.AnyAsync(
                    It.IsAny<GetCustomerSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address!.AddressLine1)
                .WithErrorMessage("Address line 1 must not exceed 60 characters");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_addressline2_toolong(
            [Frozen] Mock<IRepository<Entities.Customer>> repository,
            UpdateCustomerAddressCommandValidator sut,
            Guid customerId,
            CustomerAddress customerAddress
        )
        {
            //Arrange
            customerAddress.Address!.AddressLine2 =
                customerAddress.Address.AddressLine2 +
                customerAddress.Address.AddressLine2;

            var command = new UpdateCustomerAddressCommand(customerId, customerAddress);

            repository.Setup(_ => _.AnyAsync(
                    It.IsAny<GetCustomerSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address!.AddressLine2)
                .WithErrorMessage("Address line 2 must not exceed 60 characters");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_postalcode(
            [Frozen] Mock<IRepository<Entities.Customer>> repository,
            UpdateCustomerAddressCommandValidator sut,
            Guid customerId,
            CustomerAddress customerAddress
        )
        {
            //Arrange
            customerAddress.Address!.PostalCode = "";
            var command = new UpdateCustomerAddressCommand(customerId, customerAddress);

            repository.Setup(_ => _.AnyAsync(
                    It.IsAny<GetCustomerSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address!.PostalCode)
                .WithErrorMessage("Postal code is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_postalcode_toolong(
            [Frozen] Mock<IRepository<Entities.Customer>> repository,
            UpdateCustomerAddressCommandValidator sut,
            Guid customerId,
            CustomerAddress customerAddress
        )
        {
            //Arrange
            var command = new UpdateCustomerAddressCommand(customerId, customerAddress);

            repository.Setup(_ => _.AnyAsync(
                    It.IsAny<GetCustomerSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address!.PostalCode)
                .WithErrorMessage("Postal code must not exceed 15 characters");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_city(
            [Frozen] Mock<IRepository<Entities.Customer>> repository,
            UpdateCustomerAddressCommandValidator sut,
            Guid customerId,
            CustomerAddress customerAddress
        )
        {
            //Arrange
            customerAddress.Address!.PostalCode = customerAddress.Address!.PostalCode![..15];
            customerAddress.Address.City = "";
            var command = new UpdateCustomerAddressCommand(customerId, customerAddress);

            repository.Setup(_ => _.AnyAsync(
                    It.IsAny<GetCustomerSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address!.City)
                .WithErrorMessage("City is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_city_toolong(
            [Frozen] Mock<IRepository<Entities.Customer>> repository,
            UpdateCustomerAddressCommandValidator sut,
            Guid customerId,
            CustomerAddress customerAddress
        )
        {
            //Arrange
            customerAddress.Address!.PostalCode = customerAddress.Address!.PostalCode![..15];
            customerAddress.Address.City = customerAddress.Address!.City![..30];
            customerAddress.Address.StateProvinceCode = customerAddress.Address!.StateProvinceCode![..3];
            customerAddress.Address.CountryRegionCode = customerAddress.Address!.CountryRegionCode![..3];
            customerAddress.Address.City = "a".PadRight(35, 'b');

            var command = new UpdateCustomerAddressCommand(customerId, customerAddress);

            repository.Setup(_ => _.AnyAsync(
                    It.IsAny<GetCustomerSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address!.City)
                .WithErrorMessage("City must not exceed 30 characters");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_stateprovince(
            [Frozen] Mock<IRepository<Entities.Customer>> repository,
            UpdateCustomerAddressCommandValidator sut,
            Guid customerId,
            CustomerAddress customerAddress
        )
        {
            //Arrange
            customerAddress.Address!.PostalCode = customerAddress.Address!.PostalCode![..15];
            customerAddress.Address.City = customerAddress.Address!.City![..30];
            customerAddress.Address.StateProvinceCode = "";

            var command = new UpdateCustomerAddressCommand(customerId, customerAddress);

            repository.Setup(_ => _.AnyAsync(
                    It.IsAny<GetCustomerSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address!.StateProvinceCode)
                .WithErrorMessage("State/province is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_stateprovince_toolong(
            [Frozen] Mock<IRepository<Entities.Customer>> repository,
            UpdateCustomerAddressCommandValidator sut,
            Guid customerId,
            CustomerAddress customerAddress
        )
        {
            //Arrange
            customerAddress.Address!.PostalCode = customerAddress.Address!.PostalCode![..15];
            customerAddress.Address.City = customerAddress.Address!.City![..30];
            var command = new UpdateCustomerAddressCommand(customerId, customerAddress);

            repository.Setup(_ => _.AnyAsync(
                    It.IsAny<GetCustomerSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address!.StateProvinceCode)
                .WithErrorMessage("State/province must not exceed 3 characters");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_no_country(
            [Frozen] Mock<IRepository<Entities.Customer>> repository,
            UpdateCustomerAddressCommandValidator sut,
            Guid customerId,
            CustomerAddress customerAddress
        )
        {
            //Arrange
            customerAddress.Address!.PostalCode = customerAddress.Address!.PostalCode![..15];
            customerAddress.Address.City = customerAddress.Address!.City![..30];
            customerAddress.Address.StateProvinceCode = customerAddress.Address!.StateProvinceCode![..3];
            customerAddress.Address.CountryRegionCode = "";
            var command = new UpdateCustomerAddressCommand(customerId, customerAddress);

            repository.Setup(_ => _.AnyAsync(
                    It.IsAny<GetCustomerSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address!.CountryRegionCode)
                .WithErrorMessage("Country is required");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_country_toolong(
            [Frozen] Mock<IRepository<Entities.Customer>> repository,
            UpdateCustomerAddressCommandValidator sut,
            Guid customerId,
            CustomerAddress customerAddress
        )
        {
            //Arrange
            customerAddress.Address!.PostalCode = customerAddress.Address!.PostalCode![..15];
            customerAddress.Address.City = customerAddress.Address!.City![..30];
            customerAddress.Address.StateProvinceCode = customerAddress.Address!.StateProvinceCode![..3];
            var command = new UpdateCustomerAddressCommand(customerId, customerAddress);

            repository.Setup(_ => _.AnyAsync(
                    It.IsAny<GetCustomerSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            //Act
            var result = await sut.TestValidateAsync(command);

            //Assert
            result.ShouldHaveValidationErrorFor(command => command.CustomerAddress!.Address!.CountryRegionCode)
                .WithErrorMessage("Country must not exceed 3 characters");
        }

        [Theory]
        [AutoMoqData]
        public async Task validation_error_given_address_already_exists(
            [Frozen] Mock<IRepository<Entities.Customer>> repository,
            Entities.StoreCustomer customer,
            UpdateCustomerAddressCommandValidator sut,
            Guid customerId,
            CustomerAddress customerAddressDto
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
                customerId, 
                new CustomerAddress
                {
                    AddressType = address.AddressType!,
                    Address = new Address
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

            repository.Setup(_ => _.AnyAsync(
                    It.IsAny<GetCustomerSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(true);

            repository.Setup(x => x.SingleOrDefaultAsync(
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
