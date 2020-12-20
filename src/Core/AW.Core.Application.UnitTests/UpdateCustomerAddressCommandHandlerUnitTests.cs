using AW.Core.Application.Customer.UpdateCustomerAddress;
using Ardalis.Specification;
using AW.Core.Application.Specifications;
using AW.Core.Application.UnitTests.AutoMapper;
using AW.Core.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Moq;
using System;
using System.Linq;
using System.Threading;
using Xunit;

namespace AW.Core.Application.UnitTests
{
    public class UpdateCustomerAddressCommandHandlerUnitTests
    {
        [Fact]
        public async void Handle_Store_ExistingAddress_UpdateCustomerAddress()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();
            var stateProvince = new StateProvinceBuilder().WithTestValues().Build();
            var address = new AddressBuilder()
                .Id(new Random().Next())
                .AddressLine1("6657 Sand Pointe Lane")
                .City("Seattle")
                .StateProvince(stateProvince)
                .PostalCode("98104")
                .Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();
            

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressRepoMock = new Mock<IRepositoryBase<Domain.Person.Address>>();
            addressRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetAddressSpecification>()))
                .ReturnsAsync(address);

            var addressTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetAddressTypeSpecification>()))
                .ReturnsAsync(addressType);

            var stateProvinceRepoMock = new Mock<IRepositoryBase<Domain.Person.StateProvince>>();
            stateProvinceRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetStateProvinceSpecification>()))
                .ReturnsAsync(stateProvince);

            var handler = new UpdateCustomerAddressCommandHandler(
                mapper,
                addressRepoMock.Object,
                addressTypeRepoMock.Object,
                customerRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            //Act

            var command = new UpdateCustomerAddressCommand
            {
                AccountNumber = customer.AccountNumber,
                CustomerAddress = new CustomerAddressDto
                {
                    AddressTypeName = addressType.Name,
                    Address = mapper.Map<AddressDto>(address)
                }                
            };

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customer.Store.BusinessEntityAddresses.First().AddressID.Should().Be(address.Id);
            customerRepoMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Sales.Customer>()));
        }

        [Fact]
        public async void Handle_Person_ExistingAddress_UpdateCustomerAddress()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder()
                .WithTestValues()
                .Store(null)
                .Person(new PersonBuilder().WithTestValues().Build())
                .Build();
            var stateProvince = new StateProvinceBuilder().WithTestValues().Build();
            var address = new AddressBuilder()
                .Id(new Random().Next())
                .AddressLine1("6657 Sand Pointe Lane")
                .City("Seattle")
                .StateProvince(stateProvince)
                .PostalCode("98104")
                .Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();


            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressRepoMock = new Mock<IRepositoryBase<Domain.Person.Address>>();
            addressRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetAddressSpecification>()))
                .ReturnsAsync(address);

            var addressTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetAddressTypeSpecification>()))
                .ReturnsAsync(addressType);

            var stateProvinceRepoMock = new Mock<IRepositoryBase<Domain.Person.StateProvince>>();
            stateProvinceRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetStateProvinceSpecification>()))
                .ReturnsAsync(stateProvince);

            var handler = new UpdateCustomerAddressCommandHandler(
                mapper,
                addressRepoMock.Object,
                addressTypeRepoMock.Object,
                customerRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            //Act

            var command = new UpdateCustomerAddressCommand
            {
                AccountNumber = customer.AccountNumber,
                CustomerAddress = new CustomerAddressDto
                {
                    AddressTypeName = addressType.Name,
                    Address = mapper.Map<AddressDto>(address)
                }
            };

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customer.Person.BusinessEntityAddresses.First().AddressID.Should().Be(address.Id);
            customerRepoMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Sales.Customer>()));
        }

        [Fact]
        public async void Handle_Store_NonExistingAddress_UpdateCustomerAddress()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();
            var stateProvince = new StateProvinceBuilder().WithTestValues().Build();
            var address = new AddressBuilder()
                .Id(new Random().Next())
                .AddressLine1("6657 Sand Pointe Lane")
                .City("Seattle")
                .StateProvince(stateProvince)
                .PostalCode("98104")
                .Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();


            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressRepoMock = new Mock<IRepositoryBase<Domain.Person.Address>>();

            var addressTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetAddressTypeSpecification>()))
                .ReturnsAsync(addressType);

            var stateProvinceRepoMock = new Mock<IRepositoryBase<Domain.Person.StateProvince>>();
            stateProvinceRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetStateProvinceSpecification>()))
                .ReturnsAsync(stateProvince);

            var handler = new UpdateCustomerAddressCommandHandler(
                mapper,
                addressRepoMock.Object,
                addressTypeRepoMock.Object,
                customerRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            //Act

            var command = new UpdateCustomerAddressCommand
            {
                AccountNumber = customer.AccountNumber,
                CustomerAddress = new CustomerAddressDto
                {
                    AddressTypeName = addressType.Name,
                    Address = mapper.Map<AddressDto>(address)
                }
            };

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customer.Store.BusinessEntityAddresses.First().AddressID.Should().Be(0);
            customer.Store.BusinessEntityAddresses.First().Address.Should().NotBeNull();
            customer.Store.BusinessEntityAddresses.First().Address.StateProvinceID.Should().Be(stateProvince.Id);
            customerRepoMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Sales.Customer>()));
        }

        [Fact]
        public async void Handle_Person_NonExistingAddress_UpdateCustomerAddress()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder()
                .WithTestValues()
                .Store(null)
                .Person(new PersonBuilder().WithTestValues().Build())
                .Build();
            var stateProvince = new StateProvinceBuilder().WithTestValues().Build();
            var address = new AddressBuilder()
                .Id(new Random().Next())
                .AddressLine1("6657 Sand Pointe Lane")
                .City("Seattle")
                .StateProvince(stateProvince)
                .PostalCode("98104")
                .Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();


            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressRepoMock = new Mock<IRepositoryBase<Domain.Person.Address>>();

            var addressTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetAddressTypeSpecification>()))
                .ReturnsAsync(addressType);

            var stateProvinceRepoMock = new Mock<IRepositoryBase<Domain.Person.StateProvince>>();
            stateProvinceRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetStateProvinceSpecification>()))
                .ReturnsAsync(stateProvince);

            var handler = new UpdateCustomerAddressCommandHandler(
                mapper,
                addressRepoMock.Object,
                addressTypeRepoMock.Object,
                customerRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            //Act

            var command = new UpdateCustomerAddressCommand
            {
                AccountNumber = customer.AccountNumber,
                CustomerAddress = new CustomerAddressDto
                {
                    AddressTypeName = addressType.Name,
                    Address = mapper.Map<AddressDto>(address)
                }
            };

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customer.Person.BusinessEntityAddresses.First().AddressID.Should().Be(0);
            customer.Person.BusinessEntityAddresses.First().Address.Should().NotBeNull();
            customer.Person.BusinessEntityAddresses.First().Address.StateProvinceID.Should().Be(stateProvince.Id);
            customerRepoMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Sales.Customer>()));
        }
    }
}