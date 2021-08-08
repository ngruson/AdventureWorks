using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Handlers.DeleteStoreCustomerContact;
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

namespace AW.Services.Customer.Core.UnitTests
{
    public class DeleteStoreCustomerContactCommandUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Handle_ExistingCustomerAndContact_DeleteContact(
            [Frozen] Mock<IRepository<Entities.StoreCustomer>> customerRepoMock,
            Entities.StoreCustomer customer,
            DeleteStoreCustomerContactCommandHandler sut,
            DeleteStoreCustomerContactCommand command
        )
        {
            // Arrange
            customer.Contacts = new List<Entities.StoreCustomerContact>
            {
                new Entities.StoreCustomerContact
                {
                    ContactType = command.CustomerContact.ContactType,
                    ContactPerson = new Entities.Person
                    {
                        FirstName = command.CustomerContact.ContactPerson.FirstName,
                        MiddleName = command.CustomerContact.ContactPerson.MiddleName,
                        LastName = command.CustomerContact.ContactPerson.LastName
                    }
                }
            };

            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetStoreCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(customer);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customerRepoMock.Verify(x => x.UpdateAsync(
                It.IsAny<Entities.StoreCustomer>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public void Handle_CustomerDoesNotExist_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.StoreCustomer>> customerRepoMock,
            DeleteStoreCustomerContactCommandHandler sut,
            DeleteStoreCustomerContactCommand command
        )
        {
            // Arrange
            customerRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetStoreCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.StoreCustomer)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'storeCustomer')");
        }

        [Theory]
        [AutoMoqData]
        public void Handle_ContactPersonDoesNotExist_ThrowArgumentNullException(
            DeleteStoreCustomerContactCommandHandler sut,
            DeleteStoreCustomerContactCommand command
        )
        {
            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'contact')");
        }
    }
}