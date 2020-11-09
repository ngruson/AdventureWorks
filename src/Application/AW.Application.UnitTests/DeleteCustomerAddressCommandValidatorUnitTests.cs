using AW.Application.Customer.DeleteCustomerAddress;
using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Application.UnitTests.TestBuilders;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace AW.Application.UnitTests
{
    public class DeleteCustomerAddressCommandValidatorUnitTests
    {
        [Fact]
        public void AccountNumber_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetAddressTypeSpecification>()))
                .ReturnsAsync(addressType);

            var validator = new DeleteCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object
            );

            var command = new DeleteCustomerAddressCommand();

            validator.ShouldHaveValidationErrorFor(x => x.AccountNumber, command);
        }

        [Fact]
        public void AccountNumber_TooLong_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetAddressTypeSpecification>()))
                .ReturnsAsync(addressType);

            var validator = new DeleteCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object
            );

            var command = new DeleteCustomerAddressCommand
            {
                AccountNumber = "a".PadRight(11)
            };

            validator.ShouldHaveValidationErrorFor(x => x.AccountNumber, command);
        }

        [Fact]
        public void AddressTypeName_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IAsyncRepository<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetAddressTypeSpecification>()))
                .ReturnsAsync(addressType);

            var validator = new DeleteCustomerAddressCommandValidator(
                customerRepoMock.Object,
                addressTypeRepoMock.Object
            );

            var command = new DeleteCustomerAddressCommand();

            validator.ShouldHaveValidationErrorFor(x => x.AddressTypeName, command);
        }
    }
}