using AutoFixture.Xunit2;
using AutoMapper;
using AW.Services.Product.Core.AutoMapper;
using AW.Services.Product.Core.Exceptions;
using AW.Services.Product.Core.Handlers.CreateProduct;
using AW.Services.Product.Core.Handlers.DuplicateProduct;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace AW.Services.Product.Core.UnitTests.Handlers
{
    public class DuplicateProductCommandUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task DuplicateProductGivenProductCanBeCreated(
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            [Frozen] Mock<IMediator> mediatorMock,
            IMapper mapper,
            DuplicateProductCommandHandler sut,
            DuplicateProductCommand command,
            Entities.Product product,
            string name,
            string productNumber
        )
        {
            // Arrange
            product.SetName(name);
            product.SetProductNumber(productNumber);

            productRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetProductSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(product);

            var createdProduct = mapper.Map<Core.Handlers.CreateProduct.Product>(product);

            mediatorMock.Setup(_ => _.Send(
                    It.IsAny<CreateProductCommand>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(createdProduct);

            //Act
            var duplicatedProduct = await sut.Handle(command, CancellationToken.None);

            //Assert
            duplicatedProduct.Should().BeEquivalentTo(createdProduct);

            mediatorMock.Verify(_ => _.Send(
                    It.Is<CreateProductCommand>(_ => 
                        _.Product!.ProductNumber == $"Copy of {product.ProductNumber}" &&
                        _.Product.Name == $"Copy of {product.Name}"
                    ),
                    It.IsAny<CancellationToken>()
                )
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ThrowProductNotFoundExceptionGivenProductDoesNotExist(
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            [Frozen] Mock<IMediator> mediatorMock,
            DuplicateProductCommandHandler sut,
            DuplicateProductCommand command
        )
        {
            // Arrange
            productRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetProductSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync((Entities.Product?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ProductNotFoundException>();
            mediatorMock.Verify(_ => _.Send(
                    It.IsAny<CreateProductCommand>(),
                    It.IsAny<CancellationToken>()
                ), 
                Times.Never
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ThrowDuplicateProductExceptionGivenProductCouldNotBeStored(
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            [Frozen] Mock<IMediator> mediatorMock,
            DuplicateProductCommandHandler sut,
            DuplicateProductCommand command,
            Entities.Product product,
            string name,
            string productNumber
        )
        {
            // Arrange
            product.SetName(name);
            product.SetProductNumber(productNumber);

            productRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<GetProductSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(product);

            mediatorMock.Setup(_ => _.Send(
                    It.IsAny<CreateProductCommand>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ThrowsAsync(new Exception());

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<DuplicateProductException>();
            mediatorMock.Verify(_ => _.Send(
                    It.IsAny<CreateProductCommand>(),
                    It.IsAny<CancellationToken>()
                )
            );
        }
    }
}
