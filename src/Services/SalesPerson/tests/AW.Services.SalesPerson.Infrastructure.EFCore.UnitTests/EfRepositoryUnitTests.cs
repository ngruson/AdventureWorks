using Ardalis.Specification;
using AutoFixture.Xunit2;
using AW.Services.SalesPerson.Core.Specifications;
using AW.Services.SharedKernel.EFCore;
using AW.SharedKernel.Extensions;
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

namespace AW.Services.SalesPerson.Infrastructure.EFCore.UnitTests
{
    public class EfRepositoryUnitTests
    {
        [Theory, OmitOnRecursion]
        public async Task GetByIdAsync_ReturnsObject(
            [Frozen] Mock<DbSet<Core.Entities.SalesPerson>> mockSet,
            [Frozen] Mock<AWContext> mockContext,
            Core.Entities.SalesPerson salesPerson
        )
        {
            //Arrange
            mockSet.Setup(x => x.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()))
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
            var mockSet = salesPersons.AsQueryable().BuildMockDbSet();

            mockContext.Setup(x => x.Set<Core.Entities.SalesPerson>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Core.Entities.SalesPerson>(mockContext.Object);

            //Act
            var spec = new GetSalesPersonSpecification(
                salesPersons[0].FirstName,
                salesPersons[0].MiddleName,
                salesPersons[0].LastName
            );

            var result = await repository.GetBySpecAsync(spec);

            //Assert
            result.FullName().Should().Be(salesPersons[0].FullName());
        }

        [Theory, OmitOnRecursion]
        public async Task ListAsync_WithoutSpec_ReturnsObjects(
            [Frozen] Mock<AWContext> mockContext,
            List<Core.Entities.SalesPerson> salesPersons
        )
        {
            //Arrange
            var mockSet = salesPersons.AsQueryable().BuildMockDbSet();

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
            var mockSet = salesPersons.AsQueryable().BuildMockDbSet();

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
            var mockSet = salesPersons.AsQueryable().BuildMockDbSet();

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
            func.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public void ListAsync_WithResultSpecWithoutSelector_ThrowsSelectorNotFoundException()
        {
            //Arrange
            var mockContext = new Mock<AWContext>();
            var repository = new EfRepository<Core.Entities.SalesPerson>(mockContext.Object);

            //Act
            var spec = new GetSalesPersonsWithoutSelectorSpecification();
            Func<Task> func = async () => await repository.ListAsync(spec);

            //Assert
            func.Should().ThrowAsync<SelectorNotFoundException>();
        }

        [Theory, OmitOnRecursion]
        public async Task CountAsync_ReturnsCount(
            [Frozen] Mock<AWContext> mockContext,
            List<Core.Entities.SalesPerson> salesPersons
        )
        {
            //Arrange
            var mockSet = salesPersons.AsQueryable().BuildMockDbSet();

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
            List<Core.Entities.SalesPerson> salesPersons,
            Core.Entities.SalesPerson newSalesPerson
        )
        {
            //Arrange
            var mockSet = salesPersons.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
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
            List<Core.Entities.SalesPerson> salesPersons
        )
        {
            //Arrange
            var mockSet = salesPersons.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
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
            List<Core.Entities.SalesPerson> salesPersons
        )
        {
            //Arrange
            var mockSet = salesPersons.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
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
            List<Core.Entities.SalesPerson> salesPersons
        )
        {
            //Arrange
            var mockSet = salesPersons.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
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