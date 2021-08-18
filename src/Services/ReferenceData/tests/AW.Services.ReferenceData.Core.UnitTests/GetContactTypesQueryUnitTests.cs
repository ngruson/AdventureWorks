using Ardalis.Specification;
using AutoFixture.Xunit2;
using AW.Services.ReferenceData.Core.Handlers.ContactType.GetContactTypes;
using AW.SharedKernel.Interfaces;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using Microsoft.Extensions.Logging;
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
        public void Handle_NoContactTypesExists_ThrowException(
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
            func.Should().Throw<ArgumentNullException>();
            contactTypeRepoMock.Verify(x => x.ListAsync(It.IsAny<CancellationToken>()));
        }
    }
}