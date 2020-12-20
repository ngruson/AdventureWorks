using Ardalis.Specification;
using AW.Core.Application.Customer.DeleteCustomerAddress;
using AW.Core.Application.Specifications;
using AW.Core.Application.UnitTests.TestBuilders;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace AW.Core.Application.UnitTests
{
    public class DeleteCustomerAddressCommandValidatorUnitTests
    {
        [Fact]
        public void AccountNumber_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();
            var addressType = new AddressTypeBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetAddressTypeSpecification>()))
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

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetAddressTypeSpecification>()))
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

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var addressTypeRepoMock = new Mock<IRepositoryBase<Domain.Person.AddressType>>();
            addressTypeRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetAddressTypeSpecification>()))
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