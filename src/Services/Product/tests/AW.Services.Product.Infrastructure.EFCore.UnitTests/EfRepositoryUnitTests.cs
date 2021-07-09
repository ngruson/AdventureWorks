using AW.Services.Product.Infrastructure.EFCore;
using AW.Services.Product.Core.Specifications;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace AW.Services.Product.Persistence.EFCore.UnitTests
{
    public class EfRepositoryUnitTests
    {
        [Fact]
        public async void GetByIdAsync_ReturnsObject()
        {
            //Arrange
            var mockSet = new Mock<DbSet<Core.Entities.Product>>();
            mockSet.Setup(x => x.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()))
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
        public async void ListAllAsync_ReturnsObjects()
        {
            //Arrange
            var products = new List<Core.Entities.Product>
            {
                new Core.Entities.Product { Id = 1, ProductNumber = "AR-5381" },
                new Core.Entities.Product { Id = 2, ProductNumber = "BA-8327" }
            };
            var mockSet = products.AsQueryable().BuildMockDbSet();

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
            var mockSet = products.AsQueryable().BuildMockDbSet();

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
            var mockSet = products.AsQueryable().BuildMockDbSet();

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
        public async void CountAsync_ReturnsCount()
        {
            //Arrange
            var products = new List<Core.Entities.Product>
            {
                new Core.Entities.Product { Id = 1, ProductNumber = "CA-5965", Color = "Black" },
                new Core.Entities.Product { Id = 2, ProductNumber = "CA-6738", Color = "Black" },
                new Core.Entities.Product { Id = 3, ProductNumber = "CB-2903", Color = "Silver" }
            };
            var mockSet = products.AsQueryable().BuildMockDbSet();

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
            var mockSet = products.AsQueryable().BuildMockDbSet();

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
        public async void GetBySpecAsync_ReturnsObject()
        {
            //Arrange
            var products = new List<Core.Entities.Product>
            {
                new Core.Entities.Product { Id = 1, ProductNumber = "AR-5381" },
                new Core.Entities.Product { Id = 2, ProductNumber = "BA-8327" }
            };
            var mockSet = products.AsQueryable().BuildMockDbSet();

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
    }
}