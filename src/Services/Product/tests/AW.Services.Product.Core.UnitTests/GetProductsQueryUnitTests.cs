using Ardalis.Specification;
using AutoFixture.Xunit2;
using AW.Services.Product.Core.AutoMapper;
using AW.Services.Product.Core.Handlers.GetProducts;
using AW.Services.Product.Core.Specifications;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Product.Core.UnitTests
{
    public class GetProductsQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_ProductsExists_ReturnProducts(
            List<Entities.Product> products,
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            GetProductsQueryHandler sut,
            GetProductsQuery query
        )
        {
            // Arrange
            products.ForEach(product =>
            {
                for (int i = 0; i < product.ProductProductPhotos.Count; i++)
                {
                    product.ProductProductPhotos[0].Primary = i == 0;
                }
            });

            productRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetProductsPaginatedSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(products);

            query.OrderBy = null;

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            productRepoMock.Verify(x => x.ListAsync(
                It.IsAny<ISpecification<Entities.Product>>(),
                It.IsAny<CancellationToken>()
            ));

            for (int i = 0; i < result.Products.Count; i++)
            {
                result.Products[i].ProductNumber.Should().Be(products[i].ProductNumber);
            }
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void Handle_NoProductsExists_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            GetProductsQueryHandler sut,
            GetProductsQuery query
        )
        {
            // Arrange
            query.OrderBy = "";

            productRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetProductsPaginatedSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((List<Entities.Product>)null);

            //Act
            Func<Task> func = async() => await sut.Handle(query, CancellationToken.None);

            //Assert
            func.Should().ThrowAsync<ArgumentNullException>();
            productRepoMock.Verify(x => x.ListAsync(
                It.IsAny<ISpecification<Entities.Product>>(), 
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_ValidAscOrderBy_ReturnProducts(
            List<Entities.Product> products,
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            GetProductsQueryHandler sut,
            GetProductsQuery query
        )
        {
            //Arrange
            products.ForEach(product =>
            {
                for (int i = 0; i < product.ProductProductPhotos.Count; i++)
                {
                    product.ProductProductPhotos[i].Primary = i == 0;
                }
            });

            productRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetProductsPaginatedSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(products);

            query.OrderBy = "asc(name)";

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            productRepoMock.Verify(x => x.ListAsync(
                It.IsAny<ISpecification<Entities.Product>>(),
                It.IsAny<CancellationToken>()
            ));

            for (int i = 0; i < result.Products.Count; i++)
            {
                result.Products[i].ProductNumber.Should().Be(products[i].ProductNumber);
            }
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_ValidDescOrderBy_ReturnProducts(
            List<Entities.Product> products,
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            GetProductsQueryHandler sut,
            GetProductsQuery query
        )
        {
            //Arrange
            products.ForEach(product =>
            {
                for (int i = 0; i < product.ProductProductPhotos.Count; i++)
                {
                    product.ProductProductPhotos[i].Primary = i == 0;
                }
            });

            productRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetProductsPaginatedSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(products);

            query.OrderBy = "desc(name)";

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            productRepoMock.Verify(x => x.ListAsync(
                It.IsAny<ISpecification<Entities.Product>>(),
                It.IsAny<CancellationToken>()
            ));

            for (int i = 0; i < result.Products.Count; i++)
            {
                result.Products[i].ProductNumber.Should().Be(products[i].ProductNumber);
            }
        }
    }
}