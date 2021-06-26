using AW.Services.Customer.Persistence.EntityFramework;
using Xunit;

namespace AW.Services.Customer.Persistence.EF.UnitTests
{
    public class AWContextUnitTests
    {
        [Fact]
        public void CreateDatabase_ModelConfigurationsAreApplied()
        {
            //Arrange
            var connection = Effort.DbConnectionFactory.CreateTransient();
            var context = new AWContext(connection, true);

            //Act
            context.Database.Create();

            //Assert
            
        }
    }
}