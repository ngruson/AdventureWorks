using AW.Services.Sales.Core.Exceptions;

namespace AW.Services.Sales.Core.Entities
{
    public class SalesOrderLine
    {
        public SalesOrderLine() { }
        public SalesOrderLine(string productNumber, string productName, decimal unitPrice, decimal unitPriceDiscount, SpecialOfferProduct specialOfferProduct, short quantity = 1)
        {
            if (quantity <= 0)
            {
                throw new SalesOrderDomainException("Invalid quantity");
            }

            if (unitPrice * quantity < unitPriceDiscount)
            {
                throw new SalesOrderDomainException("The total of order items is lower than applied discount");
            }

            ProductNumber = productNumber;
            ProductName = productName;
            UnitPrice = unitPrice;
            UnitPriceDiscount = unitPriceDiscount;
            OrderQty = quantity;
            SpecialOfferProduct = specialOfferProduct;
            SpecialOfferProductId = specialOfferProduct.Id;
        }

        public int Id { get; set; }
        public int SalesOrderID { get; set; }
        public string CarrierTrackingNumber { get; set; }
        public short OrderQty { get; set; }
        public string ProductNumber { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitPriceDiscount { get; set; }
        public decimal LineTotal => (UnitPrice - UnitPriceDiscount) * OrderQty;
        public SpecialOfferProduct SpecialOfferProduct { get; set; }
        public int SpecialOfferProductId { get; set; }

        public void SetNewDiscount(decimal discount)
        {
            if (discount < 0)
            {
                throw new SalesOrderDomainException("Discount is not valid");
            }

            UnitPriceDiscount = discount;
        }

        public void AddQuantity(short quantity)
        {
            if (quantity < 0)
            {
                throw new SalesOrderDomainException("Invalid units");
            }

            OrderQty += quantity;
        }
    }
}