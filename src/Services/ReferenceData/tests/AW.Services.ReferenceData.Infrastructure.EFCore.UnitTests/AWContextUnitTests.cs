using AW.Services.ReferenceData.Core.Entities;
using AW.Services.SharedKernel.EFCore;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace AW.Services.ReferenceData.Infrastructure.EFCore.UnitTests
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
                
            var context = new AWContext(
                contextOptions,
                typeof(EfRepository<>).Assembly
            );

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