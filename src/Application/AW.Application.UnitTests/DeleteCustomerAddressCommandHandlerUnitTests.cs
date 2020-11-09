﻿using AW.Application.Customer.DeleteCustomerAddress;
using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading;
using Xunit;

namespace AW.Application.UnitTests
{
    public class DeleteCustomerAddressCommandHandlerUnitTests
    {
        [Fact]
        public async void Handle_Store_DeleteCustomerAddress()
        {
            // Arrange
            var customer = new CustomerBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var handler = new DeleteCustomerAddressCommandHandler(
                customerRepoMock.Object
            );

            //Act
            var command = new DeleteCustomerAddressCommand
            {
                AccountNumber = customer.AccountNumber,
                AddressTypeName = "Main Office"
            };
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            customer.Store.BusinessEntityAddresses.Count().Should().Be(0);
            customerRepoMock.Verify(mock => mock.UpdateAsync(It.IsAny<Domain.Sales.Customer>()), Times.Once);
        }

        [Fact]
        public async void Handle_Person_DeleteCustomerAddress()
        {
            // Arrange
            var customer = new CustomerBuilder()
                .Person(new PersonBuilder().WithTestValues().Build())
                .Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var handler = new DeleteCustomerAddressCommandHandler(
                customerRepoMock.Object
            );

            //Act
            var command = new DeleteCustomerAddressCommand
            {
                AccountNumber = customer.AccountNumber,
                AddressTypeName = "Main Office"
            };
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            customer.Person.BusinessEntityAddresses.Count().Should().Be(0);
            customerRepoMock.Verify(mock => mock.UpdateAsync(It.IsAny<Domain.Sales.Customer>()), Times.Once);
        }
    }
}