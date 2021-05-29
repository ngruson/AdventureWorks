using Ardalis.Specification;
using AW.Services.Customer.Application.DeleteCustomerAddress;
using AW.Services.Customer.Application.Specifications;
using AW.Services.Customer.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Application.UnitTests
{
    public class DeleteCustomerAddressCommandUnitTests
    {
        [Fact]
        public async void Handle_ExistingCustomerAndAddress_DeleteCustomerAddress()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteCustomerAddressCommandHandler>>();
            var customerRepoMock = new Mock<IRepositoryBase<Domain.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(new IndividualCustomerBuilder()
                    .WithTestValues()
                    .Build()
                );

            var handler = new DeleteCustomerAddressCommandHandler(
                loggerMock.Object,
                customerRepoMock.Object
            );

            //Act
            var command = new DeleteCustomerAddressCommand
            {
                AccountNumber = "AW00011000",
                CustomerAddress = new CustomerAddressDto
                {
                    AddressType = "Home",
                    Address = new AddressDto()
                }
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
            var loggerMock = new Mock<ILogger<DeleteCustomerAddressCommandHandler>>();
            var customerRepoMock = new Mock<IRepositoryBase<Domain.Customer>>();

            var handler = new DeleteCustomerAddressCommandHandler(
                loggerMock.Object,
                customerRepoMock.Object
            );

            //Act
            var command = new DeleteCustomerAddressCommand
            {
                AccountNumber = "AW00011000",
                CustomerAddress = new CustomerAddressDto
                {
                    AddressType = "Home",
                    Address = new AddressDto()
                }
            };
            Func<Task> func = async () => await handler.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'customer')");
        }

        [Fact]
        public void Handle_AddressDoesNotExist_ThrowArgumentNullException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteCustomerAddressCommandHandler>>();
            var customerRepoMock = new Mock<IRepositoryBase<Domain.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(new IndividualCustomerBuilder()
                    .WithTestValues()
                    .Addresses(new List<Domain.CustomerAddress>())
                    .Build()
                );

            var handler = new DeleteCustomerAddressCommandHandler(
                loggerMock.Object,
                customerRepoMock.Object
            );

            //Act
            var command = new DeleteCustomerAddressCommand
            {
                AccountNumber = "AW00011000",
                CustomerAddress = new CustomerAddressDto
                {
                    AddressType = "Home",
                    Address = new AddressDto()
                }
            };
            Func<Task> func = async () => await handler.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'customerAddress')");
        }
    }
}