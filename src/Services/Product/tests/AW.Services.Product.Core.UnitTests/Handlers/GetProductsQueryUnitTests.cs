using Ardalis.Specification;
using AutoFixture.Xunit2;
using AW.Services.Product.Core.AutoMapper;
using AW.Services.Product.Core.Exceptions;
using AW.Services.Product.Core.Handlers.GetProducts;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.Product.Core.UnitTests.Handlers
{
    public class GetProductsQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_ProductsExists_ReturnProducts(
            List<Entities.Product> products,
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            GetProductsQueryHandler sut,
            string category,
            string subcategory
        )
        {
            // Arrange
            productRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetProductsPaginatedSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(products);

            var query = new GetProductsQuery(0, 10, category, subcategory, "");

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            productRepoMock.Verify(x => x.ListAsync(
                It.IsAny<ISpecification<Entities.Product>>(),
                It.IsAny<CancellationToken>()
            ));

            for (var i = 0; i < result.Products!.Count; i++)
            {
                result.Products[i].ProductNumber.Should().Be(products[i].ProductNumber);
            }
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_NoProductsExists_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            GetProductsQueryHandler sut,
            string category,
            string subcategory
        )
        {
            // Arrange
            var query = new GetProductsQuery(
                0,
                10,
                category,
                subcategory,
                ""
            );

            productRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetProductsPaginatedSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new List<Entities.Product>());

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ProductsNotFoundException>()
                .WithMessage("Products not found");
            productRepoMock.Verify(x => x.ListAsync(
                It.IsAny<GetProductsPaginatedSpecification>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_ValidAscOrderBy_ReturnProducts(
            List<Entities.Product> products,
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            GetProductsQueryHandler sut,
            string category,
            string subcategory
        )
        {
            //Arrange
            productRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetProductsPaginatedSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(products);

            var query = new GetProductsQuery(0, 10, category, subcategory, "asc(name)");

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            productRepoMock.Verify(x => x.ListAsync(
                It.IsAny<ISpecification<Entities.Product>>(),
                It.IsAny<CancellationToken>()
            ));

            for (var i = 0; i < result.Products!.Count; i++)
            {
                result.Products[i].ProductNumber.Should().Be(products[i].ProductNumber);
            }
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_ValidDescOrderBy_ReturnProducts(
            List<Entities.Product> products,
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            GetProductsQueryHandler sut,
            string category,
            string subcategory
        )
        {
            //Arrange
            productRepoMock.Setup(x => x.ListAsync(
                It.IsAny<GetProductsPaginatedSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(products);

            var query = new GetProductsQuery(0, 10, category, subcategory, "desc(name)");

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            productRepoMock.Verify(x => x.ListAsync(
                It.IsAny<ISpecification<Entities.Product>>(),
                It.IsAny<CancellationToken>()
            ));

            for (var i = 0; i < result.Products!.Count; i++)
            {
                result.Products[i].ProductNumber.Should().Be(products[i].ProductNumber);
            }
        }
    }
}