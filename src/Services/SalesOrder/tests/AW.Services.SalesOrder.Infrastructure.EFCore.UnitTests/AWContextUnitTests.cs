﻿using AW.Services.SalesOrder.Core.Entities;
using AW.Services.SharedKernel.EFCore;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace AW.Services.SalesOrder.Infrastructure.EFCore.UnitTests
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
            entityTypes[0].Name.Should().Be(typeof(Address).FullName);
            entityTypes[1].Name.Should().Be(typeof(CreditCard).FullName);
            entityTypes[2].Name.Should().Be(typeof(Core.Entities.SalesOrder).FullName);            
            entityTypes[3].Name.Should().Be(typeof(SalesOrderLine).FullName);
            entityTypes[4].Name.Should().Be(typeof(SalesOrderSalesReason).FullName);
            entityTypes[5].Name.Should().Be(typeof(SalesReason).FullName);
            entityTypes[6].Name.Should().Be(typeof(SpecialOffer).FullName);
            entityTypes[7].Name.Should().Be(typeof(SpecialOfferProduct).FullName);
        }
    }
}