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
using AW.SharedKernel.UnitTesting;
using AutoFixture.Xunit2;
using AW.Services.SharedKernel.EF6;

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

        [Theory, OmitOnRecursion]
        public async Task GetByIdAsync_ReturnsObject(
            [Frozen] Mock<DbSet<Core.Entities.Product>> mockSet,
            [Frozen] Mock<AWContext> mockContext,
            Core.Entities.Product product
        )
        {
            //Arrange
            mockSet.Setup(x => x.FindAsync(
                It.IsAny<CancellationToken>(),
                It.IsAny<int>()
            ))
            .ReturnsAsync(product);

            mockContext.Setup(x => x.Set<Core.Entities.Product>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.Product>(mockContext.Object);

            //Act
            var result = await repository.GetByIdAsync(1);

            //Assert
            result.ProductNumber.Should().Be(product.ProductNumber);
        }

        [Theory, OmitOnRecursion]
        public async Task GetBySpecAsync_ReturnsObject(
            [Frozen] Mock<AWContext> mockContext,
            List<Core.Entities.Product> products
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(products);

            mockContext.Setup(x => x.Set<Core.Entities.Product>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.Product>(mockContext.Object);

            //Act
            var spec = new GetProductByProductNumberSpecification(products[0].ProductNumber);
            var product = await repository.GetBySpecAsync(spec);

            //Assert
            product.ProductNumber.Should().Be(products[0].ProductNumber);
        }

        [Theory, OmitOnRecursion]
        public async Task ListAllAsync_ReturnsObjects(
            List<Core.Entities.Product> products
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(products);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.Product>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.Product>(mockContext.Object);

            //Act
            var list = await repository.ListAsync();

            //Assert
            list.Count.Should().Be(products.Count);
        }

        [Theory, OmitOnRecursion]
        public async Task ListAsync_ReturnsObjects(
            List<Core.Entities.Product> products
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(products);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.Product>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.Product>(mockContext.Object);

            //Act
            var spec = new GetProductsByColorSpecification(products[0].Color);
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(1);
            list[0].Should().BeEquivalentTo(products[0]);
        }

        [Theory, OmitOnRecursion]
        public async Task ListAsync_WithResultSpec_ReturnsObjects(
            List<Core.Entities.Product> products
        )
        {
            //Arrange
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
            list[0].Should().Be(products[0].Name);
            list[1].Should().Be(products[1].Name);
            list[2].Should().Be(products[2].Name);
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

        [Theory, OmitOnRecursion]
        public async Task CountAsync_ReturnsCount(
            List<Core.Entities.Product> products
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(products);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.Product>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.Product>(mockContext.Object);

            //Act
            var spec = new GetProductsByColorSpecification(products[0].Color);
            var count = await repository.CountAsync(spec);

            //Assert
            count.Should().Be(1);
        }

        [Theory, OmitOnRecursion]
        public async Task AddAsync_SavesObject(
            List<Core.Entities.Product> products,
            Core.Entities.Product newProduct
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(products);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.Product>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.Product>(mockContext.Object);

            //Act
            var savedProduct = await repository.AddAsync(newProduct);

            //Assert
            mockSet.Verify(x => x.Add(It.IsAny<Core.Entities.Product>()));
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
            newProduct.Should().BeEquivalentTo(savedProduct);
        }

        [Theory, OmitOnRecursion]
        public async Task UpdateAsync_SavesObject(
            List<Core.Entities.Product> products
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(products);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.Product>())
                .Returns(mockSet.Object);
            mockContext.Setup(x => x.SetModified(It.IsAny<Core.Entities.Product>()));
            var repository = new EfRepository<Core.Entities.Product>(mockContext.Object);

            //Act
            await repository.UpdateAsync(products[0]);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Theory, OmitOnRecursion]
        public async Task DeleteAsync_ReturnsObject(
            List<Core.Entities.Product> products
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(products);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.Product>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.Product>(mockContext.Object);

            //Act
            await repository.DeleteAsync(products[0]);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Theory, OmitOnRecursion]
        public async Task DeleteRangeAsync_ReturnsObject(
            List<Core.Entities.Product> products
        )
        {
            //Arrange
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