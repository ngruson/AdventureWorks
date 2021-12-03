using AW.Services.SalesPerson.Core.Entities;
using AW.Services.SharedKernel.EFCore;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace AW.Services.SalesPerson.Infrastructure.EFCore.UnitTests
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
                typeof(EfRepository<>).Assembly,
                mockMediator.Object
            );

            //Act
            context.Database.EnsureCreated();

            //Assert
            var entityTypes = context.Model.GetEntityTypes().ToList();
            entityTypes[0].Name.Should().Be(typeof(Core.Entities.SalesPerson).FullName);
            entityTypes[1].Name.Should().Be(typeof(SalesPersonEmailAddress).FullName);
            entityTypes[2].Name.Should().Be(typeof(SalesPersonPhone).FullName);
        }
    }
}