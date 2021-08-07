using AW.Services.SharedKernel.EF6;
using Xunit;

namespace AW.Services.Product.Infrastructure.EF6.UnitTests
{
    public class AWContextUnitTests
    {
        [Fact]
        public void CreateDatabase_ModelConfigurationsAreApplied()
        {
            //Arrange
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new AWContext(
                connection, 
                true,
                typeof(EfRepository<>).Assembly
            );

            //Act
            context.Database.Delete();
            context.Database.Create();

            //Assert
            
        }
    }
}