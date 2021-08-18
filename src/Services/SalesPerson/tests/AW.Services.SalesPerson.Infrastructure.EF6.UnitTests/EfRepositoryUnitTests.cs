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
using AW.Services.SalesPerson.Core.Specifications;
using Ardalis.Specification;
using AW.SharedKernel.UnitTesting.EF6;
using AW.SharedKernel.Extensions;
using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;
using AW.Services.SharedKernel.EF6;

namespace AW.Services.SalesPerson.Infrastructure.EF6.UnitTests
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
            Core.Entities.SalesPerson salesPerson,
            [Frozen] Mock<DbSet<Core.Entities.SalesPerson>> mockSet,
            [Frozen] Mock<AWContext> mockContext
        )
        {
            //Arrange
            mockSet.Setup(x => x.FindAsync(
                It.IsAny<CancellationToken>(),
                It.IsAny<int>()
            ))
            .ReturnsAsync(salesPerson);

            mockContext.Setup(x => x.Set<Core.Entities.SalesPerson>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesPerson>(mockContext.Object);

            //Act
            var result = await repository.GetByIdAsync(1);

            //Assert
            result.FullName().Should().Be(salesPerson.FullName());
        }

        [Theory, OmitOnRecursion]
        public async Task GetBySpecAsync_ReturnsObject(
            [Frozen] Mock<AWContext> mockContext,
            List<Core.Entities.SalesPerson> salesPersons
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(salesPersons);

            mockContext.Setup(x => x.Set<Core.Entities.SalesPerson>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesPerson>(mockContext.Object);

            //Act
            var spec = new GetSalesPersonSpecification(
                salesPersons[0].FirstName,
                salesPersons[0].MiddleName,
                salesPersons[0].LastName
            );

            var salesPerson = await repository.GetBySpecAsync(spec);

            //Assert
            salesPerson.FullName().Should().Be(salesPersons[0].FullName());
        }

        [Theory, OmitOnRecursion]
        public async Task ListAsync_WithoutSpec_ReturnsObjects(
            [Frozen] Mock<AWContext> mockContext,
            List<Core.Entities.SalesPerson> salesPersons
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(salesPersons);

            mockContext.Setup(x => x.Set<Core.Entities.SalesPerson>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesPerson>(mockContext.Object);

            //Act
            var list = await repository.ListAsync();

            //Assert
            list.Count.Should().Be(salesPersons.Count);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].FullName().Should().Be(salesPersons[i].FullName());
            }
        }

        [Theory, OmitOnRecursion]
        public async Task ListAsync_WithSpec_ReturnsObjects(
            [Frozen] Mock<AWContext> mockContext,
            List<Core.Entities.SalesPerson> salesPersons
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(salesPersons);

            mockContext.Setup(x => x.Set<Core.Entities.SalesPerson>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesPerson>(mockContext.Object);

            //Act
            var spec = new GetSalesPersonsForTerritorySpecification(salesPersons[0].Territory);
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(1);
            list[0].FullName().Should().Be(salesPersons[0].FullName());
        }

        [Theory, OmitOnRecursion]
        public async Task ListAsync_WithResultSpec_ReturnsObjects(
            [Frozen] Mock<AWContext> mockContext,
            List<Core.Entities.SalesPerson> salesPersons
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(salesPersons);

            mockContext.Setup(x => x.Set<Core.Entities.SalesPerson>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesPerson>(mockContext.Object);

            //Act
            var spec = new GetSalesPersonsFullNameSpecification();
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(salesPersons.Count);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Should().Be(salesPersons[i].FullName());
            }
        }

        [Fact]
        public void ListAsync_WithNullResultSpec_ThrowsArgumentNullException()
        {
            //Arrange
            var mockContext = new Mock<AWContext>();
            var repository = new EfRepository<Core.Entities.SalesPerson>(mockContext.Object);

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
            var repository = new EfRepository<Core.Entities.SalesPerson>(mockContext.Object);

            //Act
            var spec = new GetSalesPersonsWithoutSelectorSpecification();
            Func<Task> func = async () => await repository.ListAsync<string>(spec);

            //Assert
            func.Should().Throw<SelectorNotFoundException>();
        }

        [Theory, OmitOnRecursion]
        public async Task CountAsync_ReturnsCount(
            [Frozen] Mock<AWContext> mockContext,
            List<Core.Entities.SalesPerson> salesPersons
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(salesPersons);

            mockContext.Setup(x => x.Set<Core.Entities.SalesPerson>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesPerson>(mockContext.Object);

            //Act
            var spec = new GetSalesPersonsForTerritorySpecification(salesPersons[0].Territory);
            var count = await repository.CountAsync(spec);

            //Assert
            count.Should().Be(1);
        }

        [Theory, OmitOnRecursion]
        public async Task AddAsync_SavesObject(
            [Frozen] Mock<AWContext> mockContext,
            List<Core.Entities.SalesPerson> salesPersons,
            Core.Entities.SalesPerson newSalesPerson
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(salesPersons);

            mockContext.Setup(x => x.Set<Core.Entities.SalesPerson>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesPerson>(mockContext.Object);

            //Act
            var savedSalesPerson = await repository.AddAsync(newSalesPerson);

            //Assert
            mockSet.Verify(x => x.Add(It.IsAny<Core.Entities.SalesPerson>()));
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
            newSalesPerson.Should().BeEquivalentTo(savedSalesPerson);
        }

        [Theory, OmitOnRecursion]
        public async Task UpdateAsync_SavesObject(
            [Frozen] Mock<AWContext> mockContext,
            List<Core.Entities.SalesPerson> salesPersons
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(salesPersons);

            mockContext.Setup(x => x.Set<Core.Entities.SalesPerson>())
                .Returns(mockSet.Object);
            mockContext.Setup(x => x.SetModified(It.IsAny<Core.Entities.SalesPerson>()));
            var repository = new EfRepository<Core.Entities.SalesPerson>(mockContext.Object);

            //Act
            await repository.UpdateAsync(salesPersons[0]);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Theory, OmitOnRecursion]
        public async Task DeleteAsync_ReturnsObject(
            [Frozen] Mock<AWContext> mockContext,
            List<Core.Entities.SalesPerson> salesPersons
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(salesPersons);

            mockContext.Setup(x => x.Set<Core.Entities.SalesPerson>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesPerson>(mockContext.Object);

            //Act
            await repository.DeleteAsync(salesPersons[0]);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Theory, OmitOnRecursion]
        public async Task DeleteRangeAsync_ReturnsObject(
            [Frozen] Mock<AWContext> mockContext,
            List<Core.Entities.SalesPerson> salesPersons
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(salesPersons);

            mockContext.Setup(x => x.Set<Core.Entities.SalesPerson>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesPerson>(mockContext.Object);

            //Act
            await repository.DeleteRangeAsync(salesPersons);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}