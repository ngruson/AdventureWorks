using AW.Services.Product.Core.Entities;
using AW.Services.SharedKernel.EFCore;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace AW.Services.Product.Infrastructure.EFCore.UnitTests
{
    public class AWContextUnitTests
    {
        [Theory, AutoMoqData]
        public void CreateDatabase_ModelConfigurationsAreApplied(
            Mock<IMediator> mockMediator
            )
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<AWContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new AWContext(
                contextOptions,
                mockMediator.Object,
                typeof(EfRepository<>).Assembly
            );

            //Act
            context.Database.EnsureCreated();

            //Assert
            var entityTypes = context.Model.GetEntityTypes().ToList();
            entityTypes[0].Name.Should().Be(typeof(BillOfMaterials).FullName);
            entityTypes[1].Name.Should().Be(typeof(Culture).FullName);
            entityTypes[2].Name.Should().Be(typeof(Document).FullName);
            entityTypes[3].Name.Should().Be(typeof(Illustration).FullName);
            entityTypes[4].Name.Should().Be(typeof(Location).FullName);
            entityTypes[5].Name.Should().Be(typeof(Core.Entities.Product).FullName);
            entityTypes[6].Name.Should().Be(typeof(ProductCategory).FullName);            
            entityTypes[7].Name.Should().Be(typeof(ProductCostHistory).FullName);
            entityTypes[8].Name.Should().Be(typeof(ProductDescription).FullName);
            entityTypes[9].Name.Should().Be(typeof(ProductDocument).FullName);
            entityTypes[10].Name.Should().Be(typeof(ProductInventory).FullName);
            entityTypes[11].Name.Should().Be(typeof(ProductListPriceHistory).FullName);
            entityTypes[12].Name.Should().Be(typeof(ProductModel).FullName);
            entityTypes[13].Name.Should().Be(typeof(ProductModelIllustration).FullName);
            entityTypes[14].Name.Should().Be(typeof(ProductModelProductDescriptionCulture).FullName);
            entityTypes[15].Name.Should().Be(typeof(ProductPhoto).FullName);
            entityTypes[16].Name.Should().Be(typeof(ProductProductPhoto).FullName);
            entityTypes[17].Name.Should().Be(typeof(ProductSubcategory).FullName);
            entityTypes[18].Name.Should().Be(typeof(UnitMeasure).FullName);
        }
    }
}