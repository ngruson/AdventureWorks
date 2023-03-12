using AutoFixture.Xunit2;
using AW.Services.Infrastructure;
using AW.Services.Sales.Core.Behaviors;
using AW.Services.Sales.Core.Handlers.CreateSalesOrder;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Sales.Core.UnitTests.Behaviors
{
    public class TransactionBehaviorUnitTests
    {
        [Theory, SalesOrderAutoMoqData]
        public async Task Handle_NoActiveTransaction_InvokeExecute(
            [Frozen] Mock<IDbContext> mockDbContext,
            TransactionBehavior<CreateSalesOrderCommand, bool> sut,
            CreateSalesOrderCommand command,
            RequestHandlerDelegate<bool> next
        )
        {
            //Arrange

            //Act
            var result = await sut.Handle(command, next, CancellationToken.None);

            //Assert
            mockDbContext.Verify(_ => _.Execute(
                It.IsAny<Func<Task>>()
            ));
        }

        [Theory, SalesOrderAutoMoqData]
        public async Task Handle_ActiveTransaction_InvokeNext(
            [Frozen] Mock<IDbContext> mockDbContext,
            TransactionBehavior<CreateSalesOrderCommand, bool> sut,
            CreateSalesOrderCommand command,
            RequestHandlerDelegate<bool> next
        )
        {
            //Arrange
            mockDbContext.Setup(_ => _.HasActiveTransaction)
                .Returns(true);

            //Act
            var result = await sut.Handle(command, next, CancellationToken.None);

            //Assert
            result.Should().Be(await next.Invoke());

            mockDbContext.Verify(_ => _.Execute(
                It.IsAny<Func<Task>>()
            ), Times.Never);
        }

        [Theory, SalesOrderAutoMoqData]
        public async Task Handle_ExceptionOccurred_ThrowException(
            [Frozen] Mock<IDbContext> mockDbContext,
            TransactionBehavior<CreateSalesOrderCommand, bool> sut,
            CreateSalesOrderCommand command,
            RequestHandlerDelegate<bool> next
        )
        {
            //Arrange
            mockDbContext.Setup(_ => _.Execute(
                    It.IsAny<Func<Task>>())
                )
                .ThrowsAsync(new Exception());

            //Act
            Func<Task> func = async () => await sut.Handle(command, next, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<Exception>();

            mockDbContext.Verify(_ => _.Execute(
                It.IsAny<Func<Task>>()
            ));
        }
    }
}