using AW.Services.Customer.Domain;
using AW.Services.Customer.Persistence.EFCore.UnitTests.Specifications;
using AW.Services.Customer.Persistence.EntityFrameworkCore;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace AW.Persistence.EntityFrameworkCore.UnitTests
{
    public class EfRepositoryUnitTests
    {
        [Fact]
        public async void GetByIdAsync_ReturnsObject()
        {
            //Arrange
            var persons = new List<Person>
            {
                new Person { Id = 1, FirstName = "Ken", MiddleName = "J", LastName = "Sánchez" },
                new Person { Id = 2, FirstName = "Terri", MiddleName = "Lee", LastName = "Duffy" }
            };
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
            var mockSet = persons.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Person>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Person>(mockContext.Object);

            //Act
            var list = await repository.ListAsync();

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
            var mockSet = persons.AsQueryable().BuildMockDbSet();

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
        public async void ListAsync_WithResultSpec_ReturnsObjects()
        {
            //Arrange
            var persons = new List<Person>
            {
                new Person { Id = 1, FirstName = "Ken", MiddleName = "J", LastName = "Sánchez" },
                new Person { Id = 2, FirstName = "Terri", MiddleName = "Lee", LastName = "Duffy" },
                new Person { Id = 3, FirstName = "Angela", MiddleName = "C", LastName = "Sánchez" }
            };
            var mockSet = persons.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Person>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Person>(mockContext.Object);

            //Act
            var spec = new GetPersonsLastNameSpecification();
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(3);
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
            var mockSet = persons.AsQueryable().BuildMockDbSet();

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
            var mockSet = persons.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Person>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Person>(mockContext.Object);

            //Act
            var newPerson = new Person { FirstName = "Angela", MiddleName = "C", LastName = "Sánchez" };
            var savedPerson = await repository.AddAsync(newPerson);

            //Assert
            mockSet.Verify(x => x.Add(It.IsAny<Person>()));
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
            newPerson.Should().BeEquivalentTo(savedPerson);
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
            var mockSet = persons.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Person>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Person>(mockContext.Object);

            //Act
            var spec = new GetPersonByIdSpecification(1);
            var person = await repository.GetBySpecAsync(spec);

            //Assert
            person.FirstName.Should().Be("Ken");
        }
    }
}