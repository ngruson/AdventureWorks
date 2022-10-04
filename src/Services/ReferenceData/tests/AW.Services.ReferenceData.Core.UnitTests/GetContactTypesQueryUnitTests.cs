using AutoFixture.Xunit2;
using AW.Services.ReferenceData.Core.Exceptions;
using AW.Services.ReferenceData.Core.Handlers.ContactType.GetContactTypes;
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
    public class GetContactTypesQueryUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_ContactTypesExists_ReturnContactTypes(
            List<Entities.ContactType> contactTypes,
            [Frozen] Mock<IRepository<Entities.ContactType>> contactTypeRepoMock,
            GetContactTypesQueryHandler sut,
            GetContactTypesQuery query
        )
        {
            //Arrange            
            contactTypeRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(contactTypes);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            contactTypeRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
            
            for (int i = 0; i < result.Count; i++)
            {
                result[i].Name.Should().Be(contactTypes[i].Name);
            }
        }

        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task Handle_NoContactTypesExists_ThrowContactTypesNotFoundException(
            [Frozen] Mock<IRepository<Entities.ContactType>> contactTypeRepoMock,
            GetContactTypesQueryHandler sut,
            GetContactTypesQuery query
        )
        {
            //Arrange
            contactTypeRepoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync((List<Entities.ContactType>)null);

            //Act
            Func<Task> func = async () => await sut.Handle(query, CancellationToken.None);

            //Assert
            await func.Should().ThrowAsync<ContactTypesNotFoundException>();
            contactTypeRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
        }
    }
}