using AutoFixture.Xunit2;
using AW.Services.Product.Core.Specifications;
using AW.Services.SharedKernel.EFCore;
using AW.SharedKernel.UnitTesting;
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

namespace AW.Services.Product.Infrastructure.EFCore.UnitTests
{
    public class EfRepositoryUnitTests
    {
        [Theory, OmitOnRecursion]
        public async Task GetByIdAsync_ReturnsObject(
            [Frozen] Mock<DbSet<Core.Entities.UnitMeasure>> mockSet,
            [Frozen] Mock<AWContext> mockContext,
            Core.Entities.UnitMeasure unitMeasure
        )
        {
            //Arrange
            mockSet.Setup(x => x.FindAsync(
                It.IsAny<object[]>(), 
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(unitMeasure);

            mockContext.Setup(x => x.Set<Core.Entities.UnitMeasure>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.UnitMeasure>(mockContext.Object);

            //Act
            var result = await repository.GetByIdAsync(unitMeasure.UnitMeasureCode);

            //Assert
            result.Should().BeEquivalentTo(unitMeasure);
        }

        [Theory, OmitOnRecursion]
        public async Task ListAllAsync_ReturnsObjects(
            List<Core.Entities.Product> products
        )
        {
            //Arrange
            var mockSet = products.AsQueryable().BuildMockDbSet();

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
            Tuple<string, string>[] data
        )
        {
            //Arrange
            var products = data.Select(data =>
                new Core.Entities.Product(
                    data.Item1, data.Item2
                )
            )
            .ToList();

            var mockSet = products.AsQueryable().BuildMockDbSet();

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
            list[0].Should().Be(products[0].Name);
            list[1].Should().Be(products[1].Name);
            list[2].Should().Be(products[2].Name);
        }

        [Theory, OmitOnRecursion]
        public async Task CountAsync_ReturnsCount(
            Tuple<string, string>[] data
        )
        {
            //Arrange
            var products = data.Select(data =>
                new Core.Entities.Product(
                    data.Item1, data.Item2
                )
            )
            .ToList();

            var mockSet = products.AsQueryable().BuildMockDbSet();

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
            List<Core.Entities.Product> products
        )
        {
            //Arrange
            var mockSet = products.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.Product>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.Product>(mockContext.Object);

            //Act
            var newProduct = new Core.Entities.Product( 
                products[0].ProductNumber,
                products[0].Color
            );
            var savedProduct = await repository.AddAsync(newProduct);

            //Assert
            mockSet.Verify(x => x.Add(It.IsAny<Core.Entities.Product>()));
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
            newProduct.Should().BeEquivalentTo(savedProduct);
        }

        [Theory, OmitOnRecursion]
        public async Task GetBySpecAsync_ReturnsObject(
            List<Core.Entities.Product> products
        )
        {
            //Arrange
            var mockSet = products.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Core.Entities.Product>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.Product>(mockContext.Object);

            //Act
            var spec = new GetProductByProductNumberSpecification(products[0].ProductNumber);
            var product = await repository.GetBySpecAsync(spec);

            //Assert
            product.ProductNumber.Should().Be(products[0].ProductNumber);
        }
    }
}