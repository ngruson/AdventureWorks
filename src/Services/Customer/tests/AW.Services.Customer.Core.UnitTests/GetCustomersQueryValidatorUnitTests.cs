using AW.Services.Customer.Core.Handlers.GetCustomers;
using FluentValidation.TestHelper;
using Xunit;

namespace AW.Services.Customer.Core.UnitTests
{
    public class GetCustomersQueryValidatorUnitTests
    {
        [Fact]
        public void TestValidate_ValidQuery_NoValidationError()
        {
            //Arrange
            var validator = new GetCustomersQueryValidator();
            var query = new GetCustomersQuery
            {
                PageIndex = 0,
                PageSize = 10
            };

            var result = validator.TestValidate(query);
            result.ShouldNotHaveValidationErrorFor(query => query.PageIndex);
            result.ShouldNotHaveValidationErrorFor(query => query.PageSize);
        }

        [Fact]
        public void TestValidate_InvalidPageIndex_ValidationErrorForPageIndex()
        {
            //Arrange
            var validator = new GetCustomersQueryValidator();
            var query = new GetCustomersQuery
            {
                PageIndex = -1,
                PageSize = 10
            };

            var result = validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(query => query.PageIndex);
        }

        [Fact]
        public void TestValidate_InvalidPageSize_ValidationErrorForPageSize()
        {
            //Arrange
            var validator = new GetCustomersQueryValidator();
            var query = new GetCustomersQuery
            {
                PageIndex = 0,
                PageSize = 0
            };

            var result = validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(query => query.PageSize);
        }
    }
}