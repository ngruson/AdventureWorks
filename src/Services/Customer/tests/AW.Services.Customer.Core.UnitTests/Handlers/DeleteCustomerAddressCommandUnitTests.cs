using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Handlers.DeleteCustomerAddress;
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
    public class DeleteCustomerAddressCommandUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Handle_ExistingCustomerAndAddress_DeleteCustomerAddress(
            [Frozen] Entities.Customer customer,
            [Frozen] Mock<IRepository<Entities.Customer>> customerRepoMock,
            DeleteCustomerAddressCommandHandler sut,
            DeleteCustomerAddressCommand command
        )
        {
            //Arrange
            customer.Addresses = new List<Entities.CustomerAddress>
            {
                new Entities.CustomerAddress
                {
                    AddressType = command.AddressType
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
            DeleteCustomerAddressCommandHandler sut,
            DeleteCustomerAddressCommand command
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
        public void Handle_AddressDoesNotExist_ThrowArgumentNullException(
            DeleteCustomerAddressCommandHandler sut,
            DeleteCustomerAddressCommand command
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