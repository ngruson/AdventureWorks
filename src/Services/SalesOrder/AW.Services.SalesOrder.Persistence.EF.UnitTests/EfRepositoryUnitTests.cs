using AW.Common.UnitTesting;
using Domain = AW.Services.SalesOrder.Domain;
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
using AW.Services.SalesOrder.Persistence.EntityFramework;
using AW.Services.SalesOrder.Application.Specifications;
using Ardalis.Specification;

namespace AW.Services.SalesOrder.Persistence.EF.UnitTests
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
            var mockSet = new Mock<DbSet<Domain.SalesOrder>>();
            mockSet.Setup(x => x.FindAsync(
                It.IsAny<CancellationToken>(),
                It.IsAny<int>()
            ))
            .ReturnsAsync(
                new Domain.SalesOrder { Id = 1, SalesOrderNumber = "SO43659" }
            );

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Domain.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Domain.SalesOrder>(mockContext.Object);

            //Act
            var person = await repository.GetByIdAsync(1);

            //Assert
            person.SalesOrderNumber.Should().Be("SO43659");
        }

        [Fact]
        public async void GetBySpecAsync_ReturnsObject()
        {
            //Arrange
            var salesOrders = new List<Domain.SalesOrder>
            {
                new Domain.SalesOrder { Id = 1, SalesOrderNumber = "SO43659" },
                new Domain.SalesOrder { Id = 2, SalesOrderNumber = "SO43660" }
            };
            var mockSet = GetQueryableMockDbSet(salesOrders);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Domain.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Domain.SalesOrder>(mockContext.Object);

            //Act
            var spec = new GetSalesOrderSpecification("SO43659");
            var salesOrder = await repository.GetBySpecAsync(spec);

            //Assert
            salesOrder.SalesOrderNumber.Should().Be("SO43659");
        }

        [Fact]
        public async void ListAllAsync_ReturnsObjects()
        {
            //Arrange
            var salesOrders = new List<Domain.SalesOrder>
            {
                new Domain.SalesOrder { Id = 1, SalesOrderNumber = "SO43659" },
                new Domain.SalesOrder { Id = 2, SalesOrderNumber = "SO43660" }
            };
            var mockSet = GetQueryableMockDbSet(salesOrders);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Domain.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Domain.SalesOrder>(mockContext.Object);

            //Act
            var list = await repository.ListAsync();

            //Assert
            list.Count.Should().Be(2);
        }

        [Fact]
        public async void ListAsync_ReturnsObjects()
        {
            //Arrange
            var salesOrders = new List<Domain.SalesOrder>
            {
                new Domain.SalesOrder { Id = 1, SalesOrderNumber = "SO43659", CustomerNumber = "AW00029825" },
                new Domain.SalesOrder { Id = 2, SalesOrderNumber = "SO43660", CustomerNumber = "AW00029672" },
                new Domain.SalesOrder { Id = 1, SalesOrderNumber = "SO44305", CustomerNumber = "AW00029825" }
            };
            var mockSet = GetQueryableMockDbSet(salesOrders);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Domain.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Domain.SalesOrder>(mockContext.Object);

            //Act
            var spec = new GetSalesOrdersForCustomerSpecification("AW00029825");
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(2);
            list[0].SalesOrderNumber.Should().Be("SO43659");
            list[1].SalesOrderNumber.Should().Be("SO44305");
        }

        [Fact]
        public async void ListAsync_WithResultSpec_ReturnsObjects()
        {
            //Arrange
            var salesOrders = new List<Domain.SalesOrder>
            {
                new Domain.SalesOrder { Id = 1, SalesOrderNumber = "SO43659" },
                new Domain.SalesOrder { Id = 2, SalesOrderNumber = "SO43660" }
            };
            var mockSet = GetQueryableMockDbSet(salesOrders);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Domain.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Domain.SalesOrder>(mockContext.Object);

            //Act
            var spec = new GetSalesOrdersSalesOrderNumberSpecification();
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(2);
            list[0].Should().Be("SO43659");
            list[1].Should().Be("SO43660");
        }

        [Fact]
        public void ListAsync_WithNullResultSpec_ThrowsArgumentNullException()
        {
            //Arrange
            var mockContext = new Mock<AWContext>();
            var repository = new EfRepository<Domain.SalesOrder>(mockContext.Object);

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
            var repository = new EfRepository<Domain.SalesOrder>(mockContext.Object);

            //Act
            var spec = new GetSalesOrdersWithoutSelectorSpecification();
            Func<Task> func = async () => await repository.ListAsync<string>(spec);

            //Assert
            func.Should().Throw<SelectorNotFoundException>();
        }

        [Fact]
        public async void CountAsync_ReturnsCount()
        {
            //Arrange
            var salesOrders = new List<Domain.SalesOrder>
            {
                new Domain.SalesOrder { Id = 1, SalesOrderNumber = "SO43659", CustomerNumber = "AW00029825" },
                new Domain.SalesOrder { Id = 2, SalesOrderNumber = "SO43660", CustomerNumber = "AW00029672" },
                new Domain.SalesOrder { Id = 1, SalesOrderNumber = "SO44305", CustomerNumber = "AW00029825" }
            };
            var mockSet = GetQueryableMockDbSet(salesOrders);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Domain.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Domain.SalesOrder>(mockContext.Object);

            //Act
            var spec = new GetSalesOrdersForCustomerSpecification("AW00029825");
            var count = await repository.CountAsync(spec);

            //Assert
            count.Should().Be(2);
        }

        [Fact]
        public async void AddAsync_SavesObject()
        {
            //Arrange
            var salesOrders = new List<Domain.SalesOrder>
            {
                new Domain.SalesOrder { Id = 1, SalesOrderNumber = "SO43659" },
                new Domain.SalesOrder { Id = 2, SalesOrderNumber = "SO43660" }
            };
            var mockSet = GetQueryableMockDbSet(salesOrders);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Domain.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Domain.SalesOrder>(mockContext.Object);

            //Act
            var newSalesOrder = new Domain.SalesOrder { SalesOrderNumber = "SO43661" };
            var savedSalesOrder = await repository.AddAsync(newSalesOrder);

            //Assert
            mockSet.Verify(x => x.Add(It.IsAny<Domain.SalesOrder>()));
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
            newSalesOrder.Should().BeEquivalentTo(savedSalesOrder);
        }

        [Fact]
        public async void UpdateAsync_SavesObject()
        {
            //Arrange
            var salesOrders = new List<Domain.SalesOrder>
            {
                new Domain.SalesOrder { Id = 1, SalesOrderNumber = "SO43659" },
                new Domain.SalesOrder { Id = 2, SalesOrderNumber = "SO43660" }
            };
            var mockSet = GetQueryableMockDbSet(salesOrders);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Domain.SalesOrder>())
                .Returns(mockSet.Object);
            mockContext.Setup(x => x.SetModified(It.IsAny<Domain.SalesOrder>()));
            var repository = new EfRepository<Domain.SalesOrder>(mockContext.Object);

            //Act
            var existingSalesOrder = new Domain.SalesOrder { Id = 1, SalesOrderNumber = "SO43659" };
            await repository.UpdateAsync(existingSalesOrder);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async void DeleteAsync_ReturnsObject()
        {
            //Arrange
            var salesOrder1 = new Domain.SalesOrder { Id = 1, SalesOrderNumber = "SO43659" };
            var salesOrder2 = new Domain.SalesOrder { Id = 1, SalesOrderNumber = "SO43660" };
            var salesOrders = new List<Domain.SalesOrder>
            {
                salesOrder1,
                salesOrder2
            };
            var mockSet = GetQueryableMockDbSet(salesOrders);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Domain.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Domain.SalesOrder>(mockContext.Object);

            //Act
            await repository.DeleteAsync(salesOrder1);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async void DeleteRangeAsync_ReturnsObject()
        {
            //Arrange
            var salesOrder1 = new Domain.SalesOrder { Id = 1, SalesOrderNumber = "SO43659" };
            var salesOrder2 = new Domain.SalesOrder { Id = 1, SalesOrderNumber = "SO43660" };
            var salesOrders = new List<Domain.SalesOrder>
            {
                salesOrder1,
                salesOrder2
            };
            var mockSet = GetQueryableMockDbSet(salesOrders);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Domain.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Domain.SalesOrder>(mockContext.Object);

            //Act
            await repository.DeleteRangeAsync(salesOrders);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}