using Ardalis.Specification;
using Entities = AW.Services.Customer.Core.Entities;
using AW.Services.Customer.Core.Specifications;
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
using AW.SharedKernel.UnitTesting;
using AutoFixture.Xunit2;

namespace AW.Services.Customer.Infrastructure.EFCore.UnitTests
{
    public class EfRepositoryUnitTests
    {
        [Theory]
        [AutoMoqData]
        public async void GetByIdAsync_ReturnsObject(
            [Frozen] Mock<DbSet<Entities.StoreCustomer>> mockSet,
            [Frozen] Mock<AWContext> mockContext,
            Entities.StoreCustomer customer
        )
        {
            //Arrange
            mockSet.Setup(x => x.FindAsync(It.IsAny<object[]>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(customer);

            mockContext.Setup(x => x.Set<Entities.StoreCustomer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.StoreCustomer>(mockContext.Object);

            //Act
            var result = await repository.GetByIdAsync(1);

            //Assert
            result.Name.Should().Be(customer.Name);
        }

        [Theory]
        [AutoMoqData]
        public async void GetBySpecAsync_ReturnsObject(
            List<Entities.Customer> customers,
            [Frozen] Mock<AWContext> mockContext
        )
        {
            //Arrange
            var mockSet = customers.AsQueryable().BuildMockDbSet();

            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            var spec = new GetCustomerSpecification(customers[0].AccountNumber);
            var result = await repository.GetBySpecAsync(spec);

            //Assert
            result.Should().BeEquivalentTo(customers[0]);
        }

        [Theory]
        [AutoMoqData]
        public async void ListAllAsync_ReturnsObjects(
            List<Entities.Customer> customers
        )
        {
            //Arrange
            var mockSet = customers.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            var list = await repository.ListAsync();

            //Assert
            list.Count.Should().Be(3);
        }

        [Theory]
        [AutoMoqData]
        public async void ListAsync_ReturnsObjects(
            List<Entities.StoreCustomer> customers
        )
        {
            //Arrange
            var mockSet = customers.Cast<Entities.Customer>().AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            var spec = new GetCustomersPaginatedSpecification(0, 10, Entities.CustomerType.Store, null, null);
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(3);
        }

        [Theory]
        [AutoMoqData]
        public async void ListAsync_WithResultSpec_ReturnsObjects(
            List<Entities.StoreCustomer> customers
        )
        {
            //Arrange
            var mockSet = customers.Cast<Entities.Customer>().AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            var spec = new GetCustomersAccountNumberSpecification();
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(3);
            list[0].Should().Be(customers[0].AccountNumber);
            list[1].Should().Be(customers[1].AccountNumber);
            list[2].Should().Be(customers[2].AccountNumber);
        }

        [Fact]
        public void ListAsync_WithNullResultSpec_ThrowsArgumentNullException()
        {
            //Arrange
            var mockContext = new Mock<AWContext>();
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

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
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            var spec = new GetCustomersAccountNumberWithoutSelectorSpecification();
            Func<Task> func = async () => await repository.ListAsync(spec);

            //Assert
            func.Should().Throw<SelectorNotFoundException>();
        }

        [Theory]
        [AutoMoqData]
        public async void CountAsync_ReturnsCount(
            List<Entities.StoreCustomer> customers
        )
        {
            //Arrange
            var mockSet = customers.Cast<Entities.Customer>().AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            var spec = new CountCustomersSpecification(Entities.CustomerType.Store, null, null);
            var count = await repository.CountAsync(spec);

            //Assert
            count.Should().Be(3);
        }

        [Theory]
        [AutoMoqData]
        public async void AddAsync_SavesObject(
            List<Entities.StoreCustomer> customers,
            Entities.StoreCustomer newCustomer
        )
        {
            //Arrange
            var mockSet = customers.Cast<Entities.Customer>().AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            var savedCustomer = await repository.AddAsync(newCustomer);

            //Assert
            mockSet.Verify(x => x.Add(It.IsAny<Entities.Customer>()));
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
            newCustomer.Should().BeEquivalentTo(savedCustomer);
        }

        [Theory]
        [AutoMoqData]
        public async void UpdateAsync_SavesObject(
            List<Entities.StoreCustomer> customers
        )
        {
            //Arrange
            var mockSet = customers.Cast<Entities.Customer>().AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            mockContext.Setup(x => x.SetModified(It.IsAny<Entities.Customer>()));
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            var existingCustomer = new Entities.StoreCustomer { Name = "A Bike Store", AccountNumber = "AW00000001" };
            await repository.UpdateAsync(existingCustomer);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Theory]
        [AutoMoqData]
        public async void DeleteAsync_ReturnsObject(
            List<Entities.Customer> customers
        )
        {
            //Arrange
            var mockSet = customers.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            await repository.DeleteAsync(customers[0]);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Theory]
        [AutoMoqData]
        public async void DeleteRangeAsync_ReturnsObject(
            List<Entities.Customer> customers
        )
        {
            //Arrange
            var mockSet = customers.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<Entities.Customer>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<Entities.Customer>(mockContext.Object);

            //Act
            await repository.DeleteRangeAsync(customers);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}