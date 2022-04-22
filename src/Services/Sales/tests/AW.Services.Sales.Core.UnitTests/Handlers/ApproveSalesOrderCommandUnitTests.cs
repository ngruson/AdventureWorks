using AutoFixture.Xunit2;
using AW.Services.Sales.Core.AutoMapper;
using AW.Services.Sales.Core.Entities;
using AW.Services.Sales.Core.Exceptions;
using AW.Services.Sales.Core.Handlers.ApproveSalesOrder;
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
    public class ApproveSalesOrderCommandUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_ApprovedStatusIsSet_ReturnsTrue(
            [Frozen] Mock<IRepository<SalesOrder>> salesOrderRepositoryMock,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            ApproveSalesOrderCommandHandler sut,
            ApproveSalesOrderCommand command
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
            ApproveSalesOrderCommandHandler sut,
            ApproveSalesOrderCommand command,
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
                .WithMessage("Is not possible to change the order status from Cancelled to Approved.");
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_StatusIsShipped_ThrowsSalesDomainException(
            [Frozen] Mock<IRepository<SalesOrder>> salesOrderRepositoryMock,
            ApproveSalesOrderCommandHandler sut,
            ApproveSalesOrderCommand command,
            SalesOrder salesOrder
        )
        {
            //Arrange
            salesOrder.SetShippedStatus();
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
                .WithMessage("Is not possible to change the order status from Shipped to Approved.");
        }
    }
}