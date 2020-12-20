using Ardalis.Specification;
using AW.Core.Application.Customer.DeleteCustomerAddress;
using AW.Core.Application.Specifications;
using AW.Core.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading;
using Xunit;

namespace AW.Core.Application.UnitTests
{
    public class DeleteCustomerAddressCommandHandlerUnitTests
    {
        [Fact]
        public async void Handle_Store_DeleteCustomerAddress()
        {
            // Arrange
            var customer = new CustomerBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
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

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
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