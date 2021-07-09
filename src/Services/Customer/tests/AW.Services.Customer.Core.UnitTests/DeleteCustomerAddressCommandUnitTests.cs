using AW.Services.Customer.Core.Handlers.DeleteCustomerAddress;
using AW.Services.Customer.Core.Specifications;
using AW.Services.Customer.Core.UnitTests.TestBuilders;
using AW.SharedKernel.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests
{
    public class DeleteCustomerAddressCommandUnitTests
    {
        [Fact]
        public async void Handle_ExistingCustomerAndAddress_DeleteCustomerAddress()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteCustomerAddressCommandHandler>>();
            var customerRepoMock = new Mock<IRepository<Entities.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()    
            ))
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
                    Address = new AddressDto
                    {
                        AddressLine1 = "3761 N. 14th St",
                        AddressLine2 = null,
                        PostalCode = "4700",
                        City = "Rockhampton",
                        StateProvinceCode = "QLD",
                        CountryRegionCode = "AU"
                    }
                }
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
            var loggerMock = new Mock<ILogger<DeleteCustomerAddressCommandHandler>>();
            var customerRepoMock = new Mock<IRepository<Entities.Customer>>();

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
            var customerRepoMock = new Mock<IRepository<Entities.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new IndividualCustomerBuilder()
                .WithTestValues()
                .Addresses(new List<Entities.CustomerAddress>())
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