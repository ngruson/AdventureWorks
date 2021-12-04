using AutoFixture.Xunit2;
using AW.Services.SalesOrder.Core.Exceptions;
using AW.Services.SalesOrder.Core.Handlers.CreateSalesOrder;
using AW.Services.SalesOrder.Core.Idempotency;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.SalesOrder.Core.UnitTests.Idempotency
{
    public class RequestManagerUnitTests
    {
        public class ExistAsync
        {
            [Theory, AutoMoqData]
            public async Task ExistAsync_RequestExists_ReturnTrue(
                [Frozen] Mock<IRepository<ClientRequest>> mockRepository,
                RequestManager sut,
                Guid id
            )
            {
                //Arrange

                //Act
                var result = await sut.ExistAsync(id);

                //Assert
                result.Should().Be(true);

                mockRepository.Verify(_ => _.GetByIdAsync(
                        It.IsAny<Guid>(),
                        It.IsAny<CancellationToken>()
                    )
                );
            }

            [Theory, AutoMoqData]
            public async Task ExistAsync_RequestExists_ReturnFalse(
                [Frozen] Mock<IRepository<ClientRequest>> mockRepository,
                RequestManager sut,
                Guid id
            )
            {
                //Arrange
                mockRepository.Setup(_ => _.GetByIdAsync(
                        It.IsAny<Guid>(),
                        It.IsAny<CancellationToken>()
                    ))
                    .ReturnsAsync((ClientRequest)null);

                //Act
                var result = await sut.ExistAsync(id);

                //Assert
                result.Should().Be(false);

                mockRepository.Verify(_ => _.GetByIdAsync(
                        It.IsAny<Guid>(),
                        It.IsAny<CancellationToken>()
                    )
                );
            }
        }

        public class CreateRequestForCommandAsync
        {
            [Theory, AutoMoqData]
            public async Task CreateRequestForCommandAsync_RequestDoesNotExist_AddClientRequest(
                [Frozen] Mock<IRepository<ClientRequest>> mockRepository,
                RequestManager sut,
                Guid id
            )
            {
                //Arrange
                mockRepository.Setup(_ => _.GetByIdAsync(
                        It.IsAny<Guid>(),
                        It.IsAny<CancellationToken>()
                    ))
                    .ReturnsAsync((ClientRequest)null);

                //Act
                await sut.CreateRequestForCommandAsync<CreateSalesOrderCommand>(id);

                //Assert
                mockRepository.Verify(_ => _.AddAsync(
                        It.IsAny<ClientRequest>(),
                        It.IsAny<CancellationToken>()
                    )
                );

                mockRepository.Verify(_ => _.SaveChangesAsync(
                        It.IsAny<CancellationToken>()
                    )
                );
            }

            [Theory, AutoMoqData]
            public void CreateRequestForCommandAsync_RequestExists_ThrowDomainException(
                [Frozen] Mock<IRepository<ClientRequest>> mockRepository,
                RequestManager sut,
                Guid id
            )
            {
                //Arrange

                //Act
                Func<Task> func = async() => await sut.CreateRequestForCommandAsync<CreateSalesOrderCommand>(id);

                //Assert
                func.Should().Throw<SalesOrderDomainException>();

                mockRepository.Verify(_ => _.AddAsync(
                        It.IsAny<ClientRequest>(),
                        It.IsAny<CancellationToken>()
                    ), Times.Never);

                mockRepository.Verify(_ => _.SaveChangesAsync(
                        It.IsAny<CancellationToken>()
                    ), Times.Never
                );
            }
        }
    }
}