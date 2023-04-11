using AutoFixture.Xunit2;
using AW.Services.Product.Core.Handlers.GetProductModel;
using AW.Services.Product.Core.Handlers.GetProductModels;
using AW.Services.Product.REST.API.Controllers;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AW.Services.Product.REST.API.UnitTests
{
    public class ProductModelControllerUnitTests
    {
        public class GetProductModels
        {
            [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.AutoMapper.MappingProfile))]
            public async Task ReturnProductModelsGivenProductModelsExist(
                [Frozen] Mock<IMediator> mockMediator,
                List<Core.Handlers.GetProductModels.ProductModel> productModels,
                [Greedy] ProductModelController sut
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                    It.IsAny<GetProductModelsQuery>(), 
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(productModels);

                //Act
                var actionResult = await sut.GetProductModels();

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var response = okObjectResult?.Value as List<Core.Handlers.GetProductModels.ProductModel>;
                response?.Should().BeEquivalentTo(productModels);
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnNotFoundGivenProductModelsDoNotExist(
                [Greedy] ProductModelController sut
            )
            {
                //Act
                var actionResult = await sut.GetProductModels();

                //Assert
                actionResult.Should().BeOfType<NotFoundResult>();
            }
        }

        public class GetProductModel
        {
            [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.AutoMapper.MappingProfile))]
            public async Task ReturnProductModelGivenProductModelExist(
                [Frozen] Mock<IMediator> mockMediator,
                Core.Handlers.GetProductModel.ProductModel productModel,
                [Greedy] ProductModelController sut,
                GetProductModelQuery query
            )
            {
                //Arrange
                mockMediator.Setup(x => x.Send(
                    It.IsAny<GetProductModelQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(productModel);

                //Act
                var actionResult = await sut.GetProductModel(query);

                //Assert
                var okObjectResult = actionResult as OkObjectResult;
                okObjectResult.Should().NotBeNull();

                var response = okObjectResult?.Value as Core.Handlers.GetProductModel.ProductModel;
                response?.Should().BeEquivalentTo(productModel);
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnNotFoundGivenProductModelDoesNotExist(
                [Frozen] Mock<IMediator> mockMediator,
                [Greedy] ProductModelController sut,
                GetProductModelQuery query
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetProductModelQuery>(),
                    It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync((Core.Handlers.GetProductModel.ProductModel?)null);

                //Act
                var actionResult = await sut.GetProductModel(query);

                //Assert
                actionResult.Should().BeOfType<NotFoundResult>();
            }
        }
    }
}
