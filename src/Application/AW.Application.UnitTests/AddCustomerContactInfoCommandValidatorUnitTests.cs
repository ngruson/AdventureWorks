using AW.Application.Customer.AddCustomerContactInfo;
using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Application.UnitTests.TestBuilders;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace AW.Application.UnitTests
{
    public class AddCustomerContactInfoCommandValidatorUnitTests
    {
        [Fact]
        public void AccountNumber_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var phoneNumberType = new PhoneNumberTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var phoneNumberTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.PhoneNumberType>>();
            phoneNumberTypeRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetPhoneNumberTypeSpecification>()))
                .ReturnsAsync(phoneNumberType);

            var validator = new AddCustomerContactInfoCommandValidator(
                customerRepoMock.Object,
                phoneNumberTypeRepoMock.Object
            );

            var command = new AddCustomerContactInfoCommand();

            validator.ShouldHaveValidationErrorFor(x => x.AccountNumber, command);
        }

        [Fact]
        public void AccountNumber_TooLong_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var phoneNumberType = new PhoneNumberTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var phoneNumberTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.PhoneNumberType>>();
            phoneNumberTypeRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetPhoneNumberTypeSpecification>()))
                .ReturnsAsync(phoneNumberType);

            var validator = new AddCustomerContactInfoCommandValidator(
                customerRepoMock.Object,
                phoneNumberTypeRepoMock.Object
            );

            var command = new AddCustomerContactInfoCommand
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

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var phoneNumberTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.PhoneNumberType>>();
            phoneNumberTypeRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetPhoneNumberTypeSpecification>()))
                .ReturnsAsync(phoneNumberType);

            var validator = new AddCustomerContactInfoCommandValidator(
                customerRepoMock.Object,
                phoneNumberTypeRepoMock.Object
            );

            var command = new AddCustomerContactInfoCommand();

            validator.ShouldHaveValidationErrorFor(x => x.CustomerContactInfo, command);
        }

        [Fact]
        public void Phone_ContactInfoType_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var phoneNumberType = new PhoneNumberTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var phoneNumberTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.PhoneNumberType>>();
            phoneNumberTypeRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetPhoneNumberTypeSpecification>()))
                .ReturnsAsync(phoneNumberType);

            var validator = new AddCustomerContactInfoCommandValidator(
                customerRepoMock.Object,
                phoneNumberTypeRepoMock.Object
            );

            var command = new AddCustomerContactInfoCommand
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

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var phoneNumberTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.PhoneNumberType>>();
            phoneNumberTypeRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetPhoneNumberTypeSpecification>()))
                .ReturnsAsync(phoneNumberType);

            var validator = new AddCustomerContactInfoCommandValidator(
                customerRepoMock.Object,
                phoneNumberTypeRepoMock.Object
            );

            var command = new AddCustomerContactInfoCommand
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

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var phoneNumberTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.PhoneNumberType>>();
            phoneNumberTypeRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetPhoneNumberTypeSpecification>()))
                .ReturnsAsync(phoneNumberType);

            var validator = new AddCustomerContactInfoCommandValidator(
                customerRepoMock.Object,
                phoneNumberTypeRepoMock.Object
            );

            var command = new AddCustomerContactInfoCommand
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

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var phoneNumberTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.PhoneNumberType>>();
            phoneNumberTypeRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetPhoneNumberTypeSpecification>()))
                .ReturnsAsync(phoneNumberType);

            var validator = new AddCustomerContactInfoCommandValidator(
                customerRepoMock.Object,
                phoneNumberTypeRepoMock.Object
            );

            var command = new AddCustomerContactInfoCommand
            {
                CustomerContactInfo = new CustomerContactInfoDto
                {
                    Channel = ContactInfoChannelTypeDto.Email
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerContactInfo.Value, command);
        }

        [Fact]
        public void Email_Value_TooLong_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var phoneNumberType = new PhoneNumberTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var phoneNumberTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.PhoneNumberType>>();
            phoneNumberTypeRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetPhoneNumberTypeSpecification>()))
                .ReturnsAsync(phoneNumberType);

            var validator = new AddCustomerContactInfoCommandValidator(
                customerRepoMock.Object,
                phoneNumberTypeRepoMock.Object
            );

            var command = new AddCustomerContactInfoCommand
            {
                CustomerContactInfo = new CustomerContactInfoDto
                {
                    Channel = ContactInfoChannelTypeDto.Email,
                    Value = "a".PadRight(51)
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.CustomerContactInfo.Value, command);
        }
    }
}