using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProducts;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace AW.UI.Web.Admin.Mvc.UnitTests.Services
{
    public class ProductServiceUnitTests
    {
        public class GetProducts
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnsViewModelGivenFirstPage(
                [Frozen] Mock<IMediator> mockMediator,
                ProductService sut
            )
            {
                //Arrange
                var products = Enumerable.Repeat(new Product(), 10).ToList();

                mockMediator.Setup(_ => _.Send(
                        It.IsAny<GetProductsQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(new GetProductsResult
                {
                    Products = products,
                    TotalProducts = products.Count * 10
                });

                //Act
                var viewModel = await sut.GetProducts(0, 10);

                //Assert
                viewModel.Products?.Count.Should().Be(10);
                viewModel.PaginationInfo.Should().NotBeNull();
                viewModel.PaginationInfo?.ActualPage.Should().Be(0);
                viewModel.PaginationInfo?.ItemsPerPage.Should().Be(10);
                viewModel.PaginationInfo?.TotalItems.Should().Be(100);
                viewModel.PaginationInfo?.TotalPages.Should().Be(10);
                viewModel.PaginationInfo?.Next.Should().Be("");
                viewModel.PaginationInfo?.Previous.Should().Be("disabled");
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnsViewModelGivenLastPage(
                [Frozen] Mock<IMediator> mockMediator,
                ProductService sut
            )
            {
                //Arrange
                var products = Enumerable.Repeat(new Product(), 10).ToList();

                mockMediator.Setup(_ => _.Send(
                        It.IsAny<GetProductsQuery>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(new GetProductsResult
                {
                    Products = products,
                    TotalProducts = products.Count * 10
                });

                //Act
                var viewModel = await sut.GetProducts(9, 10);

                //Assert
                viewModel.Products!.Count.Should().Be(10);
                viewModel.PaginationInfo?.Should().NotBeNull();
                viewModel.PaginationInfo?.ActualPage.Should().Be(9);
                viewModel.PaginationInfo?.ItemsPerPage.Should().Be(10);
                viewModel.PaginationInfo?.TotalItems.Should().Be(100);
                viewModel.PaginationInfo?.TotalPages.Should().Be(10);
                viewModel.PaginationInfo?.Next.Should().Be("disabled");
                viewModel.PaginationInfo?.Previous.Should().Be("");
            }
        }
    }
}
