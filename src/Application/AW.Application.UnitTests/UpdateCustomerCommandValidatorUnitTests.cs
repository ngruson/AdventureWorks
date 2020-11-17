using AW.Application.Customer.UpdateCustomer;
using Ardalis.Specification;
using AW.Application.Specifications;
using AW.Application.UnitTests.TestBuilders;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace AW.Application.UnitTests
{
    public class UpdateCustomerCommandValidatorUnitTests
    {
        [Fact]
        public void Customer_Null_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var validator = new UpdateCustomerCommandValidator(
                customerRepoMock.Object
            );

            var command = new UpdateCustomerCommand();

            validator.ShouldHaveValidationErrorFor(x => x.Customer, command);
        }

        [Fact]
        public void AccountNumber_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var validator = new UpdateCustomerCommandValidator(
                customerRepoMock.Object
            );

            var command = new UpdateCustomerCommand
            {
                Customer = new CustomerDto()
            };

            validator.ShouldHaveValidationErrorFor(x => x.Customer.AccountNumber, command);
        }

        [Fact]
        public void AccountNumber_TooLong_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var validator = new UpdateCustomerCommandValidator(
                customerRepoMock.Object
            );

            var command = new UpdateCustomerCommand
            {
                Customer = new CustomerDto
                {
                    AccountNumber = "a".PadRight(11)
                }
            };

            validator.ShouldHaveValidationErrorFor(x => x.Customer.AccountNumber, command);
        }
    }
}