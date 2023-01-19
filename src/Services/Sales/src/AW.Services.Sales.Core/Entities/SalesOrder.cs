using AW.Services.Sales.Core.Events;
using AW.Services.Sales.Core.Exceptions;
using AW.Services.Sales.Core.ValueTypes;
using AW.Services.SharedKernel.Domain;
using AW.Services.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AW.Services.Sales.Core.Entities
{
    public class SalesOrder : Entity, IAggregateRoot
    {
        public SalesOrder() { }
        public SalesOrder(string userId, string userName, string accountNumber, Customer customer, string shipMethod, Address billToAddress, Address shipToAddress, CreditCard creditCard, string cardSecurityNumber,
                string cardHolderName)
        {
            RevisionNumber = 1;
            OrderDate = DateTime.Today;
            DueDate = OrderDate.AddDays(12);
            Status = SalesOrderStatus.InProcess;
            OnlineOrderFlag = true;
            AccountNumber = accountNumber;
            Customer = customer;
            ShipMethod = shipMethod;
            TaxRate = 8;
            Freight = 0;
            BillToAddress = billToAddress;
            ShipToAddress = shipToAddress;
            CreditCard = creditCard;

            AddOrderStartedDomainEvent(userId, userName, 
                creditCard.CardType, creditCard.CardNumber,
                cardSecurityNumber, cardHolderName, 
                new DateTime(
                    creditCard.ExpYear,
                    creditCard.ExpMonth,
                    1
                )
            );
        }

        public SalesOrder(string salesOrderNumber)
        {
            SalesOrderNumber = salesOrderNumber;
        }

        public int Id { get; set; }
        public byte RevisionNumber { get; private set; }

        public DateTime OrderDate { get; private set; }

        public DateTime DueDate { get; private set; }

        public DateTime? ShipDate { get; private set; }

        public SalesOrderStatus Status { get; private set; }

        public bool OnlineOrderFlag { get; private set; }

        public string SalesOrderNumber { get; private set; }

        public string PurchaseOrderNumber { get; private set; }

        public string AccountNumber { get; private set; }
        public Customer Customer { get; private set; }
        public int CustomerID { get; private set; }

        public SalesPerson SalesPerson { get; private set; }
        public int? SalesPersonID { get; private set; }

        internal void SetSalesPerson(SalesPerson salesPerson)
        {
            SalesPerson = salesPerson;
        }

        public string Territory { get; private set; }

        public Address BillToAddress { get; private set; }

        public Address ShipToAddress { get; private set; }

        public string ShipMethod { get; private set; }

        public CreditCard CreditCard { get; private set; }
        public int CreditCardID { get; private set; }

        public decimal SubTotal
        {
            get
            {
                return OrderLines.Select(x => x.LineTotal).Sum();
            }
        }

        public decimal TaxRate { get; private set; }

        public decimal TaxAmt => SubTotal / 100 * TaxRate;

        public decimal Freight { get; private set; }

        public decimal TotalDue
        {
            get
            {
                return SubTotal + TaxAmt + Freight;
            }
        }

        public string Comment { get; private set; }

        public List<SalesOrderLine> OrderLines { get; internal set; } = new();

        public List<SalesOrderSalesReason> SalesReasons { get; internal set; } = new();

        private void AddOrderStartedDomainEvent(string userId, string userName, string cardType, string cardNumber,
                string cardSecurityNumber, string cardHolderName, DateTime cardExpiration)
        {
            var orderStartedDomainEvent = new SalesOrderStartedDomainEvent(this, userId, userName, cardType,
                                                                      cardNumber, cardSecurityNumber,
                                                                      cardHolderName, cardExpiration);

            AddDomainEvent(orderStartedDomainEvent);
        }

        public void AddOrderLine(string productNumber, string productName, decimal unitPrice, decimal unitPriceDiscount, SpecialOfferProduct specialOfferProduct, short quantity = 1)
        {
            var existingOrderLineForProduct = OrderLines
                .SingleOrDefault(o => o.ProductNumber == productNumber);

            if (existingOrderLineForProduct != null)
            {
                //if previous line exist modify it with higher discount and units..

                if (unitPriceDiscount > existingOrderLineForProduct.UnitPriceDiscount)
                {
                    existingOrderLineForProduct.SetNewDiscount(unitPriceDiscount);
                }

                existingOrderLineForProduct.AddQuantity(quantity);
            }
            else
            {
                //add validated new order item

                var orderLine = new SalesOrderLine(productNumber, productName, unitPrice, unitPriceDiscount, specialOfferProduct, quantity);
                OrderLines.Add(orderLine);
            }
        }

        private void StatusChangeException(SalesOrderStatus orderStatusToChange)
        {
            throw new SalesDomainException($"Is not possible to change the order status from {Status.Name} to {orderStatusToChange.Name}.");
        }

        public void SetApprovedStatus()
        {
            if (Status == SalesOrderStatus.Cancelled || Status == SalesOrderStatus.Shipped)
            {
                StatusChangeException(SalesOrderStatus.Approved);
            }

            Status = SalesOrderStatus.Approved;
            AddDomainEvent(new SalesOrderApprovedDomainEvent(this));
        }

        public void SetRejectedStatus()
        {
            if (Status == SalesOrderStatus.Cancelled || Status == SalesOrderStatus.Shipped)
            {
                StatusChangeException(SalesOrderStatus.Rejected);
            }

            Status = SalesOrderStatus.Rejected;
            AddDomainEvent(new SalesOrderRejectedDomainEvent(this));
        }

        public void SetCancelledStatus()
        {
            if (Status == SalesOrderStatus.Shipped)
            {
                StatusChangeException(SalesOrderStatus.Cancelled);
            }

            Status = SalesOrderStatus.Cancelled;
            AddDomainEvent(new SalesOrderCancelledDomainEvent(this));
        }

        public void SetShippedStatus()
        {
            if (Status == SalesOrderStatus.Cancelled || Status == SalesOrderStatus.Rejected)
            {
                StatusChangeException(SalesOrderStatus.Shipped);
            }

            Status = SalesOrderStatus.Shipped;
            AddDomainEvent(new SalesOrderShippedDomainEvent(this));
        }
    }
}