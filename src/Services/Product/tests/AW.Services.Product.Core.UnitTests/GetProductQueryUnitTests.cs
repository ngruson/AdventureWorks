using AutoFixture.Xunit2;
using AW.Services.Product.Core.Handlers.GetProduct;
using AW.Services.Product.Core.Specifications;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Product.Core.UnitTests
{
    public class GetProductQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async void Handle_ProductExists_ReturnProduct(
            Entities.Product product,
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            GetProductQueryHandler sut,
            GetProductQuery query
        )
        {
            // Arrange
            for (int i = 0; i < product.ProductProductPhotos.Count; i++)
            {
                product.ProductProductPhotos[0].Primary = i == 0;
            }

            productRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetProductSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(product);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            productRepoMock.Verify(x => x.GetBySpecAsync(
                It.IsAny<GetProductSpecification>(),
                It.IsAny<CancellationToken>()
            ));
            result.ProductNumber.Should().Be(product.ProductNumber);
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void Handle_ProductDoesNotExists_ThrowArgumentNullException(
            //Entities.Product product,
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            GetProductQueryHandler sut,
            GetProductQuery query
        )
        {
            // Arrange
            productRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetProductSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Product)null);

            //Act
            Func<Task> func = async() => await sut.Handle(query, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>();
            productRepoMock.Verify(x => x.GetBySpecAsync(
                It.IsAny<GetProductSpecification>(),
                It.IsAny<CancellationToken>()
            ));
        }
    }
}