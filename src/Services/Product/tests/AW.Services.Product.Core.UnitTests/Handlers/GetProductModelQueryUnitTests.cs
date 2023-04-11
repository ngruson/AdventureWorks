using AutoFixture.Xunit2;
using AW.Services.Product.Core.AutoMapper;
using AW.Services.Product.Core.Exceptions;
using AW.Services.Product.Core.Handlers.GetProductModel;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.Product.Core.UnitTests.Handlers
{
    public class GetProductModelQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ReturnProductModelGivenProductModelExists(
            Entities.ProductModel productModel,
            [Frozen] Mock<IRepository<Entities.ProductModel>> repoMock,
            GetProductModelQueryHandler sut,
            GetProductModelQuery query
        )
        {
            // Arrange
            repoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductModelSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(productModel);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().BeEquivalentTo(productModel, options => options
                .Excluding(_ => _.Id)
                .Excluding(_ => _.ProductModelIllustrations)
                .Excluding(_ => _.ProductModelProductDescriptionCultures)
            );

            repoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductModelSpecification>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ThrowProductModelNotFoundExceptionGivenProductModelDoesNotExist(
            [Frozen] Mock<IRepository<Entities.ProductModel>> repoMock,
            GetProductModelQueryHandler sut,
            GetProductModelQuery query
        )
        {
            // Arrange
            repoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductModelSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.ProductModel?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ProductModelNotFoundException>()
                .WithMessage($"Product model {query.Name} not found");
            repoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductModelSpecification>(),
                It.IsAny<CancellationToken>()
            ));
        }
    }
}
