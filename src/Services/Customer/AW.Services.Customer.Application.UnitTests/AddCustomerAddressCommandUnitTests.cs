using Ardalis.Specification;
using AW.Services.Customer.Application.AddCustomerAddress;
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
    public class AddCustomerAddressCommandUnitTests
    {
        [Fact]
        public async void Handle_CustomerExist_AddCustomerAddress()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<AddCustomerAddressCommandHandler>>();
            var customerRepoMock = new Mock<IRepositoryBase<Domain.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(new IndividualCustomerBuilder()
                    .WithTestValues()
                    .Build()
                );

            var handler = new AddCustomerAddressCommandHandler(
                loggerMock.Object,
                mapper,
                customerRepoMock.Object
            );

            //Act
            var command = new AddCustomerAddressCommand
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
            customerRepoMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Customer>()));
        }

        [Fact]
        public void Handle_CustomerDoesNotExist_ThrowArgumentNullException()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<AddCustomerAddressCommandHandler>>();
            var customerRepoMock = new Mock<IRepositoryBase<Domain.Customer>>();

            var handler = new AddCustomerAddressCommandHandler(
                loggerMock.Object,
                mapper,
                customerRepoMock.Object
            );

            //Act
            var command = new AddCustomerAddressCommand
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
    }
}