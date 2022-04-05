using AW.Services.Sales.Core.AutoMapper;
using AW.Services.Sales.Core.Entities;
using AW.Services.Sales.Core.Events;
using AW.Services.Sales.Core.ValueTypes;
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
            Customer customer,
            string shipMethod,
            Address address,
            CreditCard creditCard,
            DateTime expirationDate,
            string cardSecurityNumber,
            string cardHolderName
        )
        {
            //Arrange
            creditCard.ExpMonth = byte.Parse(expirationDate.Month.ToString());
            creditCard.ExpYear = short.Parse(expirationDate.Year.ToString());

            //Act
            var salesOrder = new SalesOrder(
                userId,
                userName,
                customer,
                shipMethod,
                address,
                address,
                creditCard,
                cardSecurityNumber,
                cardHolderName
            );

            //Assert
            salesOrder.DomainEvents.ToList()[0]
                .Should().BeAssignableTo<SalesOrderStartedDomainEvent>();
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void AddOrderLine_NoOrderLinesWithProduct_NewOrderLine(
            SalesOrder salesOrder,
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
            var orderLines = salesOrder.OrderLines.ToList();
            orderLines.Count.Should().Be(1);
            orderLines[0].OrderQty.Should().Be(1);
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void AddOrderLine_NoOrderLinesWithProductFound_NewOrderLineWithQuantity(
            SalesOrder salesOrder,
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
            var orderLines = salesOrder.OrderLines.ToList();
            orderLines.Count.Should().Be(1);
            orderLines[0].OrderQty.Should().Be(quantity);
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void AddOrderLine_OrderLineWithProductFound_UpdatedOrderLine(
            SalesOrder salesOrder,
            SalesOrderLine salesOrderLine,
            SpecialOfferProduct specialOfferProduct
        )
        {
            //Arrange
            salesOrder.AddOrderLine(
                salesOrderLine.ProductNumber,
                salesOrderLine.ProductName,
                salesOrderLine.UnitPriceDiscount,
                0,
                specialOfferProduct
            );

            var orderLines = salesOrder.OrderLines.ToList();
            var existingOrderLine = orderLines[0];
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
            orderLines.Count.Should().Be(1);
            orderLines[0].OrderQty.Should().Be((short)(originalQuantity + 1));
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void AddOrderLineWithQuantity_OrderLineWithProductFound_UpdatedOrderLine(
            SalesOrder salesOrder,
            SalesOrderLine salesOrderLine,
            SpecialOfferProduct specialOfferProduct,
            short quantity
        )
        {
            //Arrange
            salesOrder.AddOrderLine(
                salesOrderLine.ProductNumber,
                salesOrderLine.ProductName,
                salesOrderLine.UnitPriceDiscount,
                0,
                specialOfferProduct
            );

            var orderLines = salesOrder.OrderLines.ToList();
            var existingOrderLine = orderLines[0];
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
            orderLines.Count.Should().Be(1);
            orderLines[0].OrderQty.Should().Be((short)(originalQuantity + quantity));
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public void AddOrderLineWithDiscount_OrderLineWithProductFound_UpdatedOrderLine(
            SalesOrder salesOrder,
            SalesOrderLine salesOrderLine,
            SpecialOfferProduct specialOfferProduct
        )
        {
            //Arrange
            salesOrder.AddOrderLine(
                salesOrderLine.ProductNumber,
                salesOrderLine.ProductName,
                salesOrderLine.UnitPriceDiscount,
                0,
                specialOfferProduct
            );

            var orderLines = salesOrder.OrderLines.ToList();
            var existingOrderLine = orderLines[0];
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
            orderLines.Count.Should().Be(1);
            orderLines[0].UnitPriceDiscount.Should().Be((short)(originalQuantity + 1));
        }
    }
}