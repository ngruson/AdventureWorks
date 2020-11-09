using AW.Application.SalesOrder.GetSalesOrders;
using FluentValidation.TestHelper;
using Xunit;

namespace AW.Application.UnitTests
{
    public class GetSalesOrdersQueryValidatorUnitTests
    {
        [Fact]
        public void PageIndex_Negative_ValidationError()
        {
            var validator = new GetSalesOrdersQueryValidator();

            var query = new GetSalesOrdersQuery
            {
                PageIndex = -1
            };
            validator.ShouldHaveValidationErrorFor(x => x.PageIndex, query);
        }

        [Fact]
        public void PageSize_Negative_ValidationError()
        {
            var validator = new GetSalesOrdersQueryValidator();

            var query = new GetSalesOrdersQuery
            {
                PageSize = -1
            };
            validator.ShouldHaveValidationErrorFor(x => x.PageSize, query);
        }

        [Fact]
        public void PageSize_Zero_ValidationError()
        {
            var validator = new GetSalesOrdersQueryValidator();

            var query = new GetSalesOrdersQuery
            {
                PageSize = 0
            };
            validator.ShouldHaveValidationErrorFor(x => x.PageSize, query);
        }
    }
}