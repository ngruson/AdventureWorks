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
using AW.SharedKernel.UnitTesting;
using AutoFixture.Xunit2;
using AW.Services.SharedKernel.EF6;

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

        [Theory]
        [AutoMoqData]
        public async void GetByIdAsync_ReturnsObject(
            [Frozen] Mock<DbSet<Entities.StoreCustomer>> mockSet,
            [Frozen] Mock<AWContext> mockContext,
            Entities.StoreCustomer customer
        )
        {
            //Arrange
            mockSet.Setup(x => x.FindAsync(
                It.IsAny<CancellationToken>(),
                It.IsAny<int>()
            ))
            .ReturnsAsync(customer);

            mockContext.Setup(x => x.Set<Entities.StoreCustomer>())
                .Returns(mockSet.Object);

            //Act
            var repository = new EfRepository<Entities.StoreCustomer>(mockContext.Object);
            var result = await repository.GetByIdAsync(customer.Id);

            //Assert
            result.Name.Should().Be(customer.Name);
        }

        [Theory]
        [AutoMoqData]
        public async void GetBySpecAsync_ReturnsObject(
            List<Entities.StoreCustomer> customers,
            [Frozen] Mock<AWContext> mockContext
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(customers.ToList<Entities.Customer>());

            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            var spec = new GetCustomerByIdSpecification(customers[0].Id);
            var result = await repository.GetBySpecAsync(spec);

            //Assert
            result.Should().BeEquivalentTo(customers[0]);
        }

        [Theory]
        [AutoMoqData]
        public async void ListAllAsync_ReturnsObjects(
            List<Entities.Customer> customers
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(customers);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            var list = await repository.ListAsync();

            //Assert
            list.Count.Should().Be(3);
        }

        [Theory]
        [AutoMoqData]
        public async void ListAsync_ReturnsObjects(
            List<Entities.Address> addresses
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(addresses);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Address>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Address>(mockContext.Object);

            //Act
            var spec = new GetAddressesByPostalCodeSpecification(
                addresses[0].PostalCode
            );
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(1);
            list[0].Should().BeEquivalentTo(addresses[0]);
        }

        [Theory]
        [AutoMoqData]
        public async void ListAsync_WithResultSpec_ReturnsObjects(
            List<Entities.Customer> customers
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(customers);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            var spec = new GetCustomersAccountNumberSpecification();
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(3);
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

        [Theory]
        [AutoMoqData]
        public async void CountAsync_ReturnsCount(
            List<Entities.StoreCustomer> customers
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(customers.Cast<Entities.Customer>().ToList());

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            var spec = new CountCustomersSpecification(Entities.CustomerType.Store, null, null);
            var count = await repository.CountAsync(spec);

            //Assert
            count.Should().Be(3);
        }

        [Theory]
        [AutoMoqData]
        public async void AddAsync_SavesObject(
            List<Entities.StoreCustomer> customers,
            Entities.StoreCustomer newCustomer
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(customers.Cast<Entities.Customer>().ToList());

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            var savedCustomer = await repository.AddAsync(newCustomer);

            //Assert
            mockSet.Verify(x => x.Add(It.IsAny<Entities.Customer>()));
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
            newCustomer.Should().BeEquivalentTo(savedCustomer);
        }

        [Theory]
        [AutoMoqData]
        public async void UpdateAsync_SavesObject(
            List<Entities.StoreCustomer> customers
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(customers.Cast<Entities.Customer>().ToList());

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            mockContext.Setup(x => x.SetModified(It.IsAny<Entities.Customer>()));
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            await repository.UpdateAsync(customers[0]);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Theory]
        [AutoMoqData]
        public async void DeleteAsync_ReturnsObject(
            List<Entities.StoreCustomer> customers
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(customers.Cast<Entities.Customer>().ToList());

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            await repository.DeleteAsync(customers[0]);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Theory]
        [AutoMoqData]
        public async void DeleteRangeAsync_ReturnsObject(
            List<Entities.StoreCustomer> customers
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(customers.Cast<Entities.Customer>().ToList());

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