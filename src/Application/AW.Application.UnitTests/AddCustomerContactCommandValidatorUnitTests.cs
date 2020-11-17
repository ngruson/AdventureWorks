using Ardalis.Specification;
using AW.Application.Customer.AddCustomerContact;
using AW.Application.Specifications;
using AW.Application.UnitTests.TestBuilders;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace AW.Application.UnitTests
{
    public class AddCustomerContactCommandValidatorUnitTests
    {
        [Fact]
        public void AccountNumber_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var contactType = new ContactTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var contactTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.ContactType>>();
            contactTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetContactTypeSpecification>()))
                .ReturnsAsync(contactType);

            var validator = new AddCustomerContactCommandValidator(
                customerRepoMock.Object,
                contactTypeRepoMock.Object
            );

            var command = new AddCustomerContactCommand();
            
            validator.ShouldHaveValidationErrorFor(x => x.AccountNumber, command);
        }

        [Fact]
        public void AccountNumber_TooLong_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var contactType = new ContactTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var contactTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.ContactType>>();
            contactTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetContactTypeSpecification>()))
                .ReturnsAsync(contactType);

            var validator = new AddCustomerContactCommandValidator(
                customerRepoMock.Object,
                contactTypeRepoMock.Object
            );

            var command = new AddCustomerContactCommand
            {
                AccountNumber = "AW000000023456789"
            };

            validator.ShouldHaveValidationErrorFor(x => x.AccountNumber, command);
        }

        [Fact]
        public void CustomerContact_Null_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var contactType = new ContactTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var contactTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.ContactType>>();
            contactTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetContactTypeSpecification>()))
                .ReturnsAsync(contactType);

            var validator = new AddCustomerContactCommandValidator(
                customerRepoMock.Object,
                contactTypeRepoMock.Object
            );

            var command = new AddCustomerContactCommand
            {
                AccountNumber = "a"
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerContact, command);
        }

        [Fact]
        public void ContactTypeName_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var contactType = new ContactTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var contactTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.ContactType>>();
            contactTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetContactTypeSpecification>()))
                .ReturnsAsync(contactType);

            var validator = new AddCustomerContactCommandValidator(
                customerRepoMock.Object,
                contactTypeRepoMock.Object
            );

            var command = new AddCustomerContactCommand
            {
                CustomerContact = new CustomerContactDto()
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerContact.ContactTypeName, command);
        }

        [Fact]
        public void Contact_Null_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var contactType = new ContactTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var contactTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.ContactType>>();
            contactTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetContactTypeSpecification>()))
                .ReturnsAsync(contactType);

            var validator = new AddCustomerContactCommandValidator(
                customerRepoMock.Object,
                contactTypeRepoMock.Object
            );

            var command = new AddCustomerContactCommand
            {
                CustomerContact = new CustomerContactDto()
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerContact.Contact, command);
        }

        [Fact]
        public void FirstName_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var contactType = new ContactTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var contactTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.ContactType>>();
            contactTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetContactTypeSpecification>()))
                .ReturnsAsync(contactType);

            var validator = new AddCustomerContactCommandValidator(
                customerRepoMock.Object,
                contactTypeRepoMock.Object
            );

            var command = new AddCustomerContactCommand
            {
                CustomerContact = new CustomerContactDto
                {
                    Contact = new ContactDto()
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerContact.Contact.FirstName, command);
        }

        [Fact]
        public void FirstName_TooLong_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var contactType = new ContactTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var contactTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.ContactType>>();
            contactTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetContactTypeSpecification>()))
                .ReturnsAsync(contactType);

            var validator = new AddCustomerContactCommandValidator(
                customerRepoMock.Object,
                contactTypeRepoMock.Object
            );

            var command = new AddCustomerContactCommand
            {
                CustomerContact = new CustomerContactDto
                {
                    Contact = new ContactDto
                    {
                        FirstName = "a".PadRight(51, 'b')
                    }
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerContact.Contact.FirstName, command);
        }

        [Fact]
        public void LastName_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var contactType = new ContactTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var contactTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.ContactType>>();
            contactTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetContactTypeSpecification>()))
                .ReturnsAsync(contactType);

            var validator = new AddCustomerContactCommandValidator(
                customerRepoMock.Object,
                contactTypeRepoMock.Object
            );

            var command = new AddCustomerContactCommand
            {
                CustomerContact = new CustomerContactDto
                {
                    Contact = new ContactDto()
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerContact.Contact.LastName, command);
        }

        [Fact]
        public void LastName_TooLong_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var contactType = new ContactTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var contactTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.ContactType>>();
            contactTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetContactTypeSpecification>()))
                .ReturnsAsync(contactType);

            var validator = new AddCustomerContactCommandValidator(
                customerRepoMock.Object,
                contactTypeRepoMock.Object
            );

            var command = new AddCustomerContactCommand
            {
                CustomerContact = new CustomerContactDto
                {
                    Contact = new ContactDto
                    {
                        LastName = "a".PadRight(51)
                    }
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerContact.Contact.LastName, command);
        }
    }
}