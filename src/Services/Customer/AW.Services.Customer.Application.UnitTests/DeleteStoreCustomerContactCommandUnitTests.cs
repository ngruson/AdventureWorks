using Ardalis.Specification;
using AW.Services.Customer.Application.DeleteStoreCustomerContact;
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
    public class DeleteStoreCustomerContactCommandUnitTests
    {
        [Fact]
        public async void Handle_ExistingCustomerAndContact_DeleteContact()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteStoreCustomerContactCommandHandler>>();
            var customerRepoMock = new Mock<IRepositoryBase<Domain.StoreCustomer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetStoreCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new StoreCustomerBuilder()
                .WithTestValues()
                .Contacts(new List<Domain.StoreCustomerContact> 
                    {
                        new Domain.StoreCustomerContact
                        {
                            ContactType = "Owner",
                            ContactPerson = new Domain.Person
                            {
                                Title = "Mr.",
                                FirstName = "Orlando",
                                MiddleName = "N.",
                                LastName = "Gee"
                            }
                        }
                    }
                )
                .Build()
            );

            var handler = new DeleteStoreCustomerContactCommandHandler(
                loggerMock.Object,
                customerRepoMock.Object
            );

            //Act
            var command = new DeleteStoreCustomerContactCommand
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
                It.IsAny<Domain.StoreCustomer>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Fact]
        public void Handle_CustomerDoesNotExist_ThrowArgumentNullException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteStoreCustomerContactCommandHandler>>();
            var customerRepoMock = new Mock<IRepositoryBase<Domain.StoreCustomer>>();

            var handler = new DeleteStoreCustomerContactCommandHandler(
                loggerMock.Object,
                customerRepoMock.Object
            );

            //Act
            var command = new DeleteStoreCustomerContactCommand
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
            Func<Task> func = async () => await handler.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'storeCustomer')");
        }

        [Fact]
        public void Handle_ContactPersonDoesNotExist_ThrowArgumentNullException()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<DeleteStoreCustomerContactCommandHandler>>();
            var customerRepoMock = new Mock<IRepositoryBase<Domain.StoreCustomer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetStoreCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new StoreCustomerBuilder()
                .WithTestValues()
                .Contacts(new List<Domain.StoreCustomerContact>())
                .Build()
            );

            var handler = new DeleteStoreCustomerContactCommandHandler(
                loggerMock.Object,
                customerRepoMock.Object
            );

            //Act
            var command = new DeleteStoreCustomerContactCommand
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
            Func<Task> func = async () => await handler.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'contact')");
        }
    }
}