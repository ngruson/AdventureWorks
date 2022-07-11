using AutoFixture.Xunit2;
using AW.Services.Sales.Core.AutoMapper;
using AW.Services.Sales.Core.Handlers.CreateSalesOrder;
using AW.Services.Sales.Core.Handlers.Identified;
using AW.Services.Sales.Core.Idempotency;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Sales.Core.UnitTests.Handlers
{
    public class IdentifiedCommandUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_RequestDoesNotExists_HandleEmbeddedCommand(
            [Frozen] Mock<IRequestManager> requestManagerMock,
            [Frozen] Mock<IMediator> mediatorMock,
            IdentifiedCommandHandler<CreateSalesOrderCommand, bool> sut,
            IdentifiedCommand<CreateSalesOrderCommand, bool> command
        )
        {
            //Arrange
            requestManagerMock.Setup(_ => _.ExistAsync(It.IsAny<Guid>()))
                .ReturnsAsync(false);
            mediatorMock.Setup(_ => _.Send<bool>(
                It.IsAny<CreateSalesOrderCommand>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(true);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().Be(true);
            requestManagerMock.Verify(_ => _.CreateRequestForCommandAsync<CreateSalesOrderCommand>(
                It.IsAny<Guid>())
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_RequestDoesExists_ReturnFalse(
            [Frozen] Mock<IRequestManager> requestManagerMock,
            [Frozen] Mock<IMediator> mediatorMock,
            IdentifiedCommandHandler<CreateSalesOrderCommand, bool> sut,
            IdentifiedCommand<CreateSalesOrderCommand, bool> command
        )
        {
            //Arrange
            requestManagerMock.Setup(_ => _.ExistAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().Be(false);
            requestManagerMock.Verify(_ => _.CreateRequestForCommandAsync<CreateSalesOrderCommand>(
                It.IsAny<Guid>()),
                Times.Never
            );
            mediatorMock.Verify(_ => _.Send<bool>(
                    It.IsAny<CreateSalesOrderCommand>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_ExceptionIsThrown_ReturnFalse(
            [Frozen] Mock<IRequestManager> requestManagerMock,
            [Frozen] Mock<IMediator> mediatorMock,
            IdentifiedCommandHandler<CreateSalesOrderCommand, bool> sut,
            IdentifiedCommand<CreateSalesOrderCommand, bool> command
        )
        {
            //Arrange
            requestManagerMock.Setup(_ => _.ExistAsync(It.IsAny<Guid>()))
                .ReturnsAsync(false);
            mediatorMock.Setup(_ => _.Send<bool>(
                It.IsAny<CreateSalesOrderCommand>(),
                It.IsAny<CancellationToken>()
            ))
            .Throws<Exception>();

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().Be(false);
            requestManagerMock.Verify(_ => _.CreateRequestForCommandAsync<CreateSalesOrderCommand>(
                It.IsAny<Guid>())
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_UnknownCommand(
            [Frozen] Mock<IRequestManager> requestManagerMock,
            [Frozen] Mock<IMediator> mediatorMock,
            IdentifiedCommandHandler<UnknownCommand, bool> sut,
            IdentifiedCommand<UnknownCommand, bool> command
        )
        {
            //Arrange
            requestManagerMock.Setup(_ => _.ExistAsync(It.IsAny<Guid>()))
                .ReturnsAsync(false);

            mediatorMock.Setup(_ => _.Send<bool>(
                It.IsAny<UnknownCommand>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(true);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().Be(true);
            requestManagerMock.Verify(_ => _.CreateRequestForCommandAsync<UnknownCommand>(
                It.IsAny<Guid>())
            );
        }
    }
}