using AW.Services.SharedKernel.EF6;
using FluentAssertions;
using Xunit;

namespace AW.Services.SalesOrder.Infrastructure.EF6.UnitTests
{
    public class AWContextUnitTests
    {
        [Fact]
        public void CreateDatabase_ModelConfigurationsAreApplied()
        {
            using (var connection = Effort.DbConnectionFactory.CreateTransient())
            {
                //Arrange
                var context = new AWContext(
                    connection,
                    true,
                    typeof(EfRepository<>).Assembly,
                    null
                );

                //Act
                context.Database.Delete();
                context.Database.Create();
                context.Database.Connection.Open();

                //Assert
                var state = context.Database.Connection.State == System.Data.ConnectionState.Open;
                state.Should().Be(true);

                
            }
        }
    }
}