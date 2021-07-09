using Ardalis.Specification;
using AW.Services.SalesOrder.Core.Specifications;
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

namespace AW.Services.SalesOrder.Infrastructure.EFCore.UnitTests
{
    public class EfRepositoryUnitTests
    {
        [Fact]
        public async void GetByIdAsync_ReturnsObject()
        {
            //Arrange
            var mockSet = new Mock<DbSet<Core.Entities.SalesOrder>>();
            mockSet.Setup(x => x.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(
                    new Core.Entities.SalesOrder { Id = 1, SalesOrderNumber = "SO43659" }
                );

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            var salesOrder = await repository.GetByIdAsync(1);

            //Assert
            salesOrder.SalesOrderNumber.Should().Be("SO43659");
        }

        [Fact]
        public async void GetBySpecAsync_ReturnsObject()
        {
            //Arrange
            var salesOrders = new List<Core.Entities.SalesOrder>
            {
                new Core.Entities.SalesOrder { Id = 1, SalesOrderNumber = "SO43659" },
                new Core.Entities.SalesOrder { Id = 2, SalesOrderNumber = "SO43660" }
            };
            var mockSet = salesOrders.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

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
            var salesOrders = new List<Core.Entities.SalesOrder>
            {
                new Core.Entities.SalesOrder { Id = 1, SalesOrderNumber = "SO43659" },
                new Core.Entities.SalesOrder { Id = 2, SalesOrderNumber = "SO43660" }
            };
            var mockSet = salesOrders.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            var list = await repository.ListAsync();

            //Assert
            list.Count.Should().Be(2);
        }

        [Fact]
        public async void ListAsync_ReturnsObjects()
        {
            //Arrange
            var salesOrders = new List<Core.Entities.SalesOrder>
            {
                new Core.Entities.SalesOrder { Id = 1, SalesOrderNumber = "SO43659", CustomerNumber = "AW00029825" },
                new Core.Entities.SalesOrder { Id = 2, SalesOrderNumber = "SO43660", CustomerNumber = "AW00029672" },
                new Core.Entities.SalesOrder { Id = 1, SalesOrderNumber = "SO44305", CustomerNumber = "AW00029825" }
            };
            var mockSet = salesOrders.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            var spec = new GetSalesOrdersForCustomerSpecification("AW00029825");
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(2);
        }

        [Fact]
        public async void ListAsync_WithResultSpec_ReturnsObjects()
        {
            //Arrange
            var salesOrders = new List<Core.Entities.SalesOrder>
            {
                new Core.Entities.SalesOrder { Id = 1, SalesOrderNumber = "SO43659" },
                new Core.Entities.SalesOrder { Id = 2, SalesOrderNumber = "SO43660" }
            };
            var mockSet = salesOrders.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

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
            Func<Task> func = async () => await repository.ListAsync(spec);

            //Assert
            func.Should().Throw<SelectorNotFoundException>();
        }

        [Fact]
        public async void CountAsync_ReturnsCount()
        {
            //Arrange
            var salesOrders = new List<Core.Entities.SalesOrder>
            {
                new Core.Entities.SalesOrder { Id = 1, SalesOrderNumber = "SO43659", CustomerNumber = "AW00029825" },
                new Core.Entities.SalesOrder { Id = 2, SalesOrderNumber = "SO43660", CustomerNumber = "AW00029672" },
                new Core.Entities.SalesOrder { Id = 1, SalesOrderNumber = "SO44305", CustomerNumber = "AW00029825" }
            };
            var mockSet = salesOrders.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

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
            var salesOrders = new List<Core.Entities.SalesOrder>
            {
                new Core.Entities.SalesOrder { Id = 1, SalesOrderNumber = "SO43659" },
                new Core.Entities.SalesOrder { Id = 2, SalesOrderNumber = "SO43660" }
            };
            var mockSet = salesOrders.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            var newSalesOrder = new Core.Entities.SalesOrder { SalesOrderNumber = "SO43661" };
            var savedSalesOrder = await repository.AddAsync(newSalesOrder);

            //Assert
            mockSet.Verify(x => x.Add(It.IsAny<Core.Entities.SalesOrder>()));
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
            newSalesOrder.Should().BeEquivalentTo(savedSalesOrder);
        }

        [Fact]
        public async void UpdateAsync_SavesObject()
        {
            //Arrange
            var salesOrders = new List<Core.Entities.SalesOrder>
            {
                new Core.Entities.SalesOrder { Id = 1, SalesOrderNumber = "SO43659" },
                new Core.Entities.SalesOrder { Id = 2, SalesOrderNumber = "SO43660" }
            };
            var mockSet = salesOrders.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            mockContext.Setup(x => x.SetModified(It.IsAny<Core.Entities.SalesOrder>()));
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            var existingSalesOrder = new Core.Entities.SalesOrder { Id = 1, SalesOrderNumber = "SO43659" };
            await repository.UpdateAsync(existingSalesOrder);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async void DeleteAsync_ReturnsObject()
        {
            //Arrange
            var salesOrder1 = new Core.Entities.SalesOrder { Id = 1, SalesOrderNumber = "SO43659" };
            var salesOrder2 = new Core.Entities.SalesOrder { Id = 1, SalesOrderNumber = "SO43660" };
            var salesOrders = new List<Core.Entities.SalesOrder>
            {
                salesOrder1,
                salesOrder2
            };
            var mockSet = salesOrders.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.SalesOrder>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesOrder>(mockContext.Object);

            //Act
            await repository.DeleteAsync(salesOrder1);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async void DeleteRangeAsync_ReturnsObject()
        {
            //Arrange
            var salesOrder1 = new Core.Entities.SalesOrder { Id = 1, SalesOrderNumber = "SO43659" };
            var salesOrder2 = new Core.Entities.SalesOrder { Id = 1, SalesOrderNumber = "SO43660" };
            var salesOrders = new List<Core.Entities.SalesOrder>
            {
                salesOrder1,
                salesOrder2
            };
            var mockSet = salesOrders.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
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