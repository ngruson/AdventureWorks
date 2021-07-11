using Ardalis.Specification;
using AW.Services.ReferenceData.Core.Specifications;
using AW.Services.ReferenceData.Core.Entities;
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

namespace AW.Services.ReferenceData.Infrastructure.EFCore.UnitTests
{
    public class EfRepositoryUnitTests
    {
        [Theory, OmitOnRecursion]
        public async void GetByIdAsync_ReturnsObject(
            [Frozen] Mock<DbSet<AddressType>> mockSet,
            [Frozen] Mock<AWContext> mockContext,
            AddressType addressType
        )
        {
            //Arrange
            mockSet.Setup(x => x.FindAsync(
                It.IsAny<object[]>(), 
                It.IsAny<CancellationToken>()
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

        [Theory, OmitOnRecursion]
        public async void GetBySpecAsync_ReturnsObject(
            [Frozen] Mock<AWContext> mockContext,
            List<AddressType> addressTypes
        )
        {
            //Arrange
            var mockSet = addressTypes.AsQueryable().BuildMockDbSet();

            mockContext.Setup(x => x.Set<AddressType>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<AddressType>(mockContext.Object);

            //Act
            var spec = new GetAddressTypeByIdSpecification(addressTypes[0].Id);
            var result = await repository.GetBySpecAsync(spec);

            //Assert
            result.Name.Should().Be(addressTypes[0].Name);
        }

        [Theory, OmitOnRecursion]
        public async void ListAllAsync_ReturnsObjects(
            [Frozen] Mock<AWContext> mockContext,
            List<AddressType> addressTypes
        )
        {
            //Arrange
            var mockSet = addressTypes.AsQueryable().BuildMockDbSet();

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
            var mockSet = statesProvinces.AsQueryable().BuildMockDbSet();

            mockContext.Setup(x => x.Set<StateProvince>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<StateProvince>(mockContext.Object);

            //Act
            var spec = new GetStatesProvincesForCountrySpecification(statesProvinces[0].CountryRegionCode);
            var list = await repository.ListAsync(spec);

            //Assert
            list.Count.Should().Be(1);
        }

        [Theory, OmitOnRecursion]
        public async void ListAsync_WithResultSpec_ReturnsObjects(
            [Frozen] Mock<AWContext> mockContext,
            List<AddressType> addressTypes
        )
        {
            //Arrange
            var mockSet = addressTypes.AsQueryable().BuildMockDbSet();

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
            var mockSet = statesProvinces.AsQueryable().BuildMockDbSet();

            mockContext.Setup(x => x.Set<StateProvince>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<StateProvince>(mockContext.Object);

            //Act
            var spec = new GetStatesProvincesForCountrySpecification(statesProvinces[0].CountryRegionCode);
            var count = await repository.CountAsync(spec);

            //Assert
            count.Should().Be(1);
        }

        [Theory, OmitOnRecursion]
        public async void AddAsync_SavesObject(
            List<AddressType> addressTypes,
            AddressType newAddressType
        )
        {
            //Arrange
            var mockSet = addressTypes.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
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

        [Theory, OmitOnRecursion]
        public async void UpdateAsync_SavesObject(
            List<AddressType> addressTypes
        )
        {
            //Arrange
            var mockSet = addressTypes.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<AddressType>())
                .Returns(mockSet.Object);
            mockContext.Setup(x => x.SetModified(It.IsAny<AddressType>()));
            var repository = new EfRepository<AddressType>(mockContext.Object);

            //Act
            await repository.UpdateAsync(addressTypes[0]);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Theory, OmitOnRecursion]
        public async void DeleteAsync_ReturnsObject(
            List<AddressType> addressTypes
        )
        {
            //Arrange
            var mockSet = addressTypes.AsQueryable().BuildMockDbSet();

            var mockContext = new Mock<AWContext>();
            mockContext.Setup(x => x.Set<AddressType>())
                .Returns(mockSet.Object);
            var repository = new EfRepository<AddressType>(mockContext.Object);

            //Act
            await repository.DeleteAsync(addressTypes[0]);

            //Assert
            mockContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()));
        }

        [Theory, OmitOnRecursion]
        public async void DeleteRangeAsync_ReturnsObject(
            List<AddressType> addressTypes
        )
        {
            //Arrange
            var mockSet = addressTypes.AsQueryable().BuildMockDbSet();

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