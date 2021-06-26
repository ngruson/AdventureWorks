using AW.Services.SalesOrder.Domain;
using AW.Services.SalesOrder.Persistence.EntityFrameworkCore;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace AW.Services.SalesOrder.Persistence.EFCore.UnitTests
{
    public class AWContextUnitTests
    {
        [Fact]
        public void CreateDatabase_ModelConfigurationsAreApplied()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<AWContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
                
            var context = new AWContext(contextOptions);

            //Act
            context.Database.EnsureCreated();

            //Assert
            var entityTypes = context.Model.GetEntityTypes().ToList();
            entityTypes[0].Name.Should().Be(typeof(Address).FullName);
            entityTypes[1].Name.Should().Be(typeof(CreditCard).FullName);
            entityTypes[2].Name.Should().Be(typeof(Domain.SalesOrder).FullName);            
            entityTypes[3].Name.Should().Be(typeof(SalesOrderLine).FullName);
            entityTypes[4].Name.Should().Be(typeof(SalesOrderSalesReason).FullName);
            entityTypes[5].Name.Should().Be(typeof(SalesReason).FullName);
            entityTypes[6].Name.Should().Be(typeof(SpecialOffer).FullName);
            entityTypes[7].Name.Should().Be(typeof(SpecialOfferProduct).FullName);
        }
    }
}