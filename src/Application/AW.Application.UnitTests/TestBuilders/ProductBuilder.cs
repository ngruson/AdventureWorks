using System;
using System.Collections.Generic;

namespace AW.Application.UnitTests.TestBuilders
{
    public class ProductBuilder
    {
        private Domain.Production.Product product = new Domain.Production.Product();

        public ProductBuilder Id(int id)
        {
            product.Id = id;
            return this;
        }

        public ProductBuilder Name(string name)
        {
            product.Name = name;
            return this;
        }

        public ProductBuilder ProductNumber(string productNumber)
        {
            product.ProductNumber = productNumber;
            return this;
        }

        public ProductBuilder MakeFlag(bool makeFlag)
        {
            product.MakeFlag = makeFlag;
            return this;
        }

        public ProductBuilder FinishedGoodsFlag(bool finishedGoodsFlag)
        {
            product.FinishedGoodsFlag = finishedGoodsFlag;
            return this;
        }

        public ProductBuilder Color(string color)
        {
            product.Color = color;
            return this;
        }

        public ProductBuilder SafetyStockLevel(short safetyStockLevel)
        {
            product.SafetyStockLevel = safetyStockLevel;
            return this;
        }

        public ProductBuilder ReorderPoint(short reorderPoint)
        {
            product.ReorderPoint = reorderPoint;
            return this;
        }

        public ProductBuilder StandardCost(decimal standardCost)
        {
            product.StandardCost = standardCost;
            return this;
        }

        public ProductBuilder ListPrice(decimal listPrice)
        {
            product.ListPrice = listPrice;
            return this;
        }

        public ProductBuilder Size(string size)
        {
            product.Size = size;
            return this;
        }

        public ProductBuilder SizeUnitMeasureCode(Domain.Production.UnitMeasure unitMeasure)
        {
            product.SizeUnitMeasure = unitMeasure;
            return this;
        }

        public ProductBuilder Weight(decimal weight)
        {
            product.Weight = weight;
            return this;
        }

        public ProductBuilder WeightUnitMeasureCode(Domain.Production.UnitMeasure unitMeasure)
        {
            product.WeightUnitMeasure = unitMeasure;
            return this;
        }

        public ProductBuilder DaysToManufacture(int daysToManufacture)
        {
            product.DaysToManufacture = daysToManufacture;
            return this;
        }

        public ProductBuilder ProductLine(string productLine)
        {
            product.ProductLine = productLine;
            return this;
        }

        public ProductBuilder Class(string @class)
        {
            product.Class = @class;
            return this;
        }

        public ProductBuilder Style(string style)
        {
            product.Style = style;
            return this;
        }

        public ProductBuilder ProductSubcategory(Domain.Production.ProductSubcategory productSubcategory)
        {
            product.ProductSubcategory = productSubcategory;
            return this;
        }

        public ProductBuilder ProductModel(Domain.Production.ProductModel productModel)
        {
            product.ProductModel = productModel;
            return this;
        }

        public ProductBuilder SellStartDate(DateTime sellStartDate)
        {
            product.SellStartDate = sellStartDate;
            return this;
        }

        public ProductBuilder SellEndDate(DateTime sellEndDate)
        {
            product.SellEndDate = sellEndDate;
            return this;
        }

        public ProductBuilder DiscontinuedDate(DateTime discontinuedDate)
        {
            product.DiscontinuedDate = discontinuedDate;
            return this;
        }

        public Domain.Production.Product Build()
        {
            return product;
        }

        public ProductBuilder WithTestValues()
        {
            product = new Domain.Production.Product
            {
                Id = new Random().Next(),
                Name = "LL Mountain Seat Assembly",
                ProductNumber = "SA-M198",
                MakeFlag = true,
                FinishedGoodsFlag = false,
                Color = null,
                SafetyStockLevel = 500,
                ReorderPoint = 375,
                StandardCost = 98.77M,
                ListPrice = 133.34M,
                DaysToManufacture = 1,
                Class = "L",
                ProductProductPhotos = new List<Domain.Production.ProductProductPhoto>
                {
                    new ProductProductPhotoBuilder().WithTestValues().Build()
                }
            };

            return this;
        }
    }
}