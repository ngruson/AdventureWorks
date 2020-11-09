using AW.Application.Customer.DeleteCustomerContact;
using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Application.UnitTests.TestBuilders;
using FluentValidation.TestHelper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AW.Application.UnitTests
{
    public class DeleteCustomerContactCommandValidatorUnitTests
    {
        [Fact]
        public void AccountNumber_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var contactType = new ContactTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var contactTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.ContactType>>();
            contactTypeRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetContactTypeSpecification>()))
                .ReturnsAsync(contactType);

            var validator = new DeleteCustomerContactCommandValidator(
                customerRepoMock.Object,
                contactTypeRepoMock.Object
            );

            var command = new DeleteCustomerContactCommand();

            validator.ShouldHaveValidationErrorFor(x => x.AccountNumber, command);
        }

        [Fact]
        public void AccountNumber_TooLong_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var contactType = new ContactTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var contactTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.ContactType>>();
            contactTypeRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetContactTypeSpecification>()))
                .ReturnsAsync(contactType);

            var validator = new DeleteCustomerContactCommandValidator(
                customerRepoMock.Object,
                contactTypeRepoMock.Object
            );

            var command = new DeleteCustomerContactCommand
            {
                AccountNumber = "a".PadRight(11)
            };

            validator.ShouldHaveValidationErrorFor(x => x.AccountNumber, command);
        }

        [Fact]
        public void ContactTypeName_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var contactType = new ContactTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var contactTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.ContactType>>();
            contactTypeRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetContactTypeSpecification>()))
                .ReturnsAsync(contactType);

            var validator = new DeleteCustomerContactCommandValidator(
                customerRepoMock.Object,
                contactTypeRepoMock.Object
            );

            var command = new DeleteCustomerContactCommand();
            validator.ShouldHaveValidationErrorFor(x => x.ContactTypeName, command);
        }

        [Fact]
        public void Contact_Null_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var contactType = new ContactTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var contactTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.ContactType>>();
            contactTypeRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetContactTypeSpecification>()))
                .ReturnsAsync(contactType);

            var validator = new DeleteCustomerContactCommandValidator(
                customerRepoMock.Object,
                contactTypeRepoMock.Object
            );

            var command = new DeleteCustomerContactCommand();
            validator.ShouldHaveValidationErrorFor(x => x.Contact, command);
        }

        [Fact]
        public void FirstName_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var contactType = new ContactTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var contactTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.ContactType>>();
            contactTypeRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetContactTypeSpecification>()))
                .ReturnsAsync(contactType);

            var validator = new DeleteCustomerContactCommandValidator(
                customerRepoMock.Object,
                contactTypeRepoMock.Object
            );

            var command = new DeleteCustomerContactCommand
            {
                Contact = new ContactDto()
            };
            validator.ShouldHaveValidationErrorFor(x => x.Contact.FirstName, command);
        }

        [Fact]
        public void FirstName_TooLong_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var contactType = new ContactTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var contactTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.ContactType>>();
            contactTypeRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetContactTypeSpecification>()))
                .ReturnsAsync(contactType);

            var validator = new DeleteCustomerContactCommandValidator(
                customerRepoMock.Object,
                contactTypeRepoMock.Object
            );

            var command = new DeleteCustomerContactCommand
            {
                Contact = new ContactDto
                {
                    FirstName = "a".PadRight(51)
                }
            };
            validator.ShouldHaveValidationErrorFor(x => x.Contact.FirstName, command);
        }

        [Fact]
        public void LastName_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var contactType = new ContactTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var contactTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.ContactType>>();
            contactTypeRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetContactTypeSpecification>()))
                .ReturnsAsync(contactType);

            var validator = new DeleteCustomerContactCommandValidator(
                customerRepoMock.Object,
                contactTypeRepoMock.Object
            );

            var command = new DeleteCustomerContactCommand
            {
                Contact = new ContactDto()
            };
            validator.ShouldHaveValidationErrorFor(x => x.Contact.LastName, command);
        }

        [Fact]
        public void LastName_TooLong_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var contactType = new ContactTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var contactTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.ContactType>>();
            contactTypeRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetContactTypeSpecification>()))
                .ReturnsAsync(contactType);

            var validator = new DeleteCustomerContactCommandValidator(
                customerRepoMock.Object,
                contactTypeRepoMock.Object
            );

            var command = new DeleteCustomerContactCommand
            {
                Contact = new ContactDto
                {
                    LastName = "a".PadRight(51)
                }
            };
            validator.ShouldHaveValidationErrorFor(x => x.Contact.LastName, command);
        }
    }
}