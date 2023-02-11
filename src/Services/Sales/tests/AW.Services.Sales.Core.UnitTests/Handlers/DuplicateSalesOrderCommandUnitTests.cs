using AutoFixture.Xunit2;
using AW.Services.Sales.Core.Handlers.CreateSalesOrder;
using AW.Services.Sales.Core.IntegrationEvents;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System.Threading;
using System;
using Xunit;
using AW.Services.Sales.Core.AutoMapper;
using AW.Services.Sales.Core.Handlers.DuplicateSalesOrder;
using FluentAssertions;
using MediatR;
using AW.Services.Sales.Core.Handlers.GetSalesOrder;
using AW.Services.Sales.Core.Exceptions;

namespace AW.Services.Sales.Core.UnitTests.Handlers
{
    public class DuplicateSalesOrderCommandUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_SalesOrderExists_DuplicateSalesOrder(
            [Frozen] Mock<IMediator> mockMediator,
            DuplicateSalesOrderCommandHandler sut,
            DuplicateSalesOrderCommand command,
            SalesOrderDto salesOrder
        )
        {
            //Arrange
            salesOrder.Customer = new StoreCustomerDto();
            salesOrder.CreditCard!.ExpYear = Convert.ToInt16(DateTime.Now.Year);
            salesOrder.CreditCard.ExpMonth = Convert.ToByte(DateTime.Now.Month);

            mockMediator.Setup(_ => _.Send(
                It.IsAny<GetSalesOrderQuery>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(salesOrder);

            mockMediator.Setup(_ => _.Send(
                It.IsAny<CreateSalesOrderCommand>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(true);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            mockMediator.Verify(_ => _.Send(
                It.IsAny<GetSalesOrderQuery>(),
                It.IsAny<CancellationToken>()
                )
            );

            mockMediator.Verify(_ => _.Send(
                It.IsAny<CreateSalesOrderCommand>(),
                It.IsAny<CancellationToken>()
                )
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_SalesOrderDoesNotExist_ThrowSalesOrderNotFoundException(
            [Frozen] Mock<IMediator> mockMediator,
            DuplicateSalesOrderCommandHandler sut,
            DuplicateSalesOrderCommand command
        )
        {
            //Arrange
            mockMediator.Setup(_ => _.Send(
                It.IsAny<GetSalesOrderQuery>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((SalesOrderDto?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert            
            await func.Should().ThrowAsync<SalesOrderNotFoundException>()
                .WithMessage($"Sales order {command.SalesOrderNumber} not found");

            mockMediator.Verify(_ => _.Send(
                It.IsAny<GetSalesOrderQuery>(),
                It.IsAny<CancellationToken>()
                )
            );
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_CreateSalesOrderFails_ThrowDuplicateSalesOrderException(
            [Frozen] Mock<IMediator> mockMediator,
            DuplicateSalesOrderCommandHandler sut,
            DuplicateSalesOrderCommand command,
            SalesOrderDto salesOrder
        )
        {
            //Arrange
            salesOrder.Customer = new StoreCustomerDto();
            salesOrder.CreditCard!.ExpYear = Convert.ToInt16(DateTime.Now.Year);
            salesOrder.CreditCard.ExpMonth = Convert.ToByte(DateTime.Now.Month);

            mockMediator.Setup(_ => _.Send(
                It.IsAny<GetSalesOrderQuery>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(salesOrder);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<DuplicateSalesOrderException>()
                .WithMessage($"Duplicating sales order {command.SalesOrderNumber} failed");

            mockMediator.Verify(_ => _.Send(
                It.IsAny<GetSalesOrderQuery>(),
                It.IsAny<CancellationToken>()
                )
            );

            mockMediator.Verify(_ => _.Send(
                It.IsAny<CreateSalesOrderCommand>(),
                It.IsAny<CancellationToken>()
                )
            );
        }
    }
}