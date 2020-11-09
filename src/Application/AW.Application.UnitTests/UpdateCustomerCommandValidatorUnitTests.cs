using AW.Application.Customer.UpdateCustomer;
using AW.Application.Interfaces;
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

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
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

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
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

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
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