using AW.Application.Product.GetProducts;
using FluentValidation.TestHelper;
using Xunit;

namespace AW.Application.UnitTests
{
    public class GetProductsQueryValidatorUnitTests
    {
        [Fact]
        public void PageIndex_Negative_ValidationError()
        {
            var validator = new GetProductsQueryValidator();

            var query = new GetProductsQuery
            {
                PageIndex = -1
            };
            validator.ShouldHaveValidationErrorFor(x => x.PageIndex, query);
        }

        [Fact]
        public void PageSize_Negative_ValidationError()
        {
            var validator = new GetProductsQueryValidator();

            var query = new GetProductsQuery
            {
                PageSize = -1
            };
            validator.ShouldHaveValidationErrorFor(x => x.PageSize, query);
        }

        [Fact]
        public void PageSize_Zero_ValidationError()
        {
            var validator = new GetProductsQueryValidator();

            var query = new GetProductsQuery
            {
                PageSize = 0
            };
            validator.ShouldHaveValidationErrorFor(x => x.PageSize, query);
        }
    }
}