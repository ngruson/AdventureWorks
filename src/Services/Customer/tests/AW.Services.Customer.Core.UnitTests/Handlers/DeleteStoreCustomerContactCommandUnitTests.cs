using AutoFixture.Xunit2;
using AW.Services.Customer.Core.Exceptions;
using AW.Services.Customer.Core.Handlers.DeleteStoreCustomerContact;
using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests.Handlers
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
            customer.AddContact(new Entities.StoreCustomerContact(
                command.CustomerContact!.ContactType!,
                new Entities.Person(
                    command.CustomerContact.ContactPerson!.Title!,
                    command.CustomerContact.ContactPerson.Name!
                )
            ));

            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
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
        public async Task Handle_CustomerDoesNotExist_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.StoreCustomer>> customerRepoMock,
            DeleteStoreCustomerContactCommandHandler sut,
            DeleteStoreCustomerContactCommand command
        )
        {
            // Arrange
            customerRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetStoreCustomerSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.StoreCustomer?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<CustomerNotFoundException>()
                .WithMessage($"Customer {command.AccountNumber} not found");
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_ContactPersonDoesNotExist_ThrowArgumentNullException(
            DeleteStoreCustomerContactCommandHandler sut,
            DeleteStoreCustomerContactCommand command
        )
        {
            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<StoreContactNotFoundException>()
                .WithMessage($"Contact (name: {command.CustomerContact!.ContactPerson!.Name!.FullName}, type: {command.CustomerContact.ContactType}) for customer {command.AccountNumber} not found");
        }
    }
}