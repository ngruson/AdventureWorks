using Ardalis.Specification;
using AW.Core.Application.Customer.GetCustomer;
using AW.Core.Application.Specifications;
using AW.Core.Application.UnitTests.AutoMapper;
using AW.Core.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Moq;
using System.Threading;
using Xunit;

namespace AW.Core.Application.UnitTests
{
    public class GetCustomerQueryHandlerUnitTests
    {
        [Fact]
        public async void Handle_CustomerExists_ReturnCustomer()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var handler = new GetCustomerQueryHandler(
                customerRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetCustomerQuery
            {
                AccountNumber = customer.AccountNumber,
            };

            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
        }
    }
}