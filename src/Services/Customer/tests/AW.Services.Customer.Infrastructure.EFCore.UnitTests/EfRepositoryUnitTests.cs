using Ardalis.Specification;
using Entities = AW.Services.Customer.Core.Entities;
using AW.Services.Customer.Core.Specifications;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.Customer.Infrastructure.EFCore.UnitTests
{
    public class EfRepositoryUnitTests
    {
        [Fact]
        public async void GetByIdAsync_ReturnsObject()
        {
            //Arrange
            var mockSet = new Mock<DbSet<Entities.StoreCustomer>>();
            mockSet.Setup(x => x.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    new Entities.StoreCustomer { Id = 1, Name = "A Bike Store" }
                );

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.StoreCustomer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.StoreCustomer>(mockContext.Object);

            //Act
            var storeCustomer = await repository.GetByIdAsync(1);

            //Assert
            storeCustomer.Name.Should().Be("A Bike Store");
        }

        [Fact]
        public async void GetBySpecAsync_ReturnsObject()
        {
            //Arrange
            var customers = new List<Entities.Customer>
            {
                new Entities.StoreCustomer { Id = 1, Name = "A Bike Store", AccountNumber = "AW00000001" },
                new Entities.StoreCustomer { Id = 2, Name = "Progressive Sports", AccountNumber = "AW00000002" }
            };
            var mockSet = customers.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            var spec = new GetCustomerSpecification("AW00000001");
            var customer = await repository.GetBySpecAsync(spec);

            //Assert
            customer.AccountNumber.Should().Be("AW00000001");
            (customer as Entities.StoreCustomer).Name.Should().Be("A Bike Store");
        }

        [Fact]
        public async void ListAllAsync_ReturnsObjects()
        {
            //Arrange
            var customers = new List<Entities.Customer>
            {
                new Entities.StoreCustomer { Id = 1, Name = "A Bike Store" },
                new Entities.StoreCustomer { Id = 2, Name = "Progressive Sports" }
            };
            var mockSet = customers.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            var list = await repository.ListAsync();

            //Assert
            list.Count.Should().Be(2);
        }

        [Fact]
        public async void ListAsync_ReturnsObjects()
        {
            //Arrange
            var customers = new List<Entities.Customer>
            {
                new Entities.StoreCustomer { Id = 1, Name = "A Bike Store" },
                new Entities.StoreCustomer { Id = 2, Name = "Progressive Sports" }
            };
            var mockSet = customers.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            var spec = new GetCustomersPaginatedSpecification(0, 10, Entities.CustomerType.Store, null, null);
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(2);
        }

        [Fact]
        public async void ListAsync_WithResultSpec_ReturnsObjects()
        {
            //Arrange
            var customers = new List<Entities.Customer>
            {
                new Entities.StoreCustomer { Id = 1, Name = "A Bike Store", AccountNumber = "AW00000001" },
                new Entities.StoreCustomer { Id = 2, Name = "Progressive Sports", AccountNumber = "AW00000002" }
            };
            var mockSet = customers.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            var spec = new GetCustomersAccountNumberSpecification();
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(2);
            list[0].Should().Be("AW00000001");
            list[1].Should().Be("AW00000002");
        }

        [Fact]
        public void ListAsync_WithNullResultSpec_ThrowsArgumentNullException()
        {
            //Arrange
            var mockContext = new Mock<AWContext>();
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            Func<Task> func = async () => await repository.ListAsync<string>(null);

            //Assert
            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ListAsync_WithResultSpecWithoutSelector_ThrowsSelectorNotFoundException()
        {
            //Arrange
            var mockContext = new Mock<AWContext>();
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            var spec = new GetCustomersAccountNumberWithoutSelectorSpecification();
            Func<Task> func = async () => await repository.ListAsync(spec);

            //Assert
            func.Should().Throw<SelectorNotFoundException>();
        }

        [Fact]
        public async void CountAsync_ReturnsCount()
        {
            //Arrange
            var customers = new List<Entities.Customer>
            {
                new Entities.StoreCustomer { Id = 1, Name = "A Bike Store", AccountNumber = "AW00000001" },
                new Entities.StoreCustomer { Id = 2, Name = "Progressive Sports", AccountNumber = "AW00000002" }
            };
            var mockSet = customers.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            var spec = new CountCustomersSpecification(Entities.CustomerType.Store, null, null);
            var count = await repository.CountAsync(spec);

            //Assert
            count.Should().Be(2);
        }

        [Fact]
        public async void AddAsync_SavesObject()
        {
            //Arrange
            var customers = new List<Entities.Customer>
            {
                new Entities.StoreCustomer { Id = 1, Name = "A Bike Store", AccountNumber = "AW00000001" },
                new Entities.StoreCustomer { Id = 2, Name = "Progressive Sports", AccountNumber = "AW00000002" }
            };
            var mockSet = customers.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            var newCustomer = new Entities.StoreCustomer { Name = "Advanced Bike Components", AccountNumber = "AW00000003" };
            var savedCustomer = await repository.AddAsync(newCustomer);

            //Assert
            mockSet.Verify(x => x.Add(It.IsAny<Entities.Customer>()));
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
            newCustomer.Should().BeEquivalentTo(savedCustomer);
        }        

        [Fact]
        public async void UpdateAsync_SavesObject()
        {
            //Arrange
            var customers = new List<Entities.Customer>
            {
                new Entities.StoreCustomer { Id = 1, Name = "A Bike Store", AccountNumber = "AW00000001" },
                new Entities.StoreCustomer { Id = 2, Name = "Progressive Sports", AccountNumber = "AW00000002" }
            };
            var mockSet = customers.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            mockContext.Setup(x => x.SetModified(It.IsAny<Entities.Customer>()));
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            var existingCustomer = new Entities.StoreCustomer { Name = "A Bike Store", AccountNumber = "AW00000001" };
            await repository.UpdateAsync(existingCustomer);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async void DeleteAsync_ReturnsObject()
        {
            //Arrange
            var customer1 = new Entities.StoreCustomer { Id = 1, Name = "A Bike Store", AccountNumber = "AW00000001" };
            var customer2 = new Entities.StoreCustomer { Id = 2, Name = "Progressive Sports", AccountNumber = "AW00000002" };
            var customers = new List<Entities.StoreCustomer>
            {
                customer1,
                customer2
            };
            var mockSet = customers.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.StoreCustomer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.StoreCustomer>(mockContext.Object);

            //Act
            await repository.DeleteAsync(customer1);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async void DeleteRangeAsync_ReturnsObject()
        {
            //Arrange
            var customer1 = new Entities.StoreCustomer { Id = 1, Name = "A Bike Store", AccountNumber = "AW00000001" };
            var customer2 = new Entities.StoreCustomer { Id = 2, Name = "Progressive Sports", AccountNumber = "AW00000002" };
            var customers = new List<Entities.StoreCustomer>
            {
                customer1,
                customer2
            };
            var mockSet = customers.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.StoreCustomer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.StoreCustomer>(mockContext.Object);

            //Act
            await repository.DeleteRangeAsync(customers);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}