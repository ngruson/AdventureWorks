using Ardalis.Specification;
using AW.Core.Application.Customer.DeleteCustomerContactInfo;
using AW.Core.Application.Specifications;
using AW.Core.Application.UnitTests.TestBuilders;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace AW.Core.Application.UnitTests
{
    public class DeleteCustomerContactInfoCommandValidatorUnitTests
    {
        [Fact]
        public void AccountNumber_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var phoneNumberType = new PhoneNumberTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var phoneNumberTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.PhoneNumberType>>();
            phoneNumberTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetPhoneNumberTypeSpecification>()))
                .ReturnsAsync(phoneNumberType);

            var validator = new DeleteCustomerContactInfoCommandValidator(
                customerRepoMock.Object,
                phoneNumberTypeRepoMock.Object
            );

            var command = new DeleteCustomerContactInfoCommand();

            validator.ShouldHaveValidationErrorFor(x => x.AccountNumber, command);
        }

        [Fact]
        public void AccountNumber_TooLong_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var phoneNumberType = new PhoneNumberTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var phoneNumberTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.PhoneNumberType>>();
            phoneNumberTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetPhoneNumberTypeSpecification>()))
                .ReturnsAsync(phoneNumberType);

            var validator = new DeleteCustomerContactInfoCommandValidator(
                customerRepoMock.Object,
                phoneNumberTypeRepoMock.Object
            );

            var command = new DeleteCustomerContactInfoCommand
            {
                AccountNumber = "a".PadRight(11)
            };

            validator.ShouldHaveValidationErrorFor(x => x.AccountNumber, command);
        }

        [Fact]
        public void CustomerContactInfo_Null_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var phoneNumberType = new PhoneNumberTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var phoneNumberTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.PhoneNumberType>>();
            phoneNumberTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetPhoneNumberTypeSpecification>()))
                .ReturnsAsync(phoneNumberType);

            var validator = new DeleteCustomerContactInfoCommandValidator(
                customerRepoMock.Object,
                phoneNumberTypeRepoMock.Object
            );

            var command = new DeleteCustomerContactInfoCommand();

            validator.ShouldHaveValidationErrorFor(x => x.CustomerContactInfo, command);
        }

        [Fact]
        public void Phone_ContactInfoType_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var phoneNumberType = new PhoneNumberTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var phoneNumberTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.PhoneNumberType>>();
            phoneNumberTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetPhoneNumberTypeSpecification>()))
                .ReturnsAsync(phoneNumberType);

            var validator = new DeleteCustomerContactInfoCommandValidator(
                customerRepoMock.Object,
                phoneNumberTypeRepoMock.Object
            );

            var command = new DeleteCustomerContactInfoCommand
            {
                CustomerContactInfo = new CustomerContactInfoDto
                {
                    Channel = ContactInfoChannelTypeDto.Phone
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerContactInfo.ContactInfoType, command);
        }

        [Fact]
        public void Phone_Value_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var phoneNumberType = new PhoneNumberTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var phoneNumberTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.PhoneNumberType>>();
            phoneNumberTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetPhoneNumberTypeSpecification>()))
                .ReturnsAsync(phoneNumberType);

            var validator = new DeleteCustomerContactInfoCommandValidator(
                customerRepoMock.Object,
                phoneNumberTypeRepoMock.Object
            );

            var command = new DeleteCustomerContactInfoCommand
            {
                CustomerContactInfo = new CustomerContactInfoDto
                {
                    Channel = ContactInfoChannelTypeDto.Phone
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerContactInfo.Value, command);
        }

        [Fact]
        public void Phone_Value_TooLong_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var phoneNumberType = new PhoneNumberTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var phoneNumberTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.PhoneNumberType>>();
            phoneNumberTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetPhoneNumberTypeSpecification>()))
                .ReturnsAsync(phoneNumberType);

            var validator = new DeleteCustomerContactInfoCommandValidator(
                customerRepoMock.Object,
                phoneNumberTypeRepoMock.Object
            );

            var command = new DeleteCustomerContactInfoCommand
            {
                CustomerContactInfo = new CustomerContactInfoDto
                {
                    Channel = ContactInfoChannelTypeDto.Phone,
                    Value = "a".PadRight(26)
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerContactInfo.Value, command);
        }

        [Fact]
        public void Email_Value_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var phoneNumberType = new PhoneNumberTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var phoneNumberTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.PhoneNumberType>>();
            phoneNumberTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetPhoneNumberTypeSpecification>()))
                .ReturnsAsync(phoneNumberType);

            var validator = new DeleteCustomerContactInfoCommandValidator(
                customerRepoMock.Object,
                phoneNumberTypeRepoMock.Object
            );

            var command = new DeleteCustomerContactInfoCommand
            {
                CustomerContactInfo = new CustomerContactInfoDto
                {
                    Channel = ContactInfoChannelTypeDto.Phone
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerContactInfo.Value, command);
        }

        [Fact]
        public void Email_Value_TooLong_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var phoneNumberType = new PhoneNumberTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var phoneNumberTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.PhoneNumberType>>();
            phoneNumberTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetPhoneNumberTypeSpecification>()))
                .ReturnsAsync(phoneNumberType);

            var validator = new DeleteCustomerContactInfoCommandValidator(
                customerRepoMock.Object,
                phoneNumberTypeRepoMock.Object
            );

            var command = new DeleteCustomerContactInfoCommand
            {
                CustomerContactInfo = new CustomerContactInfoDto
                {
                    Channel = ContactInfoChannelTypeDto.Phone,
                    Value = "a".PadRight(51)
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerContactInfo.Value, command);
        }
    }
}