using AW.Domain.Purchasing;
using System;
using System.Collections.Generic;

namespace AW.Domain.Production
{
    public class Product
    {
        public virtual int Id { get; set; }
        public string Name { get; set; }
       
        public string ProductNumber { get; set; }

        public bool MakeFlag { get; set; }

        public bool FinishedGoodsFlag { get; set; }
        
        public string Color { get; set; }

        public short SafetyStockLevel { get; set; }

        public short ReorderPoint { get; set; }

        public decimal StandardCost { get; set; }

        public decimal ListPrice { get; set; }

        public string Size { get; set; }

        public string SizeUnitMeasureCode { get; set; }

        public string WeightUnitMeasureCode { get; set; }

        public decimal? Weight { get; set; }

        public int DaysToManufacture { get; set; }

        public string ProductLine { get; set; }

        public string Class { get; set; }

        public string Style { get; set; }

        public int? ProductSubcategoryID { get; set; }

        public int? ProductModelID { get; set; }

        public DateTime SellStartDate { get; set; }

        public DateTime? SellEndDate { get; set; }

        public DateTime? DiscontinuedDate { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<BillOfMaterials> BillOfMaterials { get; set; } = new List<BillOfMaterials>();

        public virtual ProductModel ProductModel { get; set; }

        public virtual ProductSubcategory ProductSubcategory { get; set; }

        public virtual UnitMeasure SizeUnitMeasure { get; set; }

        public virtual UnitMeasure WeightUnitMeasure { get; set; }
        
        public virtual ICollection<ProductCostHistory> ProductCostHistory { get; set; } = new List<ProductCostHistory>();

        public virtual ICollection<ProductDocument> ProductDocuments { get; set; } = new List<ProductDocument>();

        public virtual ICollection<ProductInventory> ProductInventory { get; set; } = new List<ProductInventory>();

        public virtual ICollection<ProductListPriceHistory> ProductListPriceHistory { get; set; } = new List<ProductListPriceHistory>();

        public virtual ICollection<ProductProductPhoto> ProductProductPhotos { get; set; } = new List<ProductProductPhoto>();

        public virtual ICollection<ProductReview> ProductReview { get; set; } = new List<ProductReview>();

        public virtual ICollection<ProductVendor> ProductVendor { get; set; } = new List<ProductVendor>();

        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetail { get; set; } = new List<PurchaseOrderDetail>();
    }
}