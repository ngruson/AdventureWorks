using AutoFixture.Xunit2;
using AW.Services.Product.Core.AutoMapper;
using AW.Services.Product.Core.Exceptions;
using AW.Services.Product.Core.Handlers.DeleteProduct;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.Product.Core.UnitTests.Handlers
{
    public class DeleteProductCommandUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task DeleteProductGivenProductExists(
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            DeleteProductCommandHandler sut,
            DeleteProductCommand command
        )
        {
            // Arrange

            //Act
            await sut.Handle(command, CancellationToken.None);

            //Assert
            productRepoMock.Verify(x => x.DeleteAsync(
                It.IsAny<Entities.Product>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ThrowProductNotFoundExceptionGivenProductDoesNotExist(
            [Frozen] Mock<IRepository<Entities.Product>> productRepoMock,
            DeleteProductCommandHandler sut,
            DeleteProductCommand command
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

            //Assert
            productRepoMock.Verify(x => x.DeleteAsync(
                    It.IsAny<Entities.Product>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }
    }
}
