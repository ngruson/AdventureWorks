using AW.Services.Customer.Core.Specifications;
using AW.Services.Customer.Core.UnitTests.TestBuilders;
using AW.Services.Customer.Core.Handlers.UpdateCustomer;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AW.SharedKernel.Interfaces;

namespace AW.Services.Customer.Core.UnitTests
{
    public class UpdateCustomerCommandUnitTests
    {
        [Fact]
        public async void Handle_ExistingCustomer_ReturnUpdatedCustomer()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<UpdateCustomerCommandHandler>>();
            var customerRepoMock = new Mock<IRepository<Entities.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new IndividualCustomerBuilder()
                .WithTestValues()
                .Build()
            );

            var handler = new UpdateCustomerCommandHandler(
                loggerMock.Object,
                customerRepoMock.Object,
                mapper
            );

            //Act
            var command = new UpdateCustomerCommand
            {
                Customer = new IndividualCustomerDto { AccountNumber = "AW00011000" }
            };
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customerRepoMock.Verify(x => x.UpdateAsync(
                It.IsAny<Entities.Customer>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Fact]
        public void Handle_CustomerDoesNotExist_ThrowArgumentNullException()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<UpdateCustomerCommandHandler>>();
            var customerRepoMock = new Mock<IRepository<Entities.Customer>>();

            var handler = new UpdateCustomerCommandHandler(
                loggerMock.Object,
                customerRepoMock.Object,
                mapper
            );

            //Act
            var command = new UpdateCustomerCommand
            {
                Customer = new IndividualCustomerDto { AccountNumber = "AW00011000" }
            };
            Func<Task> func = async () => await handler.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'customer')");
        }
    }
}