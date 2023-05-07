using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProductModel;
using AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProductModels;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace AW.UI.Web.Admin.Mvc.UnitTests.Services
{
    public class ProductModelServiceUnitTests
    {
        public class GetProductModels
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnsProductModels(
                [Frozen] Mock<IMediator> mockMediator,
                ProductModelService sut,
                List<Infrastructure.Api.Product.Handlers.GetProductModels.ProductModel> productModels
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                        It.IsAny<GetProductModelsQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(productModels);

                //Act
                var viewModel = await sut.GetProductModels();

                //Assert
                viewModel.Should().BeEquivalentTo(productModels, opt => opt
                    .Excluding(_ => _.Instructions)
                );
            }
        }

        public class GetProductModel
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnsProductModel(
                [Frozen] Mock<IMediator> mockMediator,
                ProductModelService sut,
                Infrastructure.Api.Product.Handlers.GetProductModel.ProductModel productModel
            )
            {
                //Arrange
                productModel.Instructions = "<xml></xml>";

                mockMediator.Setup(_ => _.Send(
                        It.IsAny<GetProductModelQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(productModel);

                //Act
                var viewModel = await sut.GetProductModel(productModel.Name!);

                //Assert
                viewModel.Should().BeEquivalentTo(productModel, opt => opt
                    .Excluding(_ => _.Instructions)
                );
            }
        }
    }
}
