using AW.Services.Customer.Core.Handlers.UpdateCustomerAddress;
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
    public class UpdateCustomerAddressCommandUnitTests
    {
        [Fact]
        public async void Handle_CustomerAndAddressExist_UpdateCustomerAddress()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<UpdateCustomerAddressCommandHandler>>();
            
            var customerRepoMock = new Mock<IRepository<Entities.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new IndividualCustomerBuilder()
                .WithTestValues()
                .Build()
            );

            var addressRepoMock = new Mock<IRepository<Entities.Address>>();
            addressRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetAddressSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new AddressBuilder()
                .WithTestValues()
                .Build()
            );

            var handler = new UpdateCustomerAddressCommandHandler(
                loggerMock.Object,
                mapper,
                customerRepoMock.Object,
                addressRepoMock.Object
            );

            //Act
            var command = new UpdateCustomerAddressCommand
            {
                AccountNumber = "AW00011000",
                CustomerAddress = new CustomerAddressDto
                {
                    AddressType = "Home",
                    Address = new AddressDto
                    {
                        AddressLine1 = "3761 N. 14th St",
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
        public async void Handle_UpdatedAddressDoesNotExist_UpdateCustomerAddress()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<UpdateCustomerAddressCommandHandler>>();

            var customerRepoMock = new Mock<IRepository<Entities.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new IndividualCustomerBuilder()
                .WithTestValues()
                .Build()
            );

            var addressRepoMock = new Mock<IRepository<Entities.Address>>();

            var handler = new UpdateCustomerAddressCommandHandler(
                loggerMock.Object,
                mapper,
                customerRepoMock.Object,
                addressRepoMock.Object
            );

            //Act
            var command = new UpdateCustomerAddressCommand
            {
                AccountNumber = "AW00011000",
                CustomerAddress = new CustomerAddressDto
                {
                    AddressType = "Home",
                    Address = new AddressDto
                    {
                        AddressLine1 = "3761 N. 14th St",
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
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<UpdateCustomerAddressCommandHandler>>();
            var customerRepoMock = new Mock<IRepository<Entities.Customer>>();
            var addressRepoMock = new Mock<IRepository<Entities.Address>>();

            var handler = new UpdateCustomerAddressCommandHandler(
                loggerMock.Object,
                mapper,
                customerRepoMock.Object,
                addressRepoMock.Object
            );

            //Act
            var command = new UpdateCustomerAddressCommand
            {
                AccountNumber = "AW00011000",
                CustomerAddress = new CustomerAddressDto
                {
                    AddressType = "Home",
                    Address = new AddressDto
                    {
                        AddressLine1 = "3761 N. 14th St",
                        PostalCode = "4700",
                        City = "Rockhampton",
                        StateProvinceCode = "QLD",
                        CountryRegionCode = "AU"
                    }
                }
            };
            Func<Task> func = async () => await handler.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'customer')");
        }

        [Fact]
        public void Handle_CustomerAddressDoesNotExist_ThrowArgumentNullException()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<UpdateCustomerAddressCommandHandler>>();

            var customerRepoMock = new Mock<IRepository<Entities.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new IndividualCustomerBuilder()
                .WithTestValues()
                .Addresses(new System.Collections.Generic.List<Entities.CustomerAddress>())
                .Build()
            );

            var addressRepoMock = new Mock<IRepository<Entities.Address>>();
            addressRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetAddressSpecification>(),
                It.IsAny<CancellationToken>()    
            ))
            .ReturnsAsync(new AddressBuilder()
                .WithTestValues()
                .Build()
            );

            var handler = new UpdateCustomerAddressCommandHandler(
                loggerMock.Object,
                mapper,
                customerRepoMock.Object,
                addressRepoMock.Object
            );

            //Act
            var command = new UpdateCustomerAddressCommand
            {
                AccountNumber = "AW00011000",
                CustomerAddress = new CustomerAddressDto
                {
                    AddressType = "Home",
                    Address = new AddressDto
                    {
                        AddressLine1 = "3761 N. 14th St",
                        PostalCode = "4700",
                        City = "Rockhampton",
                        StateProvinceCode = "QLD",
                        CountryRegionCode = "AU"
                    }
                }
            };
            Func<Task> func = async () => await handler.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'customerAddress')");
        }
    }
}