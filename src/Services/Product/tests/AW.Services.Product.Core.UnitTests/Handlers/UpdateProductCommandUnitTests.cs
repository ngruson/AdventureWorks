using AutoFixture.Xunit2;
using AW.Services.Product.Core.AutoMapper;
using AW.Services.Product.Core.Exceptions;
using AW.Services.Product.Core.Handlers.UpdateProduct;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.Product.Core.UnitTests.Handlers
{
    public class UpdateProductCommandUnitTests
    {
        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task ReturnUpdatedProductGivenProductExists(
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            Entities.Product product,
            UpdateProductCommandHandler sut,
            UpdateProductCommand command
        )
        {
            //Arrange
            command.Product!.ProductLine = Entities.ProductLine.Mountain.Name;
            command.Product!.Class = Entities.Class.High.Name;
            command.Product!.Style = Entities.Style.Universal.Name;

            productRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetProductSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            ).
            ReturnsAsync(product);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();            
            productRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductSpecification>(),
                It.IsAny<CancellationToken>()
            ));
            productRepoMock.Verify(x => x.UpdateAsync(
                It.IsAny<Entities.Product>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMapperData(typeof(MappingProfile))]
        public async Task ReturnUpdatedProductGivenProductModelHasChanged(
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            [Frozen] Mock<IRepository<Entities.ProductModel>> productModelRepoMock,
            Entities.Product product,
            Entities.ProductModel productModel,
            UpdateProductCommandHandler sut,
            UpdateProductCommand command
        )
        {
            //Arrange
            command.Product!.ProductLine = Entities.ProductLine.Mountain.Name;
            command.Product!.Class = Entities.Class.High.Name;
            command.Product!.Style = Entities.Style.Universal.Name;

            productRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetProductSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            ).
            ReturnsAsync(product);

            productModelRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetProductModelSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            ).
            ReturnsAsync(productModel);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            productRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductSpecification>(),
                It.IsAny<CancellationToken>()
            ));
            productModelRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductModelSpecification>(),
                It.IsAny<CancellationToken>()
            ));
            productRepoMock.Verify(x => x.UpdateAsync(
                It.IsAny<Entities.Product>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public async Task ThrowProductNotFoundExceptionGivenProductDoesNotExist(
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            UpdateProductCommandHandler sut,
            UpdateProductCommand command
        )
        {
            // Arrange
            productRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.Product?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ProductNotFoundException>()
                .WithMessage($"Product {command.Product!.ProductNumber} not found");
        }

        [Theory]
        [AutoMoqData]
        public async Task ThrowProductModelNotFoundExceptionGivenProductModelDoesNotExist(
            [Frozen] Mock<IRepository<Entities.ProductModel>> productModelRepoMock,
            UpdateProductCommandHandler sut,
            UpdateProductCommand command
        )
        {
            // Arrange
            productModelRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductModelSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Entities.ProductModel?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ProductModelNotFoundException>()
                .WithMessage($"Product model {command.Product!.ProductModelName} not found");
        }
    }
}
