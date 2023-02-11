using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Exceptions;
using AW.Services.Customer.Core.Handlers.UpdateCustomerAddress;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class UpdateCustomerAddressCommandUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Handle_CustomerAndAddressExist_UpdateCustomerAddress(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            Entities.IndividualCustomer customer,
            UpdateCustomerAddressCommandHandler sut,
            UpdateCustomerAddressCommand command,
            Entities.Address address
        )
        {
            //Arrange
            customer.AddAddress(
                new Entities.CustomerAddress(
                    command.CustomerAddress!.AddressType!,
                    address
                )
            );

            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(customer);

            //Act
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
        public async Task Handle_UpdatedAddressDoesNotExist_UpdateCustomerAddress(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            Entities.IndividualCustomer customer,
            [Frozen] Mock<IRepository<Entities.Address>> addressRepoMock,
            UpdateCustomerAddressCommandHandler sut,
            UpdateCustomerAddressCommand command,
            Entities.Address address
        )
        {
            //Arrange
            addressRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetAddressSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Address?)null);

            customer.AddAddress(
                new Entities.CustomerAddress(
                    command.CustomerAddress!.AddressType!,
                    address
                )
            );

            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(customer);

            //Act
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
            UpdateCustomerAddressCommandHandler sut,
            UpdateCustomerAddressCommand command
        )
        {
            // Arrange
            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Customer?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<CustomerNotFoundException>()
                .WithMessage($"Customer {command.AccountNumber} not found");
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_CustomerAddressDoesNotExist_ThrowArgumentNullException(
            UpdateCustomerAddressCommandHandler sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<AddressNotFoundException>()
                .WithMessage($"{command.CustomerAddress!.AddressType} address for customer {command.AccountNumber} not found");
        }
    }
}
