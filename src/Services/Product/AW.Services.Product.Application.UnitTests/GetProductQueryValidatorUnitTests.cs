using Ardalis.Specification;
using AW.Services.Product.Application.GetProduct;
using AW.Services.Product.Application.UnitTests.TestBuilders;
using FluentValidation.TestHelper;
using Moq;
using System.Linq;
using Xunit;

namespace AW.Services.Product.Application.UnitTests
{
    public class GetProductQueryValidatorUnitTests
    {
        [Fact]
        public void TestValidate_Valid_NoValidationError()
        {
            var product = new ProductBuilder().WithTestValues().Build();
            var mockRepository = new Mock<IRepositoryBase<Domain.Product>>();
            mockRepository.Setup(x => x.GetBySpecAsync(It.IsAny<ISpecification<Domain.Product>>()))
                .ReturnsAsync(product);

            var validator = new GetProductQueryValidator(mockRepository.Object);
            var query = new GetProductQuery
            {
                ProductNumber = "FR-R92B-58"
            };

            var result = validator.TestValidate(query);
            result.ShouldNotHaveValidationErrorFor(query => query.ProductNumber);
        }

        [Fact]
        public void TestValidate_QueryWithEmptyProductNumber_ValidationError()
        {
            var mockRepository = new Mock<IRepositoryBase<Domain.Product>>();
            var validator = new GetProductQueryValidator(mockRepository.Object);
            var query = new GetProductQuery
            {
                ProductNumber = ""
            };

            var result = validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(query => query.ProductNumber)
                .WithErrorMessage("Product number is required");
        }

        [Fact]
        public void TestValidate_QueryWithTooLongProductNumber_ValidationError()
        {
            var mockRepository = new Mock<IRepositoryBase<Domain.Product>>();
            var validator = new GetProductQueryValidator(mockRepository.Object);
            var query = new GetProductQuery
            {
                ProductNumber = "a".PadRight(26, 'b')
            };

            var result = validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(query => query.ProductNumber)
                .WithErrorMessage("Product number must not exceed 25 characters");
        }

        [Fact]
        public void TestValidate_ProductNumberDoesNotExist_ValidationError()
        {
            var mockRepository = new Mock<IRepositoryBase<Domain.Product>>();
            var validator = new GetProductQueryValidator(mockRepository.Object);
            var query = new GetProductQuery
            {
                ProductNumber = "FR-R92B-58"
            };

            var result = validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(query => query.ProductNumber)
                .WithErrorMessage("Product does not exist");
        }
    }
}