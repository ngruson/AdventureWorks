using AW.Core.Domain.Sales;
using System;
using System.Collections.Generic;

namespace AW.Core.Application.UnitTests.TestBuilders
{
    public class SalesOrderBuilder
    {
        private SalesOrderHeader salesOrder = new SalesOrderHeader();

        public SalesOrderBuilder Id(int id)
        {
            salesOrder.Id = id;
            return this;
        }

        public SalesOrderBuilder RevisionNumber(byte revisionNumber)
        {
            salesOrder.RevisionNumber = revisionNumber;
            return this;
        }

        public SalesOrderBuilder OrderDate(DateTime orderDate)
        {
            salesOrder.OrderDate = orderDate;
            return this;
        }

        public SalesOrderBuilder DueDate(DateTime dueDate)
        {
            salesOrder.DueDate = dueDate;
            return this;
        }

        public SalesOrderBuilder ShipDate(DateTime shipDate)
        {
            salesOrder.ShipDate = shipDate;
            return this;
        }

        public SalesOrderBuilder Status(Domain.Sales.SalesOrderStatus orderStatus)
        {
            salesOrder.Status = orderStatus;
            return this;
        }

        public SalesOrderBuilder OnlineOrderFlag(bool onlineOrderFlag)
        {
            salesOrder.OnlineOrderFlag = onlineOrderFlag;
            return this;
        }

        public SalesOrderBuilder SalesOrderNumber(string salesOrderNumber)
        {
            salesOrder.SalesOrderNumber = salesOrderNumber;
            return this;
        }

        public SalesOrderBuilder PurchaseOrderNumber(string purchaseOrderNumber)
        {
            salesOrder.PurchaseOrderNumber = purchaseOrderNumber;
            return this;
        }

        public SalesOrderBuilder AccountNumber(string accountNumber)
        {
            salesOrder.AccountNumber = accountNumber;
            return this;
        }

        public SalesOrderBuilder Customer(Domain.Sales.Customer customer)
        {
            salesOrder.Customer = customer;
            return this;
        }

        public SalesOrderBuilder SalesPerson(Domain.Sales.SalesPerson salesPerson)
        {
            salesOrder.SalesPerson = salesPerson;
            return this;
        }

        public SalesOrderBuilder SalesTerritory(Domain.Sales.SalesTerritory salesTerritory)
        {
            salesOrder.SalesTerritory = salesTerritory;
            return this;
        }

        public SalesOrderBuilder BillToAddress(Domain.Person.Address address)
        {
            salesOrder.BillToAddress = address;
            return this;
        }

        public SalesOrderBuilder ShipToAddress(Domain.Person.Address address)
        {
            salesOrder.ShipToAddress = address;
            return this;
        }

        public SalesOrderBuilder ShipMethod(Domain.Purchasing.ShipMethod shipMethod)
        {
            salesOrder.ShipMethod = shipMethod;
            return this;
        }

        public SalesOrderBuilder CreditCard(Domain.Sales.CreditCard creditCard)
        {
            salesOrder.CreditCard = creditCard;
            return this;
        }

        public SalesOrderBuilder CreditCardApprovalCode(string creditCardApprovalCode)
        {
            salesOrder.CreditCardApprovalCode = creditCardApprovalCode;
            return this;
        }

        public SalesOrderBuilder CurrencyRate(Domain.Sales.CurrencyRate currencyRate)
        {
            salesOrder.CurrencyRate = currencyRate;
            return this;
        }

        public SalesOrderBuilder SubTotal(decimal subTotal)
        {
            salesOrder.SubTotal = subTotal;
            return this;
        }

        public SalesOrderBuilder TaxAmt(decimal taxAmt)
        {
            salesOrder.TaxAmt = taxAmt;
            return this;
        }

        public SalesOrderBuilder Freight(decimal freight)
        {
            salesOrder.Freight = freight;
            return this;
        }

        public SalesOrderBuilder TotalDue(decimal totalDue)
        {
            salesOrder.TotalDue = totalDue;
            return this;
        }

        public Domain.Sales.SalesOrderHeader Build()
        {
            return salesOrder;
        }

        public SalesOrderBuilder WithTestValues()
        {
            salesOrder = new Domain.Sales.SalesOrderHeader
            {
                Id = new Random().Next(),
                RevisionNumber = 8,
                OrderDate = new DateTime(2011, 05, 31),
                DueDate = new DateTime(2011, 06, 12),
                ShipDate = new DateTime(2011, 06, 07),
                Status = Domain.Sales.SalesOrderStatus.Shipped,
                OnlineOrderFlag = false,
                SalesOrderNumber = "SO43659",
                PurchaseOrderNumber = "PO522145787",
                AccountNumber = "10-4020-000676",
                Customer = new CustomerBuilder().WithTestValues().Build(),
                CustomerID = new Random().Next(),
                SalesPerson = new SalesPersonBuilder().WithTestValues().Build(),
                SalesPersonID = new Random().Next(),
                SalesTerritory = new SalesTerritoryBuilder().WithTestValues().Build(),
                SalesTerritoryID = new Random().Next(),
                BillToAddress = new AddressBuilder().WithTestValues().Build(),
                BillToAddressID = new Random().Next(),
                ShipToAddress = new AddressBuilder().WithTestValues().Build(),
                ShipMethodID = new Random().Next(),
                ShipMethod = new ShipMethodBuilder().WithTestValues().Build(),
                CreditCard = new CreditCardBuilder().WithTestValues().Build(),
                CreditCardID = new Random().Next(),
                CreditCardApprovalCode = "105041Vi84182",
                CurrencyRate = new CurrencyRateBuilder().WithTestValues().Build(),
                CurrencyRateID = new Random().Next(),
                SubTotal = 20565.6206M,
                TaxAmt = 1971.5149M,
                Freight = 616.0984M,
                TotalDue = 23153.2339M,
                Comment = "Comment",
                rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.Now,
                OrderLines = CreateOrderLines(),
                SalesReasons = CreateSalesReasons()
            };

            return this;
        }

        private ICollection<SalesOrderDetail> CreateOrderLines()
        {
            return new List<SalesOrderDetail>
            {
                new SalesOrderDetail
                {
                    SalesOrderID = salesOrder.Id,
                    SalesOrder = salesOrder,
                    SalesOrderDetailID = 1,
                    CarrierTrackingNumber = "4911-403C-98",
                    OrderQty = 1,
                    ProductID = 776,
                    SpecialOfferID = 1,
                    UnitPrice = 2024.994M,
                    UnitPriceDiscount = 0,
                    LineTotal = 2024.994M,
                    rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now,
                    SpecialOfferProduct = new SpecialOfferProductBuilder().WithTestValues().Build()
                }
            };
        }

        private ICollection<SalesOrderHeaderSalesReason> CreateSalesReasons()
        {
            return new List<SalesOrderHeaderSalesReason>
            {
                new SalesOrderHeaderSalesReasonBuilder()
                    .SalesOrderID(43659)
                    .SalesReasonID(5)
                    .SalesReason(new SalesReasonBuilder().WithTestValues().Build())
                    .Build()
            };
        }
    }
}