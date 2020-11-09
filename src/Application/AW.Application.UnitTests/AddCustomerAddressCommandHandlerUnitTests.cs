﻿using AW.Application.Customer.AddCustomerAddress;
using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Application.UnitTests.AutoMapper;
using AW.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading;
using Xunit;

namespace AW.Application.UnitTests
{
    public class AddCustomerAddressCommandHandlerUnitTests
    {
        [Fact]
        public async void Handle_Store_AddressExist_AddCustomerAddress()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var stateProvince = new StateProvinceBuilder().WithTestValues().Build();
            var address = new AddressBuilder()
                .WithTestValues()
                .StateProvince(stateProvince)
                .Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();
            var customer = new CustomerBuilder().WithTestValues().Build();
            

            var addressRepoMock = new Mock<IAsyncRepository<Domain.Person.Address>>();
            addressRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetAddressSpecification>()))
                .ReturnsAsync(address);

            var addressTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetAddressTypeSpecification>()))
                .ReturnsAsync(addressType);

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var stateProvinceRepoMock = new Mock<IAsyncRepository<Domain.Person.StateProvince>>();
            stateProvinceRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetStateProvinceSpecification>()))
                .ReturnsAsync(stateProvince);

            var handler = new AddCustomerAddressCommandHandler(
                mapper,
                addressRepoMock.Object,
                addressTypeRepoMock.Object,
                customerRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            //Act
            var command = new AddCustomerAddressCommand
            {
                AccountNumber = customer.AccountNumber,
                CustomerAddress = mapper.Map<CustomerAddressDto>(
                    new BusinessEntityAddressBuilder().WithTestValues().Build()
                )
            };
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            customer.Store.BusinessEntityAddresses.Count().Should().Be(2);
            customer.Store.BusinessEntityAddresses.Last().AddressID.Should().Be(address.Id);
            customer.Store.BusinessEntityAddresses.Last().Address.Should().BeNull();
            customerRepoMock.Verify(mock => mock.UpdateAsync(It.IsAny<Domain.Sales.Customer>()), Times.Once);
        }

        [Fact]
        public async void Handle_Store_AddressDoesNotExist_AddCustomerAddress()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var stateProvince = new StateProvinceBuilder().WithTestValues().Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();
            var customer = new CustomerBuilder().WithTestValues().Build();

            var addressRepoMock = new Mock<IAsyncRepository<Domain.Person.Address>>();

            var addressTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetAddressTypeSpecification>()))
                .ReturnsAsync(addressType);

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var stateProvinceRepoMock = new Mock<IAsyncRepository<Domain.Person.StateProvince>>();
            stateProvinceRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetStateProvinceSpecification>()))
                .ReturnsAsync(stateProvince);

            var handler = new AddCustomerAddressCommandHandler(
                mapper,
                addressRepoMock.Object,
                addressTypeRepoMock.Object,
                customerRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            //Act
            var command = new AddCustomerAddressCommand
            {
                AccountNumber = customer.AccountNumber,
                CustomerAddress = mapper.Map<CustomerAddressDto>(
                    new BusinessEntityAddressBuilder().WithTestValues().Build()
                )
            };
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            customer.Store.BusinessEntityAddresses.Count().Should().Be(2);
            customer.Store.BusinessEntityAddresses.Last().AddressID.Should().Be(0);
            customer.Store.BusinessEntityAddresses.Last().Address.Should().NotBeNull();
            customer.Store.BusinessEntityAddresses.Last().Address.StateProvinceID.Should().Be(stateProvince.Id);
            customerRepoMock.Verify(mock => mock.UpdateAsync(It.IsAny<Domain.Sales.Customer>()));
        }

        [Fact]
        public async void Handle_Person_AddressExist_AddCustomerAddress()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var stateProvince = new StateProvinceBuilder().WithTestValues().Build();
            var address = new AddressBuilder()
                .WithTestValues()
                .StateProvince(stateProvince)
                .Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();
            var customer = new CustomerBuilder()
                .AccountNumber("AW00011000")
                .Person(new PersonBuilder().WithTestValues().Build())
                .Build();


            var addressRepoMock = new Mock<IAsyncRepository<Domain.Person.Address>>();
            addressRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetAddressSpecification>()))
                .ReturnsAsync(address);

            var addressTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetAddressTypeSpecification>()))
                .ReturnsAsync(addressType);

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var stateProvinceRepoMock = new Mock<IAsyncRepository<Domain.Person.StateProvince>>();
            stateProvinceRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetStateProvinceSpecification>()))
                .ReturnsAsync(stateProvince);

            var handler = new AddCustomerAddressCommandHandler(
                mapper,
                addressRepoMock.Object,
                addressTypeRepoMock.Object,
                customerRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            //Act
            var command = new AddCustomerAddressCommand
            {
                AccountNumber = customer.AccountNumber,
                CustomerAddress = mapper.Map<CustomerAddressDto>(
                    new BusinessEntityAddressBuilder().WithTestValues().Build()
                )
            };
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            customer.Person.BusinessEntityAddresses.Count().Should().Be(2);
            customer.Person.BusinessEntityAddresses.Last().AddressID.Should().Be(address.Id);
            customer.Person.BusinessEntityAddresses.Last().Address.Should().BeNull();
            customerRepoMock.Verify(mock => mock.UpdateAsync(It.IsAny<Domain.Sales.Customer>()), Times.Once);
        }

        [Fact]
        public async void Handle_Person_AddressDoesNotExist_AddCustomerAddress()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var stateProvince = new StateProvinceBuilder().WithTestValues().Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();
            var customer = new CustomerBuilder()
                .AccountNumber("AW00011000")
                .Person(new PersonBuilder().WithTestValues().Build())
                .Build();

            var addressRepoMock = new Mock<IAsyncRepository<Domain.Person.Address>>();

            var addressTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetAddressTypeSpecification>()))
                .ReturnsAsync(addressType);

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var stateProvinceRepoMock = new Mock<IAsyncRepository<Domain.Person.StateProvince>>();
            stateProvinceRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetStateProvinceSpecification>()))
                .ReturnsAsync(stateProvince);

            var handler = new AddCustomerAddressCommandHandler(
                mapper,
                addressRepoMock.Object,
                addressTypeRepoMock.Object,
                customerRepoMock.Object,
                stateProvinceRepoMock.Object
            );

            //Act
            var command = new AddCustomerAddressCommand
            {
                AccountNumber = customer.AccountNumber,
                CustomerAddress = mapper.Map<CustomerAddressDto>(
                    new BusinessEntityAddressBuilder().WithTestValues().Build()
                )
            };
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            customer.Person.BusinessEntityAddresses.Count().Should().Be(2);
            customer.Person.BusinessEntityAddresses.Last().AddressID.Should().Be(0);
            customer.Person.BusinessEntityAddresses.Last().Address.Should().NotBeNull();
            customer.Person.BusinessEntityAddresses.Last().Address.StateProvinceID.Should().Be(stateProvince.Id);
            customerRepoMock.Verify(mock => mock.UpdateAsync(It.IsAny<Domain.Sales.Customer>()));
        }
    }
}