using Ardalis.Specification;
using AW.Core.Application.Customer.GetCustomers;
using AW.Core.Application.Specifications;
using AW.Core.Application.UnitTests.AutoMapper;
using AW.Core.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace AW.Core.Application.UnitTests
{
    public class GetCustomersQueryHandlerUnitTests
    {
        [Fact]
        public async void Handle_CustomersExists_ReturnCustomers()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var customers = new List<Domain.Sales.Customer>
            {
                new CustomerBuilder().AccountNumber("AW00000001").Build(),
                new CustomerBuilder().AccountNumber("AW00000002").Build(),
                new CustomerBuilder().AccountNumber("AW00000003").Build(),
                new CustomerBuilder().AccountNumber("AW00000004").Build(),
                new CustomerBuilder().AccountNumber("AW00000005").Build(),
                new CustomerBuilder().AccountNumber("AW00000006").Build(),
                new CustomerBuilder().AccountNumber("AW00000007").Build(),
                new CustomerBuilder().AccountNumber("AW00000008").Build(),
                new CustomerBuilder().AccountNumber("AW00000009").Build(),
                new CustomerBuilder().AccountNumber("AW00000010").Build()
            };

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.ListAsync(It.IsAny<GetCustomersPaginatedSpecification>()))
                .ReturnsAsync(customers.Take(5).ToList());
            customerRepoMock.Setup(x => x.CountAsync(It.IsAny<CountCustomersSpecification>()))
                .ReturnsAsync(10);

            var handler = new GetCustomersQueryHandler(
                customerRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetCustomersQuery
            {
                PageIndex = 0,
                PageSize = 5
            };

            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Customers.Count().Should().Be(5);
            result.TotalCustomers.Should().Be(10);
        }
    }
}