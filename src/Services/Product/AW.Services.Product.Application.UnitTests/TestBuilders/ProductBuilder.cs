using System;
using System.Collections.Generic;
using System.Text;

namespace AW.Services.Product.Application.UnitTests.TestBuilders
{
    public class ProductBuilder
    {
        private Domain.Product product = new Domain.Product();

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

        public ProductBuilder SizeUnitMeasureCode(string sizeUnitMeasureCode)
        {
            product.SizeUnitMeasureCode = sizeUnitMeasureCode;
            return this;
        }

        public ProductBuilder SizeUnitMeasure(Domain.UnitMeasure sizeUnitMeasure)
        {
            product.SizeUnitMeasure = sizeUnitMeasure;
            return this;
        }

        public ProductBuilder Weight(decimal? weight)
        {
            product.Weight = weight;
            return this;
        }

        public ProductBuilder WeightUnitMeasureCode(string weightUnitMeasureCode)
        {
            product.WeightUnitMeasureCode = weightUnitMeasureCode;
            return this;
        }

        public ProductBuilder WeightUnitMeasure(Domain.UnitMeasure weightUnitMeasure)
        {
            product.WeightUnitMeasure = weightUnitMeasure;
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

        public ProductBuilder ProductSubcategoryId(int productSubcategoryId)
        {
            product.ProductSubcategoryId = productSubcategoryId;
            return this;
        }

        public ProductBuilder ProductSubcategory(Domain.ProductSubcategory productSubcategory)
        {
            product.ProductSubcategory = productSubcategory;
            return this;
        }

        public ProductBuilder ProductModelId(int productModelId)
        {
            product.ProductModelId = productModelId;
            return this;
        }

        public ProductBuilder ProductModel(Domain.ProductModel productModel)
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

        public Domain.Product Build()
        {
            return product;
        }

        public ProductBuilder WithTestValues()
        {
            product = new Domain.Product
            {
                Name = "HL Road Frame - Black, 58",
                ProductNumber = "FR-R92B-58",
                MakeFlag = true,
                FinishedGoodsFlag = true,
                Color = "Black",
                SafetyStockLevel = 500,
                ReorderPoint = 375,
                StandardCost = 1059.31M,
                ListPrice = 1431.50M,
                Size = "58",
                SizeUnitMeasureCode = "CM",
                SizeUnitMeasure = new UnitMeasureBuilder()
                    .UnitMeasureCode("CM")
                    .Name("Centimeter")
                    .Build(),
                Weight = 2.24M,
                WeightUnitMeasureCode = "LB",
                WeightUnitMeasure = new UnitMeasureBuilder()
                    .UnitMeasureCode("LB")
                    .Name("US pound")
                    .Build(),
                DaysToManufacture = 1,
                ProductLine = "R",
                Class = "H",
                Style = "U",
                ProductSubcategory = new ProductSubcategoryBuilder()
                    .WithTestValues()
                    .Id(14)
                    .Name("Road Frames")
                    .ProductCategoryId(2)
                    .ProductCategory(new ProductCategoryBuilder()
                        .WithTestValues()
                        .Id(2)
                        .Name("Components")
                        .Build()
                    )                
                    .Build(),
                ProductModelId = 6,
                ProductModel = new ProductModelBuilder()
                    .WithTestValues()
                    .Id(6)
                    .Name("HL Road Frame")
                    .Build(),
                SellStartDate = new DateTime(2008, 04, 30),
                ProductProductPhotos = new List<Domain.ProductProductPhoto>
                {
                    new Domain.ProductProductPhoto
                    {
                        Primary = true,
                        ProductPhoto = new Domain.ProductPhoto
                        {
                            ThumbNailPhoto = Encoding.ASCII.GetBytes("thumbnailPhoto"),
                            LargePhoto = Encoding.ASCII.GetBytes("largePhoto"),
                        }
                    }
                }
            };

            return this;
        }
    }
}