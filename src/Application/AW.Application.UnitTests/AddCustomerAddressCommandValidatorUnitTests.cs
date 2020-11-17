using Ardalis.Specification;
using AW.Application.Customer.AddCustomerAddress;
using AW.Application.Specifications;
using AW.Application.UnitTests.TestBuilders;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace AW.Application.UnitTests
{
    public class AddCustomerAddressCommandValidatorUnitTests
    {
        [Fact]
        public void AccountNumber_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();
            var stateProvince = new StateProvinceBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetAddressTypeSpecification>()))
                .ReturnsAsync(addressType);

            var stateProvinceRepoMock = new Mock<IRepositoryBase<Domain.Person.StateProvince>>();
            stateProvinceRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetStateProvinceSpecification>()))
                .ReturnsAsync(stateProvince);

            var validator = new AddCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new AddCustomerAddressCommand();
            
            validator.ShouldHaveValidationErrorFor(x => x.AccountNumber, command);
        }

        [Fact]
        public void AccountNumber_TooLong_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();
            var stateProvince = new StateProvinceBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetAddressTypeSpecification>()))
                .ReturnsAsync(addressType);

            var stateProvinceRepoMock = new Mock<IRepositoryBase<Domain.Person.StateProvince>>();
            stateProvinceRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetStateProvinceSpecification>()))
                .ReturnsAsync(stateProvince);

            var validator = new AddCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new AddCustomerAddressCommand
            {
                AccountNumber = "AW000000023456789",
                CustomerAddress = new CustomerAddressDto
                {
                    AddressTypeName = "Home",
                    Address = new AddressDto
                    {
                        AddressLine1 = "AddressLine1",
                        PostalCode = "PostalCode",
                        City = "City",
                        StateProvinceCode = "XX"
                    }
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.AccountNumber, command);
        }

        [Fact]
        public void CustomerAddress_Null_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();
            var stateProvince = new StateProvinceBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetAddressTypeSpecification>()))
                .ReturnsAsync(addressType);

            var stateProvinceRepoMock = new Mock<IRepositoryBase<Domain.Person.StateProvince>>();
            stateProvinceRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetStateProvinceSpecification>()))
                .ReturnsAsync(stateProvince);

            var validator = new AddCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new AddCustomerAddressCommand();

            validator.ShouldHaveValidationErrorFor(x => x.CustomerAddress, command);
        }

        [Fact]
        public void AddressTypeName_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();
            var stateProvince = new StateProvinceBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetAddressTypeSpecification>()))
                .ReturnsAsync(addressType);

            var stateProvinceRepoMock = new Mock<IRepositoryBase<Domain.Person.StateProvince>>();
            stateProvinceRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetStateProvinceSpecification>()))
                .ReturnsAsync(stateProvince);

            var validator = new AddCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new AddCustomerAddressCommand
            {
                CustomerAddress = new CustomerAddressDto
                {
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerAddress.AddressTypeName, command);
        }

        [Fact]
        public void Address_Null_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();
            var stateProvince = new StateProvinceBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetAddressTypeSpecification>()))
                .ReturnsAsync(addressType);

            var stateProvinceRepoMock = new Mock<IRepositoryBase<Domain.Person.StateProvince>>();
            stateProvinceRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetStateProvinceSpecification>()))
                .ReturnsAsync(stateProvince);

            var validator = new AddCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new AddCustomerAddressCommand
            {
                CustomerAddress = new CustomerAddressDto()
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerAddress.Address, command);
        }

        [Fact]
        public void AddressLine1_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();
            var stateProvince = new StateProvinceBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetAddressTypeSpecification>()))
                .ReturnsAsync(addressType);

            var stateProvinceRepoMock = new Mock<IRepositoryBase<Domain.Person.StateProvince>>();
            stateProvinceRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetStateProvinceSpecification>()))
                .ReturnsAsync(stateProvince);

            var validator = new AddCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new AddCustomerAddressCommand
            {
                CustomerAddress = new CustomerAddressDto
                {
                    Address = new AddressDto()
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerAddress.Address.AddressLine1, command);
        }

        [Fact]
        public void PostalCode_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();
            var stateProvince = new StateProvinceBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetAddressTypeSpecification>()))
                .ReturnsAsync(addressType);

            var stateProvinceRepoMock = new Mock<IRepositoryBase<Domain.Person.StateProvince>>();
            stateProvinceRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetStateProvinceSpecification>()))
                .ReturnsAsync(stateProvince);

            var validator = new AddCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new AddCustomerAddressCommand
            {
                CustomerAddress = new CustomerAddressDto
                {
                    Address = new AddressDto()
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerAddress.Address.PostalCode, command);
        }

        [Fact]
        public void PostalCode_TooLong_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();
            var stateProvince = new StateProvinceBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetAddressTypeSpecification>()))
                .ReturnsAsync(addressType);

            var stateProvinceRepoMock = new Mock<IRepositoryBase<Domain.Person.StateProvince>>();
            stateProvinceRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetStateProvinceSpecification>()))
                .ReturnsAsync(stateProvince);

            var validator = new AddCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new AddCustomerAddressCommand
            {
                CustomerAddress = new CustomerAddressDto
                {
                    Address = new AddressDto
                    {
                        PostalCode = "aaaaabbbbbcccccddddd"
                    }
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerAddress.Address.PostalCode, command);
        }

        [Fact]
        public void City_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();
            var stateProvince = new StateProvinceBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetAddressTypeSpecification>()))
                .ReturnsAsync(addressType);

            var stateProvinceRepoMock = new Mock<IRepositoryBase<Domain.Person.StateProvince>>();
            stateProvinceRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetStateProvinceSpecification>()))
                .ReturnsAsync(stateProvince);

            var validator = new AddCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new AddCustomerAddressCommand
            {
                CustomerAddress = new CustomerAddressDto
                {
                    Address = new AddressDto()
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerAddress.Address.City, command);
        }

        [Fact]
        public void City_TooLong_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();
            var stateProvince = new StateProvinceBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetAddressTypeSpecification>()))
                .ReturnsAsync(addressType);

            var stateProvinceRepoMock = new Mock<IRepositoryBase<Domain.Person.StateProvince>>();
            stateProvinceRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetStateProvinceSpecification>()))
                .ReturnsAsync(stateProvince);

            var validator = new AddCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new AddCustomerAddressCommand
            {
                CustomerAddress = new CustomerAddressDto
                {
                    Address = new AddressDto
                    {
                        City = "aaaaabbbbbcccccdddddeeeeefffffggggg"
                    }
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerAddress.Address.City, command);
        }

        [Fact]
        public void StateProvinceCode_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();
            var stateProvince = new StateProvinceBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetAddressTypeSpecification>()))
                .ReturnsAsync(addressType);

            var stateProvinceRepoMock = new Mock<IRepositoryBase<Domain.Person.StateProvince>>();
            stateProvinceRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetStateProvinceSpecification>()))
                .ReturnsAsync(stateProvince);

            var validator = new AddCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new AddCustomerAddressCommand
            {
                CustomerAddress = new CustomerAddressDto
                {
                    Address = new AddressDto()
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerAddress.Address.StateProvinceCode, command);
        }

        [Fact]
        public void StateProvinceCode_TooLong_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();
            var stateProvince = new StateProvinceBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetAddressTypeSpecification>()))
                .ReturnsAsync(addressType);

            var stateProvinceRepoMock = new Mock<IRepositoryBase<Domain.Person.StateProvince>>();
            stateProvinceRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetStateProvinceSpecification>()))
                .ReturnsAsync(stateProvince);

            var validator = new AddCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new AddCustomerAddressCommand
            {
                CustomerAddress = new CustomerAddressDto
                {
                    Address = new AddressDto
                    {
                        StateProvinceCode = "aaaaa"
                    }
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerAddress.Address.StateProvinceCode, command);
        }
    }
}