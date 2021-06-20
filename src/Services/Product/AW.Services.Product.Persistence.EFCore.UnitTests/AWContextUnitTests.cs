using AW.Services.Product.Persistence.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace AW.Services.Product.Persistence.EFCore.UnitTests
{
    public class AWContextUnitTests
    {
        [Fact]
        public void Test()
        {
            //Arrange
            var contextOptions = new DbContextOptionsBuilder<AWContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
                
            var context = new AWContext(contextOptions);

            //Act
            context.Database.EnsureCreated();

            //Assert
        }
    }
}