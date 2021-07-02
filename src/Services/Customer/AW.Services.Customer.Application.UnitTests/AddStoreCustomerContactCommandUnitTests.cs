using Ardalis.Specification;
using AW.Services.Customer.Application.AddStoreCustomerContact;
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
    public class AddStoreCustomerContactCommandUnitTests
    {
        [Fact]
        public async void Handle_CustomerExist_AddStoreCustomerContact()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<AddStoreCustomerContactCommandHandler>>();
            var customerRepoMock = new Mock<IRepositoryBase<Domain.StoreCustomer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetStoreCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new StoreCustomerBuilder()
                .WithTestValues()
                .Build()
            );

            var handler = new AddStoreCustomerContactCommandHandler(
                loggerMock.Object,
                mapper,
                customerRepoMock.Object
            );

            //Act
            var command = new AddStoreCustomerContactCommand
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
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customerRepoMock.Verify(x => x.UpdateAsync(
                It.IsAny<Domain.StoreCustomer>(),
                It.IsAny<CancellationToken>()    
            ));
        }

        [Fact]
        public void Handle_CustomerDoesNotExist_ThrowArgumentNullException()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var loggerMock = new Mock<ILogger<AddStoreCustomerContactCommandHandler>>();
            var customerRepoMock = new Mock<IRepositoryBase<Domain.StoreCustomer>>();

            var handler = new AddStoreCustomerContactCommandHandler(
                loggerMock.Object,
                mapper,
                customerRepoMock.Object
            );

            //Act
            var command = new AddStoreCustomerContactCommand
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
    }
}