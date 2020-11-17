using Ardalis.Specification;
using AW.Application.Customer.DeleteCustomerContact;
using AW.Application.Specifications;
using AW.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading;
using Xunit;

namespace AW.Application.UnitTests
{
    public class DeleteCustomerContactCommandHandlerUnitTests
    {
        [Fact]
        public async void Handle_Store_DeleteCustomerContact()
        {
            // Arrange
            var customer = new CustomerBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var handler = new DeleteCustomerContactCommandHandler(
                customerRepoMock.Object
            );

            //Act
            var command = new DeleteCustomerContactCommand
            {
                AccountNumber = customer.AccountNumber,
                ContactTypeName = "Owner",
                Contact = new ContactDto
                {
                    FirstName = "Orlando",
                    MiddleName = "N.",
                    LastName = "Gee"
                }
            };

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            customer.Store.BusinessEntityContacts.Count().Should().Be(0);
            customerRepoMock.Verify(mock => mock.UpdateAsync(It.IsAny<Domain.Sales.Customer>()), Times.Once);
        }
    }
}