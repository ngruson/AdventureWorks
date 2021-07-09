using AW.Services.Customer.Core.Handlers.AddCustomer;
using AW.SharedKernel.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests
{
    public class AddCustomerCommandUnitTests
    {
        [Fact]
        public async void Handle_NewCustomer_ReturnCustomer()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<AddCustomerCommandHandler>>();
            var customerRepoMock = new Mock<IRepository<Entities.Customer>>();

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
                It.IsAny<Entities.Customer>(),
                It.IsAny<CancellationToken>()
            ));
        }
    }
}