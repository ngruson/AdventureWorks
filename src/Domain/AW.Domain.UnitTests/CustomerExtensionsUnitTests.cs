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
            var customer = new Customer
            {
                Store = new Store
                {
                    Name = "A Bike Store"
                }
            };

            //Assert
            customer.Name().Should().Be("A Bike Store");
        }

        [Fact]
        public void CustomerName_PersonWithMiddleName()
        {
            //Arrange
            var customer = new Customer
            {
                Person = new Person.Person
                {
                    FirstName = "Orlando",
                    MiddleName = "N.",
                    LastName = "Gee"
                }
            };

            //Act
            string customerName = customer.Name();

            //Assert
            customerName.Should().Be("Orlando N. Gee");
        }

        [Fact]
        public void CustomerName_PersonWithoutMiddleName()
        {
            //Arrange
            var customer = new Customer
            {
                Person = new Person.Person
                {
                    FirstName = "Orlando",
                    LastName = "Gee"
                }
            };

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