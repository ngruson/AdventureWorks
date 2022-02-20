using AutoFixture.Xunit2;
using AW.Services.Product.Core.AutoMapper;
using AW.Services.Product.Core.Handlers.GetAllProductsWithPhotos;
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
    public class GetAllProductsWithPhotosQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_ProductsExists_ReturnProducts(
            //Entities.Product product,
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            GetAllProductsWithPhotosQueryHandler sut,
            GetAllProductsWithPhotosQuery query,
            List<Entities.Product> products,
            Entities.ProductProductPhoto photo
        )
        {
            // Arrange
            products.ForEach(p =>
                p.AddProductPhoto(photo)
            );

            productRepoMock.Setup(_ => _.ListAsync(
                    It.IsAny<GetAllProductsWithPhotosSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(products);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            productRepoMock.Verify(x => x.ListAsync(
                It.IsAny<GetAllProductsWithPhotosSpecification>(),
                It.IsAny<CancellationToken>()
            ));
            result.Count.Should().Be(products.Count);
            result.ForEach(x => x.Photos.Count.Should().BeGreaterThan(0));
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_NoProductsExists_ThrowArgumentException(
            //Entities.Product product,
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            GetAllProductsWithPhotosQueryHandler sut,
            GetAllProductsWithPhotosQuery query
        )
        {
            // Arrange
            productRepoMock.Setup(_ => _.ListAsync(
                    It.IsAny<GetAllProductsWithPhotosSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync((List<Entities.Product>)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert            
            await func.Should().ThrowAsync<ArgumentNullException>();
        }
    }
}