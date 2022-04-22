using AutoFixture.Xunit2;
using AW.Services.Sales.Core.AutoMapper;
using AW.Services.Sales.Core.Entities;
using AW.Services.Sales.Core.Exceptions;
using AW.Services.Sales.Core.Handlers.ShipSalesOrder;
using AW.Services.Sales.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Sales.Core.UnitTests.Handlers
{
    public class ShipSalesOrderCommandUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_ShippedStatusIsSet_ReturnsTrue(
            [Frozen] Mock<IRepository<SalesOrder>> salesOrderRepositoryMock,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            ShipSalesOrderCommandHandler sut,
            ShipSalesOrderCommand command
        )
        {
            //Arrange
            mockUnitOfWork.Setup(_ => _.SaveEntitiesAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            salesOrderRepositoryMock.Setup(_ => _.UnitOfWork).Returns(mockUnitOfWork.Object);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().BeTrue();
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_StatusIsCanceled_ThrowsSalesDomainException(
            [Frozen] Mock<IRepository<SalesOrder>> salesOrderRepositoryMock,
            ShipSalesOrderCommandHandler sut,
            ShipSalesOrderCommand command,
            SalesOrder salesOrder
        )
        {
            //Arrange
            salesOrder.SetCancelledStatus();
            salesOrderRepositoryMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetSalesOrderSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(salesOrder);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<SalesDomainException>()
                .WithMessage("Is not possible to change the order status from Cancelled to Shipped.");
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_StatusIsRejected_ThrowsSalesDomainException(
            [Frozen] Mock<IRepository<SalesOrder>> salesOrderRepositoryMock,
            ShipSalesOrderCommandHandler sut,
            ShipSalesOrderCommand command,
            SalesOrder salesOrder
        )
        {
            //Arrange
            salesOrder.SetRejectedStatus();
            salesOrderRepositoryMock.Setup(_ => _.GetBySpecAsync(
                    It.IsAny<GetSalesOrderSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            )
            .ReturnsAsync(salesOrder);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<SalesDomainException>()
                .WithMessage("Is not possible to change the order status from Rejected to Shipped.");
        }
    }
}