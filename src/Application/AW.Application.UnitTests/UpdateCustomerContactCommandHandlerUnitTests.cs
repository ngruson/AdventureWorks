using AW.Application.Customer.UpdateCustomerContact;
using Ardalis.Specification;
using AW.Application.Specifications;
using AW.Application.UnitTests.AutoMapper;
using AW.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace AW.Application.UnitTests
{
    public class UpdateCustomerContactCommandHandlerUnitTests
    {
        [Fact]
        public async void Handle_Store_ExistingContact_UpdateCustomerContact()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();
            var person = new PersonBuilder().WithTestValues().Build();
            var contactType = new ContactTypeBuilder().WithTestValues().Build();
            var emailAddress = new EmailAddressBuilder().WithTestValues().Build();

            var contactTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.ContactType>>();
            contactTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetContactTypeSpecification>()))
                .ReturnsAsync(contactType);

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var personRepoMock = new Mock<IRepositoryBase<Domain.Person.Person>>();
            personRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetPersonSpecification>()))
                .ReturnsAsync(person);

            var emailAddressRepoMock = new Mock<IRepositoryBase<Domain.Person.EmailAddress>>();
            emailAddressRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetEmailAddressSpecification>()))
                .ReturnsAsync(emailAddress);

            var handler = new UpdateCustomerContactCommandHandler(
                customerRepoMock.Object,
                personRepoMock.Object
            );

            //Act
            var command = new UpdateCustomerContactCommand
            {
                AccountNumber = customer.AccountNumber,
                CustomerContact = new CustomerContactDto
                {
                    ContactTypeName = contactType.Name,
                    Contact = mapper.Map<ContactDto>(person)
                }
            };
            command.CustomerContact.Contact.Suffix = "Suffix";

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            customer.Store.BusinessEntityContacts.First().Person.Suffix.Should().Be("Suffix");
            customerRepoMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Sales.Customer>()));
            personRepoMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Person.Person>()));
        }

        [Fact]
        public async void Handle_Store_AddEmailAddress_UpdateCustomerContact()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder().WithTestValues().Build();
            var person = new PersonBuilder().WithTestValues().Build();
            var contactType = new ContactTypeBuilder().WithTestValues().Build();
            var emailAddress = new EmailAddressBuilder().WithTestValues().Build();

            var contactTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.ContactType>>();
            contactTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetContactTypeSpecification>()))
                .ReturnsAsync(contactType);

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var personRepoMock = new Mock<IRepositoryBase<Domain.Person.Person>>();
            personRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetPersonSpecification>()))
                .ReturnsAsync(person);

            var emailAddressRepoMock = new Mock<IRepositoryBase<Domain.Person.EmailAddress>>();
            emailAddressRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetEmailAddressSpecification>()))
                .ReturnsAsync(emailAddress);

            var handler = new UpdateCustomerContactCommandHandler(
                customerRepoMock.Object,
                personRepoMock.Object
            );

            //Act
            var command = new UpdateCustomerContactCommand
            {
                AccountNumber = customer.AccountNumber,
                CustomerContact = new CustomerContactDto
                {
                    ContactTypeName = contactType.Name,
                    Contact = mapper.Map<ContactDto>(person)
                }
            };
            command.CustomerContact.Contact.EmailAddresses = new List<EmailAddressDto>
            {
                new EmailAddressDto
                {
                    EmailAddress = "demo2@adventure-works.com"
                }
            };

            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            person.EmailAddresses.Count().Should().Be(2);
            customerRepoMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Sales.Customer>()));
            personRepoMock.Verify(x => x.UpdateAsync(It.IsAny<Domain.Person.Person>()));
        }
    }
}