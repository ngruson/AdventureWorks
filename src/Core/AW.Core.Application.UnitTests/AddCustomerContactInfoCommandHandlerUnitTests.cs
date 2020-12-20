using Ardalis.Specification;
using AW.Core.Application.Customer.AddCustomerContactInfo;
using AW.Core.Application.Specifications;
using AW.Core.Application.UnitTests.AutoMapper;
using AW.Core.Application.UnitTests.TestBuilders;
using FluentAssertions;
using Moq;
using System.Linq;
using System.Threading;
using Xunit;

namespace AW.Core.Application.UnitTests
{
    public class AddCustomerContactInfoCommandHandlerUnitTests
    {
        [Fact]
        public async void Handle_Person_Email_AddCustomerContactInfo()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder()
                .Person(new PersonBuilder().WithTestValues().Build())
                .Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var phoneNumberTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.PhoneNumberType>>();

            var handler = new AddCustomerContactInfoCommandHandler(
                customerRepoMock.Object,
                phoneNumberTypeRepoMock.Object
            );

            //Act
            var command = new AddCustomerContactInfoCommand
            {
                AccountNumber = customer.AccountNumber,
                CustomerContactInfo = new CustomerContactInfoDto
                {
                    Channel = ContactInfoChannelTypeDto.Email,
                    Value = "demo@adventure-works.com"
                }
            };
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            customer.Person.EmailAddresses.Count().Should().Be(2);
            customerRepoMock.Verify(mock => mock.UpdateAsync(It.IsAny<Domain.Sales.Customer>()), Times.Once);
        }

        [Fact]
        public async void Handle_Person_Phone_AddCustomerContactInfo()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();
            var customer = new CustomerBuilder()
                .Person(new PersonBuilder().WithTestValues().Build())
                .Build();
            var phoneNumberType = new PhoneNumberTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var phoneNumberTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.PhoneNumberType>>();
            phoneNumberTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetPhoneNumberTypeSpecification>()))
                .ReturnsAsync(phoneNumberType);

            var handler = new AddCustomerContactInfoCommandHandler(
                customerRepoMock.Object,
                phoneNumberTypeRepoMock.Object
            );

            //Act
            var command = new AddCustomerContactInfoCommand
            {
                AccountNumber = customer.AccountNumber,
                CustomerContactInfo = new CustomerContactInfoDto
                {
                    Channel = ContactInfoChannelTypeDto.Phone,
                    ContactInfoType = "Cell",
                    Value = "123-456789"
                }
            };
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            customer.Person.PhoneNumbers.Count().Should().Be(2);
            customerRepoMock.Verify(mock => mock.UpdateAsync(It.IsAny<Domain.Sales.Customer>()), Times.Once);
        }
    }
}