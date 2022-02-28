using AutoFixture.Xunit2;
using AW.Services.Sales.Core.AutoMapper;
using AW.Services.Sales.Core.Handlers.GetSalesOrder;
using AW.Services.Sales.Core.Specifications;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Sales.Core.UnitTests.Handlers
{
    public class GetSalesOrderQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_SalesOrderWithIndividualCustomer_Exists_ReturnSalesOrder(
            string userId,
            string userName,
            string cardSecurityNumber,
            string cardHolderName,
            Core.Entities.IndividualCustomer customer,
            string shipMethod,
            ValueTypes.Address billToAddress,
            ValueTypes.Address shipToAddress,
            Core.Entities.CreditCard creditCard,
            [Frozen] Mock<IRepository<Core.Entities.SalesOrder>> salesOrderRepoMock,
            GetSalesOrderQueryHandler sut,
            GetSalesOrderQuery query
        )
        {
            //Arrange
            creditCard.ExpYear = short.Parse(DateTime.Today.Year.ToString());
            creditCard.ExpMonth = byte.Parse(DateTime.Today.Month.ToString());

            var salesOrder = new Core.Entities.SalesOrder(
                userId,
                userName,
                customer,
                shipMethod,
                billToAddress,
                shipToAddress,
                creditCard,
                cardSecurityNumber,
                cardHolderName
            );

            salesOrderRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetFullSalesOrderSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(salesOrder);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            salesOrderRepoMock.Verify(x => x.GetBySpecAsync(
                It.IsAny<GetFullSalesOrderSpecification>(),
                It.IsAny<CancellationToken>()
            ));
            result.SalesOrderNumber.Should().Be(salesOrder.SalesOrderNumber);
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_SalesOrderWithStoreCustomer_Exists_ReturnSalesOrder(
            string userId,
            string userName,
            string cardSecurityNumber,
            string cardHolderName,
            Core.Entities.StoreCustomer customer,
            string shipMethod,
            ValueTypes.Address billToAddress,
            ValueTypes.Address shipToAddress,
            Core.Entities.CreditCard creditCard,
            [Frozen] Mock<IRepository<Core.Entities.SalesOrder>> salesOrderRepoMock,
            GetSalesOrderQueryHandler sut,
            GetSalesOrderQuery query
        )
        {
            //Arrange
            creditCard.ExpYear = short.Parse(DateTime.Today.Year.ToString());
            creditCard.ExpMonth = byte.Parse(DateTime.Today.Month.ToString());

            var salesOrder = new Core.Entities.SalesOrder(
                userId,
                userName,
                customer,
                shipMethod,
                billToAddress,
                shipToAddress,
                creditCard,
                cardSecurityNumber,
                cardHolderName
            );

            salesOrderRepoMock.Setup(x => x.GetBySpecAsync(
                It.IsAny<GetFullSalesOrderSpecification>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(salesOrder);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            salesOrderRepoMock.Verify(x => x.GetBySpecAsync(
                It.IsAny<GetFullSalesOrderSpecification>(),
                It.IsAny<CancellationToken>()
            ));
            result.SalesOrderNumber.Should().Be(salesOrder.SalesOrderNumber);
        }
    }
}
