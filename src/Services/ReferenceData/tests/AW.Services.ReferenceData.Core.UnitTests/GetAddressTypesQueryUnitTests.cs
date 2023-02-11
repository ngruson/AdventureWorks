using AutoFixture.Xunit2;
using AW.Services.ReferenceData.Core.Exceptions;
using AW.Services.ReferenceData.Core.Handlers.AddressType.GetAddressTypes;
using AW.Services.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.ReferenceData.Core.UnitTests
{
    public class GetAddressTypesQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_AddressTypesExists_ReturnAddressTypes(
            List<Entities.AddressType> addressTypes,
            [Frozen] Mock<IRepository<Entities.AddressType>> addressTypeRepoMock,
            GetAddressTypesQueryHandler sut,
            GetAddressTypesQuery query
        )
        {
            //Arrange
            addressTypeRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(addressTypes);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            addressTypeRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));

            for (int i = 0; i < result.Count; i++)
            {
                result[i].Name.Should().Be(addressTypes[i].Name);
            }
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_NoAddressTypesExists_ThrowAddressTypesNotFoundException(
            [Frozen] Mock<IRepository<Entities.AddressType>> addressTypeRepoMock,
            GetAddressTypesQueryHandler sut,
            GetAddressTypesQuery query
        )
        {
            //Arrange
            addressTypeRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Entities.AddressType>());

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<AddressTypesNotFoundException>();
            addressTypeRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
        }
    }
}