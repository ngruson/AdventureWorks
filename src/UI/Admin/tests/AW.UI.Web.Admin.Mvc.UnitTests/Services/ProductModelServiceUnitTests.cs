using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProductModels;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace AW.UI.Web.Admin.Mvc.UnitTests.Services
{
    public class ProductModelServiceUnitTests
    {
        public class GetProducts
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnsViewModel(
                [Frozen] Mock<IMediator> mockMediator,
                ProductModelService sut,
                List<ProductModel> productModels
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
                viewModel.Should().BeEquivalentTo(productModels);
            }
        }
    }
}
