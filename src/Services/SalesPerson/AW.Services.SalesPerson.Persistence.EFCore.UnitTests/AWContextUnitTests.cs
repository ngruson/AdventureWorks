using AW.Services.SalesPerson.Domain;
using AW.Services.SalesPerson.Persistence.EntityFrameworkCore;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace AW.Services.SalesPerson.Persistence.EFCore.UnitTests
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
            entityTypes[0].Name.Should().Be(typeof(Domain.SalesPerson).FullName);
            entityTypes[1].Name.Should().Be(typeof(SalesPersonEmailAddress).FullName);
            entityTypes[2].Name.Should().Be(typeof(SalesPersonPhone).FullName);
        }
    }
}