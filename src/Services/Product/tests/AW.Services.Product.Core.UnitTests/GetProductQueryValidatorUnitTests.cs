using AutoFixture.Xunit2;
using AW.Services.Product.Core.Handlers.GetProduct;
using AW.Services.Product.Core.Specifications;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentValidation.TestHelper;
using Moq;
using System.Threading;
using Xunit;

namespace AW.Services.Product.Core.UnitTests
{
    public class GetProductQueryValidatorUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void TestValidate_Valid_NoValidationError(
            GetProductQueryValidator sut,
            GetProductQuery query
        )
        {
            query.ProductNumber = query.ProductNumber.Substring(0, 25);
            var result = sut.TestValidate(query);
            result.ShouldNotHaveValidationErrorFor(query => query.ProductNumber);
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void TestValidate_QueryWithEmptyProductNumber_ValidationError(
            GetProductQueryValidator sut,
            GetProductQuery query
        )
        {
            query.ProductNumber = "";
            var result = sut.TestValidate(query);
            result.ShouldHaveValidationErrorFor(query => query.ProductNumber)
                .WithErrorMessage("Product number is required");
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void TestValidate_QueryWithTooLongProductNumber_ValidationError(
            GetProductQueryValidator sut,
            GetProductQuery query
        )
        {
            var result = sut.TestValidate(query);
            result.ShouldHaveValidationErrorFor(query => query.ProductNumber)
                .WithErrorMessage("Product number must not exceed 25 characters");
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void TestValidate_ProductNumberDoesNotExist_ValidationError(
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            GetProductQueryValidator sut,
            GetProductQuery query
        )
        {
            productRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetProductSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Product)null);

            var result = sut.TestValidate(query);
            result.ShouldHaveValidationErrorFor(query => query.ProductNumber)
                .WithErrorMessage("Product does not exist");
        }
    }
}