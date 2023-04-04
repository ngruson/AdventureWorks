using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Admin.Mvc.Services;
using AW.UI.Web.Admin.Mvc.ViewModels.Product;
using AW.UI.Web.SharedKernel.Product.Handlers.CreateProduct;
using AW.UI.Web.SharedKernel.Product.Handlers.DeleteProduct;
using AW.UI.Web.SharedKernel.Product.Handlers.DuplicateProduct;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProduct;
using AW.UI.Web.SharedKernel.Product.Handlers.GetProducts;
using AW.UI.Web.SharedKernel.Product.Handlers.UpdateProduct;
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
                var products = Enumerable.Repeat(
                    new SharedKernel.Product.Handlers.GetProducts.Product(), 
                    10
                ).ToList();

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
                var products = Enumerable.Repeat(
                    new SharedKernel.Product.Handlers.GetProducts.Product(), 
                    10
                ).ToList();

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

        public class GetProductDetail
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ReturnsProductViewModelGivenProductExists(
                [Frozen] Mock<IMediator> mockMediator,
                ProductService sut,
                string productNumber,
                SharedKernel.Product.Handlers.GetProduct.Product product
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetProductQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(product);

                //Act
                var viewModel = await sut.GetProductDetail(productNumber);

                //Assert
                viewModel.Product.Should().BeEquivalentTo(product, opt => opt
                    .Excluding(_ => _.StandardCost)
                    .Excluding(_ => _.ListPrice)
                );
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ThrowsArgumentNullExceptionGivenProductDoesNotExist(
                [Frozen] Mock<IMediator> mockMediator,
                ProductService sut,
                string productNumber
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetProductQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync((SharedKernel.Product.Handlers.GetProduct.Product?)null);

                //Act
                Func<Task> func = async () => await sut.GetProductDetail(productNumber);

                //Assert
                await func.Should().ThrowAsync<ArgumentNullException>();
                mockMediator.Verify(x => x.Send(
                    It.IsAny<GetProductQuery>(),
                    It.IsAny<CancellationToken>()
                ));
            }
        }

        public class AddProduct
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task AddProductSucceeds(
                [Frozen] Mock<IMediator> mockMediator,
                ProductService sut,
                AddProductViewModel viewModel
            )
            {
                //Arrange

                //Act
                await sut.AddProduct(viewModel);

                //Assert
                mockMediator.Verify(_ => _.Send(
                    It.IsAny<CreateProductCommand>(),
                    It.IsAny<CancellationToken>()
                ));
            }
        }

        public class UpdateProduct
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task UpdateProductGivenProductExists(
                [Frozen] Mock<IMediator> mockMediator,
                ProductService sut,
                EditProductViewModel viewModel,
                SharedKernel.Product.Handlers.GetProduct.Product product
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetProductQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(product);

                //Act
                await sut.UpdateProduct(viewModel);

                //Assert
                mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetProductQuery>(),
                    It.IsAny<CancellationToken>()
                ));

                mockMediator.Verify(_ => _.Send(
                    It.IsAny<UpdateProductCommand>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ThrowArgumentNullExceptionGivenProductDoesNotExist(
                [Frozen] Mock<IMediator> mockMediator,
                ProductService sut,
                EditProductViewModel viewModel
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetProductQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync((SharedKernel.Product.Handlers.GetProduct.Product?)null);

                //Act
                Func<Task> func = async () => await sut.UpdateProduct(viewModel);

                //Assert
                await func.Should().ThrowAsync<ArgumentNullException>();

                mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetProductQuery>(),
                    It.IsAny<CancellationToken>()
                ));

                mockMediator.Verify(_ => _.Send(
                    It.IsAny<UpdateProductCommand>(),
                    It.IsAny<CancellationToken>()
                ), Times.Never);
            }
        }

        public class UpdatePricing
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task UpdatePricingGivenProductExists(
                [Frozen] Mock<IMediator> mockMediator,
                ProductService sut,
                EditPricingViewModel viewModel,
                SharedKernel.Product.Handlers.GetProduct.Product product
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetProductQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(product);

                //Act
                await sut.UpdatePricing(viewModel);

                //Assert
                mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetProductQuery>(),
                    It.IsAny<CancellationToken>()
                ));

                mockMediator.Verify(_ => _.Send(
                    It.IsAny<UpdateProductCommand>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ThrowArgumentNullExceptionGivenProductDoesNotExist(
                [Frozen] Mock<IMediator> mockMediator,
                ProductService sut,
                EditPricingViewModel viewModel
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetProductQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync((SharedKernel.Product.Handlers.GetProduct.Product?)null);

                //Act
                Func<Task> func = async () => await sut.UpdatePricing(viewModel);

                //Assert
                await func.Should().ThrowAsync<ArgumentNullException>();

                mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetProductQuery>(),
                    It.IsAny<CancellationToken>()
                ));

                mockMediator.Verify(_ => _.Send(
                    It.IsAny<UpdateProductCommand>(),
                    It.IsAny<CancellationToken>()
                ), Times.Never);
            }
        }

        public class UpdateProductOrganization
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task UpdateProductOrganizationGivenProductExists(
                [Frozen] Mock<IMediator> mockMediator,
                ProductService sut,
                EditProductOrganizationViewModel viewModel,
                SharedKernel.Product.Handlers.GetProduct.Product product
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetProductQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync(product);

                //Act
                await sut.UpdateProductOrganization(viewModel);

                //Assert
                mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetProductQuery>(),
                    It.IsAny<CancellationToken>()
                ));

                mockMediator.Verify(_ => _.Send(
                    It.IsAny<UpdateProductCommand>(),
                    It.IsAny<CancellationToken>()
                ));
            }

            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task ThrowArgumentNullExceptionGivenProductDoesNotExist(
                [Frozen] Mock<IMediator> mockMediator,
                ProductService sut,
                EditProductOrganizationViewModel viewModel
            )
            {
                //Arrange
                mockMediator.Setup(_ => _.Send(
                    It.IsAny<GetProductQuery>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync((SharedKernel.Product.Handlers.GetProduct.Product?)null);

                //Act
                Func<Task> func = async () => await sut.UpdateProductOrganization(viewModel);

                //Assert
                await func.Should().ThrowAsync<ArgumentNullException>();

                mockMediator.Verify(_ => _.Send(
                    It.IsAny<GetProductQuery>(),
                    It.IsAny<CancellationToken>()
                ));

                mockMediator.Verify(_ => _.Send(
                    It.IsAny<UpdateProductCommand>(),
                    It.IsAny<CancellationToken>()
                ), Times.Never);
            }
        }

        public class DeleteProduct
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task DeleteProductGivenProductExists(
                [Frozen] Mock<IMediator> mediator,
                ProductService sut,
                string productNumber
            )
            {
                //Arrange

                //Act
                await sut.DeleteProduct(productNumber);

                //Assert
                mediator.Verify(_ => _.Send(
                        It.IsAny<DeleteProductCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                );
            }
        }

        public class DuplicateProduct
        {
            [Theory, AutoMapperData(typeof(MappingProfile))]
            public async Task DuplicateProductGivenProductExists(
                [Frozen] Mock<IMediator> mediator,
                ProductService sut,
                string productNumber
            )
            {
                //Arrange

                //Act
                await sut.DuplicateProduct(productNumber);

                //Assert
                mediator.Verify(_ => _.Send(
                        It.IsAny<DuplicateProductCommand>(),
                        It.IsAny<CancellationToken>()
                    )
                );
            }
        }
    }
}
