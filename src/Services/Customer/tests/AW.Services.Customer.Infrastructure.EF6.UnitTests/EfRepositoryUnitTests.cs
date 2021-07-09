using Ardalis.Specification;
using Entities = AW.Services.Customer.Core.Entities;
using AW.SharedKernel.UnitTesting.EF6;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using AW.Services.Customer.Core.Specifications;

namespace AW.Services.Customer.Infrastructure.EF6.UnitTests
{
    public class EfRepositoryUnitTests
    {
        private static Mock<DbSet<T>> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var data = sourceList.AsQueryable();

            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<T>(data.Provider));
            mockSet.As<IDbAsyncEnumerable<T>>().Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<T>(data.GetEnumerator()));

            mockSet.Setup(m => m.Add(It.IsAny<T>())).Callback((T obj) => sourceList.Add(obj));
            mockSet.Setup(m => m.Remove(It.IsAny<T>())).Callback((T obj) => sourceList.Remove(obj));

            return mockSet;
        }

        [Fact]
        public async void GetByIdAsync_ReturnsObject()
        {
            //Arrange
            var mockSet = new Mock<DbSet<Entities.StoreCustomer>>();
            mockSet.Setup(x => x.FindAsync(
                It.IsAny<CancellationToken>(),
                It.IsAny<int>()
            ))
            .ReturnsAsync(
                new Entities.StoreCustomer { Id = 1, Name = "A Bike Store" }
            );

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.StoreCustomer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.StoreCustomer>(mockContext.Object);

            //Act
            var person = await repository.GetByIdAsync(1);

            //Assert
            person.Name.Should().Be("A Bike Store");
        }

        [Fact]
        public async void GetBySpecAsync_ReturnsObject()
        {
            //Arrange
            var address1 = new Entities.Address { AddressLine1 = "2251 Elliot Avenue", PostalCode = "98104", City = "Seattle", StateProvinceCode = "WA", CountryRegionCode = "US" };
            var address2 = new Entities.Address { AddressLine1 = "3207 S Grady Way", PostalCode = "98055", City = "Renton", StateProvinceCode = "WA", CountryRegionCode = "US" };
            var addresses = new List<Entities.Address>
            {
                address1, address2
            };
            var mockSet = GetQueryableMockDbSet(addresses);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Address>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Address>(mockContext.Object);

            //Act
            var spec = new GetAddressSpecification(
                "2251 Elliot Avenue",
                null,
                "98104",
                "Seattle",
                "WA",
                "US"
            );
            var address = await repository.GetBySpecAsync(spec);

            //Assert
            address.Should().BeEquivalentTo(address1);
        }

        [Fact]
        public async void ListAllAsync_ReturnsObjects()
        {
            //Arrange
            var customers = new List<Entities.Customer>
            {
                new Entities.StoreCustomer { Id = 1, Name = "A Bike Store", AccountNumber = "AW00000001" },
                new Entities.StoreCustomer { Id = 2, Name = "Progressive Sports", AccountNumber = "AW00000002" }
            };
            var mockSet = GetQueryableMockDbSet(customers);

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
            var address1 = new Entities.Address { AddressLine1 = "2251 Elliot Avenue", PostalCode = "98104", City = "Seattle", StateProvinceCode = "WA", CountryRegionCode = "US" };
            var address2 = new Entities.Address { AddressLine1 = "3207 S Grady Way", PostalCode = "98055", City = "Renton", StateProvinceCode = "WA", CountryRegionCode = "US" };
            var addresses = new List<Entities.Address>
            {
                address1, address2
            };
            var mockSet = GetQueryableMockDbSet(addresses);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Address>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Address>(mockContext.Object);

            //Act
            var spec = new GetAddressesByPostalCodeSpecification("98104");
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(1);
            list[0].Should().BeEquivalentTo(address1);
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
            var mockSet = GetQueryableMockDbSet(customers);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            var spec = new GetCustomersAccountNumberSpecification();
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(2);
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
            Func<Task> func = async () => await repository.ListAsync<string>(spec);

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
            var mockSet = GetQueryableMockDbSet(customers);

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
            var mockSet = GetQueryableMockDbSet(customers);

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
            var mockSet = GetQueryableMockDbSet(customers);

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
            var customers = new List<Entities.Customer>
            {
                customer1,
                customer2
            };
            var mockSet = GetQueryableMockDbSet(customers);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

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
            var customers = new List<Entities.Customer>
            {
                customer1,
                customer2
            };
            var mockSet = GetQueryableMockDbSet(customers);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            await repository.DeleteRangeAsync(customers);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}