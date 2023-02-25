using AW.Services.Sales.Core.Exceptions;

namespace AW.Services.Sales.Core.Entities
{
    public class SalesOrderLine
    {
        public SalesOrderLine() { }
        public SalesOrderLine(string productNumber, string productName, decimal unitPrice, decimal unitPriceDiscount, SpecialOffer specialOffer, short quantity = 1)
        {
            if (quantity <= 0)
            {
                throw new SalesDomainException("Invalid quantity");
            }

            if (unitPrice * quantity < unitPriceDiscount)
            {
                throw new SalesDomainException("The total of order items is lower than applied discount");
            }

            ProductNumber = productNumber;
            ProductName = productName;
            UnitPrice = unitPrice;
            UnitPriceDiscount = unitPriceDiscount;
            OrderQty = quantity;
            SpecialOffer = specialOffer;
            SpecialOfferId = specialOffer.Id;
            ProductLine = ProductLine.Mountain;
        }

        public int Id { get; set; }
        public int SalesOrderID { get; set; }
        public string? CarrierTrackingNumber { get; set; }
        public short OrderQty { get; set; }
        public string? ProductNumber { get; set; }
        public string? ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitPriceDiscount { get; set; }
        public decimal LineTotal => Math.Round(Math.Round(UnitPrice, 2) * (1 - UnitPriceDiscount) * OrderQty, 2);
        public SpecialOffer? SpecialOffer { get; set; }
        public int SpecialOfferId { get; set; }
        public byte[]? ThumbNailPhoto { get; set; }
        public string? Color { get; set; }
        public ProductLine? ProductLine { get; private set; }
        public Class? Class { get; private set; }
        public Style? Style { get; private set; }

        public void SetNewDiscount(decimal discount)
        {
            if (discount < 0)
            {
                throw new SalesDomainException("Discount is not valid");
            }

            UnitPriceDiscount = discount;
        }

        public void AddQuantity(short quantity)
        {
            if (quantity < 0)
            {
                throw new SalesDomainException("Invalid units");
            }

            OrderQty += quantity;
        }
    }
}
