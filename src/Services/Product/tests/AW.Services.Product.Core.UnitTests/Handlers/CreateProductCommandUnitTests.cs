using AutoFixture.Xunit2;
using AW.Services.Product.Core.AutoMapper;
using AW.Services.Product.Core.Entities;
using AW.Services.Product.Core.Exceptions;
using AW.Services.Product.Core.Handlers.CreateProduct;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.Product.Core.UnitTests.Handlers
{
    public class CreateProductCommandUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ReturnProductGivenProductCreationSucceeds(
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            [Frozen] Mock<IRepository<ProductSubcategory>> productSubcategoryRepoMock,
            [Frozen] Mock<IRepository<ProductModel>> productModelRepoMock,
            [Frozen] Mock<IRepository<UnitMeasure>> unitMeasureRepoMock,
            CreateProductCommandHandler sut,
            CreateProductCommand command
        )
        {
            // Arrange
            command.Product!.ProductLine = ProductLine.Road.ToString();
            command.Product.Class = Class.High.ToString();
            command.Product.Style = Style.Mens.ToString();

            //Act
            var product = await sut.Handle(command, CancellationToken.None);

            //Assert
            product.Should().BeEquivalentTo(command.Product, config => config
                .Excluding(_ => _.SizeUnitMeasureCode)
                .Excluding(_ => _.WeightUnitMeasureCode)
                .Excluding(_ => _.ProductModelName)
                .Excluding(_ => _.ProductSubcategoryName)
                .Excluding(_ => _.ProductCategoryName)
            );

            productSubcategoryRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductSubcategorySpecification>(),
                It.IsAny<CancellationToken>()
            ));

            productModelRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductModelSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            unitMeasureRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetUnitMeasureSpecification>(),
                It.IsAny<CancellationToken>()
            ), Times.Exactly(2));

            productRepoMock.Verify(x => x.AddAsync(
                It.IsAny<Entities.Product>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ReturnProductGivenSizeUnitMeasureCodeInRequestIsNull(
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            [Frozen] Mock<IRepository<ProductSubcategory>> productSubcategoryRepoMock,
            [Frozen] Mock<IRepository<ProductModel>> productModelRepoMock,
            [Frozen] Mock<IRepository<UnitMeasure>> unitMeasureRepoMock,
            CreateProductCommandHandler sut,
            CreateProductCommand command
        )
        {
            // Arrange
            command.Product!.ProductLine = ProductLine.Road.ToString();
            command.Product.Class = Class.High.ToString();
            command.Product.Style = Style.Mens.ToString();
            command.Product.SizeUnitMeasureCode = null;

            //Act
            var product = await sut.Handle(command, CancellationToken.None);

            //Assert
            product.Should().BeEquivalentTo(command.Product, config => config
                .Excluding(_ => _.SizeUnitMeasureCode)
                .Excluding(_ => _.WeightUnitMeasureCode)
                .Excluding(_ => _.ProductModelName)
                .Excluding(_ => _.ProductSubcategoryName)
                .Excluding(_ => _.ProductCategoryName)
            );

            productSubcategoryRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductSubcategorySpecification>(),
                It.IsAny<CancellationToken>()
            ));

            productModelRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductModelSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            unitMeasureRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetUnitMeasureSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            productRepoMock.Verify(x => x.AddAsync(
                It.IsAny<Entities.Product>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ReturnProductGivenWeightUnitMeasureCodeInRequestIsNull(
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            [Frozen] Mock<IRepository<ProductSubcategory>> productSubcategoryRepoMock,
            [Frozen] Mock<IRepository<ProductModel>> productModelRepoMock,
            [Frozen] Mock<IRepository<UnitMeasure>> unitMeasureRepoMock,
            CreateProductCommandHandler sut,
            CreateProductCommand command
        )
        {
            // Arrange
            command.Product!.ProductLine = ProductLine.Road.ToString();
            command.Product.Class = Class.High.ToString();
            command.Product.Style = Style.Mens.ToString();
            command.Product.WeightUnitMeasureCode = null;

            //Act
            var product = await sut.Handle(command, CancellationToken.None);

            //Assert
            product.Should().BeEquivalentTo(command.Product, config => config
                .Excluding(_ => _.SizeUnitMeasureCode)
                .Excluding(_ => _.WeightUnitMeasureCode)
                .Excluding(_ => _.ProductModelName)
                .Excluding(_ => _.ProductSubcategoryName)
                .Excluding(_ => _.ProductCategoryName)
            );

            productSubcategoryRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductSubcategorySpecification>(),
                It.IsAny<CancellationToken>()
            ));

            productModelRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductModelSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            unitMeasureRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetUnitMeasureSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            productRepoMock.Verify(x => x.AddAsync(
                It.IsAny<Entities.Product>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ThrowProductSubcategoryNotFoundExceptionGivenSubcategoryDoesNotExist(
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            [Frozen] Mock<IRepository<ProductSubcategory>> productSubcategoryRepoMock,
            [Frozen] Mock<IRepository<ProductModel>> productModelRepoMock,
            [Frozen] Mock<IRepository<UnitMeasure>> unitMeasureRepoMock,
            CreateProductCommandHandler sut,
            CreateProductCommand command
        )
        {
            // Arrange
            command.Product!.ProductLine = ProductLine.Road.ToString();
            command.Product.Class = Class.High.ToString();
            command.Product.Style = Style.Mens.ToString();

            productSubcategoryRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductSubcategorySpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((ProductSubcategory?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ProductSubcategoryNotFoundException>();

            productSubcategoryRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductSubcategorySpecification>(),
                It.IsAny<CancellationToken>()
            ));

            productModelRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductModelSpecification>(),
                It.IsAny<CancellationToken>()
            ), Times.Never);

            unitMeasureRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetUnitMeasureSpecification>(),
                It.IsAny<CancellationToken>()
            ), Times.Never);

            productRepoMock.Verify(x => x.AddAsync(
                It.IsAny<Entities.Product>(),
                It.IsAny<CancellationToken>()
            ), Times.Never);
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ThrowProductModelNotFoundExceptionGivenProductModelDoesNotExist(
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            [Frozen] Mock<IRepository<ProductSubcategory>> productSubcategoryRepoMock,
            [Frozen] Mock<IRepository<ProductModel>> productModelRepoMock,
            [Frozen] Mock<IRepository<UnitMeasure>> unitMeasureRepoMock,
            CreateProductCommandHandler sut,
            CreateProductCommand command
        )
        {
            // Arrange
            command.Product!.ProductLine = ProductLine.Road.ToString();
            command.Product.Class = Class.High.ToString();
            command.Product.Style = Style.Mens.ToString();

            productModelRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductModelSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((ProductModel?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ProductModelNotFoundException>();

            productSubcategoryRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductSubcategorySpecification>(),
                It.IsAny<CancellationToken>()
            ));

            productModelRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductModelSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            unitMeasureRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetUnitMeasureSpecification>(),
                It.IsAny<CancellationToken>()
            ), Times.Never);

            productRepoMock.Verify(x => x.AddAsync(
                It.IsAny<Entities.Product>(),
                It.IsAny<CancellationToken>()
            ), Times.Never);
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ThrowUnitMeasureNotFoundExceptionGivenUnitMeasureDoesNotExist(
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            [Frozen] Mock<IRepository<ProductSubcategory>> productSubcategoryRepoMock,
            [Frozen] Mock<IRepository<ProductModel>> productModelRepoMock,
            [Frozen] Mock<IRepository<UnitMeasure>> unitMeasureRepoMock,
            CreateProductCommandHandler sut,
            CreateProductCommand command
        )
        {
            // Arrange
            command.Product!.ProductLine = ProductLine.Road.ToString();
            command.Product.Class = Class.High.ToString();
            command.Product.Style = Style.Mens.ToString();

            unitMeasureRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetUnitMeasureSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((UnitMeasure?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<UnitMeasureNotFoundException>();

            productSubcategoryRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductSubcategorySpecification>(),
                It.IsAny<CancellationToken>()
            ));

            productModelRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetProductModelSpecification>(),
                It.IsAny<CancellationToken>()
            ));

            unitMeasureRepoMock.Verify(x => x.SingleOrDefaultAsync(
                It.IsAny<GetUnitMeasureSpecification>(),
                It.IsAny<CancellationToken>()
            ), Times.Once);

            productRepoMock.Verify(x => x.AddAsync(
                It.IsAny<Entities.Product>(),
                It.IsAny<CancellationToken>()
            ), Times.Never);
        }
    }
}
