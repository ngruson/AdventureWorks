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
using Ardalis.Specification;
using AW.SharedKernel.UnitTesting.EF6;
using AW.Services.SalesOrder.Core.Specifications;
using AW.SharedKernel.UnitTesting;
using AutoFixture.Xunit2;
using AW.Services.SharedKernel.EF6;

namespace AW.Services.SalesOrder.Infrastructure.EF6.UnitTests
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

        [Theory, OmitOnRecursion]
        public async Task GetByIdAsync_ReturnsObject(
            Core.Entities.SalesOrder salesOrder,
            [Frozen] Mock<DbSet<Core.Entities.SalesOrder>> mockSet,
            [Frozen] Mock<AWContext> mockContext
        )
        {
            //Arrange
            mockSet.Setup(x => x.FindAsync(
                It.IsAny<CancellationToken>(),
                It.IsAny<int>()
            ))
            .ReturnsAsync(salesOrder);

            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            var result = await repository.GetByIdAsync(1);

            //Assert
            result.SalesOrderNumber.Should().Be(salesOrder.SalesOrderNumber);
        }

        [Theory, OmitOnRecursion]
        public async Task GetBySpecAsync_ReturnsObject(
            [Frozen] Mock<AWContext> mockContext,
            List<Core.Entities.SalesOrder> salesOrders
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(salesOrders);

            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            var spec = new GetSalesOrderSpecification(salesOrders[0].SalesOrderNumber);
            var result = await repository.GetBySpecAsync(spec);

            //Assert
            result.SalesOrderNumber.Should().Be(salesOrders[0].SalesOrderNumber);
        }

        [Theory, OmitOnRecursion]
        public async Task ListAllAsync_ReturnsObjects(
            [Frozen] Mock<AWContext> mockContext,
            List<Core.Entities.SalesOrder> salesOrders
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(salesOrders);

            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            var list = await repository.ListAsync();

            //Assert
            list.Count.Should().Be(salesOrders.Count);
        }

        [Theory, OmitOnRecursion]
        public async Task ListAsync_ReturnsObjects(
            [Frozen] Mock<AWContext> mockContext,
            List<Core.Entities.SalesOrder> salesOrders
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(salesOrders);

            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            var spec = new GetSalesOrdersForCustomerSpecification(salesOrders[0].CustomerNumber);
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(1);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].SalesOrderNumber.Should().Be(salesOrders[i].SalesOrderNumber);
            }
        }

        [Theory, OmitOnRecursion]
        public async Task ListAsync_WithResultSpec_ReturnsObjects(
            [Frozen] Mock<AWContext> mockContext,
            List<Core.Entities.SalesOrder> salesOrders
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(salesOrders);

            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            var spec = new GetSalesOrdersSalesOrderNumberSpecification();
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(salesOrders.Count);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Should().Be(salesOrders[i].SalesOrderNumber);
            }
        }

        [Fact]
        public void ListAsync_WithNullResultSpec_ThrowsArgumentNullException()
        {
            //Arrange
            var mockContext = new Mock<AWContext>();
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

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
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            var spec = new GetSalesOrdersWithoutSelectorSpecification();
            Func<Task> func = async () => await repository.ListAsync<string>(spec);

            //Assert
            func.Should().Throw<SelectorNotFoundException>();
        }

        [Theory, OmitOnRecursion]
        public async Task CountAsync_ReturnsCount(
            [Frozen] Mock<AWContext> mockContext,
            List<Core.Entities.SalesOrder> salesOrders
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(salesOrders);

            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            var spec = new GetSalesOrdersForCustomerSpecification(salesOrders[0].CustomerNumber);
            var count = await repository.CountAsync(spec);

            //Assert
            count.Should().Be(1);
        }

        [Theory, OmitOnRecursion]
        public async Task AddAsync_SavesObject(
            [Frozen] Mock<AWContext> mockContext,
            List<Core.Entities.SalesOrder> salesOrders,
            Core.Entities.SalesOrder newSalesOrder
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(salesOrders);

            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            var savedSalesOrder = await repository.AddAsync(newSalesOrder);

            //Assert
            mockSet.Verify(x => x.Add(It.IsAny<Core.Entities.SalesOrder>()));
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
            newSalesOrder.Should().BeEquivalentTo(savedSalesOrder);
        }

        [Theory, OmitOnRecursion]
        public async Task UpdateAsync_SavesObject(
            [Frozen] Mock<AWContext> mockContext,
            List<Core.Entities.SalesOrder> salesOrders
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(salesOrders);

            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            mockContext.Setup(x => x.SetModified(It.IsAny<Core.Entities.SalesOrder>()));
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            await repository.UpdateAsync(salesOrders[0]);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Theory, OmitOnRecursion]
        public async Task DeleteAsync_ReturnsObject(
            [Frozen] Mock<AWContext> mockContext,
            List<Core.Entities.SalesOrder> salesOrders
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(salesOrders);

            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            await repository.DeleteAsync(salesOrders[0]);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Theory, OmitOnRecursion]
        public async Task DeleteRangeAsync_ReturnsObject(
            [Frozen] Mock<AWContext> mockContext,
            List<Core.Entities.SalesOrder> salesOrders
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(salesOrders);

            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            await repository.DeleteRangeAsync(salesOrders);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}