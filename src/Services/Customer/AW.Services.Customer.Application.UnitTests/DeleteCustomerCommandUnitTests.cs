using Ardalis.Specification;
using AW.Services.Customer.Application.DeleteCustomer;
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
    public class DeleteCustomerCommandUnitTests
    {
        [Fact]
        public async void Handle_ExistingCustomer_DeleteCustomer()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteCustomerCommandHandler>>();
            var customerRepoMock = new Mock<IRepositoryBase<Domain.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(new IndividualCustomerBuilder()
                    .WithTestValues()
                    .Build()
                );

            var handler = new DeleteCustomerCommandHandler(
                loggerMock.Object,
                customerRepoMock.Object
            );

            //Act
            var command = new DeleteCustomerCommand();
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customerRepoMock.Verify(x => x.DeleteAsync(It.IsAny<Domain.Customer>()));
        }

        [Fact]
        public void Handle_CustomerDoesNotExist_ThrowArgumentNullException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteCustomerCommandHandler>>();
            var customerRepoMock = new Mock<IRepositoryBase<Domain.Customer>>();

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