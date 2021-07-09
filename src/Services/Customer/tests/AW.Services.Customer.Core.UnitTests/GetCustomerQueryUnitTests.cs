using AW.Services.Customer.Core.Handlers.GetCustomer;
using AW.Services.Customer.Core.Specifications;
using AW.Services.Customer.Core.UnitTests.TestBuilders;
using AW.SharedKernel.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests
{
    public class GetCustomerQueryUnitTests
    {
        [Fact]
        public async void Handle_CustomerExists_ReturnCustomer()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var customer1 = new StoreCustomerBuilder()
                .WithTestValues()
                .Build();
            var customer2 = new IndividualCustomerBuilder()
                .WithTestValues()
                .Build();

            var loggerMock = new Mock<ILogger<GetCustomerQueryHandler>>();
            var customerRepoMock = new Mock<IRepository<Entities.Customer>>();
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
            var customerRepoMock = new Mock<IRepository<Entities.Customer>>();

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