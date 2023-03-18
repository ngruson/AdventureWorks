using Ardalis.Specification;
using AutoFixture.Xunit2;
using AW.Services.Product.Core.AutoMapper;
using AW.Services.Product.Core.Exceptions;
using AW.Services.Product.Core.Handlers.GetProductModels;
using AW.Services.Product.Core.Handlers.GetProducts;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.Product.Core.UnitTests.Handlers
{
    public class GetProductModelsQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ReturnProductModelsGivenProductModelsExist(
            List<Entities.ProductModel> productModels,
            [Frozen] Mock<IRepository<Entities.ProductModel>> repoMock,
            GetProductModelsQueryHandler sut,
            GetProductModelsQuery query
        )
        {
            // Arrange
            repoMock.Setup(x => x.ListAsync(
                It.IsAny<GetProductModelsSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(productModels);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            repoMock.Verify(x => x.ListAsync(
                It.IsAny<ISpecification<Entities.ProductModel>>(),
                It.IsAny<CancellationToken>()
            ));

            result.Should().BeEquivalentTo(productModels, options => options
                .Excluding(_ => _.Id)
                .Excluding(_ => _.ProductModelIllustrations)
                .Excluding(_ => _.ProductModelProductDescriptionCultures)
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ThrowProductModelsNotFoundExceptionGivenNoProductModelsExist(
            [Frozen] Mock<IRepository<Entities.ProductModel>> repoMock,
            GetProductModelsQueryHandler sut,
            GetProductModelsQuery query
        )
        {
            // Arrange
            repoMock.Setup(x => x.ListAsync(
                It.IsAny<GetProductModelsSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new List<Entities.ProductModel>());

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ProductModelsNotFoundException>()
                .WithMessage("Product models not found");
            repoMock.Verify(x => x.ListAsync(
                It.IsAny<GetProductModelsSpecification>(),
                It.IsAny<CancellationToken>()
            ));
        }
    }
}
