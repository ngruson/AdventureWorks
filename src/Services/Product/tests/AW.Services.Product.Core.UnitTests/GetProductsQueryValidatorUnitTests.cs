using AW.Services.Product.Core.Handlers.GetProducts;
using FluentValidation.TestHelper;
using Xunit;

namespace AW.Services.Product.Core.UnitTests
{
    public class GetProductsQueryValidatorUnitTests
    {
        [Fact]
        public void TestValidate_ValidAscOrderBy_NoValidationError()
        {
            var sut = new GetProductsQueryValidator();
            var query = new GetProductsQuery
            {
                OrderBy = "asc(name)"
            };

            var result = sut.TestValidate(query);
            result.ShouldNotHaveValidationErrorFor(query => query.OrderBy);
        }

        [Fact]
        public void TestValidate_ValidDescOrderBy_NoValidationError()
        {
            var sut = new GetProductsQueryValidator();
            var query = new GetProductsQuery
            {
                OrderBy = "desc(name)"
            };

            var result = sut.TestValidate(query);
            result.ShouldNotHaveValidationErrorFor(query => query.OrderBy);
        }

        [Fact]
        public void TestValidate_InvalidDirectionOrderBy_ValidationError()
        {
            var sut = new GetProductsQueryValidator();
            var query = new GetProductsQuery
            {
                OrderBy = "fail(name)"
            };

            var result = sut.TestValidate(query);
            result.ShouldHaveValidationErrorFor(query => query.OrderBy);
        }

        [Fact]
        public void TestValidate_NoOpeningBracketOrderBy_ValidationError()
        {
            var sut = new GetProductsQueryValidator();
            var query = new GetProductsQuery
            {
                OrderBy = "fail_name)"
            };

            var result = sut.TestValidate(query);
            result.ShouldHaveValidationErrorFor(query => query.OrderBy);
        }

        [Fact]
        public void TestValidate_NoClosingBracketOrderBy_ValidationError()
        {
            var sut = new GetProductsQueryValidator();
            var query = new GetProductsQuery
            {
                OrderBy = "fail(name"
            };

            var result = sut.TestValidate(query);
            result.ShouldHaveValidationErrorFor(query => query.OrderBy);
        }
    }
}