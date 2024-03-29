﻿using AW.Services.ReferenceData.Core.Entities;
using AW.Services.ReferenceData.Infrastructure.EFCore.Configurations;
using AW.Services.SharedKernel.EFCore;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AW.Services.ReferenceData.Infrastructure.EFCore.UnitTests
{
    public class AWContextUnitTests
    {
        [Theory, AutoMoqData]
        public void CreateDatabase_ModelConfigurationsAreApplied(
            Mock<IMediator> mockMediator,
            Mock<ILogger<AWContext>> mockLogger
            )
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<AWContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
                
            var context = new AWContext(
                mockLogger.Object,
                contextOptions,
                mockMediator.Object,
                typeof(AddressTypeConfiguration).Assembly
            );

            //Act
            context.Database.EnsureCreated();

            //Assert
            var entityTypes = context.Model.GetEntityTypes().ToList();
            entityTypes[0].Name.Should().Be(typeof(AddressType).FullName);
            entityTypes[1].Name.Should().Be(typeof(ContactType).FullName);
            entityTypes[2].Name.Should().Be(typeof(CountryRegion).FullName);
            entityTypes[3].Name.Should().Be(typeof(ShipMethod).FullName);
            entityTypes[4].Name.Should().Be(typeof(StateProvince).FullName);
            entityTypes[5].Name.Should().Be(typeof(Territory).FullName);
        }
    }
}