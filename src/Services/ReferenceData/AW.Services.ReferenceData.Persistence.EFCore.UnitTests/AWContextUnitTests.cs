using AW.Services.ReferenceData.Domain;
using AW.Services.ReferenceData.Persistence.EntityFrameworkCore;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace AW.Services.ReferenceData.Persistence.EFCore.UnitTests
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
            entityTypes[0].Name.Should().Be(typeof(AddressType).FullName);
            entityTypes[1].Name.Should().Be(typeof(ContactType).FullName);
            entityTypes[2].Name.Should().Be(typeof(CountryRegion).FullName);
            entityTypes[3].Name.Should().Be(typeof(StateProvince).FullName);
            entityTypes[4].Name.Should().Be(typeof(Territory).FullName);
        }
    }
}