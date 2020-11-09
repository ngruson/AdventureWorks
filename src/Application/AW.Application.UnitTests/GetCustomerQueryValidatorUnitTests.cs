using AW.Application.Customer.GetCustomer;
using AW.Application.Interfaces;
using AW.Application.Specifications;
using AW.Application.UnitTests.TestBuilders;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace AW.Application.UnitTests
{
    public class GetCustomerQueryValidatorUnitTests
    {
        [Fact]
        public void AccountNumber_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var validator = new GetCustomerQueryValidator(
                customerRepoMock.Object                
            );

            var query = new GetCustomerQuery();
            validator.ShouldHaveValidationErrorFor(x => x.AccountNumber, query);
        }

        [Fact]
        public void AccountNumber_TooLong_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IAsyncRepository<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetCustomerSpecification>()))
                .ReturnsAsync(customer);

            var validator = new GetCustomerQueryValidator(
                customerRepoMock.Object
            );

            var query = new GetCustomerQuery { 
                AccountNumber = "a".PadRight(11)
            };

            validator.ShouldHaveValidationErrorFor(x => x.AccountNumber, query);
        }
    }
}