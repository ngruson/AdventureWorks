using AutoFixture.Xunit2;
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
        public class GetProducts
        {
            [Theory, AutoMapperData(typeof(MappingProfile), typeof(Core.AutoMapper.MappingProfile))]
            public async Task ReturnProductModelsGivenProductModelsExist(
                [Frozen] Mock<IMediator> mockMediator,
                List<ProductModel> productModels,
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

                var response = okObjectResult?.Value as List<ProductModel>;
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
    }
}
