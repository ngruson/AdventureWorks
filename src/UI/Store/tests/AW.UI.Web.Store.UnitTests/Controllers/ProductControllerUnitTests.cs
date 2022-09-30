using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProductCategories;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProducts;
using AW.UI.Web.Store.Controllers;
using AW.UI.Web.Store.ViewModels.Product;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.UI.Web.Store.UnitTests.Controllers
{
    public class ProductControllerUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Index_WithProductCategoryFilter_ReturnsProductsViewModel(
            [Frozen] Mock<IMediator> mockMediator,
            Mock<ILogger<ProductController>> mockLogger,
            GetProductsResult productsResult,
            List<ProductCategory> categories,
            string productCategory
        )
        {
            //Arrange
            productsResult.TotalProducts = 20;

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetProductsQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(productsResult);

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetProductCategoriesQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(categories);

            var sut = new ProductController(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockMediator.Object
            );

            //Act
            var actionResult = await sut.Index(0, 5, productCategory, null);

            //Assert
            var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
            var viewModel = viewResult.Model.Should().BeAssignableTo<ProductsViewModel>().Subject;
            viewModel.Title.Should().Be(productCategory);
            viewModel.ProductCategory.Should().Be(productCategory);
            viewModel.ProductSubcategory.Should().BeNull();
            viewModel.ProductCategories.Should().BeEquivalentTo(categories);
            viewModel.Products.Count.Should().Be(productsResult.Products.Count);
            viewModel.PaginationInfo.ActualPage.Should().Be(0);
            viewModel.PaginationInfo.ItemsPerPage.Should().Be(3);
            viewModel.PaginationInfo.TotalItems.Should().Be(productsResult.TotalProducts);
            viewModel.PaginationInfo.TotalPages.Should().Be(4);
            viewModel.PaginationInfo.Next.Should().BeNullOrEmpty();
            viewModel.PaginationInfo.Previous.Should().Be("disabled");
        }

        [Theory, AutoMoqData]
        public async Task Index_WithOddProductCount_ReturnsProductsViewModel(            
            [Frozen] Mock<IMediator> mockMediator,
            Mock<ILogger<ProductController>> mockLogger,
            GetProductsResult productsResult,
            List<ProductCategory> categories,
            string productCategory
        )
        {
            //Arrange
            productsResult.TotalProducts = 21;

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetProductsQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(productsResult);

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetProductCategoriesQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(categories);

            var sut = new ProductController(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockMediator.Object
            );

            //Act
            var actionResult = await sut.Index(0, 5, productCategory, null);

            //Assert
            var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
            var viewModel = viewResult.Model.Should().BeAssignableTo<ProductsViewModel>().Subject;
            viewModel.PaginationInfo.ActualPage.Should().Be(0);
            viewModel.PaginationInfo.ItemsPerPage.Should().Be(3);
            viewModel.PaginationInfo.TotalItems.Should().Be(21);
            viewModel.PaginationInfo.TotalPages.Should().Be(5);            
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Index_FirstPageWithProductCategoryAndSubcategoryFilter_ReturnsProductsViewModel(
            [Frozen] Mock<IMediator> mockMediator,
            Mock<ILogger<ProductController>> mockLogger,
            GetProductsResult productsResult,
            List<ProductCategory> categories,
            string productCategory,
            string productSubcategory
        )
        {
            //Arrange
            productsResult.TotalProducts = 20;


            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetProductsQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(productsResult);

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetProductCategoriesQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(categories);

            var sut = new ProductController(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockMediator.Object
            );

            //Act
            var actionResult = await sut.Index(0, 5, productCategory, productSubcategory);

            //Assert
            var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
            var viewModel = viewResult.Model.Should().BeAssignableTo<ProductsViewModel>().Subject;
            viewModel.Title.Should().Be(productSubcategory);
            viewModel.ProductCategory.Should().Be(productCategory);
            viewModel.ProductSubcategory.Should().Be(productSubcategory);
            viewModel.ProductCategories.Should().BeEquivalentTo(categories);
            viewModel.Products.Count.Should().Be(productsResult.Products.Count);
            viewModel.PaginationInfo.ActualPage.Should().Be(0);
            viewModel.PaginationInfo.ItemsPerPage.Should().Be(3);
            viewModel.PaginationInfo.TotalItems.Should().Be(20);
            viewModel.PaginationInfo.TotalPages.Should().Be(4);
            viewModel.PaginationInfo.Next.Should().BeNullOrEmpty();
            viewModel.PaginationInfo.Previous.Should().Be("disabled");
        }

        [Theory, AutoMoqData]
        public async Task Index_SecondPageWithProductCategoryAndSubcategoryFilter_ReturnsProductsViewModel(
            [Frozen] Mock<IMediator> mockMediator,
            Mock<ILogger<ProductController>> mockLogger,
            GetProductsResult productsResult,
            List<ProductCategory> categories,
            string productCategory,
            string productSubcategory
        )
        {
            //Arrange
            productsResult.TotalProducts = 20;

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetProductsQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(productsResult);

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetProductCategoriesQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(categories);

            var sut = new ProductController(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockMediator.Object
            );

            //Act
            var actionResult = await sut.Index(1, 5, productCategory, productSubcategory);

            //Assert
            var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
            var viewModel = viewResult.Model.Should().BeAssignableTo<ProductsViewModel>().Subject;
            viewModel.Title.Should().Be(productSubcategory);
            viewModel.ProductCategory.Should().Be(productCategory);
            viewModel.ProductSubcategory.Should().Be(productSubcategory);
            viewModel.ProductCategories.Should().BeEquivalentTo(categories);
            viewModel.Products.Count.Should().Be(productsResult.Products.Count);
            viewModel.PaginationInfo.ActualPage.Should().Be(1);
            viewModel.PaginationInfo.ItemsPerPage.Should().Be(3);
            viewModel.PaginationInfo.TotalItems.Should().Be(20);
            viewModel.PaginationInfo.TotalPages.Should().Be(4);
            viewModel.PaginationInfo.Next.Should().BeNullOrEmpty();
            viewModel.PaginationInfo.Previous.Should().BeNullOrEmpty();
        }

        [Theory, AutoMoqData]
        public async Task Index_LastPageWithProductCategoryAndSubcategoryFilter_ReturnsProductsViewModel(
            [Frozen] Mock<IMediator> mockMediator,
            Mock<ILogger<ProductController>> mockLogger,
            GetProductsResult productsResult,
            List<ProductCategory> categories,
            string productCategory,
            string productSubcategory
        )
        {
            //Arrange
            productsResult.TotalProducts = 20;

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetProductsQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(productsResult);

            mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetProductCategoriesQuery>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(categories);

            var sut = new ProductController(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockMediator.Object
            );

            //Act
            var actionResult = await sut.Index(3, 5, productCategory, productSubcategory);

            //Assert
            var viewResult = actionResult.Should().BeAssignableTo<ViewResult>().Subject;
            var viewModel = viewResult.Model.Should().BeAssignableTo<ProductsViewModel>().Subject;
            viewModel.Title.Should().Be(productSubcategory);
            viewModel.ProductCategory.Should().Be(productCategory);
            viewModel.ProductSubcategory.Should().Be(productSubcategory);
            viewModel.ProductCategories.Should().BeEquivalentTo(categories);
            viewModel.Products.Count.Should().Be(3);
            viewModel.PaginationInfo.ActualPage.Should().Be(3);
            viewModel.PaginationInfo.ItemsPerPage.Should().Be(3);
            viewModel.PaginationInfo.TotalItems.Should().Be(20);
            viewModel.PaginationInfo.TotalPages.Should().Be(4);
            viewModel.PaginationInfo.Next.Should().Be("disabled");
            viewModel.PaginationInfo.Previous.Should().BeNullOrEmpty();
        }

        [Theory, AutoMoqData]
        public async Task Index_WithNoProductCategoryFilter_ThrowsException(
            Mock<IMediator> mockMediator,
            Mock<ILogger<ProductController>> mockLogger
        )
        {
            //Arrange

            var sut = new ProductController(
                mockLogger.Object,
                Mapper.CreateMapper(),
                mockMediator.Object
            );

            //Act
            Func<Task> func = async () => await sut.Index(0, 5, null, null);

            //Assert
            await func.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Required input productCategory was empty. (Parameter 'productCategory')");
        }
    }
}