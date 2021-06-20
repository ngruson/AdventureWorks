using AW.Services.Customer.Persistence.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace AW.Services.Customer.Persistence.EFCore.UnitTests
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