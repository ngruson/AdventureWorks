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
using AutoFixture.Xunit2;
using AW.SharedKernel.UnitTesting;

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

        [Theory, AutoMoqData]
        public async void GetByIdAsync_ReturnsObject(
            [Frozen] Mock<DbSet<AddressType>> mockSet,
            [Frozen] Mock<AWContext> mockContext,
            AddressType addressType
        )
        {
            //Arrange
            mockSet.Setup(x => x.FindAsync(
                It.IsAny<CancellationToken>(),
                It.IsAny<int>()
            ))
            .ReturnsAsync(addressType);

            mockContext.Setup(x => x.Set<AddressType>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<AddressType>(mockContext.Object);

            //Act
            var result = await repository.GetByIdAsync(1);

            //Assert
            result.Name.Should().Be(addressType.Name);
        }

        [Theory, AutoMoqData]
        public async void GetBySpecAsync_ReturnsObject(
            [Frozen] Mock<AWContext> mockContext,
            List<AddressType> addressTypes
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(addressTypes);

            mockContext.Setup(x => x.Set<AddressType>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<AddressType>(mockContext.Object);

            //Act
            var spec = new GetAddressTypeByIdSpecification(addressTypes[0].Id);
            var addressType = await repository.GetBySpecAsync(spec);

            //Assert
            addressType.Name.Should().Be(addressTypes[0].Name);
        }

        [Theory, AutoMoqData]
        public async void ListAllAsync_ReturnsObjects(
            [Frozen] Mock<AWContext> mockContext,
            List<AddressType> addressTypes
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(addressTypes);

            mockContext.Setup(x => x.Set<AddressType>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<AddressType>(mockContext.Object);

            //Act
            var list = await repository.ListAsync();

            //Assert
            list.Count.Should().Be(addressTypes.Count);
        }

        [Theory, OmitOnRecursion]
        public async void ListAsync_ReturnsObjects(
            [Frozen] Mock<AWContext> mockContext,
            List<StateProvince> statesProvinces
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(statesProvinces);

            mockContext.Setup(x => x.Set<StateProvince>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<StateProvince>(mockContext.Object);

            //Act
            var spec = new GetStatesProvincesForCountrySpecification(statesProvinces[0].CountryRegionCode);
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(1);
            list[0].Should().BeEquivalentTo(statesProvinces[0]);
        }

        [Theory, AutoMoqData]
        public async void ListAsync_WithResultSpec_ReturnsObjects(
            [Frozen] Mock<AWContext> mockContext,
            List<AddressType> addressTypes
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(addressTypes);

            mockContext.Setup(x => x.Set<AddressType>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<AddressType>(mockContext.Object);

            //Act
            var spec = new GetAddressTypesNameSpecification();
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(addressTypes.Count);

            for (int i = 0; i < list.Count; i++)
            {
                list[i].Should().Be(addressTypes[i].Name);
            }
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

        [Theory, OmitOnRecursion]
        public async void CountAsync_ReturnsCount(
            [Frozen] Mock<AWContext> mockContext,
            List<StateProvince> statesProvinces
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(statesProvinces);

            mockContext.Setup(x => x.Set<StateProvince>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<StateProvince>(mockContext.Object);

            //Act
            var spec = new GetStatesProvincesForCountrySpecification(statesProvinces[0].CountryRegionCode);
            var count = await repository.CountAsync(spec);

            //Assert
            count.Should().Be(1);
        }

        [Theory, AutoMoqData]
        public async void AddAsync_SavesObject(
            [Frozen] Mock<AWContext> mockContext,
            List<AddressType> addressTypes,
            AddressType newAddressType
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(addressTypes);

            mockContext.Setup(x => x.Set<AddressType>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<AddressType>(mockContext.Object);

            //Act
            var savedAddressType = await repository.AddAsync(newAddressType);

            //Assert
            mockSet.Verify(x => x.Add(It.IsAny<AddressType>()));
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
            newAddressType.Should().BeEquivalentTo(savedAddressType);
        }

        [Theory, AutoMoqData]
        public async void UpdateAsync_SavesObject(
            [Frozen] Mock<AWContext> mockContext,
            List<AddressType> addressTypes
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(addressTypes);

            mockContext.Setup(x => x.Set<AddressType>())
                .Returns(mockSet.Object);
            mockContext.Setup(x => x.SetModified(It.IsAny<AddressType>()));
            var repository = new EfRepository<AddressType>(mockContext.Object);

            //Act
            await repository.UpdateAsync(addressTypes[0]);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Theory, AutoMoqData]
        public async void DeleteAsync_ReturnsObject(
            [Frozen] Mock<AWContext> mockContext,
            List<AddressType> addressTypes
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(addressTypes);

            mockContext.Setup(x => x.Set<AddressType>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<AddressType>(mockContext.Object);

            //Act
            await repository.DeleteAsync(addressTypes[0]);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Theory, AutoMoqData]
        public async void DeleteRangeAsync_ReturnsObject(
            [Frozen] Mock<AWContext> mockContext,
            List<AddressType> addressTypes
        )
        {
            //Arrange
            var mockSet = GetQueryableMockDbSet(addressTypes);

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