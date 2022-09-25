using AutoFixture.Xunit2;
using AW.Services.Product.Core.AutoMapper;
using AW.Services.Product.Core.Handlers.GetProduct;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentValidation.TestHelper;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Product.Core.UnitTests
{
    public class GetProductQueryValidatorUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task TestValidate_Valid_NoValidationError(
            GetProductQueryValidator sut,
            GetProductQuery query
        )
        {
            query.ProductNumber = query.ProductNumber.Substring(0, 25);
            var result = await sut.TestValidateAsync(query);
            result.ShouldNotHaveValidationErrorFor(query => query.ProductNumber);
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task TestValidate_QueryWithEmptyProductNumber_ValidationError(
            GetProductQueryValidator sut,
            GetProductQuery query
        )
        {
            query.ProductNumber = "";
            var result = await sut.TestValidateAsync(query);
            result.ShouldHaveValidationErrorFor(query => query.ProductNumber)
                .WithErrorMessage("Product number is required");
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task TestValidate_QueryWithTooLongProductNumber_ValidationError(
            GetProductQueryValidator sut,
            GetProductQuery query
        )
        {
            var result = await sut.TestValidateAsync(query);
            result.ShouldHaveValidationErrorFor(query => query.ProductNumber)
                .WithErrorMessage("Product number must not exceed 25 characters");
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task TestValidate_ProductNumberDoesNotExist_ValidationError(
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            GetProductQueryValidator sut,
            GetProductQuery query
        )
        {
            query.ProductNumber = query.ProductNumber.Substring(0, 25);

            productRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Product)null);

            var result = await sut.TestValidateAsync(query);
            result.ShouldHaveValidationErrorFor(query => query.ProductNumber)
                .WithErrorMessage("Product does not exist");
        }
    }
}