using AW.Application.UnitTests.TestBuilders;
using AW.Domain.Sales;
using FluentAssertions;
using Xunit;

namespace AW.Domain.UnitTests
{
    public class CustomerExtensionsUnitTests
    {
        [Fact]
        public void CustomerName_Store()
        {
            //Arrange
            //Act
            var customer = new CustomerBuilder().WithTestValues().Build();

            //Assert
            customer.Name().Should().Be("A Bike Store");
        }

        [Fact]
        public void CustomerName_PersonWithMiddleName()
        {
            //Arrange
            var customer = new CustomerBuilder()
                .Person(new PersonBuilder().WithTestValues().Build())
                .Build();

            //Act
            string customerName = customer.Name();

            //Assert
            customerName.Should().Be("Orlando N. Gee");
        }

        [Fact]
        public void CustomerName_PersonWithoutMiddleName()
        {
            //Arrange
            var customer = new CustomerBuilder()
                .Person(new PersonBuilder()
                    .WithTestValues()
                    .MiddleName("")
                    .Build()
                )
                .Build();

            //Act
            string customerName = customer.Name();

            //Assert
            customerName.Should().Be("Orlando Gee");
        }

        [Fact]
        public void Customer_Null_ReturnNull()
        {
            //Arrange
            var customer = new Customer();

            //Act
            string customerName = customer.Name();

            //Assert
            customerName.Should().BeNull();
        }
    }
}