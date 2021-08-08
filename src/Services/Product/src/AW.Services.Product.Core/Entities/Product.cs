using AW.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;

namespace AW.Services.Product.Core.Entities
{
    public class Product : IAggregateRoot
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

        public int? ProductSubcategoryId { get; set; }

        public int? ProductModelId { get; set; }

        public DateTime SellStartDate { get; set; }

        public DateTime? SellEndDate { get; set; }

        public DateTime? DiscontinuedDate { get; set; }

        public virtual ProductModel ProductModel { get; set; }

        public virtual ProductSubcategory ProductSubcategory { get; set; }

        public virtual UnitMeasure SizeUnitMeasure { get; set; }

        public virtual UnitMeasure WeightUnitMeasure { get; set; }
        

        public List<ProductProductPhoto> ProductProductPhotos { get; set; } = new List<ProductProductPhoto>();
    }
}