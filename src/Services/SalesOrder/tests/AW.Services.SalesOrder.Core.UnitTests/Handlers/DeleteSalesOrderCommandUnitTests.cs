﻿using AutoFixture.Xunit2;
using AW.Services.SalesOrder.Core.Handlers.DeleteSalesOrder;
using AW.Services.SalesOrder.Core.Specifications;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.SalesOrder.Core.UnitTests.Handlers
{
    public class DeleteSalesOrderCommandUnitTests
    {
        [Theory, AutoMoqData()]
        public async Task Handle_SalesOrderExists_DeleteSalesOrder(
            [Frozen] Mock<IRepository<Core.Entities.SalesOrder>> salesOrderRepositoryMock,
            DeleteSalesOrderCommandHandler sut,
            DeleteSalesOrderCommand command
        )
        {
            //Arrange

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().Be(Unit.Value);

            salesOrderRepositoryMock.Verify(_ => _.DeleteAsync(
                It.IsAny<Core.Entities.SalesOrder>(),
                It.IsAny<CancellationToken>())
            );
        }

        [Theory, AutoMoqData()]
        public void Handle_SalesOrderDoesNotExist_ThrowArgumentException(
            [Frozen] Mock<IRepository<Core.Entities.SalesOrder>> salesOrderRepositoryMock,
            DeleteSalesOrderCommandHandler sut,
            DeleteSalesOrderCommand command
        )
        {
            //Arrange
            salesOrderRepositoryMock.Setup(_ => _.GetBySpecAsync(
                It.IsAny<GetSalesOrderSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Core.Entities.SalesOrder)null);

            //Act
            Func<Task> func = async() => await sut.Handle(command, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentException>();

            salesOrderRepositoryMock.Verify(_ => _.DeleteAsync(
                    It.IsAny<Core.Entities.SalesOrder>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Never
            );
        }
    }
}