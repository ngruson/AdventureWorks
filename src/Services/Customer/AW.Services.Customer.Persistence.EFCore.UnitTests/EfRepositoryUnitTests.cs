using Ardalis.Specification;
using AW.Services.Customer.Domain;
using AW.Services.Customer.Persistence.EFCore.UnitTests.Specifications;
using AW.Services.Customer.Persistence.EntityFrameworkCore;
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

namespace AW.Services.Customer.Persistence.EFCore.UnitTests
{
    public class EfRepositoryUnitTests
    {
        [Fact]
        public async void GetByIdAsync_ReturnsObject()
        {
            //Arrange
            var mockSet = new Mock<DbSet<Person>>();
            mockSet.Setup(x => x.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()))
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
            list[0].Should().Be("Sánchez");
            list[1].Should().Be("Duffy");
            list[2].Should().Be("Sánchez");
        }

        [Fact]
        public void ListAsync_WithNullResultSpec_ThrowsArgumentNullException()
        {
            //Arrange
            var mockContext = new Mock<AWContext>();
            var repository = new EfRepository<Person>(mockContext.Object);

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
            var repository = new EfRepository<Person>(mockContext.Object);

            //Act
            var spec = new GetPersonsLastNameWithoutSelectorSpecification();
            Func<Task> func = async () => await repository.ListAsync(spec);

            //Assert
            func.Should().Throw<SelectorNotFoundException>();
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
        public async void GetBySpecAsync_ReturnsObject()
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

        [Fact]
        public async void UpdateAsync_SavesObject()
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
            mockContext.Setup(x => x.SetModified(It.IsAny<Person>()));
            var repository = new EfRepository<Person>(mockContext.Object);

            //Act
            var existingPerson = new Person { Id = 1, FirstName = "Ken", MiddleName = "C", LastName = "Sánchez" };
            await repository.UpdateAsync(existingPerson);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async void DeleteAsync_ReturnsObject()
        {
            //Arrange
            var person1 = new Person { Id = 1, FirstName = "Ken", MiddleName = "J", LastName = "Sánchez" };
            var person2 = new Person { Id = 2, FirstName = "Terri", MiddleName = "Lee", LastName = "Duffy" };
            var persons = new List<Person>
            {
                person1,
                person2
            };
            var mockSet = persons.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Person>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Person>(mockContext.Object);

            //Act
            await repository.DeleteAsync(person1);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async void DeleteRangeAsync_ReturnsObject()
        {
            //Arrange
            var person1 = new Person { Id = 1, FirstName = "Ken", MiddleName = "J", LastName = "Sánchez" };
            var person2 = new Person { Id = 2, FirstName = "Terri", MiddleName = "Lee", LastName = "Duffy" };
            var persons = new List<Person>
            {
                person1,
                person2
            };
            var mockSet = persons.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Person>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Person>(mockContext.Object);

            //Act
            await repository.DeleteRangeAsync(persons);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}