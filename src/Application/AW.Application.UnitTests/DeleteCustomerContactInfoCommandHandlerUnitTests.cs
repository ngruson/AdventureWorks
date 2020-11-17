using Ardalis.Specification;
using AW.Application.Customer.DeleteCustomerContactInfo;
using AW.Application.Specifications;
using AW.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading;
using Xunit;

namespace AW.Application.UnitTests
{
    public class DeleteCustomerContactInfoCommandHandlerUnitTests
    {
        [Fact]
        public async void Handle_Person_Email_DeleteCustomerContactInfo()
        {
            // Arrange
            var customer = new CustomerBuilder()
                .Person(new PersonBuilder().WithTestValues().Build())
                .Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var handler = new DeleteCustomerContactInfoCommandHandler(
                customerRepoMock.Object
            );

            //Act
            var command = new DeleteCustomerContactInfoCommand
            {
                AccountNumber = customer.AccountNumber,
                CustomerContactInfo = new CustomerContactInfoDto
                {
                    Channel = ContactInfoChannelTypeDto.Email,
                    Value = "demo@adventure-works.com"
                }
                
            };

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            customer.Person.EmailAddresses.Count().Should().Be(0);
            customerRepoMock.Verify(mock => mock.UpdateAsync(It.IsAny<Domain.Sales.Customer>()), Times.Once);
        }

        [Fact]
        public async void Handle_Person_PhoneNumber_DeleteCustomerContactInfo()
        {
            // Arrange
            var customer = new CustomerBuilder()
                .Person(new PersonBuilder().WithTestValues().Build())
                .Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var handler = new DeleteCustomerContactInfoCommandHandler(
                customerRepoMock.Object
            );

            //Act
            var command = new DeleteCustomerContactInfoCommand
            {
                AccountNumber = customer.AccountNumber,
                CustomerContactInfo = new CustomerContactInfoDto
                {
                    Channel = ContactInfoChannelTypeDto.Phone,
                    ContactInfoType = "Cell",
                    Value = "245-555-0173"
                }
            };

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            customer.Person.PhoneNumbers.Count().Should().Be(0);
            customerRepoMock.Verify(mock => mock.UpdateAsync(It.IsAny<Domain.Sales.Customer>()), Times.Once);
        }
    }
}