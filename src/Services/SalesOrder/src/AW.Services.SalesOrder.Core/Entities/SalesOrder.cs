using AW.Services.SalesOrder.Core.Events;
using AW.SharedKernel.Domain;
using AW.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AW.Services.SalesOrder.Core.Entities
{
    public class SalesOrder : Entity, IAggregateRoot
    {
        public SalesOrder() { }
        public SalesOrder(string userId, string userName, string customerNumber, string shipMethod, Address billToAddress, Address shipToAddress, string cardType, string cardNumber, string cardSecurityNumber,
                string cardHolderName, DateTime cardExpiration)
        {
            RevisionNumber = 1;
            OrderDate = DateTime.Today;
            DueDate = OrderDate.AddDays(12);
            Status = SalesOrderStatus.InProcess;
            OnlineOrderFlag = true;
            CustomerNumber = customerNumber;
            ShipMethod = shipMethod;
            TaxRate = 8;
            Freight = 0;
            BillToAddress = billToAddress;
            ShipToAddress = shipToAddress;

            AddOrderStartedDomainEvent(userId, userName, cardType, cardNumber,
                cardSecurityNumber, cardHolderName, cardExpiration);
        }

        public int Id { get; set; }
        public byte RevisionNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime? ShipDate { get; set; }

        public SalesOrderStatus Status { get; set; }

        public bool OnlineOrderFlag { get; set; }

        public string SalesOrderNumber { get; set; }

        public string PurchaseOrderNumber { get; set; }

        public string AccountNumber { get; set; }
        public string CustomerNumber { get; set; }

        public string SalesPerson { get; set; }

        public string Territory { get; set; }

        public Address BillToAddress { get; set; }

        public Address ShipToAddress { get; set; }

        public string ShipMethod { get; set; }

        public CreditCard CreditCard { get; set; }

        public decimal SubTotal
        {
            get
            {
                return OrderLines.Select(x => x.LineTotal).Sum();
            }
        }

        public decimal TaxRate { get; set; }

        public decimal TaxAmt => (SubTotal / 100) * TaxRate;

        public decimal Freight { get; set; }

        public decimal TotalDue
        {
            get
            {
                return SubTotal + TaxAmt + Freight;
            }
        }

        public string Comment { get; set; }

        public List<SalesOrderLine> OrderLines { get; set; } = new List<SalesOrderLine>();

        public List<SalesOrderSalesReason> SalesReasons { get; set; } = new List<SalesOrderSalesReason>();

        private void AddOrderStartedDomainEvent(string userId, string userName, string cardType, string cardNumber,
                string cardSecurityNumber, string cardHolderName, DateTime cardExpiration)
        {
            var orderStartedDomainEvent = new OrderStartedDomainEvent(this, userId, userName, cardType,
                                                                      cardNumber, cardSecurityNumber,
                                                                      cardHolderName, cardExpiration);

            this.AddDomainEvent(orderStartedDomainEvent);
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
    }
}