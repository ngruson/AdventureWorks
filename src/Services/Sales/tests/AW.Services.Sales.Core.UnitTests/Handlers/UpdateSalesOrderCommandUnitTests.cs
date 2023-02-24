using Ardalis.Specification;
using AutoFixture.Xunit2;
using AW.Services.Sales.Core.Exceptions;
using AW.Services.Sales.Core.Handlers.UpdateSalesOrder;
using AW.Services.Sales.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.Services.Sales.Core.UnitTests.Handlers
{
    public class UpdateSalesOrderCommandUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Handle_ExistingSalesOrder_ReturnUpdatedSalesOrder(
            [Frozen] Mock<IRepository<Core.Entities.SalesOrder>> salesOrderRepoMock,
            [Frozen] Mock<IRepository<Core.Entities.SpecialOfferProduct>> specialOfferProductRepoMock,
            Core.Entities.SalesOrder salesOrder,
            Core.Entities.SpecialOfferProduct specialOfferProduct,
            UpdateSalesOrderCommandHandler sut,
            UpdateSalesOrderCommand command
        )
        {
            //Arrange
            for (int i = 0; i < command.SalesOrder!.OrderLines?.Count; i++)
            {
                salesOrder.OrderLines.Add(new Core.Entities.SalesOrderLine
                {
                    ProductNumber = command.SalesOrder.OrderLines[i].ProductNumber
                });
            }

            salesOrderRepoMock.Setup(_ => _.SingleOrDefaultAsync(
                    It.IsAny<ISingleResultSpecification<Core.Entities.SalesOrder>>(),
                    It.IsAny<CancellationToken>()
                )
            ).
            ReturnsAsync(salesOrder);

            specialOfferProductRepoMock.Setup(_ => _.FirstOrDefaultAsync(
                    It.IsAny<GetSpecialOfferProductSpecification>(),
                    It.IsAny<CancellationToken>()
                )
            ).
            ReturnsAsync(specialOfferProduct);

            //Act
            var result = await sut.Handle(command, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            salesOrderRepoMock.Verify(x => x.UpdateAsync(
                It.IsAny<Core.Entities.SalesOrder>(),
                It.IsAny<CancellationToken>()
            ));
        }

        [Theory]
        [AutoMoqData]
        public async Task Handle_SalesOrderDoesNotExist_ThrowArgumentNullException(
            [Frozen] Mock<IRepository<Core.Entities.SalesOrder>> salesOrderRepoMock,
            UpdateSalesOrderCommandHandler sut,
            UpdateSalesOrderCommand command
        )
        {
            // Arrange
            salesOrderRepoMock.Setup(x => x.SingleOrDefaultAsync(
                It.IsAny<GetSalesOrderSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync((Core.Entities.SalesOrder?)null);

            //Act
            Func<Task> func = async () => await sut.Handle(command, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<SalesOrderNotFoundException>()
                .WithMessage($"Sales order {command.SalesOrder!.SalesOrderNumber} not found");
        }
    }
}
