using Ardalis.Specification;
using AW.Application.Product.GetProduct;
using AW.Application.Specifications;
using AW.Application.UnitTests.TestBuilders;
using FluentValidation.TestHelper;
using Moq;
using Xunit;

namespace AW.Application.UnitTests
{
    public class GetProductQueryValidatorUnitTests
    {
        [Fact]
        public void ProductNumber_Empty_ValidationError()
        {
            var product = new ProductBuilder().WithTestValues().Build();

            var productRepoMock = new Mock<IRepositoryBase<Domain.Production.Product>>();
            productRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetProductSpecification>()))
                .ReturnsAsync(product);

            var validator = new GetProductQueryValidator(
                productRepoMock.Object
            );

            var query = new GetProductQuery();
            validator.ShouldHaveValidationErrorFor(x => x.ProductNumber, query);
        }

        [Fact]
        public void ProductNumber_TooLong_ValidationError()
        {
            var product = new ProductBuilder().WithTestValues().Build();

            var productRepoMock = new Mock<IRepositoryBase<Domain.Production.Product>>();
            productRepoMock.Setup(x => x.GetBySpecAsync(It.IsAny<GetProductSpecification>()))
                .ReturnsAsync(product);

            var validator = new GetProductQueryValidator(
                productRepoMock.Object
            );

            var query = new GetProductQuery
            {
                ProductNumber = "a".PadRight(26)
            };
            validator.ShouldHaveValidationErrorFor(x => x.ProductNumber, query);
        }

        [Fact]
        public void Product_DoesNotExist_ValidationError()
        {
            var productRepoMock = new Mock<IRepositoryBase<Domain.Production.Product>>();
            var validator = new GetProductQueryValidator(productRepoMock.Object);

            var query = new GetProductQuery();
            validator.ShouldHaveValidationErrorFor(x => x.ProductNumber, query);
        }
    }
}