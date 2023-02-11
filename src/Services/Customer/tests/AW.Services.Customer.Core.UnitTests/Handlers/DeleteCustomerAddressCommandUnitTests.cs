using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Exceptions;
using AW.Services.Customer.Core.Handlers.DeleteCustomerAddress;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
{
    public class DeleteCustomerAddressCommandUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Handle_ExistingCustomerAndAddress_DeleteCustomerAddress(
            [Frozen] Entities.IndividualCustomer customer,
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            DeleteCustomerAddressCommandHandler sut,
            DeleteCustomerAddressCommand command,
            Entities.Address address
        )
        {
            //Arrange
            customer.AddAddress(
                new Entities.CustomerAddress(
                    command.AddressType,
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
            DeleteCustomerAddressCommandHandler sut,
            DeleteCustomerAddressCommand command
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
        public async Task Handle_AddressDoesNotExist_ThrowArgumentNullException(
            DeleteCustomerAddressCommandHandler sut,
            DeleteCustomerAddressCommand command
        )
        {
            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<AddressNotFoundException>()
                .WithMessage($"{command.AddressType} address for customer {command.AccountNumber} not found");
        }
    }
}