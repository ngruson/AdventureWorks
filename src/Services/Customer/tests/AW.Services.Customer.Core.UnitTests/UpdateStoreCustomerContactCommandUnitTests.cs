using AW.Services.Customer.Core.Handlers.UpdateStoreCustomerContact;
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
    public class UpdateStoreCustomerContactCommandUnitTests
    {
        [Fact]
        public async void Handle_CustomerAndContactExist_UpdateStoreCustomerContact()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<UpdateStoreCustomerContactCommandHandler>>();
            var customerRepoMock = new Mock<IRepository<Entities.StoreCustomer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetStoreCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new StoreCustomerBuilder()
                .WithTestValues()
                .Build()
            );

            var handler = new UpdateStoreCustomerContactCommandHandler(
                loggerMock.Object,
                mapper,
                customerRepoMock.Object
            );

            //Act
            var command = new UpdateStoreCustomerContactCommand
            {
                AccountNumber = "AW00000001",
                CustomerContact = new StoreCustomerContactDto
                {
                    ContactType = "Owner",
                    ContactPerson = new PersonDto
                    {
                        Title = "Mr.",
                        FirstName = "Orlando",
                        MiddleName = "N.",
                        LastName = "Gee"
                    }
                }
            };
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customerRepoMock.Verify(x => x.UpdateAsync(
                It.IsAny<Entities.StoreCustomer>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Fact]
        public void Handle_CustomerDoesNotExist_ThrowArgumentNullException()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<UpdateStoreCustomerContactCommandHandler>>();
            var customerRepoMock = new Mock<IRepository<Entities.StoreCustomer>>();

            var handler = new UpdateStoreCustomerContactCommandHandler(
                loggerMock.Object,
                mapper,
                customerRepoMock.Object
            );

            //Act
            var command = new UpdateStoreCustomerContactCommand
            {
                AccountNumber = "AW00000001",
                CustomerContact = new StoreCustomerContactDto
                {
                    ContactType = "Order Administrator",
                    ContactPerson = new PersonDto
                    {
                        Title = "Mr.",
                        FirstName = "Orlando",
                        MiddleName = "N.",
                        LastName = "Gee"
                    }
                }
            };
            Func<Task> func = async () => await handler.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'storeCustomer')");
        }

        [Fact]
        public void Handle_ContactDoesNotExist_ThrowArgumentNullException()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<UpdateStoreCustomerContactCommandHandler>>();
            var customerRepoMock = new Mock<IRepository<Entities.StoreCustomer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetStoreCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new StoreCustomerBuilder()
                .WithTestValues()
                .Contacts(new List<Entities.StoreCustomerContact>())
                .Build()
            );

            var handler = new UpdateStoreCustomerContactCommandHandler(
                loggerMock.Object,
                mapper,
                customerRepoMock.Object
            );

            //Act
            var command = new UpdateStoreCustomerContactCommand
            {
                AccountNumber = "AW00000001",
                CustomerContact = new StoreCustomerContactDto
                {
                    ContactType = "Order Administrator",
                    ContactPerson = new PersonDto
                    {
                        Title = "Mr.",
                        FirstName = "Orlando",
                        MiddleName = "N.",
                        LastName = "Gee"
                    }
                }
            };
            Func<Task> func = async () => await handler.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'contact')");
        }
    }
}