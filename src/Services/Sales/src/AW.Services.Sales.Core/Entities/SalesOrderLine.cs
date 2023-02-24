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
            SpecialOfferProduct = specialOfferProduct;
            SpecialOfferProductId = specialOfferProduct.Id;
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
        public SpecialOfferProduct? SpecialOfferProduct { get; set; }
        public int SpecialOfferProductId { get; set; }
        public byte[]? ThumbNailPhoto { get; set; }
        public string? Color { get; set; }
        public string? ProductLine { get; set; }
        public string? Class { get; set; }
        public string? Style { get; set; }

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
