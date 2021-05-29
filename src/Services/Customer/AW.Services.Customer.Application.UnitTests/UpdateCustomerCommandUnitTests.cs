using Ardalis.Specification;
using AW.Services.Customer.Application.Specifications;
using AW.Services.Customer.Application.UnitTests.TestBuilders;
using AW.Services.Customer.Application.UpdateCustomer;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Application.UnitTests
{
    public class UpdateCustomerCommandUnitTests
    {
        [Fact]
        public async void Handle_ExistingCustomer_ReturnUpdatedCustomer()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<UpdateCustomerCommandHandler>>();
            var customerRepoMock = new Mock<IRepositoryBase<Domain.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
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
            customerRepoMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Customer>()));
        }

        [Fact]
        public void Handle_CustomerDoesNotExist_ThrowArgumentNullException()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<UpdateCustomerCommandHandler>>();
            var customerRepoMock = new Mock<IRepositoryBase<Domain.Customer>>();

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