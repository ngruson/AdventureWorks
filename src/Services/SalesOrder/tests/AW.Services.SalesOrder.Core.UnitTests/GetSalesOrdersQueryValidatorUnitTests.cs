using AW.Services.SalesOrder.Core.Handlers.GetSalesOrders;
using FluentValidation.TestHelper;
using Xunit;

namespace AW.Services.SalesOrder.Core.UnitTests
{
    public class GetSalesOrdersQueryValidatorUnitTests
    {
        [Fact]
        public void TestValidate_WithValidPageSize_NoValidationError()
        {
            //Arrange
            var sut = new GetSalesOrdersQueryValidator();
            var query = new GetSalesOrdersQuery
            {
                PageSize = 1
            };

            var result = sut.TestValidate(query);
            result.ShouldNotHaveValidationErrorFor(query => query.PageSize);
        }

        [Fact]
        public void TestValidate_WithInvalidPageSize_ValidationError()
        {
            //Arrange
            var sut = new GetSalesOrdersQueryValidator();
            var query = new GetSalesOrdersQuery
            {
                PageSize = 0
            };

            var result = sut.TestValidate(query);
            result.ShouldHaveValidationErrorFor(query => query.PageSize)
                .WithErrorMessage("Page size must be positive");
        }

        [Fact]
        public void TestValidate_WithValidPageIndex_NoValidationError()
        {
            //Arrange
            var sut = new GetSalesOrdersQueryValidator();
            var query = new GetSalesOrdersQuery
            {
                PageIndex = 0
            };

            var result = sut.TestValidate(query);
            result.ShouldNotHaveValidationErrorFor(query => query.PageIndex);
        }

        [Fact]
        public void TestValidate_WithInvalidPageIndex_ValidationError()
        {
            //Arrange
            var sut = new GetSalesOrdersQueryValidator();
            var query = new GetSalesOrdersQuery
            {
                PageIndex = -1
            };

            var result = sut.TestValidate(query);
            result.ShouldHaveValidationErrorFor(query => query.PageIndex)
                .WithErrorMessage("Page index must be 0 or positive");
        }
    }
}