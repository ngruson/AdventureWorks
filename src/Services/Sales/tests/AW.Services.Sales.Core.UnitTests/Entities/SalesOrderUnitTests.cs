using AW.Services.Sales.Core.Entities;
using AW.Services.Sales.Core.Events;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace AW.Services.Sales.Core.UnitTests.Entities
{
    public class SalesOrderUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void Create_HasOrderStartedDomainEvent(
            string userId,
            string userName,
            string customerNumber,
            string shipMethod,
            Address address,
            string cardType,
            string cardNumber,
            string cardSecurityNumber,
            string cardHolderName,
            DateTime cardExpiration
        )
        {
            //Arrange

            //Act
            var salesOrder = new Core.Entities.SalesOrder(
                userId,
                userName,
                customerNumber,
                shipMethod,
                address,
                address,
                cardType,
                cardNumber,
                cardSecurityNumber,
                cardHolderName,
                cardExpiration
            );

            //Assert
            salesOrder.DomainEvents.ToList()[0]
                .Should().BeAssignableTo<OrderStartedDomainEvent>();
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void AddOrderLine_NoOrderLinesWithProduct_NewOrderLine(
            Core.Entities.SalesOrder salesOrder,
            string productNumber,
            string productName,
            decimal unitPrice,
            SpecialOfferProduct specialOfferProduct
        )
        {
            //Arrange

            //Act
            salesOrder.AddOrderLine(
                productNumber,
                productName,
                unitPrice,
                0,
                specialOfferProduct
            );

            //Assert
            salesOrder.OrderLines.Count.Should().Be(4);
            salesOrder.OrderLines[3].OrderQty.Should().Be(1);
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void AddOrderLine_NoOrderLinesWithProductFound_NewOrderLineWithQuantity(
            Core.Entities.SalesOrder salesOrder,
            string productNumber,
            string productName,
            decimal unitPrice,
            SpecialOfferProduct specialOfferProduct,
            short quantity
        )
        {
            //Arrange

            //Act
            salesOrder.AddOrderLine(
                productNumber,
                productName,
                unitPrice,
                0,
                specialOfferProduct,
                quantity
            );

            //Assert
            salesOrder.OrderLines.Count.Should().Be(4);
            salesOrder.OrderLines[3].OrderQty.Should().Be(quantity);
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void AddOrderLine_OrderLineWithProductFound_UpdatedOrderLine(
            Core.Entities.SalesOrder salesOrder,
            SpecialOfferProduct specialOfferProduct
        )
        {
            //Arrange
            var existingOrderLine = salesOrder.OrderLines[0];
            var originalQuantity = existingOrderLine.OrderQty;

            //Act
            salesOrder.AddOrderLine(
                existingOrderLine.ProductNumber,
                existingOrderLine.ProductName,
                existingOrderLine.UnitPriceDiscount,
                0,
                specialOfferProduct
            );

            //Assert
            salesOrder.OrderLines.Count.Should().Be(3);
            salesOrder.OrderLines[0].OrderQty.Should().Be((short)(originalQuantity + 1));
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void AddOrderLineWithQuantity_OrderLineWithProductFound_UpdatedOrderLine(
            Core.Entities.SalesOrder salesOrder,
            SpecialOfferProduct specialOfferProduct,
            short quantity
        )
        {
            //Arrange
            var existingOrderLine = salesOrder.OrderLines[0];
            var originalQuantity = existingOrderLine.OrderQty;

            //Act
            salesOrder.AddOrderLine(
                existingOrderLine.ProductNumber,
                existingOrderLine.ProductName,
                existingOrderLine.UnitPrice,
                existingOrderLine.UnitPriceDiscount,
                specialOfferProduct,
                quantity
            );

            //Assert
            salesOrder.OrderLines.Count.Should().Be(3);
            salesOrder.OrderLines[0].OrderQty.Should().Be((short)(originalQuantity + quantity));
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void AddOrderLineWithDiscount_OrderLineWithProductFound_UpdatedOrderLine(
            Core.Entities.SalesOrder salesOrder,
            SpecialOfferProduct specialOfferProduct
        )
        {
            //Arrange
            var existingOrderLine = salesOrder.OrderLines[0];
            var originalQuantity = existingOrderLine.UnitPriceDiscount;

            //Act
            salesOrder.AddOrderLine(
                existingOrderLine.ProductNumber,
                existingOrderLine.ProductName,
                existingOrderLine.UnitPrice,
                existingOrderLine.UnitPriceDiscount + 1,
                specialOfferProduct
            );

            //Assert
            salesOrder.OrderLines.Count.Should().Be(3);
            salesOrder.OrderLines[0].UnitPriceDiscount.Should().Be((short)(originalQuantity + 1));
        }
    }
}