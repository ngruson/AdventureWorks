using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Admin.Mvc.Controllers;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Product;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProductCategories;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AW.UI.Web.Admin.Mvc.UnitTests.Controllers
{
    public class ProductControllerUnitTests
    {
        public class Index
        {
            [Theory, AutoMoqData]
            public async Task Index_ReturnsViewModel(
                [Frozen] Mock<IProductService> productService,
                ProductIndexViewModel viewModel,
                [Greedy] ProductController sut
            )
            {
                //Arrange
                productService.Setup(x => x.GetProducts(
                    It.IsAny<int>(),
                    It.IsAny<int>()
                ))
                .ReturnsAsync(viewModel);

                //Act
                var actionResult = await sut.Index(
                    0
                );

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);
            }
        }

        public class Detail
        {
            [Theory, AutoMoqData]
            public async Task ReturnsViewModelGivenProductExists(
                [Frozen] Mock<IProductService> productService,
                [Frozen] Mock<IMediator> mockMediator,
                ProductDetailViewModel viewModel,
                [Greedy] ProductController sut,
                string productNumber,
                List<ProductCategory> categories
            )
            {
                //Arrange
                productService.Setup(x => x.GetProductDetail(
                    It.IsAny<string>()
                ))
                .ReturnsAsync(viewModel);

                categories[0].Name = viewModel.Product!.ProductCategoryName;

                mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetProductCategoriesQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(categories);

                //Act
                var actionResult = await sut.Detail(productNumber);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
                viewResult.Model.Should().Be(viewModel);
            }
        }

        public class AddProduct
        {
            [Theory, AutoMoqData]
            public async Task AddProduct_Ok(
                AddProductViewModel viewModel,
                [Greedy] ProductController sut
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.AddProduct(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                viewResult.ActionName.Should().Be(nameof(ProductController.Detail));
                viewResult.RouteValues!.Count.Should().Be(1);
                viewResult.RouteValues.ContainsKey("productNumber");
                viewResult.RouteValues.Values.ToList()[0].Should().Be(viewModel.Product!.ProductNumber);
            }
        }

        public class UpdateProduct
        {
            [Theory, AutoMoqData]
            public async Task UpdateProductGivenProductExists(
                EditProductViewModel viewModel,
                [Greedy] ProductController sut
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.UpdateProduct(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                viewResult.ActionName.Should().Be(nameof(ProductController.Detail));
                viewResult.RouteValues!.Count.Should().Be(1);
                viewResult.RouteValues.ContainsKey("productNumber");
                viewResult.RouteValues.Values.ToList()[0].Should().Be(viewModel.Product!.ProductNumber);
            }
        }

        public class UpdatePricing
        {
            [Theory, AutoMoqData]
            public async Task UpdatePricingGivenProductExists(
                EditPricingViewModel viewModel,
                [Greedy] ProductController sut
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.UpdatePricing(viewModel);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                viewResult.ActionName.Should().Be(nameof(ProductController.Detail));
                viewResult.RouteValues!.Count.Should().Be(1);
                viewResult.RouteValues.ContainsKey("productNumber");
                viewResult.RouteValues.Values.ToList()[0].Should().Be(viewModel.Product!.ProductNumber);
            }
        }

        public class DeleteProducts
        {
            [Theory, AutoMoqData]
            public async Task DeleteProductsGivenProductNumbers(
                string[] productNumbers,
                [Greedy] ProductController sut
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.DeleteProducts(productNumbers);

                //Assert
                actionResult.Should().BeOfType<OkResult>();
            }
        }

        public class DeleteProduct
        {
            [Theory, AutoMoqData]
            public async Task DeleteProductGivenProductExists(
                string productNumber,
                [Greedy] ProductController sut
            )
            {
                //Arrange

                //Act
                var actionResult = await sut.DeleteProduct(productNumber);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                viewResult.ActionName.Should().Be(nameof(ProductController.Index));
            }
        }

        public class DuplicateProduct
        {
            [Theory, AutoMoqData]
            public async Task DuplicateProductGivenProductExists(
                string productNumber,
                [Frozen] Mock<IProductService> productService,
                [Greedy] ProductController sut,
                SharedKernel.Product.Handlers.DuplicateProduct.Product product
            )
            {
                //Arrange
                productService.Setup(_ => _.DuplicateProduct(
                        It.IsAny<string>()
                    )
                ).
                ReturnsAsync(product);

                //Act
                var actionResult = await sut.DuplicateProduct(productNumber);

                //Assert
                var viewResult = actionResult.Should().BeAssignableTo<RedirectToActionResult>().Subject;
                viewResult.ActionName.Should().Be(nameof(ProductController.Detail));
                viewResult.RouteValues!.Count.Should().Be(1);
                viewResult.RouteValues.ContainsKey("productNumber");
                viewResult.RouteValues.Values.ToList()[0].Should().Be(product!.ProductNumber);
            }
        }
    }
}
