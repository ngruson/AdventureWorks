using AW.Services.Product.Core.Handlers.GetProduct;
using AW.Services.Product.Core.Specifications;
using AW.Services.Product.Core.UnitTests.TestBuilders;
using AW.SharedKernel.Interfaces;
using FluentValidation.TestHelper;
using Moq;
using System.Threading;
using Xunit;

namespace AW.Services.Product.Core.UnitTests
{
    public class GetProductQueryValidatorUnitTests
    {
        [Fact]
        public void TestValidate_Valid_NoValidationError()
        {
            var product = new ProductBuilder().WithTestValues().Build();
            var mockRepository = new Mock<IRepository<Entities.Product>>();
            mockRepository.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetProductSpecification>(),
                It.IsAny<CancellationToken>()
            ))
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
            var mockRepository = new Mock<IRepository<Entities.Product>>();
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
            var mockRepository = new Mock<IRepository<Entities.Product>>();
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
            var mockRepository = new Mock<IRepository<Entities.Product>>();
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