using Ardalis.Specification;
using AW.Services.Customer.Application.AddCustomer;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using Xunit;

namespace AW.Services.Customer.Application.UnitTests
{
    public class AddCustomerCommandUnitTests
    {
        [Fact]
        public async void Handle_NewCustomer_ReturnCustomer()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<AddCustomerCommandHandler>>();
            var customerRepoMock = new Mock<IRepositoryBase<Domain.Customer>>();

            var handler = new AddCustomerCommandHandler(
                loggerMock.Object,
                customerRepoMock.Object,
                mapper
            );

            //Act
            var command = new AddCustomerCommand
            {
                Customer = new IndividualCustomerDto { AccountNumber = "AW00011000" }
            };
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customerRepoMock.Verify(x => x.AddAsync(
                It.IsAny<Domain.Customer>(),
                It.IsAny<CancellationToken>()
            ));
        }
    }
}