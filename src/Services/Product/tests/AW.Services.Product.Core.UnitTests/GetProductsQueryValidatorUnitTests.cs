using AW.Services.Product.Core.AutoMapper;
using AW.Services.Product.Core.Handlers.GetProducts;
using AW.SharedKernel.UnitTesting;
using FluentValidation.TestHelper;
using Xunit;

namespace AW.Services.Product.Core.UnitTests
{
    public class GetProductsQueryValidatorUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void TestValidate_ValidAscOrderBy_NoValidationError(
            GetProductsQueryValidator sut,
            string category,
            string subcategory
        )
        {
            //Arrange
            var query = new GetProductsQuery(0, 10, category, subcategory, "asc(name)");
            
            //Act
            var result = sut.TestValidate(query);

            //Assert
            result.ShouldNotHaveValidationErrorFor(query => query.OrderBy);
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void TestValidate_ValidDescOrderBy_NoValidationError(
            GetProductsQueryValidator sut,
            string category,
            string subcategory
        )
        {
            //Arrange
            var query = new GetProductsQuery(0, 10, category, subcategory, "desc(name)");

            //Act
            var result = sut.TestValidate(query);

            //Assert
            result.ShouldNotHaveValidationErrorFor(query => query.OrderBy);
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void TestValidate_InvalidDirectionOrderBy_ValidationError(
            GetProductsQueryValidator sut,
            string category,
            string subcategory
        )
        {
            //Arrange
            var query = new GetProductsQuery(0, 10, category, subcategory, "fail(name)");

            //Act
            var result = sut.TestValidate(query);

            //Assert
            result.ShouldHaveValidationErrorFor(query => query.OrderBy)
                .WithErrorMessage("Order by is not valid");
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void TestValidate_NoOpeningBracketOrderBy_ValidationError(
            GetProductsQueryValidator sut,
            string category,
            string subcategory
        )
        {
            //Arrange
            var query = new GetProductsQuery(0, 10, category, subcategory, "asc_name)");

            //Act
            var result = sut.TestValidate(query);

            //Assert
            result.ShouldHaveValidationErrorFor(query => query.OrderBy)
                .WithErrorMessage("Order by is not valid");
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void TestValidate_NoClosingBracketOrderBy_ValidationError(
            GetProductsQueryValidator sut,
            string category,
            string subcategory
        )
        {
            //Arrange
            var query = new GetProductsQuery(0, 10, category, subcategory, "asc(name");

            //Act
            var result = sut.TestValidate(query);

            //Assert
            result.ShouldHaveValidationErrorFor(query => query.OrderBy)
                .WithErrorMessage("Order by is not valid");
        }
    }
}