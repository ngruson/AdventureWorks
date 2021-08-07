using Entities = AW.Services.Customer.Core.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;
using AW.Services.SharedKernel.EFCore;

namespace AW.Services.Customer.Infrastructure.EFCore.UnitTests
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
            entityTypes[0].Name.Should().Be(typeof(Entities.Address).FullName);
            entityTypes[1].Name.Should().Be(typeof(Entities.Customer).FullName);
            entityTypes[2].Name.Should().Be(typeof(Entities.CustomerAddress).FullName);            
            entityTypes[3].Name.Should().Be(typeof(Entities.IndividualCustomer).FullName);
            entityTypes[4].Name.Should().Be(typeof(Entities.Person).FullName);
            entityTypes[5].Name.Should().Be(typeof(Entities.PersonEmailAddress).FullName);
            entityTypes[6].Name.Should().Be(typeof(Entities.PersonPhone).FullName);
            entityTypes[7].Name.Should().Be(typeof(Entities.SalesOrder).FullName);
            entityTypes[8].Name.Should().Be(typeof(Entities.StoreCustomer).FullName);
            entityTypes[9].Name.Should().Be(typeof(Entities.StoreCustomerContact).FullName);
        }
    }
}