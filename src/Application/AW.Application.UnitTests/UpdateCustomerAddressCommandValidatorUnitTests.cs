using AW.Application.Customer.UpdateCustomerAddress;
using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Application.UnitTests.AutoMapper;
using AW.Application.UnitTests.TestBuilders;
using FluentValidation.TestHelper;
using Moq;
using System.Linq;
using Xunit;

namespace AW.Application.UnitTests
{
    public class UpdateCustomerAddressCommandValidatorUnitTests
    {
        [Fact]
        public void AccountNumber_Empty_ValidationError()
        {
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();
            var address = new AddressBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.AddressType>>();
            var stateProvinceRepoMock = new Mock<IAsyncRepository<Domain.Person.StateProvince>>();

            var validator = new UpdateCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new UpdateCustomerAddressCommand();

            validator.ShouldHaveValidationErrorFor(x => x.AccountNumber, command);
        }

        [Fact]
        public void AccountNumber_TooLong_ValidationError()
        {
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();
            var address = new AddressBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.AddressType>>();
            var stateProvinceRepoMock = new Mock<IAsyncRepository<Domain.Person.StateProvince>>();

            var validator = new UpdateCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new UpdateCustomerAddressCommand
            {
                AccountNumber = "a".PadRight(11)
            };

            validator.ShouldHaveValidationErrorFor(x => x.AccountNumber, command);
        }

        [Fact]
        public void CustomerAddress_Null_ValidationError()
        {
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);
                
            var addressTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.AddressType>>();
            var stateProvinceRepoMock = new Mock<IAsyncRepository<Domain.Person.StateProvince>>();

            var validator = new UpdateCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new UpdateCustomerAddressCommand();

            validator.ShouldHaveValidationErrorFor(x => x.CustomerAddress, command);
        }

        [Fact]
        public void AddressTypeName_Empty_ValidationError()
        {
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();
            var address = new AddressBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.AddressType>>();
            var stateProvinceRepoMock = new Mock<IAsyncRepository<Domain.Person.StateProvince>>();

            var validator = new UpdateCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new UpdateCustomerAddressCommand
            {
                CustomerAddress = new CustomerAddressDto()
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerAddress.AddressTypeName, command);
        }

        [Fact]
        public void AddressTypeName_DoesNotExist_ValidationError()
        {
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();
            var address = new AddressBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.AddressType>>();
            var stateProvinceRepoMock = new Mock<IAsyncRepository<Domain.Person.StateProvince>>();

            var validator = new UpdateCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new UpdateCustomerAddressCommand
            {
                CustomerAddress = new CustomerAddressDto
                {
                    AddressTypeName = "DoesNotExist"
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerAddress.AddressTypeName, command);
        }

        [Fact]
        public void Address_Null_ValidationError()
        {
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.AddressType>>();
            var stateProvinceRepoMock = new Mock<IAsyncRepository<Domain.Person.StateProvince>>();

            var validator = new UpdateCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new UpdateCustomerAddressCommand
            {
                CustomerAddress = new CustomerAddressDto()
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerAddress.Address, command);
        }

        [Fact]
        public void AddressLine1_Empty_ValidationError()
        {
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.AddressType>>();
            var stateProvinceRepoMock = new Mock<IAsyncRepository<Domain.Person.StateProvince>>();

            var validator = new UpdateCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new UpdateCustomerAddressCommand
            {
                CustomerAddress = new CustomerAddressDto 
                { 
                    Address = new AddressDto()
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerAddress.Address.AddressLine1, command);
        }

        [Fact]
        public void AddressLine1_TooLong_ValidationError()
        {
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.AddressType>>();
            var stateProvinceRepoMock = new Mock<IAsyncRepository<Domain.Person.StateProvince>>();

            var validator = new UpdateCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new UpdateCustomerAddressCommand
            {
                CustomerAddress = new CustomerAddressDto
                {
                    Address = new AddressDto 
                    {
                        AddressLine1 = "a".PadRight(61)
                    }
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerAddress.Address.AddressLine1, command);
        }

        [Fact]
        public void AddressLine2_TooLong_ValidationError()
        {
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.AddressType>>();
            var stateProvinceRepoMock = new Mock<IAsyncRepository<Domain.Person.StateProvince>>();

            var validator = new UpdateCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new UpdateCustomerAddressCommand
            {
                CustomerAddress = new CustomerAddressDto
                {
                    Address = new AddressDto
                    {
                        AddressLine2 = "a".PadRight(61)
                    }
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerAddress.Address.AddressLine2, command);
        }

        [Fact]
        public void PostalCode_Empty_ValidationError()
        {
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.AddressType>>();
            var stateProvinceRepoMock = new Mock<IAsyncRepository<Domain.Person.StateProvince>>();

            var validator = new UpdateCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new UpdateCustomerAddressCommand
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
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.AddressType>>();
            var stateProvinceRepoMock = new Mock<IAsyncRepository<Domain.Person.StateProvince>>();

            var validator = new UpdateCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new UpdateCustomerAddressCommand
            {
                CustomerAddress = new CustomerAddressDto
                {
                    Address = new AddressDto
                    {
                        PostalCode = "a".PadRight(16)
                    }
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerAddress.Address.PostalCode, command);
        }

        [Fact]
        public void City_Empty_ValidationError()
        {
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.AddressType>>();
            var stateProvinceRepoMock = new Mock<IAsyncRepository<Domain.Person.StateProvince>>();

            var validator = new UpdateCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new UpdateCustomerAddressCommand
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
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.AddressType>>();
            var stateProvinceRepoMock = new Mock<IAsyncRepository<Domain.Person.StateProvince>>();

            var validator = new UpdateCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new UpdateCustomerAddressCommand
            {
                CustomerAddress = new CustomerAddressDto
                {
                    Address = new AddressDto
                    {
                        City = "a".PadRight(31)
                    }
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerAddress.Address.City, command);
        }

        [Fact]
        public void StateProvinceCode_Empty_ValidationError()
        {
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.AddressType>>();
            var stateProvinceRepoMock = new Mock<IAsyncRepository<Domain.Person.StateProvince>>();

            var validator = new UpdateCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new UpdateCustomerAddressCommand
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
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.AddressType>>();
            var stateProvinceRepoMock = new Mock<IAsyncRepository<Domain.Person.StateProvince>>();

            var validator = new UpdateCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new UpdateCustomerAddressCommand
            {
                CustomerAddress = new CustomerAddressDto
                {
                    Address = new AddressDto
                    {
                        StateProvinceCode = "a".PadRight(4)
                    }
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerAddress.Address.StateProvinceCode, command);
        }

        [Fact]
        public void StateProvinceCode_DoesNotExist_ValidationError()
        {
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.AddressType>>();
            var stateProvinceRepoMock = new Mock<IAsyncRepository<Domain.Person.StateProvince>>();

            var validator = new UpdateCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            var command = new UpdateCustomerAddressCommand
            {
                CustomerAddress = new CustomerAddressDto
                {
                    Address = new AddressDto
                    {
                        StateProvinceCode = "ABC"
                    }
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerAddress.Address.StateProvinceCode, command);
        }
    }
}