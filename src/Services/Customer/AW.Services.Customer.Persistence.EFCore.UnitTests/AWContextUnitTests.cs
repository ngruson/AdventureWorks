using AW.Services.Customer.Domain;
using AW.Services.Customer.Persistence.EntityFrameworkCore;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace AW.Services.Customer.Persistence.EFCore.UnitTests
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
            entityTypes[1].Name.Should().Be(typeof(Domain.Customer).FullName);
            entityTypes[2].Name.Should().Be(typeof(CustomerAddress).FullName);            
            entityTypes[3].Name.Should().Be(typeof(IndividualCustomer).FullName);
            entityTypes[4].Name.Should().Be(typeof(Person).FullName);
            entityTypes[5].Name.Should().Be(typeof(PersonEmailAddress).FullName);
            entityTypes[6].Name.Should().Be(typeof(PersonPhone).FullName);
            entityTypes[7].Name.Should().Be(typeof(SalesOrder).FullName);
            entityTypes[8].Name.Should().Be(typeof(StoreCustomer).FullName);
            entityTypes[9].Name.Should().Be(typeof(StoreCustomerContact).FullName);
        }
    }
}