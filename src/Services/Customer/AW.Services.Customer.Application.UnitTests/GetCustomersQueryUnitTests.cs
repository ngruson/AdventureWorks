using Ardalis.Specification;
using AW.Services.Customer.Application.GetCustomers;
using AW.Services.Customer.Application.Specifications;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Application.UnitTests
{
    public class GetCustomersQueryUnitTests
    {
        [Fact]
        public async void Handle_CustomersExists_ReturnCustomers()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();            
            var customer1 = new TestBuilders.StoreCustomerBuilder()
                .WithTestValues()
                .Build();
            var customer2 = new TestBuilders.IndividualCustomerBuilder()
                .WithTestValues()
                .Build();

            var loggerMock = new Mock<ILogger<GetCustomersQueryHandler>>();
            var customerRepoMock = new Mock<IRepositoryBase<Domain.Customer>>();
            customerRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetCustomersPaginatedSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new List<Domain.Customer>
            {
                customer1,
                customer2
            });
            customerRepoMock.Setup(x => x.CountAsync(
                It.IsAny<CountCustomersSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(2);

            var handler = new GetCustomersQueryHandler(
                loggerMock.Object,
                mapper,
                customerRepoMock.Object
            );

            //Act
            var query = new GetCustomersQuery
            {
                PageIndex = 0,
                PageSize = 10,
                CustomerType = null,
                Territory = "",
                AccountNumber = ""
            };
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customerRepoMock.Verify(x => x.ListAsync(
                It.IsAny<ISpecification<Domain.Customer>>(),
                It.IsAny<CancellationToken>()
            ));
            result.TotalCustomers.Should().Be(2);
            result.Customers[0].Should().BeAssignableTo<StoreCustomerDto>();
            result.Customers[1].Should().BeAssignableTo<IndividualCustomerDto>();
        }

        [Fact]
        public void Handle_NoCustomersExists_ThrowArgumentNullException()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();

            var loggerMock = new Mock<ILogger<GetCustomersQueryHandler>>();
            var customerRepoMock = new Mock<IRepositoryBase<Domain.Customer>>();

            var handler = new GetCustomersQueryHandler(
                loggerMock.Object,
                mapper,
                customerRepoMock.Object
            );

            //Act
            var query = new GetCustomersQuery();
            Func<Task> func = async() => await handler.Handle(query, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'customers')");
        }
    }
}