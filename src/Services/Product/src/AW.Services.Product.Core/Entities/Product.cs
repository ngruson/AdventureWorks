using AW.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AW.Services.Product.Core.Entities
{
    public class Product : IAggregateRoot
    {
        public Product()
        {
        }
        public Product(string productNumber, string color)
        {
            ProductNumber = productNumber;
            Color = color;
        }

        private int Id { get; set; }
        public string Name { get; private set; }
       
        public string ProductNumber { get; private set; }

        public bool MakeFlag { get; private set; }

        public bool FinishedGoodsFlag { get; private set; }
        
        public string Color { get; private set; }

        public short SafetyStockLevel { get; private set; }

        public short ReorderPoint { get; private set; }

        public decimal StandardCost { get; private set; }

        public decimal ListPrice { get; private set; }

        public string Size { get; private set; }

        public string SizeUnitMeasureCode { get; private set; }

        public string WeightUnitMeasureCode { get; private set; }
        public decimal? Weight { get; private set; }

        public int DaysToManufacture { get; private set; }

        public string ProductLine { get; private set; }

        public string Class { get; private set; }

        public string Style { get; private set; }

        public int? ProductSubcategoryId { get; private set; }

        public int? ProductModelId { get; private set; }

        public DateTime SellStartDate { get; private set; }

        public DateTime? SellEndDate { get; private set; }

        public DateTime? DiscontinuedDate { get; private set; }

        public virtual ProductModel ProductModel { get; private set; }

        public virtual ProductSubcategory ProductSubcategory { get; private set; }

        public virtual UnitMeasure SizeUnitMeasure { get; private set; }

        public virtual UnitMeasure WeightUnitMeasure { get; private set; }

        public IEnumerable<ProductProductPhoto> ProductProductPhotos => _productProductPhotos.ToList();
        private List<ProductProductPhoto> _productProductPhotos = new();

        public void AddProductPhoto(ProductProductPhoto photo)
        {
            _productProductPhotos.Add(photo);
        }

        public void SetPrimaryPhoto(ProductProductPhoto photo)
        {
            foreach (var item in _productProductPhotos)
            {
                item.Primary = photo == item;
            }
        }
    }
}