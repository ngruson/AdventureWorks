using AutoFixture.Xunit2;
using AW.Services.ReferenceData.Core;
using AW.Services.ReferenceData.Core.Handlers.ContactType.GetContactTypes;
using AW.SharedKernel.UnitTesting;
using FluentAssertions;
using MediatR;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AW.Services.ReferenceData.WCF.UnitTests
{
    public class ContactTypeServiceUnitTests
    {
        [Theory, AutoMapperData(typeof(MappingProfile))]
        public async Task ListContactTypes_ReturnsContactTypes(
            List<ContactType> contactTypes,
            [Frozen] Mock<IMediator> mockMediator,
            ContactTypeService sut
        )
        {
            //Arrange
            mockMediator.Setup(x => x.Send(
                It.IsAny<GetContactTypesQuery>(), 
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(contactTypes);

            //Act
            var result = await sut.ListContactTypes();

            //Assert
            result.Should().NotBeNull();
            result.ContactTypes.Count().Should().Be(contactTypes.Count);
        }
    }
}