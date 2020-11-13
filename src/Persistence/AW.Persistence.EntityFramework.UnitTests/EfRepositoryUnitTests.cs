using AW.Domain.Person;
using AW.Persistence.EntityFramework.UnitTests.Mocking;
using AW.Persistence.EntityFramework.UnitTests.Specifications;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Xunit;

namespace AW.Persistence.EntityFramework.UnitTests
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
            var mockSet = new Mock<DbSet<Person>>();
            mockSet.Setup(x => x.FindAsync(It.IsAny<int>()))
                .ReturnsAsync(
                    new Person { Id = 1, FirstName = "Ken", MiddleName = "J", LastName = "Sánchez" }
                );

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Person>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Person>(mockContext.Object);

            //Act
            var person = await repository.GetByIdAsync(1);

            //Assert
            person.FirstName.Should().Be("Ken");
        }

        [Fact]
        public async void ListAllAsync_ReturnsObjects()
        {
            //Arrange
            var persons = new List<Person>
            {
                new Person { Id = 1, FirstName = "Ken", MiddleName = "J", LastName = "Sánchez" },
                new Person { Id = 2, FirstName = "Terri", MiddleName = "Lee", LastName = "Duffy" }
            };
            var mockSet = GetQueryableMockDbSet(persons);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Person>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Person>(mockContext.Object);

            //Act
            var list = await repository.ListAllAsync();

            //Assert
            list.Count.Should().Be(2);
        }

        [Fact]
        public async void ListAsync_ReturnsObjects()
        {
            //Arrange
            var persons = new List<Person>
            {
                new Person { Id = 1, FirstName = "Ken", MiddleName = "J", LastName = "Sánchez" },
                new Person { Id = 2, FirstName = "Terri", MiddleName = "Lee", LastName = "Duffy" },
                new Person { Id = 3, FirstName = "Angela", MiddleName = "C", LastName = "Sánchez" }
            };
            var mockSet = GetQueryableMockDbSet(persons);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Person>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Person>(mockContext.Object);

            //Act
            var spec = new GetPersonsByLastNameSpecification("Sánchez");
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(2);
        }

        [Fact]
        public async void CountAsync_ReturnsCount()
        {
            //Arrange
            var persons = new List<Person>
            {
                new Person { Id = 1, FirstName = "Ken", MiddleName = "J", LastName = "Sánchez" },
                new Person { Id = 2, FirstName = "Terri", MiddleName = "Lee", LastName = "Duffy" },
                new Person { Id = 3, FirstName = "Angela", MiddleName = "C", LastName = "Sánchez" }
            };
            var mockSet = GetQueryableMockDbSet(persons);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Person>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Person>(mockContext.Object);

            //Act
            var spec = new GetPersonsByLastNameSpecification("Sánchez");
            var count = await repository.CountAsync(spec);

            //Assert
            count.Should().Be(2);
        }

        [Fact]
        public async void AddAsync_SavesObject()
        {
            //Arrange
            var persons = new List<Person>
            {
                new Person { Id = 1, FirstName = "Ken", MiddleName = "J", LastName = "Sánchez" },
                new Person { Id = 2, FirstName = "Terri", MiddleName = "Lee", LastName = "Duffy" }
            };
            var mockSet = GetQueryableMockDbSet(persons);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Person>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Person>(mockContext.Object);

            //Act
            var newPerson = new Person { FirstName = "Angela", MiddleName = "C", LastName = "Sánchez" };
            var savedPerson = await repository.AddAsync(newPerson);

            //Assert
            mockSet.Verify(x => x.Add(It.IsAny<Person>()));
            mockContext.Verify(x => x.SaveChangesAsync());
            newPerson.Should().BeEquivalentTo(savedPerson);
        }

        [Fact]
        public async void UpdateAsync_SavesObject()
        {
            //Arrange
            var persons = new List<Person>
            {
                new Person { Id = 1, FirstName = "Ken", MiddleName = "J", LastName = "Sánchez" },
                new Person { Id = 2, FirstName = "Terri", MiddleName = "Lee", LastName = "Duffy" }
            };
            var mockSet = GetQueryableMockDbSet(persons);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Person>())
                .Returns(mockSet.Object);
            mockContext.Setup(x => x.SetModified(It.IsAny<Person>()));
            var repository = new EfRepository<Person>(mockContext.Object);

            //Act
            var existingPerson = new Person { Id = 1, FirstName = "Ken", MiddleName = "C", LastName = "Sánchez" };
            await repository.UpdateAsync(existingPerson);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync());
        }

        [Fact]
        public async void FirstOrDefaultAsync_ReturnsObject()
        {
            //Arrange
            var persons = new List<Person>
            {
                new Person { Id = 1, FirstName = "Ken", MiddleName = "J", LastName = "Sánchez" },
                new Person { Id = 2, FirstName = "Terri", MiddleName = "Lee", LastName = "Duffy" }
            };
            var mockSet = GetQueryableMockDbSet(persons);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Person>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Person>(mockContext.Object);

            //Act
            var spec = new GetPersonByIdSpecification(1);
            var person = await repository.FirstOrDefaultAsync(spec);

            //Assert
            person.FirstName.Should().Be("Ken");
        }
    }
}