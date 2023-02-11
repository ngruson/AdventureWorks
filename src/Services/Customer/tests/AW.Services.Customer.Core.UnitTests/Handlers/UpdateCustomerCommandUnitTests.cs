using AW.Services.Customer.Core.Specifications;
using AW.Services.Customer.Core.Handlers.UpdateCustomer;
using FluentAssertions;
using Moq;
using Xunit;
using AW.SharedKernel.UnitTesting;
using AutoFixture.Xunit2;
using AW.Services.SharedKernel.Interfaces;
using AW.Services.Customer.Core.Exceptions;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class UpdateCustomerCommandUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Handle_ExistingCustomer_ReturnUpdatedCustomer(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            UpdateCustomerCommandHandler sut,
            string accountNumber
        )
        {
            //Act
            var command = new UpdateCustomerCommand(new StoreCustomerDto
            {
                AccountNumber = accountNumber
            });

            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customerRepoMock.Verify(x => x.UpdateAsync(
                It.IsAny<Entities.Customer>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_CustomerDoesNotExist_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            UpdateCustomerCommandHandler sut,
            string accountNumber
        )
        {
            // Arrange
            var command = new UpdateCustomerCommand(new StoreCustomerDto
            {
                AccountNumber = accountNumber
            });

            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Customer?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<CustomerNotFoundException>()
                .WithMessage($"Customer {command.Customer!.AccountNumber} not found");
        }
    }
}