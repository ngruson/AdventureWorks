using Ardalis.Specification;
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
using AW.SharedKernel.UnitTesting.EF6;
using AW.Services.Product.Core.Specifications;

namespace AW.Services.Product.Infrastructure.EF6.UnitTests
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
            var mockSet = new Mock<DbSet<Core.Entities.Product>>();
            mockSet.Setup(x => x.FindAsync(
                It.IsAny<CancellationToken>(),
                It.IsAny<int>()
            ))
            .ReturnsAsync(
                new Core.Entities.Product { Id = 1, ProductNumber = "AR-5381" }
            );

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.Product>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.Product>(mockContext.Object);

            //Act
            var product = await repository.GetByIdAsync(1);

            //Assert
            product.ProductNumber.Should().Be("AR-5381");
        }

        [Fact]
        public async void GetBySpecAsync_ReturnsObject()
        {
            //Arrange
            var products = new List<Core.Entities.Product>
            {
                new Core.Entities.Product { Id = 1, ProductNumber = "AR-5381" },
                new Core.Entities.Product { Id = 2, ProductNumber = "BA-8327" }
            };
            var mockSet = GetQueryableMockDbSet(products);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.Product>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.Product>(mockContext.Object);

            //Act
            var spec = new GetProductByProductNumberSpecification("BA-8327");
            var product = await repository.GetBySpecAsync(spec);

            //Assert
            product.ProductNumber.Should().Be("BA-8327");
        }

        [Fact]
        public async void ListAllAsync_ReturnsObjects()
        {
            //Arrange
            var products = new List<Core.Entities.Product>
            {
                new Core.Entities.Product { Id = 1, ProductNumber = "AR-5381" },
                new Core.Entities.Product { Id = 2, ProductNumber = "BA-8327" }
            };
            var mockSet = GetQueryableMockDbSet(products);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.Product>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.Product>(mockContext.Object);

            //Act
            var list = await repository.ListAsync();

            //Assert
            list.Count.Should().Be(2);
        }

        [Fact]
        public async void ListAsync_ReturnsObjects()
        {
            //Arrange
            var products = new List<Core.Entities.Product>
            {
                new Core.Entities.Product { Id = 1, ProductNumber = "CA-5965", Color = "Black" },
                new Core.Entities.Product { Id = 2, ProductNumber = "CA-6738", Color = "Black" },
                new Core.Entities.Product { Id = 3, ProductNumber = "CB-2903", Color = "Silver" }
            };
            var mockSet = GetQueryableMockDbSet(products);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.Product>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.Product>(mockContext.Object);

            //Act
            var spec = new GetProductsByColorSpecification("Black");
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(2);
        }

        [Fact]
        public async void ListAsync_WithResultSpec_ReturnsObjects()
        {
            //Arrange
            var products = new List<Core.Entities.Product>
            {
                new Core.Entities.Product { Id = 1, ProductNumber = "AR-5381", Name = "Adjustable Race" },
                new Core.Entities.Product { Id = 2, ProductNumber = "BA-8327", Name = "Bearing Ball" },
                new Core.Entities.Product { Id = 3, ProductNumber = "BE-2349", Name = "BB Ball Bearing" }
            };
            var mockSet = GetQueryableMockDbSet(products);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.Product>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.Product>(mockContext.Object);

            //Act
            var spec = new GetProductsNameSpecification();
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(3);
            list[0].Should().Be("Adjustable Race");
            list[1].Should().Be("Bearing Ball");
            list[2].Should().Be("BB Ball Bearing");
        }

        [Fact]
        public void ListAsync_WithNullResultSpec_ThrowsArgumentNullException()
        {
            //Arrange
            var mockContext = new Mock<AWContext>();
            var repository = new EfRepository<Core.Entities.Product>(mockContext.Object);

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
            var repository = new EfRepository<Core.Entities.Product>(mockContext.Object);

            //Act
            var spec = new GetProductsNameWithoutSelectorSpecification();
            Func<Task> func = async () => await repository.ListAsync(spec);

            //Assert
            func.Should().Throw<SelectorNotFoundException>();
        }

        [Fact]
        public async void CountAsync_ReturnsCount()
        {
            //Arrange
            var products = new List<Core.Entities.Product>
            {
                new Core.Entities.Product { Id = 1, ProductNumber = "CA-5965", Color = "Black" },
                new Core.Entities.Product { Id = 2, ProductNumber = "CA-6738", Color = "Black" },
                new Core.Entities.Product { Id = 3, ProductNumber = "CB-2903", Color = "Silver" }
            };
            var mockSet = GetQueryableMockDbSet(products);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.Product>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.Product>(mockContext.Object);

            //Act
            var spec = new GetProductsByColorSpecification("Black");
            var count = await repository.CountAsync(spec);

            //Assert
            count.Should().Be(2);
        }

        [Fact]
        public async void AddAsync_SavesObject()
        {
            //Arrange
            var products = new List<Core.Entities.Product>
            {
                new Core.Entities.Product { Id = 1, ProductNumber = "AR-5381" },
                new Core.Entities.Product { Id = 2, ProductNumber = "BA-8327" }
            };
            var mockSet = GetQueryableMockDbSet(products);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.Product>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.Product>(mockContext.Object);

            //Act
            var newProduct = new Core.Entities.Product { ProductNumber = "BE-2349" };
            var savedProduct = await repository.AddAsync(newProduct);

            //Assert
            mockSet.Verify(x => x.Add(It.IsAny<Core.Entities.Product>()));
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
            newProduct.Should().BeEquivalentTo(savedProduct);
        }

        [Fact]
        public async void UpdateAsync_SavesObject()
        {
            //Arrange
            var products = new List<Core.Entities.Product>
            {
                new Core.Entities.Product { Id = 1, ProductNumber = "AR-5381" },
                new Core.Entities.Product { Id = 2, ProductNumber = "BA-8327" }
            };
            var mockSet = GetQueryableMockDbSet(products);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.Product>())
                .Returns(mockSet.Object);
            mockContext.Setup(x => x.SetModified(It.IsAny<Core.Entities.Product>()));
            var repository = new EfRepository<Core.Entities.Product>(mockContext.Object);

            //Act
            var existingPerson = new Core.Entities.Product { Id = 1, ProductNumber = "AR-5381", Name = "Adjustable Race" };
            await repository.UpdateAsync(existingPerson);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async void DeleteAsync_ReturnsObject()
        {
            //Arrange
            var product1 = new Core.Entities.Product { Id = 1, ProductNumber = "AR-5381" };
            var product2 = new Core.Entities.Product { Id = 2, ProductNumber = "BA-8327" };
            var products = new List<Core.Entities.Product>
            {
                product1,
                product2
            };
            var mockSet = GetQueryableMockDbSet(products);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.Product>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.Product>(mockContext.Object);

            //Act
            await repository.DeleteAsync(product1);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async void DeleteRangeAsync_ReturnsObject()
        {
            //Arrange
            var product1 = new Core.Entities.Product { Id = 1, ProductNumber = "AR-5381" };
            var product2 = new Core.Entities.Product { Id = 2, ProductNumber = "BA-8327" };
            var products = new List<Core.Entities.Product>
            {
                product1,
                product2
            };
            var mockSet = GetQueryableMockDbSet(products);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.Product>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.Product>(mockContext.Object);

            //Act
            await repository.DeleteRangeAsync(products);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}