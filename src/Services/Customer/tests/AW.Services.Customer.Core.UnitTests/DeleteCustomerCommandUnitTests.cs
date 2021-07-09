using AW.Services.Customer.Core.Handlers.DeleteCustomer;
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
    public class DeleteCustomerCommandUnitTests
    {
        [Fact]
        public async void Handle_ExistingCustomer_DeleteCustomer()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteCustomerCommandHandler>>();
            var customerRepoMock = new Mock<IRepository<Entities.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new IndividualCustomerBuilder()
                .WithTestValues()
                .Build()
            );

            var handler = new DeleteCustomerCommandHandler(
                loggerMock.Object,
                customerRepoMock.Object
            );

            //Act
            var command = new DeleteCustomerCommand { AccountNumber = "AW00011000" };
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customerRepoMock.Verify(x => x.DeleteAsync(
                It.IsAny<Entities.Customer>(),
                It.IsAny<CancellationToken>()    
            ));
        }

        [Fact]
        public void Handle_CustomerDoesNotExist_ThrowArgumentNullException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteCustomerCommandHandler>>();
            var customerRepoMock = new Mock<IRepository<Entities.Customer>>();

            var handler = new DeleteCustomerCommandHandler(
                loggerMock.Object,
                customerRepoMock.Object
            );

            //Act
            var command = new DeleteCustomerCommand();
            Func<Task> func = async () => await handler.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'customer')");
        }
    }
}