using Ardalis.Specification;
using AW.Services.Customer.Application.GetCustomer;
using AW.Services.Customer.Application.Specifications;
using AW.Services.Customer.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Application.UnitTests
{
    public class GetCustomerQueryUnitTests
    {
        [Fact]
        public async void Handle_CustomerExists_ReturnCustomer()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var customer1 = new TestBuilders.StoreCustomerBuilder()
                .WithTestValues()
                .Build();
            var customer2 = new TestBuilders.IndividualCustomerBuilder()
                .WithTestValues()
                .Build();

            var loggerMock = new Mock<ILogger<GetCustomerQueryHandler>>();
            var customerRepoMock = new Mock<IRepositoryBase<Domain.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new StoreCustomerBuilder()
                .WithTestValues()
                .Build()
            );

            var handler = new GetCustomerQueryHandler(
                loggerMock.Object,
                mapper,
                customerRepoMock.Object
            );

            //Act
            var query = new GetCustomerQuery();
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customerRepoMock.Verify(x => x.GetBySpecAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Fact]
        public void Handle_CustomerNotFound_ThrowArgumentNullException()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();

            var loggerMock = new Mock<ILogger<GetCustomerQueryHandler>>();
            var customerRepoMock = new Mock<IRepositoryBase<Domain.Customer>>();

            var handler = new GetCustomerQueryHandler(
                loggerMock.Object,
                mapper,
                customerRepoMock.Object
            );

            //Act
            var query = new GetCustomerQuery();
            Func<Task> func = async () => await handler.Handle(query, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'customer')");
        }
    }
}