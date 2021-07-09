using Ardalis.Specification;
using AW.SharedKernel.UnitTesting.EF6;
using AW.Services.ReferenceData.Core.Specifications;
using AW.Services.ReferenceData.Core.Entities;
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

namespace AW.Services.ReferenceData.Infrastructure.EF6.UnitTests
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
            var mockSet = new Mock<DbSet<AddressType>>();
            mockSet.Setup(x => x.FindAsync(
                It.IsAny<CancellationToken>(),
                It.IsAny<int>()
            ))
            .ReturnsAsync(
                new AddressType { Id = 1, Name = "Main Office" }
            );

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<AddressType>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<AddressType>(mockContext.Object);

            //Act
            var addressType = await repository.GetByIdAsync(1);

            //Assert
            addressType.Name.Should().Be("Main Office");
        }

        [Fact]
        public async void GetBySpecAsync_ReturnsObject()
        {
            //Arrange
            var addressTypes = new List<AddressType>
            {
                new AddressType { Id = 1, Name = "Main Office" },
                new AddressType { Id = 2, Name = "Home" }
            };
            var mockSet = GetQueryableMockDbSet(addressTypes);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<AddressType>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<AddressType>(mockContext.Object);

            //Act
            var spec = new GetAddressTypeByIdSpecification(1);
            var addressType = await repository.GetBySpecAsync(spec);

            //Assert
            addressType.Name.Should().Be("Main Office");
        }

        [Fact]
        public async void ListAllAsync_ReturnsObjects()
        {
            //Arrange
            var addressTypes = new List<AddressType>
            {
                new AddressType { Id = 1, Name = "Main Office" },
                new AddressType { Id = 2, Name = "Home" }
            };
            var mockSet = GetQueryableMockDbSet(addressTypes);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<AddressType>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<AddressType>(mockContext.Object);

            //Act
            var list = await repository.ListAsync();

            //Assert
            list.Count.Should().Be(2);
        }

        [Fact]
        public async void ListAsync_ReturnsObjects()
        {
            //Arrange
            var statesProvinces = new List<StateProvince>
            {
                new StateProvince { Name = "Georgia", CountryRegionCode = "US" },
                new StateProvince { Name = "Texas", CountryRegionCode = "US" },
                new StateProvince { Name = "Alberta", CountryRegionCode = "CA" }
            };
            var mockSet = GetQueryableMockDbSet(statesProvinces);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<StateProvince>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<StateProvince>(mockContext.Object);

            //Act
            var spec = new GetStatesProvincesForCountrySpecification("US");
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(2);
        }

        [Fact]
        public async void ListAsync_WithResultSpec_ReturnsObjects()
        {
            //Arrange
            var addressTypes = new List<AddressType>
            {
                new AddressType { Id = 1, Name = "Main Office" },
                new AddressType { Id = 2, Name = "Home" }
            };
            var mockSet = GetQueryableMockDbSet(addressTypes);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<AddressType>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<AddressType>(mockContext.Object);

            //Act
            var spec = new GetAddressTypesNameSpecification();
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(2);
            list[0].Should().Be("Main Office");
            list[1].Should().Be("Home");
        }

        [Fact]
        public void ListAsync_WithNullResultSpec_ThrowsArgumentNullException()
        {
            //Arrange
            var mockContext = new Mock<AWContext>();
            var repository = new EfRepository<AddressType>(mockContext.Object);

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
            var repository = new EfRepository<AddressType>(mockContext.Object);

            //Act
            var spec = new GetAddressTypesNameWithoutSelectorSpecification();
            Func<Task> func = async () => await repository.ListAsync(spec);

            //Assert
            func.Should().Throw<SelectorNotFoundException>();
        }

        [Fact]
        public async void CountAsync_ReturnsCount()
        {
            //Arrange
            var statesProvinces = new List<StateProvince>
            {
                new StateProvince { Name = "Georgia", CountryRegionCode = "US" },
                new StateProvince { Name = "Texas", CountryRegionCode = "US" },
                new StateProvince { Name = "Alberta", CountryRegionCode = "CA" }
            };
            var mockSet = GetQueryableMockDbSet(statesProvinces);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<StateProvince>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<StateProvince>(mockContext.Object);

            //Act
            var spec = new GetStatesProvincesForCountrySpecification("US");
            var count = await repository.CountAsync(spec);

            //Assert
            count.Should().Be(2);
        }

        [Fact]
        public async void AddAsync_SavesObject()
        {
            //Arrange
            var addressTypes = new List<AddressType>
            {
                new AddressType { Id = 1, Name = "Main Office" },
                new AddressType { Id = 2, Name = "Home" }
            };
            var mockSet = GetQueryableMockDbSet(addressTypes);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<AddressType>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<AddressType>(mockContext.Object);

            //Act
            var newAddressType = new AddressType { Name = "Billing" };
            var savedAddressType = await repository.AddAsync(newAddressType);

            //Assert
            mockSet.Verify(x => x.Add(It.IsAny<AddressType>()));
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
            newAddressType.Should().BeEquivalentTo(savedAddressType);
        }

        [Fact]
        public async void UpdateAsync_SavesObject()
        {
            //Arrange
            var addressTypes = new List<AddressType>
            {
                new AddressType { Id = 1, Name = "Main Office" },
                new AddressType { Id = 2, Name = "Home" }
            };
            var mockSet = GetQueryableMockDbSet(addressTypes);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<AddressType>())
                .Returns(mockSet.Object);
            mockContext.Setup(x => x.SetModified(It.IsAny<AddressType>()));
            var repository = new EfRepository<AddressType>(mockContext.Object);

            //Act
            var existingAddressType = new AddressType { Id = 1, Name = "Main Office changed" };
            await repository.UpdateAsync(existingAddressType);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async void DeleteAsync_ReturnsObject()
        {
            //Arrange
            var addressType1 = new AddressType { Id = 1, Name = "Main Office" };
            var addressType2 = new AddressType { Id = 1, Name = "Home" };
            var addressTypes = new List<AddressType>
            {
                addressType1,
                addressType2
            };
            var mockSet = GetQueryableMockDbSet(addressTypes);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<AddressType>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<AddressType>(mockContext.Object);

            //Act
            await repository.DeleteAsync(addressType1);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async void DeleteRangeAsync_ReturnsObject()
        {
            //Arrange
            var addressType1 = new AddressType { Id = 1, Name = "Main Office" };
            var addressType2 = new AddressType { Id = 1, Name = "Home" };
            var addressTypes = new List<AddressType>
            {
                addressType1,
                addressType2
            };
            var mockSet = GetQueryableMockDbSet(addressTypes);

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<AddressType>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<AddressType>(mockContext.Object);

            //Act
            await repository.DeleteRangeAsync(addressTypes);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }
    }
}