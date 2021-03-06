using Ardalis.Specification;
using AW.Services.Product.Application.GetProducts;
using AW.Services.Product.Application.Specifications;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Product.Application.UnitTests
{
    public class GetProductsQueryUnitTests
    {
        [Fact]
        public async void Handle_ProductsExists_ReturnProducts()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();

            #region Product 1
            var product1 = new TestBuilders.ProductBuilder().WithTestValues().Build();
            #endregion

            #region Product 2
            var product2 = new TestBuilders.ProductBuilder()
                .WithTestValues()
                .Id(718)
                .Name("HL Road Frame - Red, 44")
                .ProductNumber("FR-R92R-44")
                .MakeFlag(true)
                .FinishedGoodsFlag(true)
                .Color("Red")
                .SafetyStockLevel(500)
                .ReorderPoint(375)
                .StandardCost(868.6342M)
                .ListPrice(1431.50M)
                .Size("44")
                .SizeUnitMeasureCode("CM")
                .SizeUnitMeasure(new TestBuilders.UnitMeasureBuilder()
                    .UnitMeasureCode("CM")
                    .Name("Centimeter")
                    .Build()
                )
                .Weight(2.12M)
                .WeightUnitMeasureCode("LB")
                .WeightUnitMeasure(new TestBuilders.UnitMeasureBuilder()
                    .UnitMeasureCode("LB")
                    .Name("US pound")
                    .Build()
                )
                .DaysToManufacture(1)
                .ProductLine("R")
                .Class("H")
                .Style("U")
                .ProductSubcategoryId(14)
                .ProductSubcategory(new TestBuilders.ProductSubcategoryBuilder()
                    .WithTestValues()
                    .Id(14)
                    .Name("Road Frames")
                    .ProductCategoryId(2)
                    .ProductCategory(new TestBuilders.ProductCategoryBuilder()
                        .WithTestValues()
                        .Id(2)
                        .Name("Components")
                        .Build()
                    )
                    .Build()
                )
                .ProductModelId(6)
                .ProductModel(new TestBuilders.ProductModelBuilder()
                    .WithTestValues()
                    .Id(6)
                    .Name("HL Road Frame")
                    .Build()
                )
                .SellStartDate(new DateTime(2011, 05, 31))
                .Build();
            #endregion

            var loggerMock = new Mock<ILogger<GetProductsQueryHandler>>();
            var productRepoMock = new Mock<IRepositoryBase<Domain.Product>>();
            productRepoMock.Setup(x => x.ListAsync(It.IsAny<GetProductsPaginatedSpecification>()))
                .ReturnsAsync(new List<Domain.Product>
                {
                    product1,
                    product2
                });

            var handler = new GetProductsQueryHandler(
                loggerMock.Object,
                productRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetProductsQuery();
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            productRepoMock.Verify(x => x.ListAsync(It.IsAny<ISpecification<Domain.Product>>()));
            result.ToList()[0].ProductNumber.Should().Be("FR-R92B-58");
            result.ToList()[1].ProductNumber.Should().Be("FR-R92R-44");
        }

        [Fact]
        public void Handle_NoProductsExists_ThrowArgumentNullException()
        {
            // Arrange
            var mapper = Mapper.CreateMapper();

            var loggerMock = new Mock<ILogger<GetProductsQueryHandler>>();
            var productRepoMock = new Mock<IRepositoryBase<Domain.Product>>();

            var handler = new GetProductsQueryHandler(
                loggerMock.Object,
                productRepoMock.Object,
                mapper
            );

            //Act
            var query = new GetProductsQuery();
            Func<Task> func = async() => await handler.Handle(query, CancellationToken.None);

            //Assert
            func.Should().Throw<ArgumentNullException>();
            productRepoMock.Verify(x => x.ListAsync(It.IsAny<ISpecification<Domain.Product>>()));
        }
    }
}