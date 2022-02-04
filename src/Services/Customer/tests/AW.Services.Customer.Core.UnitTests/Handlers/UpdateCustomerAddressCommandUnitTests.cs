using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Handlers.UpdateCustomerAddress;
using AW.Services.Customer.Core.Specifications;
using AW.SharedKernel.Interfaces;
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
    public class UpdateCustomerAddressCommandUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Handle_CustomerAndAddressExist_UpdateCustomerAddress(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            Entities.Customer customer,
            UpdateCustomerAddressCommandHandler sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            customer.Addresses = new List<Entities.CustomerAddress>
            {
                new Entities.CustomerAddress
                {
                    AddressType = command.CustomerAddress.AddressType
                }
            };

            customerRepoMock.Setup(x => x.GetBySpecAsync(
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
            Entities.Customer customer,
            [Frozen] Mock<IRepository<Entities.Address>> addressRepoMock,
            UpdateCustomerAddressCommandHandler sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Arrange
            addressRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetAddressSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Address)null);

            customer.Addresses = new List<Entities.CustomerAddress>
            {
                new Entities.CustomerAddress
                {
                    AddressType = command.CustomerAddress.AddressType
                }
            };

            customerRepoMock.Setup(x => x.GetBySpecAsync(
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
        public void Handle_CustomerDoesNotExist_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            UpdateCustomerAddressCommandHandler sut,
            UpdateCustomerAddressCommand command
        )
        {
            // Arrange
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Customer)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'customer')");
        }

        [Theory]
        [AutoMoqData]
        public void Handle_CustomerAddressDoesNotExist_ThrowArgumentNullException(
            UpdateCustomerAddressCommandHandler sut,
            UpdateCustomerAddressCommand command
        )
        {
            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'customerAddress')");
        }
    }
}