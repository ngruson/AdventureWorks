using AW.UI.Web.Infrastructure.ApiClients.ProductApi.Models;
using AW.UI.Web.Store.Controllers;
using AW.UI.Web.Store.Services;
using AW.UI.Web.Store.ViewModels.Product;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AW.UI.Web.Store.UnitTests
{
    public class ProductControllerUnitTests
    {
        [Fact]
        public async void Index_WithProductCategoryFilter_ReturnsProductsViewModel()
        {
            //Arrange
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(x => x.GetProductsAsync(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>()
            ))
            .ReturnsAsync(new GetProductsResult
                {
                    Products = new List<Product>
                    {
                        new Product(),
                        new Product(),
                        new Product(),
                        new Product(),
                        new Product(),
                    },
                    TotalProducts = 20
                }
            );
            mockProductService.Setup(x => x.GetCategoriesAsync())
                .ReturnsAsync(new List<ProductCategory>
                {
                    new ProductCategory { Name = "Bikes" },
                    new ProductCategory { Name = "Components" }
                });

            //Act
            var controller = new ProductController(
                Mapper.CreateMapper(),
                mockProductService.Object
            );
            var actionResult = await controller.Index(0, 5, "Bikes", null);

            //Assert
            var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
            var viewModel = viewResult.Model.Should().BeAssignableTo<ProductsViewModel>().Subject;
            viewModel.Title.Should().Be("Bikes");
            viewModel.ProductCategory.Should().Be("Bikes");
            viewModel.ProductSubcategory.Should().BeNull();
            viewModel.ProductCategories.Should().NotBeNull();
            viewModel.ProductCategories.Count.Should().Be(2);
            viewModel.Products.Count().Should().Be(5);
            viewModel.PaginationInfo.ActualPage.Should().Be(0);
            viewModel.PaginationInfo.ItemsPerPage.Should().Be(5);
            viewModel.PaginationInfo.TotalItems.Should().Be(20);
            viewModel.PaginationInfo.TotalPages.Should().Be(4);
            viewModel.PaginationInfo.Next.Should().BeNullOrEmpty();
            viewModel.PaginationInfo.Previous.Should().Be("disabled");
        }

        [Fact]
        public async void Index_WithOddProductCount_ReturnsProductsViewModel()
        {
            //Arrange
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(x => x.GetProductsAsync(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>()
            ))
            .ReturnsAsync(new GetProductsResult
            {
                Products = new List<Product>
                    {
                        new Product(),
                        new Product(),
                        new Product(),
                        new Product(),
                        new Product(),
                    },
                TotalProducts = 21
            }
            );
            mockProductService.Setup(x => x.GetCategoriesAsync())
                .ReturnsAsync(new List<ProductCategory>
                {
                    new ProductCategory { Name = "Bikes" },
                    new ProductCategory { Name = "Components" }
                });

            //Act
            var controller = new ProductController(
                Mapper.CreateMapper(),
                mockProductService.Object
            );
            var actionResult = await controller.Index(0, 5, "Bikes", null);

            //Assert
            var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
            var viewModel = viewResult.Model.Should().BeAssignableTo<ProductsViewModel>().Subject;
            viewModel.PaginationInfo.ActualPage.Should().Be(0);
            viewModel.PaginationInfo.ItemsPerPage.Should().Be(5);
            viewModel.PaginationInfo.TotalItems.Should().Be(21);
            viewModel.PaginationInfo.TotalPages.Should().Be(5);            
        }

        [Fact]
        public async void Index_FirstPageWithProductCategoryAndSubcategoryFilter_ReturnsProductsViewModel()
        {
            //Arrange
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(x => x.GetProductsAsync(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>()
            ))
            .ReturnsAsync(new GetProductsResult
            {
                Products = new List<Product>
                    {
                        new Product(),
                        new Product(),
                        new Product(),
                        new Product(),
                        new Product(),
                    },
                TotalProducts = 20
            }
            );
            mockProductService.Setup(x => x.GetCategoriesAsync())
                .ReturnsAsync(new List<ProductCategory>
                {
                    new ProductCategory { Name = "Bikes" },
                    new ProductCategory { Name = "Components" }
                });

            //Act
            var controller = new ProductController(
                Mapper.CreateMapper(),
                mockProductService.Object
            );
            var actionResult = await controller.Index(0, 5, "Bikes", "Mountain Bikes");

            //Assert
            var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
            var viewModel = viewResult.Model.Should().BeAssignableTo<ProductsViewModel>().Subject;
            viewModel.Title.Should().Be("Mountain Bikes");
            viewModel.ProductCategory.Should().Be("Bikes");
            viewModel.ProductSubcategory.Should().Be("Mountain Bikes");
            viewModel.ProductCategories.Should().NotBeNull();
            viewModel.ProductCategories.Count.Should().Be(2);
            viewModel.Products.Count().Should().Be(5);
            viewModel.PaginationInfo.ActualPage.Should().Be(0);
            viewModel.PaginationInfo.ItemsPerPage.Should().Be(5);
            viewModel.PaginationInfo.TotalItems.Should().Be(20);
            viewModel.PaginationInfo.TotalPages.Should().Be(4);
            viewModel.PaginationInfo.Next.Should().BeNullOrEmpty();
            viewModel.PaginationInfo.Previous.Should().Be("disabled");
        }

        [Fact]
        public async void Index_SecondPageWithProductCategoryAndSubcategoryFilter_ReturnsProductsViewModel()
        {
            //Arrange
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(x => x.GetProductsAsync(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>()
            ))
            .ReturnsAsync(new GetProductsResult
            {
                Products = new List<Product>
                    {
                        new Product(),
                        new Product(),
                        new Product(),
                        new Product(),
                        new Product(),
                    },
                TotalProducts = 20
            }
            );
            mockProductService.Setup(x => x.GetCategoriesAsync())
                .ReturnsAsync(new List<ProductCategory>
                {
                    new ProductCategory { Name = "Bikes" },
                    new ProductCategory { Name = "Components" }
                });

            //Act
            var controller = new ProductController(
                Mapper.CreateMapper(),
                mockProductService.Object
            );
            var actionResult = await controller.Index(1, 5, "Bikes", "Mountain Bikes");

            //Assert
            var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
            var viewModel = viewResult.Model.Should().BeAssignableTo<ProductsViewModel>().Subject;
            viewModel.Title.Should().Be("Mountain Bikes");
            viewModel.ProductCategory.Should().Be("Bikes");
            viewModel.ProductSubcategory.Should().Be("Mountain Bikes");
            viewModel.ProductCategories.Should().NotBeNull();
            viewModel.ProductCategories.Count.Should().Be(2);
            viewModel.Products.Count().Should().Be(5);
            viewModel.PaginationInfo.ActualPage.Should().Be(1);
            viewModel.PaginationInfo.ItemsPerPage.Should().Be(5);
            viewModel.PaginationInfo.TotalItems.Should().Be(20);
            viewModel.PaginationInfo.TotalPages.Should().Be(4);
            viewModel.PaginationInfo.Next.Should().BeNullOrEmpty();
            viewModel.PaginationInfo.Previous.Should().BeNullOrEmpty();
        }

        [Fact]
        public async void Index_LastPageWithProductCategoryAndSubcategoryFilter_ReturnsProductsViewModel()
        {
            //Arrange
            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(x => x.GetProductsAsync(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>()
            ))
            .ReturnsAsync(new GetProductsResult
            {
                Products = new List<Product>
                    {
                        new Product(),
                        new Product(),
                        new Product(),
                        new Product(),
                        new Product(),
                    },
                TotalProducts = 20
            }
            );
            mockProductService.Setup(x => x.GetCategoriesAsync())
                .ReturnsAsync(new List<ProductCategory>
                {
                    new ProductCategory { Name = "Bikes" },
                    new ProductCategory { Name = "Components" }
                });

            //Act
            var controller = new ProductController(
                Mapper.CreateMapper(),
                mockProductService.Object
            );
            var actionResult = await controller.Index(3, 5, "Bikes", "Mountain Bikes");

            //Assert
            var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
            var viewModel = viewResult.Model.Should().BeAssignableTo<ProductsViewModel>().Subject;
            viewModel.Title.Should().Be("Mountain Bikes");
            viewModel.ProductCategory.Should().Be("Bikes");
            viewModel.ProductSubcategory.Should().Be("Mountain Bikes");
            viewModel.ProductCategories.Should().NotBeNull();
            viewModel.ProductCategories.Count.Should().Be(2);
            viewModel.Products.Count().Should().Be(5);
            viewModel.PaginationInfo.ActualPage.Should().Be(3);
            viewModel.PaginationInfo.ItemsPerPage.Should().Be(5);
            viewModel.PaginationInfo.TotalItems.Should().Be(20);
            viewModel.PaginationInfo.TotalPages.Should().Be(4);
            viewModel.PaginationInfo.Next.Should().Be("disabled");
            viewModel.PaginationInfo.Previous.Should().BeNullOrEmpty();
        }

        [Fact]
        public void Index_WithNoProductCategoryFilter_ThrowsException()
        {
            //Arrange
            var mockProductService = new Mock<IProductService>();

            //Act
            var controller = new ProductController(
                Mapper.CreateMapper(),
                mockProductService.Object
            );
            Func<Task> func = async () => await controller.Index(0, 5, null, null);

            //Assert
            func.Should().Throw<ArgumentNullException>();
        }
    }
}