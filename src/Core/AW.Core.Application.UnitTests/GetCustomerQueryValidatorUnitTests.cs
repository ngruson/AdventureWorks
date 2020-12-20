using Ardalis.Specification;
using AW.Core.Application.Customer.GetCustomer;
using AW.Core.Application.Specifications;
using AW.Core.Application.UnitTests.TestBuilders;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace AW.Core.Application.UnitTests
{
    public class GetCustomerQueryValidatorUnitTests
    {
        [Fact]
        public void AccountNumber_Empty_ValidationError()
        {
            var customer = new CustomerBuilder().WithTestValues().Build();

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
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

            var customerRepoMock = new Mock<IRepositoryBase<Domain.Sales.Customer>>();
            customerRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetCustomerSpecification>()))
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