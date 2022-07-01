using AutoFixture.Xunit2;
using AW.SharedKernel.Caching;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetContactTypes;
using FluentAssertions;
using Moq;

namespace AW.UI.Web.SharedKernel.UnitTests.ReferenceData.Handlers
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