using AW.Common.Extensions;
using AW.Common.UnitTesting;
using Domain = AW.Services.SalesPerson.Domain;
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
using AW.Services.SalesPerson.Persistence.EntityFramework;
using AW.Services.SalesPerson.Application.Specifications;
using Ardalis.Specification;

namespace AW.Services.SalesPerson.Persistence.EF.UnitTests
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
            var mockSet = new Mock<DbSet<Domain.SalesPerson>>();
            mockSet.Setup(x => x.FindAsync(
                It.IsAny<CancellationToken>(),
                It.IsAny<int>()
            ))
            .ReturnsAsync(
                new Domain.SalesPerson { Id = 1, FirstName = "Stephen", MiddleName = "Y", LastName = "Jiang" }
            );

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Domain.SalesPerson>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Domain.SalesPerson>(mockContext.Object);

            //Act
            var salesPerson = await repository.GetByIdAsync(1);

            //Assert
            salesPerson.FullName().Should().Be("Stephen Y Jiang");
        }

        [Fact]
        public async void GetBySpecAsync_ReturnsObject()
        {
            //Arrange
            var salesPersons = new List<Domain.SalesPerson>
            {
                new Domain.SalesPerson { Id = 1, FirstName = "Stephen", MiddleName = "Y", LastName = "Jiang" },
                new Domain.SalesPerson { Id = 2, FirstName = "Michael", MiddleName = "G", LastName = "Blythe" }
            };
            var mockSet = GetQueryableMockDbSet(salesPersons);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Domain.SalesPerson>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Domain.SalesPerson>(mockContext.Object);

            //Act
            var spec = new GetSalesPersonSpecification("Stephen", "Y", "Jiang");
            var salesPerson = await repository.GetBySpecAsync(spec);

            //Assert
            salesPerson.FullName().Should().Be("Stephen Y Jiang");
        }

        [Fact]
        public async void ListAsync_WithoutSpec_ReturnsObjects()
        {
            //Arrange
            var salesPersons = new List<Domain.SalesPerson>
            {
                new Domain.SalesPerson { Id = 1, FirstName = "Stephen", MiddleName = "Y", LastName = "Jiang" },
                new Domain.SalesPerson { Id = 2, FirstName = "Michael", MiddleName = "G", LastName = "Blythe" }
            };
            var mockSet = GetQueryableMockDbSet(salesPersons);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Domain.SalesPerson>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Domain.SalesPerson>(mockContext.Object);

            //Act
            var list = await repository.ListAsync();

            //Assert
            list.Count.Should().Be(2);
            list[0].FullName().Should().Be("Stephen Y Jiang");
            list[1].FullName().Should().Be("Michael G Blythe");
        }

        [Fact]
        public async void ListAsync_WithSpec_ReturnsObjects()
        {
            //Arrange
            var salesPersons = new List<Domain.SalesPerson>
            {
                new Domain.SalesPerson { Id = 1, FirstName = "Stephen", MiddleName = "Y", LastName = "Jiang" },
                new Domain.SalesPerson { Id = 2, FirstName = "Linda", MiddleName = "C", LastName = "Mitchell", Territory = "Southwest" },
                new Domain.SalesPerson { Id = 2, FirstName = "Shu", MiddleName = "K", LastName = "Ito", Territory = "Southwest" }
            };
            var mockSet = GetQueryableMockDbSet(salesPersons);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Domain.SalesPerson>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Domain.SalesPerson>(mockContext.Object);

            //Act
            var spec = new GetSalesPersonsForTerritorySpecification("Southwest");
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(2);
            list[0].FullName().Should().Be("Linda C Mitchell");
            list[1].FullName().Should().Be("Shu K Ito");
        }

        [Fact]
        public async void ListAsync_WithResultSpec_ReturnsObjects()
        {
            //Arrange
            var salesPersons = new List<Domain.SalesPerson>
            {
                new Domain.SalesPerson { Id = 1, FirstName = "Stephen", MiddleName = "Y", LastName = "Jiang" },
                new Domain.SalesPerson { Id = 2, FirstName = "Michael", MiddleName = "G", LastName = "Blythe" }
            };
            var mockSet = GetQueryableMockDbSet(salesPersons);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Domain.SalesPerson>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Domain.SalesPerson>(mockContext.Object);

            //Act
            var spec = new GetSalesPersonsFullNameSpecification();
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(2);
            list[0].Should().Be("Stephen Y Jiang");
            list[1].Should().Be("Michael G Blythe");
        }

        [Fact]
        public void ListAsync_WithNullResultSpec_ThrowsArgumentNullException()
        {
            //Arrange
            var mockContext = new Mock<AWContext>();
            var repository = new EfRepository<Domain.SalesPerson>(mockContext.Object);

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
            var repository = new EfRepository<Domain.SalesPerson>(mockContext.Object);

            //Act
            var spec = new GetSalesPersonsWithoutSelectorSpecification();
            Func<Task> func = async () => await repository.ListAsync<string>(spec);

            //Assert
            func.Should().Throw<SelectorNotFoundException>();
        }

        [Fact]
        public async void CountAsync_ReturnsCount()
        {
            //Arrange
            var salesPersons = new List<Domain.SalesPerson>
            {
                new Domain.SalesPerson { Id = 1, FirstName = "Stephen", MiddleName = "Y", LastName = "Jiang" },
                new Domain.SalesPerson { Id = 2, FirstName = "Linda", MiddleName = "C", LastName = "Mitchell", Territory = "Southwest" },
                new Domain.SalesPerson { Id = 2, FirstName = "Shu", MiddleName = "K", LastName = "Ito", Territory = "Southwest" }
            };
            var mockSet = GetQueryableMockDbSet(salesPersons);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Domain.SalesPerson>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Domain.SalesPerson>(mockContext.Object);

            //Act
            var spec = new GetSalesPersonsForTerritorySpecification("Southwest");
            var count = await repository.CountAsync(spec);

            //Assert
            count.Should().Be(2);
        }

        [Fact]
        public async void AddAsync_SavesObject()
        {
            //Arrange
            var salesPersons = new List<Domain.SalesPerson>
            {
                new Domain.SalesPerson { Id = 1, FirstName = "Stephen", MiddleName = "Y", LastName = "Jiang" },
                new Domain.SalesPerson { Id = 2, FirstName = "Michael", MiddleName = "G", LastName = "Blythe" }
            };
            var mockSet = GetQueryableMockDbSet(salesPersons);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Domain.SalesPerson>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Domain.SalesPerson>(mockContext.Object);

            //Act
            var newSalesPerson = new Domain.SalesPerson { FirstName = "Linda", MiddleName = "C", LastName = "Mitchell" };
            var savedSalesPerson = await repository.AddAsync(newSalesPerson);

            //Assert
            mockSet.Verify(x => x.Add(It.IsAny<Domain.SalesPerson>()));
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
            newSalesPerson.Should().BeEquivalentTo(savedSalesPerson);
        }

        [Fact]
        public async void UpdateAsync_SavesObject()
        {
            //Arrange
            var salesPersons = new List<Domain.SalesPerson>
            {
                new Domain.SalesPerson { Id = 1, FirstName = "Stephen", MiddleName = "Y", LastName = "Jiang" },
                new Domain.SalesPerson { Id = 2, FirstName = "Michael", MiddleName = "G", LastName = "Blythe" }
            };
            var mockSet = GetQueryableMockDbSet(salesPersons);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Domain.SalesPerson>())
                .Returns(mockSet.Object);
            mockContext.Setup(x => x.SetModified(It.IsAny<Domain.SalesPerson>()));
            var repository = new EfRepository<Domain.SalesPerson>(mockContext.Object);

            //Act
            var existingSalesPerson = new Domain.SalesPerson { Id = 1, FirstName = "Stephen", MiddleName = "Y", LastName = "Jiang" };
            await repository.UpdateAsync(existingSalesPerson);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async void DeleteAsync_ReturnsObject()
        {
            //Arrange
            var salesPerson1 = new Domain.SalesPerson { Id = 1, FirstName = "Stephen", MiddleName = "Y", LastName = "Jiang" };
            var salesPerson2 = new Domain.SalesPerson { Id = 2, FirstName = "Michael", MiddleName = "G", LastName = "Blythe" };
            var salesPersons = new List<Domain.SalesPerson>
            {
                salesPerson1,
                salesPerson2
            };
            var mockSet = GetQueryableMockDbSet(salesPersons);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Domain.SalesPerson>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Domain.SalesPerson>(mockContext.Object);

            //Act
            await repository.DeleteAsync(salesPerson1);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async void DeleteRangeAsync_ReturnsObject()
        {
            //Arrange
            var salesPerson1 = new Domain.SalesPerson { Id = 1, FirstName = "Stephen", MiddleName = "Y", LastName = "Jiang" };
            var salesPerson2 = new Domain.SalesPerson { Id = 2, FirstName = "Michael", MiddleName = "G", LastName = "Blythe" };
            var salesPersons = new List<Domain.SalesPerson>
            {
                salesPerson1,
                salesPerson2
            };
            var mockSet = GetQueryableMockDbSet(salesPersons);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Domain.SalesPerson>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Domain.SalesPerson>(mockContext.Object);

            //Act
            await repository.DeleteRangeAsync(salesPersons);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}