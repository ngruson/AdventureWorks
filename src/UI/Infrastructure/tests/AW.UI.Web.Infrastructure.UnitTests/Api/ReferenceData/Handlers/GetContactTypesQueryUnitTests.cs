using AutoFixture.Xunit2;
using AW.SharedKernel.Caching;
using AW.SharedKernel.UnitTesting;
using AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetContactTypes;
using FluentAssertions;
using Moq;
using Xunit;

namespace AW.UI.Web.Infrastructure.UnitTests.Api.ReferenceData.Handlers
{
    public class GetContactTypesQueryUnitTests
    {
        [Theory, AutoMoqData]
        public async Task Handle_ReturnContactTypes(
            List<ContactType> contactTypes,
            [Frozen] Mock<ICache<ContactType>> cacheMock,
            GetContactTypesQueryHandler sut,
            GetContactTypesQuery query
        )
        {
            //Arrange
            cacheMock.Setup(x => x.GetData())
                .ReturnsAsync(contactTypes);

            //Act
            var result = await sut.Handle(query, CancellationToken.None);

            //Assert
            cacheMock.Verify(x => x.GetData());
            result.Should().BeEquivalentTo(contactTypes);
        }
    }
}
