using Ardalis.Specification;
using AW.Application.Customer.AddCustomerContact;
using AW.Application.Specifications;
using AW.Application.UnitTests.AutoMapper;
using AW.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Application.UnitTests
{
    public class AddCustomerContactCommandHandlerUnitTests
    {
        [Fact]
        public async void Handle_Store_ContactExists_AddCustomerContact()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var contactType = new ContactTypeBuilder().WithTestValues().Build();
            var customer = new CustomerBuilder().WithTestValues().Build();
            var person = new PersonBuilder().WithTestValues().Build();

            var contactTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.ContactType>>();
            contactTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetContactTypeSpecification>()))
                .ReturnsAsync(contactType);

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var personRepoMock = new Mock<IRepositoryBase<Domain.Person.Person>>();
            personRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetPersonSpecification>()))
                .ReturnsAsync(person);

            var handler = new AddCustomerContactCommandHandler(
                mapper,
                customerRepoMock.Object,
                contactTypeRepoMock.Object,
                personRepoMock.Object
            );

            //Act
            var command = new AddCustomerContactCommand
            {
                AccountNumber = customer.AccountNumber,
                CustomerContact = mapper.Map<CustomerContactDto>(
                    new BusinessEntityContactBuilder().WithTestValues().Build()
                )
            };
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            customer.Store.BusinessEntityContacts.Count().Should().Be(2);
            customer.Store.BusinessEntityContacts.Last().PersonID.Should().Be(person.Id);
            customer.Store.BusinessEntityContacts.Last().Person.Should().BeNull();
            customerRepoMock.Verify(mock => mock.UpdateAsync(It.IsAny<Domain.Sales.Customer>()), Times.Once);
        }

        [Fact]
        public async void Handle_Store_ContactDoesNotExists_AddCustomerContact()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var contactType = new ContactTypeBuilder().WithTestValues().Build();
            var customer = new CustomerBuilder().WithTestValues().Build();
            var person = new PersonBuilder().WithTestValues().Build();

            var contactTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.ContactType>>();
            contactTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetContactTypeSpecification>()))
                .ReturnsAsync(contactType);

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var personRepoMock = new Mock<IRepositoryBase<Domain.Person.Person>>();
            personRepoMock.SetupSequence(x => x.GetBySpecAsync(It.IsAny<GetPersonSpecification>()))
                .Returns(Task.FromResult<Domain.Person.Person>(null))
                .Returns(Task.FromResult(person));

            var handler = new AddCustomerContactCommandHandler(
                mapper,
                customerRepoMock.Object,
                contactTypeRepoMock.Object,
                personRepoMock.Object
            );

            //Act
            var command = new AddCustomerContactCommand
            {
                AccountNumber = customer.AccountNumber,
                CustomerContact = mapper.Map<CustomerContactDto>(
                    new BusinessEntityContactBuilder().WithTestValues().Build()
                )
            };
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            customer.Store.BusinessEntityContacts.Count().Should().Be(2);
            customer.Store.BusinessEntityContacts.Last().PersonID.Should().Be(0);
            customer.Store.BusinessEntityContacts.Last().Person.Should().NotBeNull();
            customerRepoMock.Verify(mock => mock.UpdateAsync(It.IsAny<Domain.Sales.Customer>()), Times.Once);
            personRepoMock.Verify(mock => mock.UpdateAsync(It.IsAny<Domain.Person.Person>()), Times.Once);
        }
    }
}